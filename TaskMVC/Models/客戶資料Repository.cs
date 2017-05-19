using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace TaskMVC.Models
{   
	public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
	{
        public override IQueryable<客戶資料> All()
        {
            return base.All().Where(o => o.是否已刪除 == false);
        }

        public IQueryable<客戶資料> All(bool showAll)
        {
            if (showAll)
                return base.All();
            else
                return this.All();
        }

        public 客戶資料 GetCustomerById(int id)
        {
            return this.All().FirstOrDefault(o => o.Id == id);
        }

        public IQueryable<客戶資料> GetCustomerList(bool showAll = false)
        {
            IQueryable<客戶資料> all;// = this.All();
            if (showAll)
                all = this.All(true);
            else
                all = this.All();

            return all.OrderByDescending(o => o.Id).Take(10);
        }

        public void Update(客戶資料 customer)
        {
            //this.UnitOfWork.Context.Entry(customer).State = EntityState.Modified;
            var entry = this.UnitOfWork.Context.Entry(customer);

            //foreach (var item in entry.CurrentValues.PropertyNames.Except(new string[] { "Id", "是否已刪除" }))
            //{
            //entry.Property(item).IsModified = true;
            //}

            entry.State = EntityState.Modified;
            entry.Property(o => o.是否已刪除).IsModified = false;
            //entry.Property(o => o.Id).IsModified = false;
        }

        public override void Delete(客戶資料 entity)
        {
            this.UnitOfWork.Context.Configuration.ValidateOnSaveEnabled = false;
            entity.是否已刪除 = true;
            //this.UnitOfWork.Context.Entry(entity).Property(o => o.是否已刪除).IsModified = true;
        }

        public bool IsExist(int id)
        {
            return All().Any(o => o.Id == id);
        }

        public IQueryable<客戶資料> GetSearchCustomerList(string customerName, string customerType)
        {
            var list = All();

            customerName = (customerName + "").Trim();
            if (customerName != "")
                list = list.Where(o => o.客戶名稱.Contains(customerName));

            int outInt = 0;
            if (int.TryParse((customerType + "").Trim(), out outInt) && outInt != 0)
                list = list.Where(o => o.客戶分類 == outInt);

            return list;
        }
        
        
    }

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}