using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskMVC.Models;

namespace TaskMVC.Controllers
{
    public class ValidateController : BaseController
    {
        private 客戶資料Entities db = new 客戶資料Entities();
        // GET: ValidEmailRepeat
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult CheckEmailRepeat(string Id, string 客戶Id, string Email)
        {
            bool isValidate = false;

            if (!string.IsNullOrEmpty(Id))
            {
                if (Id == "0")
                {
                    if (!db.客戶聯絡人.Where(o => o.是否已刪除 == false && o.客戶Id.ToString() == 客戶Id && o.Email == Email).Any())
                        isValidate = true;
                }
                else
                {
                    if (!db.客戶聯絡人.Where(o => o.是否已刪除 == false && o.Id.ToString() != Id && o.客戶Id.ToString() == 客戶Id && o.Email == Email).Any())
                        isValidate = true;
                }
            }

            return Json(isValidate, JsonRequestBehavior.AllowGet);
        }
    }
}