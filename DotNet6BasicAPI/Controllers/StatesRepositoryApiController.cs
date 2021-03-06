#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DotNet6BasicAPI.DAL.DBModels;
using DotNet6BasicAPI.Models.DTO;
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

        // GET: api/StatesRepositoryApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StateViewModel>>> Get()
        {
            try
            {
                var draw = Request.Query["draw"].FirstOrDefault();
                var start = Request.Query["start"].FirstOrDefault();
                var length = Request.Query["length"].FirstOrDefault();
                var sortColumn = Request.Query["columns[" + Request.Query["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Query["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Query["search[value]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : -1;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                List<StateViewModel> data = new();
                //query
                var query = _stateRepository.Get();

                //Sorting 
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    //_data = _data.OrderBy(sortColumn + " " + sortColumnDirection);
                    query = query.OrderBy(o => o.Name);
                }

                //Search   
                if (!string.IsNullOrEmpty(searchValue))
                {
                    query = query.Where(m => m.Name.Contains(searchValue)
                                                || m.Name.Contains(searchValue));
                }

                //total number of rows count     
                recordsTotal = query.Count();

                //Paging     
                query = pageSize < 0 ? query : query.Skip(skip).Take(pageSize);

                data = await query.Select(s => new StateViewModel { Id = s.Id, Name = s.Name })
                .ToListAsync();
                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
                return Ok(jsonData);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // GET: api/StatesRepositoryApi/5
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

        // PUT: api/StatesRepositoryApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, EditStateViewModel editStateViewModel)
        {
            if (ModelState.IsValid)
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
            else
            {
                return BadRequest();
            }
        }

        // POST: api/StatesRepositoryApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StateViewModel>> Post(AddStateViewModel addStateViewModel)
        {
            if (ModelState.IsValid)
            {
                StateViewModel stateViewModel = await _stateRepository.Add(addStateViewModel);
                return CreatedAtAction("Get", new { id = stateViewModel.Id }, stateViewModel);
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE: api/StatesRepositoryApi/5
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
