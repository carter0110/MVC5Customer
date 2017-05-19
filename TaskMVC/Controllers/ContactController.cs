using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaskMVC.Models;

namespace TaskMVC.Controllers
{
    public class ContactController : BaseController
    {
        private 客戶聯絡人Repository repoContact = RepositoryHelper.Get客戶聯絡人Repository();
        private 客戶資料Repository repoCustomer = RepositoryHelper.Get客戶資料Repository();
        //private 客戶資料Entities db = new 客戶資料Entities();

        // GET: Contact
        public ActionResult Index(string contactName, string jobTitle)
        {
            ViewData.Model = repoContact.GetSearchContactList(contactName, jobTitle);

            return View();
        }

        public JsonResult GetDetails(int id)
        {

            //var list = repoContact.UnitOfWork.Context.Set<客戶聯絡人>().Include("客戶資料").Where(o => o.是否已刪除 == false);
            //var list = repoContact.UnitOfWork.Context.Entry(new 客戶聯絡人()).Reference<客戶資料>(o => o.);
            //var list = repoContact.GetSearchContactList(contactName, jobTitle).OrderByDescending(o => o.Id);

            var data = repoContact.GetContactById(id);
            if (data == null)
                return Json(null, JsonRequestBehavior.AllowGet);

            repoContact.UnitOfWork.Context.Configuration.LazyLoadingEnabled = false;

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            //var data = repoContact.GetContactById(id.Value);
            //if (data != null)
            //{
            //    ViewData.Model = data;
            //    return View();
            //}
            //else
            //    return HttpNotFound();
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.客戶Ids = new SelectList(repoCustomer.All(), "Id", "客戶名稱");

            return View();
        }

        [HttpPost]
        public ActionResult Create(客戶聯絡人 contact)
        {
            //if (ModelState.IsValid && (Request.Form["客戶Id"] + "") != "")
            //{
            //    contact.客戶Id = int.Parse(Request.Form["客戶Id"].ToString());
            //    db.客戶聯絡人.Add(contact);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            if (ModelState.IsValid)
            {
                //contact.客戶Id = int.Parse((contact.客戶Id + "").Trim());
                repoContact.Add(contact);
                repoContact.UnitOfWork.Commit();

                return RedirectToAction("Index");
            }

            ViewBag.客戶Ids = new SelectList(repoCustomer.All(), "Id", "客戶名稱", contact.客戶Id);

            ViewData.Model = contact;
            return View();
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var data = repoContact.GetContactById(id.Value);

            if (data!= null)
            {
                ViewBag.客戶Ids = new SelectList(repoCustomer.All(), "Id", "客戶名稱", data.客戶Id);
                ViewData.Model = data;
                return View();
            }
            else
                return HttpNotFound();
        }

        [HttpPost]
        public ActionResult Edit(int? id, 客戶聯絡人 contact)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (ModelState.IsValid)
            {
                if (repoContact.IsExist(id.Value))
                {
                    repoContact.Edit(contact);
                    repoContact.UnitOfWork.Commit();

                    return RedirectToAction("Index");
                }
                else
                    return HttpNotFound();
            }

            ViewBag.客戶Ids = new SelectList(repoCustomer.All(), "Id", "客戶名稱", contact.客戶Id);
            ViewData.Model = contact;
            return View();
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var data = repoContact.GetContactById(id.Value);
            if (data != null)
            {
                ViewData.Model = data;
                return View();
            }
            else
                return HttpNotFound();
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var data = repoContact.GetContactById(id.Value);
            if (data != null)
            {
                repoContact.Delete(data);
                repoContact.UnitOfWork.Commit();

                return RedirectToAction("Index");
            }
            else
                return HttpNotFound();
        }
    }
}