using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fuel_Price_Prophecy.Models
{
	public class FuelQuote
	{
		[Key]
		public int ClientQuoteId { get; set; }

		[Required]
		[Display(Name = "Delivery Address")]
		public string DeliveryAddress { get; set; }

		[Required]
		[Display(Name = "Number of Gallon")]
		public int GallonRequested { get; set; }

		[Required]
		[Display(Name = "Delivery Date")]
		public string DeliveryDate { get; set; }

		[Required]
		[Display(Name = "Calculated Price Per Gallon")]
		public double SuggestedPrice { get; set; }

		[Required]
		[Display(Name = "Total")]
		public double TotalAmountDue { get; set; }

		[Required]
		public virtual Client Client { get; set; }
	}
}