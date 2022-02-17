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

namespace DotNet6BasicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatesApiController : ControllerBase
    {
        private readonly JWTAuthDBContext _context;

        public StatesApiController(JWTAuthDBContext context)
        {
            _context = context;
        }

        // GET: api/StatesApi
        [HttpGet("Search")]
        public async Task<ActionResult<IEnumerable<StateViewModel>>> GetSearch()
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
                var query = _context.States.AsQueryable();

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

        // GET: api/StatesApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StateViewModel>> Get(int id)
        {
            var state = await _context.States.FindAsync(id);

            if (state == null)
            {
                return NotFound();
            }

            return new StateViewModel { Id = state.Id, Name = state.Name };
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

            State state = _context.States.Find(id);
            if (state == null)
            {
                return NotFound();
            }

            state.Name = editStateViewModel.Name;
            _context.Entry(state).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StateExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/StatesApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StateViewModel>> Post(AddStateViewModel addStateViewModel)
        {
            State state = new State
            {
                Name = addStateViewModel.Name
            };

            _context.States.Add(state);
            await _context.SaveChangesAsync();

            StateViewModel model = new StateViewModel { Id = state.Id, Name = state.Name };
            return CreatedAtAction("Get", new { id = state.Id }, model);
        }

        // DELETE: api/StatesApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var state = await _context.States.FindAsync(id);
            if (state == null)
            {
                return NotFound();
            }

            _context.States.Remove(state);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StateExists(int id)
        {
            return _context.States.Any(e => e.Id == id);
        }
    }
}
