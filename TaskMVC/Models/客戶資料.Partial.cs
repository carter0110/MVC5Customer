namespace TaskMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    [MetadataType(typeof(客戶資料MetaData))]
    public partial class 客戶資料
    {
        //public int 銀行帳戶數量
        //{
            //get { return this.客戶銀行資訊.Count; }
        //}

        //public int 聯絡人數量 { get { return this.客戶聯絡人.Count(o=>o.是否已刪除==false && o.客戶Id== this.Id); } }
    }
    
    public partial class 客戶資料MetaData
    {
        [Required]
        public int Id { get; set; }
        
        [StringLength(5, ErrorMessage= "客戶名稱長度不得大於 5 個字")]
        [Required]
        public string 客戶名稱 { get; set; }
        
        [Range(0, 99999999, ErrorMessage = "統一編號請輸入數字")]
        [StringLength(8, MinimumLength = 8, ErrorMessage= "統一編號請輸入 8 位數")]
        [Required]
        public string 統一編號 { get; set; }
        
        [Phone]
        [StringLength(12, ErrorMessage= "電話字數不得大於 12 個字元")]
        [Required]
        public string 電話 { get; set; }
        
        [Phone]
        [StringLength(12, ErrorMessage= "傳真字數不得大於 12 個字元")]
        public string 傳真 { get; set; }
        
        [StringLength(100, ErrorMessage= "地址字數不得大於 100 個字元")]
        public string 地址 { get; set; }
        
        [EmailAddress]
        [StringLength(100, ErrorMessage= "Email字數不得大於 100 個字元")]
        public string Email { get; set; }
        [Required]
        public bool 是否已刪除 { get; set; }


        public int 客戶分類 { get; set; }

        public virtual ICollection<客戶銀行資訊> 客戶銀行資訊 { get; set; }
        public virtual ICollection<客戶聯絡人> 客戶聯絡人 { get; set; }

        

    }
}
