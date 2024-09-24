using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teleg_training.DBEntities;

namespace Teleg_training.Repository
{
    internal class ProgramListRepository : IRepository<DBProgramList>
    {
        public readonly UnitOfWork _unitOfWork;
        public readonly DbSet<DBProgramList> _programListSet;
        public ProgramListRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _programListSet = _unitOfWork.Context.Set<DBProgramList>();
        }
        public void Create(DBProgramList item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public DBProgramList? Get(int id)
        {
            return _programListSet.Find(id);
        }
        public DBProgramList? GetbyName(string Name)
        {
            return _programListSet.Where(p => p.Name == Name).FirstOrDefault();
        }
        public IEnumerable<DBProgramList> GetAll()
        {
            return _programListSet;
        }

        public void Update( DBProgramList item)
        {
            _programListSet.Update(item);
        }
        public async Task<DBProgramList?> GetbyNameAsync(string name)
        {
            return await _programListSet.Where(p => p.Name == name).FirstOrDefaultAsync();
        }
        public async Task<List<DBProgramList>> GetAllAsync()
        {
            return await _programListSet.ToListAsync();
        }

    }
}
