using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teleg_training.DBEntities;

namespace Teleg_training.Repository
{
    public class UserRepository : IRepository<DBUser>
    {
        public readonly DbSet<DBUser> _userSet;
        public readonly UnitOfWork _unitOfWork;
        public UserRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userSet = _unitOfWork.Context.Set<DBUser>();
        }
        public void Create(DBUser item)
        {
            _userSet.Add(item);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public DBUser? Get(int id)
        {
            return _userSet.Find(id);
        }
        public DBUser GetbyTGId(long id)
        {
            return _userSet.Where(p => p.TGId == id).FirstOrDefault();
        }
        public IEnumerable<DBUser> GetAll()
        {
            return _userSet;
        }

        public void Update(DBUser item)
        {
            throw new NotImplementedException();
        }
        public async Task<DBUser?> GetByTGIdAsync(long tgId)
        {
            return await _userSet.FirstOrDefaultAsync(u => u.TGId == tgId);
        }
        public async Task<List<DBUser>> GetAllAsync()
        {
            return await _userSet.ToListAsync();
        }
    }
}
