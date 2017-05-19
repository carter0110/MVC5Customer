using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskMVC.Models
{
    public class CustomerUsing
    {
        //private List<string> customerType;

        //public List<string> CustomerType
        //{
        //    get
        //    {
        //        if (customerType.Count == 0)
        //        {
        //            customerType = new List<string>
        //            {
        //                "年度貴賓會員",
        //                "VIP會員",
        //                "一般會員",
        //                "非會員"
        //            };
        //        }
        //        return customerType;
        //    }

        //}



        //}
        public enum CustomerType
        {
            年度貴賓會員 = 1,
            VIP會員 = 2,
            一般會員 = 3,
            非會員 = 4
        }


        #region Get客戶分類
        public static List<SelectListItem> Get客戶分類()
        {
            return Get客戶分類(0);
        }

        public static List<SelectListItem> Get客戶分類(string selectTxt)
        {
            int outInt = 0;
            if (int.TryParse((selectTxt + "").Trim(), out outInt))
                return Get客戶分類(outInt);

            return Get客戶分類();
        }

        public static List<SelectListItem> Get客戶分類(int selectVal)
        {
            var type = new List<SelectListItem>();
            type.Add(new SelectListItem() { Text = "--請選擇--", Value = "0" });
            foreach (var item in Enum.GetValues(typeof(CustomerType)))
            {
                type.Add(new SelectListItem() { Text = item.ToString(), Value = (int)item + "" });
                if (selectVal == (int)item)
                    type[type.Count - 1].Selected = true;
            }
            return type;
        } 
        #endregion
    }
}