using eCommerce.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Repository
{
    public class GenericUnitOfWork : IDisposable
    {
        private DB_ECOMMERCEEntities DBEntity = new DB_ECOMMERCEEntities();
        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public IRepository<TB_ENTITY_TYPE> GetRepositoryInstance<TB_ENTITY_TYPE>() where TB_ENTITY_TYPE : class
        {
            return new GenericRepository<TB_ENTITY_TYPE>(DBEntity);
        }

        public void SaveChanges()
        {
            DBEntity.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    DBEntity.Dispose();
                }
            }
            this.disposed = true;
        }


    }
}
