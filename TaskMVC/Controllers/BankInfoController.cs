using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaskMVC.Models;

namespace TaskMVC.Controllers
{
    public class BankInfoController : BaseController
    {
        //private 客戶資料Entities db = new 客戶資料Entities();
        private 客戶銀行資訊Repository repoBankInfo = RepositoryHelper.Get客戶銀行資訊Repository();
        private 客戶資料Repository repoCustomer = RepositoryHelper.Get客戶資料Repository();

        // GET: BankInfo
        public ActionResult Index(string bankName)
        {
            ViewData.Model = repoBankInfo.GetSearchBankInfoList(bankName).OrderByDescending(o => o.Id);

            return View();
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var data = repoBankInfo.GetBankInfoById(id.Value);
            if (data != null)
            {
                ViewData.Model = data;
                return View();
            }
            else
                return HttpNotFound();
        }

        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(repoCustomer.All(), "Id", "客戶名稱");

            return View();
        }

        [HttpPost]
        public ActionResult Create(客戶銀行資訊 bankInfo)
        {
            if (ModelState.IsValid)
            {
                if (repoCustomer.IsExist(bankInfo.客戶Id))
                {
                    repoBankInfo.Add(bankInfo);
                    repoBankInfo.UnitOfWork.Commit();

                    return RedirectToAction("Index");
                }
                else
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.客戶Id = new SelectList(repoCustomer.All(), "Id", "客戶名稱", bankInfo.客戶Id);

            ViewData.Model = bankInfo;

            return View();
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var data = repoBankInfo.GetBankInfoById(id.Value);
            if (data != null)
            {
                ViewBag.客戶Id = new SelectList(repoCustomer.All(), "Id", "客戶名稱", data.客戶Id);

                ViewData.Model = data;

                return View();
            }
            else
                return HttpNotFound();
        }

        [HttpPost]
        public ActionResult Edit(int? id, 客戶銀行資訊 bankInfo)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (ModelState.IsValid)
            {
                if (repoBankInfo.IsExist(id.Value))
                {
                    if (repoCustomer.IsExist(bankInfo.客戶Id))
                    {
                        repoBankInfo.Add(bankInfo);
                        repoBankInfo.UnitOfWork.Commit();

                        return RedirectToAction("Index");
                    }
                }
                else
                    return HttpNotFound();
            }

            ViewBag.客戶Id = new SelectList(repoCustomer.All(), "Id", "客戶名稱", bankInfo.客戶Id);

            ViewData.Model = bankInfo;

            return View();
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var data = repoBankInfo.GetBankInfoById(id.Value);
            if (data != null)
            {
                ViewData.Model = data;
                return View();
            }
            else
                return HttpNotFound();
        }

        [ActionName("Delete")]
        [HttpPost]
        public ActionResult DeleteConfirm(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var data = repoBankInfo.GetBankInfoById(id.Value);
            if (data != null)
            {
                repoBankInfo.Delete(data);
                repoBankInfo.UnitOfWork.Commit();

                return RedirectToAction("Index");
            }
            else
                return HttpNotFound();

        }
    }
}