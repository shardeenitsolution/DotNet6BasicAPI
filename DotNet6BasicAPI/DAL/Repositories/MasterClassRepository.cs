using DotNet6BasicAPI.DAL.DBModels;
using DotNet6BasicAPI.Models.DTO;
using LIbrary.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DotNet6BasicAPI.DAL.Repositories
{
    public class MasterClassRepository
    {
        private readonly JWTAuthDBContext _context;

        public MasterClassRepository(JWTAuthDBContext context)
        {
            _context = context;
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
            var masterClass = _context.MasterClasses.Find(editMasterClassModel.Id);
            if (masterClass == null)
            {
                return null;
            }

            masterClass.Name = editMasterClassModel.Name;
            masterClass.MasterCourseId = editMasterClassModel.MasterCourseId;
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
            MasterClass masterClass = new MasterClass
            {
                Name = addMasterClassModel.Name,
                MasterCourseId = addMasterClassModel.MasterCourseId,
                IsActive = true,
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
