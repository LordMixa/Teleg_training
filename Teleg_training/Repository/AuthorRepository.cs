using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teleg_training.DBEntities;

namespace Teleg_training.Repository
{
    public class AuthorRepository : IRepository<DBAuthor>
    {
        public readonly DbSet<DBAuthor> _authorSet;
        public readonly UnitOfWork _unitOfWork;
        public AuthorRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _authorSet = _unitOfWork.Context.Set<DBAuthor>();
        }
        DBAuthor ?IRepository<DBAuthor>.Get(int id)
        {
            return _authorSet.Find(id);
        }

        void IRepository<DBAuthor>.Create(DBAuthor item)
        {
            throw new NotImplementedException();
        }

        void IRepository<DBAuthor>.Update( DBAuthor item)
        {
            throw new NotImplementedException();
        }

        void IRepository<DBAuthor>.Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DBAuthor> GetAll()
        {
            return _authorSet;
        }
        public async Task<List<DBAuthor>> GetAllAsync()
        {
            return await _authorSet.ToListAsync();
        }
    }
}
