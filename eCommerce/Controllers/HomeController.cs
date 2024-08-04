using eCommerce.DAL;
using eCommerce.Models.Home;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.Controllers
{
    public class HomeController : Controller
    {
        DB_ECOMMERCEEntities context = new DB_ECOMMERCEEntities();
        public ActionResult Index(string search, int? page)
        {
            HomeIndexViewModel viewModel = new HomeIndexViewModel();
            return View(viewModel.CreateModel(search, 12, page));
        }
        public ActionResult AddtoCart(int productID, string url)
        {
            if (Session["cart"] == null)
            {

                //var cart = new List<Item>();
                List<Item> cart = new List<Item>();
                var product = context.TB_PRODUCT.Find(productID);
                cart.Add(new Item()
                {
                    Product = product,
                    Quantity = 1
                });
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var count = cart.Count(); 
                var product = context.TB_PRODUCT.Find(productID);
                //foreach(var item in cart)
                //{
                //    if(item.Product.PRODUCT_ID == productID)
                //    {
                //        int prevQty = item.Quantity;
                //        cart.Remove(item);
                //        cart.Add(new Item()
                //        {
                //            Product = product,
                //            Quantity = prevQty+1
                //        });
                //        break;
                //    }
                //    else
                //    {
                //        cart.Add(new Item()
                //        {
                //            Product = product,
                //            Quantity = 1
                //        });
                //    }
                //}
                for (int i = 0; i< count; i++)
                {
                    if (cart[i].Product.PRODUCT_ID == productID)
                    {
                        int prevQty = cart[i].Quantity;
                        cart.Remove(cart[i]);
                        cart.Add(new Item()
                        {
                            Product = product,
                            Quantity = prevQty + 1
                        });
                        break;
                    }
                    else
                    {
                        var prd = cart.Where(x=>x.Product.PRODUCT_ID == productID).SingleOrDefault();
                        if(prd == null)
                        {
                            cart.Add(new Item()
                            {
                                Product = product,
                                Quantity = 1
                            });
                        }
                    }
                }
                Session["cart"] = cart;
            }
            return Redirect("Index");
            //return Redirect(url);

        }
        public ActionResult RemoveFromCart(int productID)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            //var product = context.TB_PRODUCT.Find(productID);
            foreach (var item in cart)
            {
                if (item.Product.PRODUCT_ID == productID)
                {
                    cart.Remove(item);
                    break;
                }
            }
            Session["cart"] = cart;
            return Redirect("Index");
        }
        public ActionResult DecreaseQty(int productID) 
        { 
            if (Session["cart"] != null)
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var product = context.TB_PRODUCT.Find(productID);
                foreach (var item in cart)
                {
                    if(item.Product.PRODUCT_ID == productID)
                    {
                        int prevQty = item.Quantity;
                            if(prevQty > 0)
                        {
                            cart.Remove(item);
                            cart.Add(new Item()
                            {
                                Product = product,
                                Quantity = prevQty - 1
                            });
                        }
                            break;
                    }
                }
                Session["cart"] = cart;
            }
            return Redirect("Checkout");
        }
        public ActionResult Checkout()
        {
            return View("Checkout");
        }
        public ActionResult CheckoutDetails()
        {
            return View();
        }
        public ActionResult Products()
        {
            ViewBag.Message = "Your product page.";

            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Cotizaziones";

            return View();
        }

    }
}