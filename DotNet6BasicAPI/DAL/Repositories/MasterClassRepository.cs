using DotNet6BasicAPI.DAL.DBModels;
using DotNet6BasicAPI.Models.DTO;
using LIbrary.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DotNet6BasicAPI.DAL.Repositories
{
    public class MasterClassRepository
    {
        private readonly JWTAuthDBContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MasterClassRepository(JWTAuthDBContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<MasterClassModel>> GetAsync()
        {
            return await _context
                .MasterClasses.Select(s => new MasterClassModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    MasterCourseId = s.MasterCourseId,
                    IsActive = s.IsActive,
                })
                .ToListAsync();
        }

        public async Task<MasterClassModel?> GetAsync(int id)
        {
            var masterClass = await _context.MasterClasses.FindAsync(id);
            if (masterClass == null)
                return null;
            else
                return new MasterClassModel
                {
                    Id = masterClass.Id,
                    Name = masterClass.Name,
                    MasterCourseId = masterClass.MasterCourseId,
                    IsActive = masterClass.IsActive,
                };
        }

        public async Task<MasterClassModel?> Update(EditMasterClassModel editMasterClassModel)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var masterClass = _context.MasterClasses.Find(editMasterClassModel.Id);
            if (masterClass == null)
            {
                return null;
            }

            masterClass.Name = editMasterClassModel.Name;
            masterClass.MasterCourseId = editMasterClassModel.MasterCourseId;
            masterClass.ModifiedBy = userId;
            masterClass.ModifiedOn = DateTime.UtcNow;

            _context.Entry(masterClass).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw ex;
            }

            return new MasterClassModel
            {
                Id = masterClass.Id,
                Name = masterClass.Name,
                MasterCourseId = masterClass.MasterCourseId,
                IsActive = masterClass.IsActive,
            };
        }

        public async Task<MasterClassModel> Add(AddMasterClassModel addMasterClassModel)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            MasterClass masterClass = new MasterClass
            {
                Name = addMasterClassModel.Name,
                MasterCourseId = addMasterClassModel.MasterCourseId,
                IsActive = true,
                ModifiedOn = DateTime.UtcNow,
                ModifiedBy = userId
            };

            _context.MasterClasses.Add(masterClass);
            await _context.SaveChangesAsync();

            return new MasterClassModel
            {
                Id = masterClass.Id,
                Name = masterClass.Name,
                MasterCourseId = masterClass.MasterCourseId,
                IsActive = masterClass.IsActive,
            };
        }

        public async Task<MasterClassModel?> Delete(int id)
        {
            var masterClass = await _context.MasterClasses.FindAsync(id);
            if (masterClass == null)
            {
                return null;
            }

            _context.MasterClasses.Remove(masterClass);
            await _context.SaveChangesAsync();

            return new MasterClassModel
            {
                Id = masterClass.Id,
                Name = masterClass.Name,
                MasterCourseId = masterClass.MasterCourseId,
                IsActive = masterClass.IsActive
            };
        }
    }
}
