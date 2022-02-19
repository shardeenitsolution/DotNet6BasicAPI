using DotNet6BasicAPI.DAL.DBModels;
using LIbrary.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DotNet6BasicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MasterCourseController : ControllerBase
    {
        private readonly JWTAuthDBContext _context;

        public MasterCourseController(JWTAuthDBContext context)
        {
            _context = context;
        }

        // GET: api/StatesApi
        [HttpGet("Search")]
        public async Task<ActionResult<IEnumerable<MasterCourseModel>>> GetSearch()
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

                List<MasterCourseModel> data = new();
                //query
                var query = _context.MasterCourses.AsQueryable();

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

                data = await query.Select(s => new MasterCourseModel { Id = s.Id, Name = s.Name })
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
        public async Task<ActionResult<MasterCourseModel>> Get(int id)
        {
            var state = await _context.MasterCourses.FindAsync(id);

            if (state == null)
            {
                return NotFound();
            }

            return new MasterCourseModel { Id = state.Id, Name = state.Name };
        }

        // PUT: api/StatesApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, EditMasterCourseModel editMasterCourseModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ModelState.IsValid)
            {
                if (id != editMasterCourseModel.Id)
                {
                    return BadRequest();
                }

                var masterCourse = _context.MasterCourses.Find(id);
                if (masterCourse == null)
                {
                    return NotFound();
                }

                masterCourse.Name = editMasterCourseModel.Name;
                masterCourse.Code = editMasterCourseModel.Code;
                masterCourse.LaunchDate = editMasterCourseModel.LaunchDate;
                masterCourse.SemesterNumber = editMasterCourseModel.SemesterNumber;
                masterCourse.ModifiedOn = DateTime.UtcNow;
                masterCourse.ModifiedBy = userId;
                _context.Entry(masterCourse).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MasterCourseExists(id))
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
            else
            {
                return BadRequest();
            }
        }

        // POST: api/StatesApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MasterCourseModel>> Post(AddMasterCourseModel addMasterCourseModel)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                MasterCourse masterCourse = new MasterCourse
                {
                    Name = addMasterCourseModel.Name,
                    Code = addMasterCourseModel.Code,
                    SemesterNumber = addMasterCourseModel.SemesterNumber,
                    LaunchDate = addMasterCourseModel.LaunchDate,
                    ModifiedOn = DateTime.UtcNow,
                    ModifiedBy = userId,
                    IsActive = true
                };

                _context.MasterCourses.Add(masterCourse);
                await _context.SaveChangesAsync();

                MasterCourseModel model = new MasterCourseModel
                {
                    Id = masterCourse.Id,
                    Name = masterCourse.Name,
                    Code=masterCourse.Code,
                    IsActive = masterCourse.IsActive,
                    LaunchDate=masterCourse.LaunchDate,
                    SemesterNumber=masterCourse.SemesterNumber,
                };
                return CreatedAtAction("Get", new { id = masterCourse.Id }, model);
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE: api/StatesApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var masterCourse = await _context.MasterCourses.FindAsync(id);
            if (masterCourse == null)
            {
                return NotFound();
            }

            _context.MasterCourses.Remove(masterCourse);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MasterCourseExists(int id)
        {
            return _context.MasterCourses.Any(e => e.Id == id);
        }
    }
}
