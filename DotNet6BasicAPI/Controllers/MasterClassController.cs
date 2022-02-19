using DotNet6BasicAPI.DAL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LIbrary.DTOs;
using Microsoft.AspNetCore.Authorization;
using DotNet6BasicAPI.BAL.Constants;

namespace DotNet6BasicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = UserRoles.Admin)]
    public class MasterClassController : ControllerBase
    {
        private readonly MasterClassRepository _masterClassRepository;

        public MasterClassController(MasterClassRepository masterClassRepository)
        {
            _masterClassRepository = masterClassRepository;
        }

        // GET: api/MasterClass
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterClassModel>>> Get()
        {
            return await _masterClassRepository.GetAsync();
        }

        // GET: api/MasterClass/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MasterClassModel>> Get(int id)
        {
            var masterClassModel = await _masterClassRepository.GetAsync(id);

            if (masterClassModel == null)
            {
                return NotFound();
            }

            return masterClassModel;
        }

        // PUT: api/MasterClass/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, EditMasterClassModel editMasterClassModel)
        {
            if (ModelState.IsValid)
            {
                if (id != editMasterClassModel.Id)
                {
                    return BadRequest();
                }

                var masterClassModel = await _masterClassRepository.Update(editMasterClassModel);
                if (masterClassModel == null)
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

        // POST: api/MasterClass
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MasterClassModel>> Post(AddMasterClassModel addMasterClassModel)
        {
            if (ModelState.IsValid)
            {
                MasterClassModel masterClassModel = await _masterClassRepository.Add(addMasterClassModel);
                return CreatedAtAction("Get", new { id = masterClassModel.Id }, masterClassModel);
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE: api/MasterClass/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var masterClassModel = await _masterClassRepository.Delete(id);
            if (masterClassModel == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
