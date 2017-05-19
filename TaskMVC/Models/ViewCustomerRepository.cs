using System;
using System.Linq;
using System.Collections.Generic;
	
namespace TaskMVC.Models
{   
	public  class ViewCustomerRepository : EFRepository<ViewCustomer>, IViewCustomerRepository
	{

	}

	public  interface IViewCustomerRepository : IRepository<ViewCustomer>
	{

	}
}