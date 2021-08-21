using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using mccotter_net_api.Models;
using mccotter_net_api.DataAccess;
using Microsoft.AspNetCore.Http;

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
        [HttpGet]
        public ActionResult<List<Disc>> GetDiscs() => _dataAccessProvider.GetDiscs();

        // GET by Id action
        [HttpGet("{id}")]
        public Disc GetDisc(int id)
        {
            return _dataAccessProvider.GetDisc(id);
        }

        // POST action
        [HttpPost]
        public IActionResult AddDisc([FromBody] Disc disc)
        {      
            if(ModelState.IsValid)
            {
                _dataAccessProvider.AddDisc(disc);
                return Ok($"You have successfully added disc named: \nNAME: {disc.name} \nID: {disc.id}");
            }      
            return BadRequest();
        }

        // PUT action
        /// <summary>
        /// Updates an existing disc with the new values provided in Body.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Disc/{id}
        ///     {
        ///         "id": 1,
        ///         "name": "Sheriff",
        ///         "manufacturer": "Dynamic Discs",
        ///         "weight": 171,
        ///         "speed": 12,
        ///         "glide": 5,
        ///         "turn": -1.5,
        ///         "fade": 2,
        ///         "inbag": true,
        ///         "farthest": 300,
        ///         "plastic": "Lucid"
        ///     }
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="disc"></param>
        /// <param name="item"></param>
        /// <returns>An IActionResult response that shows whether the action was completed successfully or not.</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>  
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateDisc(int id, [FromBody] Disc disc)
        {
            var existingDisc = _dataAccessProvider.GetDisc(id);

            if (existingDisc == null)
                return NotFound("Failed to find disc by id: " + id);

            if (existingDisc.id != disc.id)
                return BadRequest("Existing disc id does not match id provided in JSON Body.\nPlease check your input again and make sure the ids match, thanks!");

            if (ModelState.IsValid)
            {
                _dataAccessProvider.UpdateDisc(disc); 
                return CreatedAtAction(nameof(Disc), new { id = disc.id }, disc);
            }
            else
            {
                return BadRequest("Model does not match the standard, please see schema to verify everything matches up!");
            }          
        }

        // DELETE action
        [HttpDelete("{id}")]
        public IActionResult DeleteDisc(int id)
        {
            var disc = _dataAccessProvider.GetDisc(id);

            if (disc == null)
                return NotFound("Failed to find disc by id: " + id);

            _dataAccessProvider.DeleteDisc(id);

            return Ok("You have successfully deleted disc id: " + id);
        }
    }
}