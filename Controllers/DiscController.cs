using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using mccotter_net_api.Models;
using mccotter_net_api.DataAccess;
using Microsoft.AspNetCore.Http;
using System;

namespace mccotter_net_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DiscController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;
        public DiscController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        // GET all action
        /// <summary>
        /// Get all discs.
        /// </summary>
        /// <remarks>
        /// <h1><b>Function returns a list of Disc JSON objects that were pulled from the Postgres DB.</b></h1>
        /// </remarks>
        /// <response code="200">Returns JSON list of Disc Objects</response>
        /// <response code="500">Failed to get data from database.</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<Disc>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public ActionResult<List<Disc>> GetDiscs()
        {
            try
            {
                return _dataAccessProvider.GetDiscs();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed to get data from database with exception: " + ex.Message);
            }
        }

        // GET by Id action
        /// <summary>
        /// Get Single Disc by ID.
        /// </summary>
        /// <remarks>
        /// <h1><b>Function returns a JSON object that were pulled from the Postgres DB matching the given ID.</b></h1>
        /// </remarks>
        /// <param name="id"></param>
        /// <response code="200">Returns JSON Disc Object</response>
        /// <response code="404">Return NotFound when id does not match anything id in the database.</response>
        /// <response code="500">Failed to get data from database.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Disc), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public ActionResult<Disc> GetDisc(int id)
        {
            try
            {
                var existingDisc = _dataAccessProvider.GetDisc(id);

                if (existingDisc == null)
                    return NotFound("Failed to find disc by id: " + id);
                else
                    return existingDisc;
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed to get data from database with exception: " + ex.Message);
            }
        }

        // POST action
        /// <summary>
        /// Create a new Disc into the DB with values provided in Body.
        /// </summary>
        /// <remarks>
        /// <h1><b>Please make sure to set id to 0, the DB will give it an id automatically.</b></h1>
        /// </remarks>
        /// <param name="disc"></param>
        /// <response code="200">Returns a 200 OK with the name and id of the disc created.</response>
        /// <response code="400">Returns a 400 Bad Request for JSON body that does not meet standards.</response> 
        /// <response code="500">Failed to get data from database.</response> 
        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public IActionResult AddDisc([FromBody] Disc disc)
        {      
            if(ModelState.IsValid)
            {
                try
                {
                    _dataAccessProvider.AddDisc(disc); 
                    return Ok($"You have successfully CREATED disc named: \nNAME: {disc.name} \nID: {disc.id}");               
                }
                catch (System.Exception ex)
                {
                    return StatusCode(500, "Failed to Update the database with exception: " + ex.Message);
                }
            }      
            return BadRequest("Model does not match the standard, id MUST BE 0, please see schema to verify everything matches up!");
        }

        // PUT action
        /// <summary>
        /// Updates an existing disc with the new values provided in Body.
        /// </summary>
        /// <remarks>
        /// <h1><b>Please make sure to set id to an existing disc id in the database, use /disc for all discs in database.</b></h1>
        /// </remarks>
        /// <param name="disc"></param>
        /// <response code="200">Returns a 200 OK with the name and id of the disc updated.</response>
        /// <response code="404">Returns a 404 NotFound when id does not match and id in database.</response>
        /// <response code="400">Returns a 400 Bad Request for JSON body that does not meet standards.</response>
        /// <response code="500">Failed to get data from database.</response> 
        [HttpPut]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateDisc([FromBody] Disc disc)
        {
            if (ModelState.IsValid || disc.id == 0)
            {
                try
                {
                    var existingDisc = _dataAccessProvider.GetDisc(disc.id);

                    if (existingDisc == null)
                        return NotFound("Failed to find disc by id: " + disc.id);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, "Failed to get data from database with exception: " + ex.Message);
                }

                try
                {
                    _dataAccessProvider.UpdateDisc(disc);
                    return Ok($"You have successfully UPDATED disc named: \nNAME: {disc.name} \nID: {disc.id}"); 
                }
                catch (Exception ex)
                {
                    return StatusCode(500, "Failed to Update the database with exception: " + ex.Message);
                }
            }
            else
            {
                return BadRequest("Model does not match the standard or is set to 0, please see schema to verify everything matches up!");
            }          
        }

        // DELETE action
        /// <summary>
        /// Delete Disc from Database.
        /// </summary>
        /// <remarks>
        /// <h1><b>Deleting a disc from the database, function returns whether or not the disc has been deleted.</b></h1>
        /// </remarks>
        /// <param name="id"></param>
        /// <response code="200">Returns success when the disc has been removed and returns id of removed Disc.</response>
        /// <response code="404">Return NotFound when the ID doesn't match anything in the Database.</response>
        /// <response code="500">Failed to get data from database.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteDisc(int id)
        {
            try
            {
                var existingDisc = _dataAccessProvider.GetDisc(id);

                if (existingDisc == null)
                    return NotFound("Failed to find disc by id: " + id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed to get data from database with exception: " + ex.Message);
            }

            try
            {
                _dataAccessProvider.DeleteDisc(id); 
                return Ok("You have successfully deleted disc id: " + id);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, "Failed to Update the database with exception: " + ex.Message);
            }
        }
    }
}