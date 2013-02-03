using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeverageApi.Models {
	public class ToDoList {

			//["Hidden"]
			public int Id { get; set; }

			//["Require"]
			public string Title { get; set; }

			public DateTime DueDate { get; set; }

			public bool Completed { get; set; }
	
	}
}