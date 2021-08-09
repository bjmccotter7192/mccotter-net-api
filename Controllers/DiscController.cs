using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using mccotter_net_api.Models;
using mccotter_net_api.Services;

namespace ContosoPizza.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DiscController : ControllerBase
    {
        public DiscController()
        {
        }

        // GET all action
        [HttpGet]
        public ActionResult<List<Disc>> GetAll() => DiscService.GetAll();

        // GET by Id action
        [HttpGet("{id}")]
        public ActionResult<Disc> Get(int id)
        {
            var disc = DiscService.Get(id);

            if(disc == null)
                return NotFound();

            return disc;
        }

        // POST action
        [HttpPost]
        public IActionResult Create(Disc disc)
        {            
            // This code will save the pizza and return a result
            DiscService.Add(disc);
            return CreatedAtAction(nameof(Create), new { id = disc.Id }, disc);
        }

        // PUT action
        [HttpPut("{id}")]
        public IActionResult Update(int id, Disc pizza)
        {
            if (id != pizza.Id)
                return BadRequest();

            var existingPizza = DiscService.Get(id);
            if(existingPizza is null)
                return NotFound();

            DiscService.Update(pizza);           

            return NoContent();
        }

        // DELETE action
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var pizza = DiscService.Get(id);

            if (pizza is null)
                return NotFound();

            DiscService.Delete(id);

            return NoContent();
        }
    }
}