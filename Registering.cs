using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Fuel_Price_Prophecy.Models
{
	public class Registering
	{
		[Required]
		[StringLength(255)]
		public String Email { get; set; }

		public string Password { get; set; }

		[Display(Name = "Confirm Password")]
		public string ConfirmPassword { get; set; }

		public string OutcomeOfCheckRegistration { get; set; }
	}
}