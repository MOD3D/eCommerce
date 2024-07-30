using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Repository
{
    public interface IRepository<TB_ENTITY> where TB_ENTITY : class
    {
        IEnumerable<TB_ENTITY> GetProduct(); //YA
        IEnumerable<TB_ENTITY> GetAllRecords();//YA
        IQueryable<TB_ENTITY> GetAllRecordsIqueryable();//YA
        int GetAllrecordCount();//YA
        void Add(TB_ENTITY entity);//YA
        void Update(TB_ENTITY entity);//YA
        void UpdateByWhereClause(Expression<Func<TB_ENTITY,bool>> wherePredict, Action<TB_ENTITY> ForEachPredict);//YA
        TB_ENTITY GetFirstorDefault(int recordID);//YA
        void Remove(TB_ENTITY entity);//YA
        void RemoveByWhereClause(Expression<Func<TB_ENTITY, bool>> wherePredict);//YA
        void RemoveRangeByWhereClause(Expression<Func<TB_ENTITY, bool>> wherePredict);//YA
        void InactiveAndDeleteMarkByWhereClause(Expression<Func<TB_ENTITY, bool>> wherePredict, Action<TB_ENTITY> ForEachPredict);
        TB_ENTITY GetFirstorDefaultByParameter(Expression<Func<TB_ENTITY, bool>> wherePredict);
        IEnumerable<TB_ENTITY> GetListParameter(Expression<Func<TB_ENTITY, bool>> wherePredict);
        IEnumerable<TB_ENTITY> GetResultBySqlProcedure(string query, params object[] parameters);
        IEnumerable<TB_ENTITY> GetRecordsToShow(int PageNo, int PageSize, int CurrentPage, Expression<Func<TB_ENTITY, bool>> wherePredict, Expression<Func<TB_ENTITY, int>> orderByPredict);
    
    }
}
