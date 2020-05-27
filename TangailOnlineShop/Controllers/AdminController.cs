using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TangailOnlineShop.DAL;
using TangailOnlineShop.Models;
using TangailOnlineShop.Repository;

namespace TangailOnlineShop.Controllers
{
   
    public class AdminController : Controller
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
       
          public List<SelectListItem> GetCategory()
       
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var cat = _unitOfWork.GetRepositoryInstance<Tbl_Category>().GetAllRecords();
            foreach(var item in cat)
            {
                list.Add(new SelectListItem { Value = item.CategoryId.ToString(), Text = item.CategoryName });
               
            }
            return list;
        }
    public ActionResult Dashboard()
    {
            return View();
        }
        public ActionResult Categories()
        {
             List < Tbl_Category > allcategories = _unitOfWork.GetRepositoryInstance<Tbl_Category>().GetAllRecordsIQueryable().Where(i => i.IsDelete == false).ToList();
            return View(allcategories);
        }
        public ActionResult AddCategory()
        {

            return UpdateCategory(0); 
        }
        //[HttpPost]
        //public ActionResult AddCategory(Tbl_Category tb)
        //{

        //    _unitOfWork.GetRepositoryInstance<Tbl_Category>().Add(tb);
        //    return RedirectToAction("Categories");
        //}

        public ActionResult UpdateCategory(int categoryId)
        {
            CategoryDetail cd;
            if (Convert.ToString(categoryId) != null)
            {
                cd = JsonConvert.DeserializeObject<CategoryDetail>(JsonConvert.SerializeObject(_unitOfWork.GetRepositoryInstance<Tbl_Category>().GetFirstorDefault(categoryId)));
            }
            else
            {
                cd = new CategoryDetail();
            }
            return View("UpdateCategory", cd);

        }
        public ActionResult CategoryEdit(int catId)
        {
           
            return View(_unitOfWork.GetRepositoryInstance<Tbl_Category>().GetFirstorDefault(catId));
        }

        [HttpPost]
        public ActionResult CategoryEdit(Tbl_Category tbl)
        { 
            
            _unitOfWork.GetRepositoryInstance<Tbl_Category>().Update(tbl);
            return RedirectToAction("Categories");
        }
        public ActionResult Product()
        {
            return View(_unitOfWork.GetRepositoryInstance<Tbl_product>().GetProduct());
        }
        public ActionResult ProductEdit(int productId)
        {
            ViewBag.CategoryList = GetCategory();
            return View(_unitOfWork.GetRepositoryInstance<Tbl_product>().GetFirstorDefault(productId));
        }

        [HttpPost]
        public ActionResult ProductEdit(Tbl_product tbl, HttpPostedFileBase file)
        {
            string pic = null;
            if (file != null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/ProductImg/"), pic);
                file.SaveAs(path);
            }
            tbl.ProductImage = file!=null? pic:tbl.ProductImage;
            tbl.ModifiedDate = DateTime.Now;
            _unitOfWork.GetRepositoryInstance<Tbl_product>().Update(tbl);
            return RedirectToAction("Product");
        }
        public ActionResult ProductAdd()
        {
            ViewBag.CategoryList = GetCategory();
            return View();
        }
        [HttpPost]
        public ActionResult ProductAdd(Tbl_product tbl,HttpPostedFileBase file)
        {
            string pic = null;
            if(file != null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/ProductImg/"), pic);
                file.SaveAs(path); 
            }
            tbl.ProductImage = pic;
            tbl.CreatedDate = DateTime.Now;
            _unitOfWork.GetRepositoryInstance<Tbl_product>().Add(tbl);
            return RedirectToAction("Product");
        }
    }
}