using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using mccotter_net_api.Models;
using mccotter_net_api.DataAccess;

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
                return Ok();
            }      
            return BadRequest();
        }

        // PUT action
        [HttpPut("{id}")]
        public IActionResult UpdateDisc([FromBody] Disc disc)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.UpdateDisc(disc); 
                return Ok();
            }
            else
            {
                return BadRequest();
            }          
        }

        // DELETE action
        [HttpDelete("{id}")]
        public IActionResult DeleteDisc(int id)
        {
            var disc = _dataAccessProvider.GetDisc(id);

            if (disc == null)
                return NotFound();

            _dataAccessProvider.DeleteDisc(id);

            return Ok();
        }
    }
}