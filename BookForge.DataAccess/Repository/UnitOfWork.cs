using BookForge.DataAccess.Data;
using BookForge.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookForge.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public UnitOfWork (ApplicationDbContext db) 
        {
            _db = db;
            Category = new CatogryRepository(_db);
        }
        public ICategoryRepository Category { get;private set; }

        public void save()
        {
            _db.SaveChanges();
        }
    }
}
