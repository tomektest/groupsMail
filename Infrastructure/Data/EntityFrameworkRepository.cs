using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Data
{
    public class EntityFrameworkRepository<T>: IRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext _applicationDbContext;

        public EntityFrameworkRepository(ApplicationDbContext ApplicationDbContext)
        {
            _applicationDbContext = ApplicationDbContext;
        }

        public IEnumerable<T> GetAll()
        {
            return _applicationDbContext.Set<T>().AsEnumerable();
        }
    }
}
