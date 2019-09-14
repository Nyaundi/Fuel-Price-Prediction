using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fuel_Price_Prophecy.Models;

namespace Fuel_Price_Prophecy.ViewModels
{
	public class ClientLoginViewModel
	{
		public Client Client { get; set; }
		public string OutcomeOfValidatingLogin { get; set; }
	}
}