using ClosedXML.Excel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaskMVC.Models;
using static TaskMVC.Models.CustomerUsing;

namespace TaskMVC.Controllers
{
    public class CustomerController : BaseController
    {
        private 客戶資料Repository repo = RepositoryHelper.Get客戶資料Repository();
        //private 客戶資料Entities db = new 客戶資料Entities();

        // GET: Customer
        public ActionResult Index(string customerName, string customerType, int page = 1, string orderCustomerName = "")
        {
            ViewBag.客戶分類List = Get客戶分類(customerType);

            page =  page < 1 ? 1 : page;

            ViewData.Model = repo.GetSearchCustomerList(customerName, customerType).OrderByDescending(o => o.Id).GetOrderCustomerList(orderCustomerName).ToPagedList(page, 2);
            //

            return View();
        }



        public ActionResult ExportXLSX()
        {
            var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("Customer");
            //ws.Cell()

            return File("", "");
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "id should not be null.");

            var data = repo.GetCustomerById(id.Value);
            if (data != null)
            {
                ViewData.Model = data;
                return View();
            }
            else
                return HttpNotFound("Not found customer.");
        }
        
        public ActionResult Create()
        {
            ViewBag.客戶分類List = Get客戶分類();

            return View();
        }

        [HttpPost]
        public ActionResult Create(客戶資料 customer)
        {
            if (ModelState.IsValid)
            {
                repo.Add(customer);
                repo.UnitOfWork.Commit();

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.客戶分類List = Get客戶分類(customer.客戶分類.Value);
                ViewData.Model = customer;
                return View();
            }
        }

        public ActionResult Update(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "id should not be null.");

            var data = repo.GetCustomerById(id.Value);

            if (data != null)
            {
                ViewBag.客戶分類List = Get客戶分類(data.客戶分類.HasValue ? data.客戶分類.Value : 0);
                ViewData.Model = data;
                return View();
            }
            else
                return HttpNotFound("Not found customer.");
        }

        [HttpPost]
        public ActionResult Update(int? id, 客戶資料 customer)
        {
            if(id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "id should not be null.");

            if (ModelState.IsValid)
            {
                if(repo.IsExist(id.Value))
                {
                    ViewBag.客戶分類List = Get客戶分類(customer.客戶分類.HasValue ? customer.客戶分類.Value : 0);

                    repo.Update(customer);
                    repo.UnitOfWork.Commit();

                    return RedirectToAction("Index");
                }
                else
                    return HttpNotFound("Not found customer.");
            }
            ViewData.Model = customer;

            return View();
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "id should not be null.");

            var data = repo.GetCustomerById(id.Value);

            if (data != null)
            {
                ViewData.Model = data;
                return View();
            }
            else
                return HttpNotFound("Not found customer.");
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "id should not be null.");

            var data = repo.GetCustomerById(id.Value);

            if (data != null)
            {
                repo.Delete(data);
                repo.UnitOfWork.Commit();

                return RedirectToAction("Index");
            }
            else
                return HttpNotFound("Not found customer.");
        }
    }

    public static class CustomerExtend {
        public static IQueryable<客戶資料> GetOrderCustomerList(this IQueryable<客戶資料> list, string orderCustomerName)
        {
            if (orderCustomerName == "asc")
                list = list.OrderBy(o => o.客戶名稱);
            else if (orderCustomerName == "desc")
                list = list.OrderByDescending(o => o.客戶名稱);
            return list;
        }
    }
}