using eCommerce.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Repository
{
    public class GenericRepository<TB_ENTITY> : IRepository<TB_ENTITY> where TB_ENTITY : class
    {
        DbSet<TB_ENTITY> _dbSet;
        private DB_ECOMMERCEEntities _DBEntity;
        public GenericRepository(DB_ECOMMERCEEntities DBEntity)
        {
            _DBEntity = DBEntity;
            _dbSet = _DBEntity.Set<TB_ENTITY>();
        }
        IEnumerable<TB_ENTITY> IRepository<TB_ENTITY>.GetProduct()
        {
            return _dbSet.ToList();
        }
        public void Add(TB_ENTITY entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _dbSet.Add(entity);
            _DBEntity.SaveChanges();
            // Revisar CATEGORY_ID si el ID no existe explota 
        }
        public void Update(TB_ENTITY entity)
        {
            // Esta molestando la actualizaicion de datos al momento de modificar
            if (entity == null)
            {
                throw new ArgumentNullException("entity", "El valor no puede ser nulo.");
            }
            _dbSet.Attach(entity);
            _DBEntity.Entry(entity).State = EntityState.Modified;
            _DBEntity.SaveChanges();
        }
        public int GetAllrecordCount()
        {
            return _dbSet.Count();
        }
        public IEnumerable<TB_ENTITY> GetAllRecords()
        {
            return _dbSet.ToList();
        }
        public IQueryable<TB_ENTITY> GetAllRecordsIqueryable()
        {
            return _dbSet;
        }
        public TB_ENTITY GetFirstorDefault(int recordID)
        {
            return _dbSet.Find(recordID);
        }
        public TB_ENTITY GetFirstorDefaultByParameter(Expression<Func<TB_ENTITY, bool>> wherePredict)
        {
            return _dbSet.Where(wherePredict).FirstOrDefault();
        }
        public IEnumerable<TB_ENTITY> GetListParameter(Expression<Func<TB_ENTITY, bool>> wherePredict)
        {
            return _dbSet.Where(wherePredict).ToList();
        }
        public IEnumerable<TB_ENTITY> GetRecordsToShow(int PageNo, int PageSize, int CurrentPage, Expression<Func<TB_ENTITY, bool>> wherePredict, Expression<Func<TB_ENTITY, int>> orderByPredict)
        {
            if(wherePredict != null)
            {
                return _dbSet.OrderBy(orderByPredict).Where(wherePredict).ToList();
            }
            else
            {
                return _dbSet.OrderBy(orderByPredict).ToList();
            }
        }
        public IEnumerable<TB_ENTITY> GetResultBySqlProcedure(string query, params object[] parameters)
        {
            if(parameters != null)
            {
                return _DBEntity.Database.SqlQuery<TB_ENTITY>(query, parameters).ToList();
            }
            else
            {
                return _DBEntity.Database.SqlQuery<TB_ENTITY>(query).ToList();
            }
        }
        public void InactiveAndDeleteMarkByWhereClause(Expression<Func<TB_ENTITY, bool>> wherePredict, Action<TB_ENTITY> ForEachPredict)
        {
            _dbSet.Where(wherePredict).ToList().ForEach(ForEachPredict);
        }
        public void Remove(TB_ENTITY entity)
        {
            if(_DBEntity.Entry(entity).State == EntityState.Detached) 
                _dbSet.Attach(entity);
            _dbSet.Remove(entity);
        }
        public void RemoveByWhereClause(Expression<Func<TB_ENTITY, bool>> wherePredict)
        {
            TB_ENTITY entity = _dbSet.Where(wherePredict).FirstOrDefault();
            Remove(entity);
        }
        public void RemoveRangeByWhereClause(Expression<Func<TB_ENTITY, bool>> wherePredict)
        {
            List<TB_ENTITY> entity = _dbSet.Where(wherePredict).ToList();
            foreach(var ent in entity)
            {
                Remove(ent);
            }
        }
        public void UpdateByWhereClause(Expression<Func<TB_ENTITY, bool>> wherePredict, Action<TB_ENTITY> ForEachPredict)
        {
            _dbSet.Where(wherePredict).ToList().ForEach(ForEachPredict);
        }

    }
}
