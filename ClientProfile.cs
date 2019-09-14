using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fuel_Price_Prophecy.Models
{
	public class ClientProfile
	{
		[ForeignKey ("Client")]
		public int ClientProfileId { get; set; }

		[Required]
		[StringLength(50)]
		public string Name { get; set; }

		[Required]
		[StringLength(100)]
		[Display(Name = "Address 1")]
		public string Address1 { get; set; }

		[StringLength(100)]
		[Display(Name = "Address 2")]
		public string Address2 { get; set; }

		[Required]
		[StringLength(100)]
		public string City { get; set; }

		[Required]
		public string State { get; set; }

		[Required]
		[StringLength(9, MinimumLength = 5, ErrorMessage = "Must be at least 5 characters long.")]
		public string Zipcode { get; set; }


		public virtual Client Client { get; set; }
	}
}