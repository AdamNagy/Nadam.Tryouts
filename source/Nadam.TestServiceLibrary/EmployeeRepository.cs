using System;
using System.Collections.Generic;
<<<<<<< HEAD
using Nadam.Global.ConsoleShell.Helpers;
=======
using Nadam.ConsoleShell.Helpers;
>>>>>>> master

namespace Nadam.TestServiceLibrary
{
	public class EmployeeRepository
	{
		public string ShouldNotAppear { get; set; }

		[Command("GetEmployees")]
		public IEnumerable<Employee> Get()
		{
			return new List<Employee>()
			{
				new Employee()
				{
					Dob = DateTime.Now.AddDays(-290304),
					Salaray = 240000,
					name = "Cement Elek"
				},
				new Employee()
				{
					Dob = DateTime.Now.AddDays(-270304),
					Salaray = 280000,
					name = "Plum Pal"
				},
				new Employee()
				{
					Dob = DateTime.Now.AddDays(-190304),
					Salaray = 180000,
					name = "Gipsz Jakab"
				}
			};
		}

		[IgnoreAsCommand]
		public void Add()
		{

		}
	}

	[IgnoreAsCommand]
	public class SomeOtherClass
	{
		public string SayHello()
		{
			return "Hi Adam";
		}
	}
}
