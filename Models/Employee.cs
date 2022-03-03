using System;
namespace marketplace_services_CSI5112.Models
{
	public class Employee
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public Employee(int Id, string Name) 
		{
			this.Id = Id;
			this.Name = Name;
		}
	}
}

