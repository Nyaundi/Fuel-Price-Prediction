using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fuel_Price_Prophecy.Models;

namespace Fuel_Price_Prophecy.ViewModels
{
	public class FuelQuoteViewModel
	{
		public int ClientId { get; set; }
		public FuelQuote FuelQuote { get; set; }
		public string OutComeOfValidateInput { get; set; }
	}
}