using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Fuel_Price_Prophecy.Models
{
	public class Client
	{
		[Key]
		public int ClientId { get; set; }

		[Required]
		[StringLength(255)]
		public string Email { get; set; }

		[Required]
		[StringLength(255)]
		public string Password { get; set; }

		public virtual ClientProfile ClientProfile { get; set; }

		public virtual List<FuelQuote> FuelQuotes { get; set; }
	}
}