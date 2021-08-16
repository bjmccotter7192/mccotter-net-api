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
        public ActionResult<Disc> Get(int id)
        {
            var disc = _dataAccessProvider.GetDisc(id);

            if(disc == null)
                return NotFound();

            return disc;
        }

        // POST action
        [HttpPost]
        public IActionResult Create(Disc disc)
        {            
            _dataAccessProvider.AddDisc(disc);
            return CreatedAtAction(nameof(Create), new { id = disc.id }, disc);
        }

        // PUT action
        [HttpPut("{id}")]
        public IActionResult Update(int id, Disc disc)
        {
            if (id != disc.id)
                return BadRequest();

            var currentDisc = _dataAccessProvider.GetDisc(id);
            if(currentDisc is null)
                return NotFound();

            _dataAccessProvider.UpdateDisc(disc);           

            return NoContent();
        }

        // DELETE action
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var disc = _dataAccessProvider.GetDisc(id);

            if (disc is null)
                return NotFound();

            _dataAccessProvider.DeleteDisc(id);

            return NoContent();
        }
    }
}