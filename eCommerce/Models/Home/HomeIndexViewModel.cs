using eCommerce.DAL;
using eCommerce.Repository;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Linq;
using System.Web;

namespace eCommerce.Models.Home
{
    public class HomeIndexViewModel
    {
        //metodo no estetico???
        public GenericUnitOfWork _unitofWork = new GenericUnitOfWork();
        DB_ECOMMERCEEntities context = new DB_ECOMMERCEEntities();
        //public IEnumerable<TB_PRODUCT> listOfProduct { get; set; }
        public IPagedList<TB_PRODUCT> listOfProduct { get; set; }

        public HomeIndexViewModel CreateModel(string search, int pageSize, int? page)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@search",search??(object)DBNull.Value)
            };
            //IEnumerable<TB_PRODUCT> data = context.Database.SqlQuery<TB_PRODUCT>("GetBySearch @search", param).ToList().ToPagedList(page ?? 1, pageSize);

            IPagedList<TB_PRODUCT> data = context.Database.SqlQuery<TB_PRODUCT>("GetBySearch @search", param).ToList().ToPagedList(page ?? 1,pageSize);
            return new HomeIndexViewModel
            {
                //listOfProduct = _unitofWork.GetRepositoryInstance<TB_PRODUCT>().GetAllRecords()
                listOfProduct= data
            };
        }
        //public DB_ECOMMERCEEntities context = new DB_ECOMMERCEEntities();
        //public List<TB_PRODUCT> ListOfProduct {  get; set; }
        //public HomeIndexViewModel CreateModel()
        //{
        //    return new HomeIndexViewModel
        //    {
        //        ListOfProduct = context.TB_PRODUCT.ToList()
        //    }; 
        //}
    }
    
}
