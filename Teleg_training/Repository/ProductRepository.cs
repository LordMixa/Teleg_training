using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teleg_training.DBEntities;

namespace Teleg_training.Repository
{
    internal class ProductRepository : IRepository<DBProduct>
    {
        public readonly UnitOfWork _unitOfWork;
        public readonly DbSet<DBProduct> _productSet;
        public ProductRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _productSet = _unitOfWork.Context.Set<DBProduct>();
        }
        public void Create(DBProduct item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public DBProduct? Get(int id)
        {
            return _productSet.Find(id);
        }

        public IEnumerable<DBProduct> GetAll()
        {
            return _productSet;
        }
        public async Task<List<DBProduct>> GetAllAsync()
        {
            return await _productSet.ToListAsync();
        }
        public void Update( DBProduct item)
        {
            throw new NotImplementedException();
        }
    }
}
