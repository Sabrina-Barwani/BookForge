using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookForge.DataAccess.Repository.IRepository
{
    // generic repository interface to perform CRUD operations on any model
    public interface IRepository<T> where T : class
    {
        // T is the model class can be any model

        IEnumerable<T> GetAll(); // get all records of the model from database ( retirve all category)

        // filltererd record , only one
        T Get(Expression<Func<T,bool>> filter); // get a single record of the model from database (retirve a single category)
        
        // add remove one entity
        void Add(T entity);
        void Remove(T entity);
        // remove range of entities
        void RemoveRange(IEnumerable<T> entity);





    }
}
