using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TangailOnlineShop.DAL;
using TangailOnlineShop.Repository;

namespace TangailOnlineShop.Models.Home
{
    public class HomeIndexViewModel
    {
        public GenericUnitOfWork unitOfWork = new GenericUnitOfWork();
        public IEnumerable<Tbl_product> ListOfProduct { get; set; }
        public HomeIndexViewModel CreateModel()
        {
            return new HomeIndexViewModel
            {
                ListOfProduct = unitOfWork.GetRepositoryInstance<Tbl_product>().GetAllRecords()
            };
        }
    }
}