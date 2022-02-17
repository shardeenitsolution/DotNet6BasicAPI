#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DotNet6BasicAPI.DAL.DBModels;
using DotNet6BasicAPI.Models.DAO;
using DotNet6BasicAPI.DAL.Repositories;

namespace DotNet6BasicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatesRepositoryApiController : ControllerBase
    {
        private readonly StateRepository _stateRepository;

        public StatesRepositoryApiController(StateRepository stateRepository)
        {
            _stateRepository = stateRepository;
        }

        // GET: api/StatesApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StateViewModel>>> Get()
        {
            return await _stateRepository.GetAsync();
        }

        // GET: api/StatesApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StateViewModel>> Get(int id)
        {
            var state = await _stateRepository.GetAsync(id);

            if (state == null)
            {
                return NotFound();
            }

            return state;
        }

        // PUT: api/StatesApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, EditStateViewModel editStateViewModel)
        {
            if (id != editStateViewModel.Id)
            {
                return BadRequest();
            }

            StateViewModel stateViewModel = await _stateRepository.Update(editStateViewModel);
            if (stateViewModel == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        // POST: api/StatesApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StateViewModel>> Post(AddStateViewModel addStateViewModel)
        {
            StateViewModel stateViewModel = await _stateRepository.Add(addStateViewModel);
            return CreatedAtAction("Get", new { id = stateViewModel.Id }, stateViewModel);
        }

        // DELETE: api/StatesApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var stateViewModel = await _stateRepository.Delete(id);
            if (stateViewModel == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
