using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace TaskMVC.Models
{   
	public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
	{
        public override IQueryable<客戶銀行資訊> All()
        {
            return base.All().Where(o => o.是否已刪除 == false);
        }

        public IQueryable<客戶銀行資訊> All(bool showAll)
        {
            if (showAll)
                return base.All();
            else
                return this.All();
        }

        public 客戶銀行資訊 GetBankInfoById(int id)
        {
            return this.All().FirstOrDefault(o => o.Id == id);
        }

        public bool IsExist(int id)
        {
            return this.All().Any(o => o.Id == id);
        }

        public void Edit(客戶銀行資訊 bankInfo)
        {
            var entry = this.UnitOfWork.Context.Entry(bankInfo);
            entry.State = EntityState.Modified;
            entry.Property(o => o.是否已刪除).IsModified = false;
        }

        public override void Delete(客戶銀行資訊 entity)
        {
            this.UnitOfWork.Context.Configuration.ValidateOnSaveEnabled = false;
            entity.是否已刪除 = true;
        }

        public IQueryable<客戶銀行資訊> GetSearchBankInfoList(string bankName)
        {
            var list = this.All();

            bankName = (bankName + "").Trim();
            if (bankName != "")
                list = list.Where(o => o.銀行名稱.Contains(bankName));

            return list;
        }
    }

	public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{

	}
}