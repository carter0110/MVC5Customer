namespace TaskMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;

    [MetadataType(typeof(客戶聯絡人MetaData))]
    public partial class 客戶聯絡人 : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var db = new 客戶資料Entities();
            if (this.Id ==0)
            {
                if (db.客戶聯絡人.Count(o => o.是否已刪除 == false && o.客戶Id == this.客戶Id && o.Email == this.Email) > 0)
                    yield return new ValidationResult("同一客戶的聯絡人email不可重複!", new string[] { "Email" });
            }
            else
            {
                if (db.客戶聯絡人.Count(o => o.是否已刪除 == false && o.客戶Id == this.客戶Id && o.Id != this.Id && o.Email == this.Email) > 0)
                    yield return new ValidationResult("同一客戶的聯絡人email不可重複!", new string[] { "Email" });
            }
        }
    }

    public partial class 客戶聯絡人MetaData 
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [Remote("CheckEmailRepeat", "Validate", HttpMethod = "Post", AdditionalFields = "Id,Email", ErrorMessage = "FE Validate: 同一客戶的聯絡人email不可重複!")]
        public int 客戶Id { get; set; }
        
        [StringLength(10, ErrorMessage= "職稱字數不得大於 10 個字元")]
        [Required]
        public string 職稱 { get; set; }
        
        [StringLength(10, ErrorMessage= "姓名字數不得大於 10 個字元")]
        [Required]
        public string 姓名 { get; set; }
        
        [EmailAddress]
        [StringLength(100, ErrorMessage= "Email字數不得大於 250 個字元")]
        [Required]
        [Remote("CheckEmailRepeat", "Validate", HttpMethod = "Post", AdditionalFields = "Id,客戶Id", ErrorMessage = "FE Validate: 同一客戶的聯絡人email不可重複!")]
        public string Email { get; set; }
        
        [RegularExpression(@"09\d{2}-\d{6}", ErrorMessage ="請輸入正確的手機格式09XX-XXXXXX")]
        [StringLength(11, ErrorMessage= "手機號碼長度不符", MinimumLength = 11)]
        public string 手機 { get; set; }
        
        [StringLength(12, ErrorMessage= "電話字數不得大於 12 個字元")]
        public string 電話 { get; set; }
        [Required]
        public bool 是否已刪除 { get; set; }
    
        public virtual 客戶資料 客戶資料 { get; set; }
    }
}
