using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fuel_Price_Prophecy.Models;

namespace Fuel_Price_Prophecy.ViewModels
{
	public class ProfileManagementViewModel
	{
		public Client Client { get; set; }
		public ClientProfile ClientProfile { get; set; }
		public List<string> listOfState = new List<string>
		{
			"AL", "AK", "AZ", "AR", "CA", "CO", "CT", "DE", "FL", "GA", "HI", "ID", "IL",
			"IN", "IA","KS", "KY", "LA", "ME","MD", "MA", "MI", "MN", "MS", "MO", "MT", "NE",
			"NV", "NH", "NJ","NM", "NY", "NC", "ND", "OH", "OK", "OR", "PA","RI", "SC",
			"SD", "TN", "TX", "UT", "VT","VA", "WA", "WV", "WI", "WY"
		};
		public string OutcomeOfValidatingProfiles { get; set; }
	}
}