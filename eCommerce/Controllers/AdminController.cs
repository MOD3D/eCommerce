using eCommerce.DAL;
using eCommerce.Models;
using eCommerce.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;

namespace eCommerce.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        public ActionResult Dashboard()
        {
            return View();
        }
        public List<SelectListItem> GetCategory()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            try
            {
                var cat = _unitOfWork.GetRepositoryInstance<TB_CATEGORY>().GetAllRecords();
                foreach (var item in cat)
                {
                    list.Add(new SelectListItem { Value = item.CATEGORY_ID.ToString(), Text = item.CATEGORY_NAME });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las categorias", ex);
            }
            return list;
        }
        public ActionResult Categories() 
        {
            //llama a la tabla categorias
            List<TB_CATEGORY> allcategories = _unitOfWork.GetRepositoryInstance<TB_CATEGORY>().GetAllRecordsIqueryable().Where(i=>i.IS_DELETE==false).ToList();
            return View(allcategories);
        }
        public ActionResult AddCategory()
        {
            return UpdateCategory(0);
        }
        public ActionResult UpdateCategory(int? CATEGORY_ID)
        {
            CategoryDetail categoryID;

            if (CATEGORY_ID.HasValue)
            {
                var category = _unitOfWork.GetRepositoryInstance<TB_CATEGORY>().GetFirstorDefault(CATEGORY_ID.Value);
                if (category != null)
                {
                    categoryID = JsonConvert.DeserializeObject<CategoryDetail>(JsonConvert.SerializeObject(category));
                }
                else
                {
                    categoryID = new CategoryDetail(); // O maneja el caso donde la categoría no se encuentra
                }
            }
            else
            {
                categoryID = new CategoryDetail();
            }

            return View("UpdateCategory", categoryID);
        }
        public ActionResult CategoryEdit(int categoryID)
        {
            //return View(_unitOfWork.GetRepositoryInstance<TB_CATEGORY>().GetFirstorDefault(categoryID));
            var category = _unitOfWork.GetRepositoryInstance<TB_CATEGORY>().GetFirstorDefault(categoryID);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CategoryEdit(TB_CATEGORY category)
        {
            if (category == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "El valor no puede ser nulo.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var categoryRepo = _unitOfWork.GetRepositoryInstance<TB_CATEGORY>();

                    if (category.CATEGORY_ID == 0)
                    {
                        categoryRepo.Add(category);
                    }
                    else
                    {
                        categoryRepo.Update(category);
                    }

                    _unitOfWork.SaveChanges();
                    return RedirectToAction("Categories");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al guardar los cambios: " + ex.Message);
                }
            }

            return View(category);
        }
        public ActionResult Product()
        {
            return View(_unitOfWork.GetRepositoryInstance<TB_PRODUCT>().GetProduct());
        }
        public ActionResult ProductEdit(int? recordID)
        {
            //ViewBag.CategoryList = GetCategory();
            //return View(_unitOfWork.GetRepositoryInstance<TB_PRODUCT>().GetFirstorDefault(recordID));
            if (!recordID.HasValue)
            {
                return RedirectToAction("Error");
            }

            var product = _unitOfWork.GetRepositoryInstance<TB_PRODUCT>().GetFirstorDefault(recordID.Value);
            if (product == null)
            {
                return RedirectToAction("NotFound");
            }
            ViewBag.CategoryList = GetCategory();
            return View(product);
        }
        [HttpPost]
        public ActionResult ProductEdit(TB_PRODUCT recordID, HttpPostedFileBase file)
        {
           
            string pic = null;
            if (file != null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/ProductImg/"), pic);
                // file is uploaded
                file.SaveAs(path);
            }
            recordID.PRODUCT_IMAGE = file!=null?pic:recordID.PRODUCT_IMAGE;
            //
            recordID.MODIFIED_DATE = DateTime.Now;
            _unitOfWork.GetRepositoryInstance<TB_PRODUCT>().Update(recordID);
            _unitOfWork.SaveChanges();
            //return RedirectToAction("Product");
           if (recordID == null)
            {
                return RedirectToAction("Error");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    recordID.MODIFIED_DATE = DateTime.Now;
                    _unitOfWork.GetRepositoryInstance<TB_PRODUCT>().Update(recordID);
                    _unitOfWork.SaveChanges();
                    return RedirectToAction("Product");
                }
                catch (Exception ex)
                {
                    // Manejar la excepción según sea necesario
                    ModelState.AddModelError("", "Error al actualizar el producto: " + ex.Message);
                }
            }
            ViewBag.CategoryList = GetCategory();
            return RedirectToAction("Product");
        }
        public ActionResult ProductAdd()
        {
           ViewBag.CategoryList = GetCategory();
           return View();
        }
        [HttpPost]
        public ActionResult ProductAdd(TB_PRODUCT recordID, HttpPostedFileBase file)
        {
            string pic = null;
            if (file != null) 
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/ProductImg/"),pic);
                // file is uploaded
                file.SaveAs(path);
            }
            recordID.PRODUCT_IMAGE = pic;
            recordID.CREATE_DATE = DateTime.Today;
            _unitOfWork.GetRepositoryInstance<TB_PRODUCT>().Add(recordID);
            return RedirectToAction("Product");
        }

    }
}