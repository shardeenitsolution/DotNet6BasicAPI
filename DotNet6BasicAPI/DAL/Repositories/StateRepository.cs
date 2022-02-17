using DotNet6BasicAPI.DAL.DBModels;
using DotNet6BasicAPI.Models.DAO;
using Microsoft.EntityFrameworkCore;

namespace DotNet6BasicAPI.DAL.Repositories
{
    public class StateRepository
    {
        private readonly JWTAuthDBContext _context;

        public StateRepository(JWTAuthDBContext context)
        {
            _context = context;
        }
        public async Task<List<StateViewModel>> GetAsync()
        {
            return await _context
                .States.Select(s => new StateViewModel { Id = s.Id, Name = s.Name })
                .ToListAsync();
        }

        public async Task<StateViewModel?> GetAsync(int id)
        {
            var state = await _context.States.FindAsync(id);
            if (state == null)
                return null;
            else
                return new StateViewModel { Id = state.Id, Name = state.Name };
        }

        public async Task<StateViewModel?> Update(EditStateViewModel editStateViewModel)
        {
            State? state = _context.States.Find(editStateViewModel.Id);
            if (state == null)
            {
                return null;
            }

            state.Name = editStateViewModel.Name;
            _context.Entry(state).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw ex;
            }

            return new StateViewModel { Id = state.Id, Name = state.Name };
        }

        public async Task<StateViewModel> Add(AddStateViewModel addStateViewModel)
        {
            State state = new State
            {
                Name = addStateViewModel.Name
            };

            _context.States.Add(state);
            await _context.SaveChangesAsync();

            return new StateViewModel { Id = state.Id, Name = state.Name };
        }

        public async Task<StateViewModel?> Delete(int id)
        {
            var state = await _context.States.FindAsync(id);
            if (state == null)
            {
                return null;
            }

            _context.States.Remove(state);
            await _context.SaveChangesAsync();

            return new StateViewModel { Id = state.Id, Name = state.Name };
        }
    }
}
