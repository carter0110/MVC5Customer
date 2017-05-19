using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace TaskMVC.Models
{   
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{
        public override IQueryable<客戶聯絡人> All()
        {
            return base.All().Where(o => o.是否已刪除 == false);
        }

        public IQueryable<客戶聯絡人> All(bool showAll)
        {
            if (showAll)
                return base.All();
            else
                return All();
        }

        public 客戶聯絡人 GetContactById(int id)
        {
            return All().FirstOrDefault(o => o.Id == id);
        }

        public bool IsExist(int id)
        {
            return All().Any(o => o.Id == id);
        }

        public void Edit(客戶聯絡人 contact)
        {
            var entry = this.UnitOfWork.Context.Entry(contact);

            entry.State = EntityState.Modified;
            entry.Property(o => o.是否已刪除).IsModified = false;
        }

        public override void Delete(客戶聯絡人 entity)
        {
            this.UnitOfWork.Context.Configuration.ValidateOnSaveEnabled = false;
            entity.是否已刪除 = true;
        }


        public IQueryable<客戶聯絡人> GetSearchContactList(string contactName, string jobTitle)
        {
            var list = this.All();

            contactName = (contactName + "").Trim();
            if (contactName != "")
                list = list.Where(o => o.姓名.Contains(contactName));

            jobTitle = (jobTitle + "").Trim();
            if (jobTitle != "")
                list = list.Where(o => o.職稱.Contains(jobTitle));

            return list;
        }

        


    }

	public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}