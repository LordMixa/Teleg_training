using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teleg_training.DBEntities;

namespace Teleg_training.Repository
{
    public class LikeRepository : IRepository<DBLike>
    {
        public readonly DbSet<DBLike> _likeSet;
        public readonly UnitOfWork _unitOfWork;
        public LikeRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _likeSet = _unitOfWork.Context.Set<DBLike>();
        }
        public void Create(DBLike item)
        {
            _likeSet.Add(item);
        }

        public void Delete(int id)
        {
            DBLike like = _likeSet.Find(id);
            if (like != null)
                _likeSet.Remove(like);
        }

        public DBLike? Get(int id)
        {
            return _likeSet.Find(id);
        }

        public IEnumerable<DBLike> GetAll()
        {
            return _likeSet;
        }

        public void Update(DBLike item)
        {
            _likeSet.Update(item);
        }
        public async Task<List<DBLike>> GetAllAsync()
        {
            return await _likeSet.ToListAsync();
        }

        public async Task DeleteAsync(int likeId)
        {
            var like = await _likeSet.FindAsync(likeId);
            if (like != null)
            {
                _likeSet.Remove(like);
            }
        }

        public async Task CreateAsync(DBLike like)
        {
            await _likeSet.AddAsync(like);
        }
    }
}
