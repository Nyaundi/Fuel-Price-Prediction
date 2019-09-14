using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fuel_Price_Prophecy.Models;
using Fuel_Price_Prophecy.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Fuel_Price_Prophecy.Controllers
{
    public class ClientController : Controller
    {
		public int AddTest(int num1, int num2)
		{
			int result = 0;
			try
			{
				result += num1;
				result += num2;
			}
			catch
			{
				return num1 + num2;
			}
			return num1 + num2;
		}
		
        public ActionResult SignInPage()
        {
			var client = new Client();
			var viewModel = new ClientLoginViewModel
			{
				Client = client
			};
			return View(viewModel);
        }

		[HttpPost]
		public ActionResult SignIn(ClientLoginViewModel viewModel)
		{
			if(string.IsNullOrEmpty(viewModel.Client.Email) || string.IsNullOrEmpty(viewModel.Client.Password))
			{
				viewModel.OutcomeOfValidatingLogin = "SomeNullInput";
				return View("SignInPage", viewModel);
			}

			var emailValidator = new EmailAddressAttribute();
			if(!emailValidator.IsValid(viewModel.Client.Email))
			{
				viewModel.OutcomeOfValidatingLogin = "InvalidEmail";
				return View("SignInPage", viewModel);
			}

			using (var db = new MyDbContext())
			{
				var query = from b in db.ClientsTable
							orderby b.ClientId
							select b;
				bool isClient = false;
				foreach(var item in query)
				{
					if(item.Email == viewModel.Client.Email && item.Password == viewModel.Client.Password)
					{
						isClient = true;
						viewModel.Client.ClientId = item.ClientId;
						break;
					}
				}
				if(isClient)
				{
					/*bool isFirstTime = true;
					var queryProfile = from b in db.ClientProfileTable
									   select b;
					ClientProfile profile = null;
					foreach(var item in queryProfile)
					{
						if (item.Client == viewModel.Client)
						{
							isFirstTime = false;
							profile = item;
						}
							
					}

					if (isFirstTime == true)
					{*/
						var clientProfile = new ClientProfile();
						var profileViewModel = new ProfileManagementViewModel()
						{
							ClientProfile = clientProfile,
							Client = viewModel.Client
						};

						return View("ProfileManagementPage", profileViewModel);
					/*}
					else
					{
						//return Content("Is not first time user");
						var fuelQuote = new FuelQuote
						{
							DeliveryAddress = profile.Address1
											+ " " + profile.Address2 + ", "
											+ profile.City + ", "
											+ profile.State + ", "
											+ profile.Zipcode
						};
						var fuelQuoteViewModel = new FuelQuoteViewModel
						{
							ClientId = viewModel.Client.ClientId,
							FuelQuote = fuelQuote,
						};
						return View("FuelQuoteForm", fuelQuoteViewModel);
					}*/
				}
				else
				{
					viewModel.OutcomeOfValidatingLogin = "NotAClient";
					return View("SignInPage", viewModel);
				}
			}
		}

		public ActionResult FromQuoteToProfile(int clientId)
		{
			Client client = null;
			using (var db = new MyDbContext())
			{
				var query = from b in db.ClientsTable
							select b;

				foreach(var item in query)
				{
					if (item.ClientId == clientId)
						client = item;
				}
			}
			var clientProfile = new ClientProfile();
			var profileViewModel = new ProfileManagementViewModel()
			{
				ClientProfile = clientProfile,
				Client = client
			};

			return View("ProfileManagementPage", profileViewModel);
		}

		public ActionResult RegisterPage()
		{
			var registering = new Registering();
			return View(registering);
		}

		[HttpPost]
		public ActionResult Register(Registering registering)
		{
			if(registering != null)
			{
				using (var db = new MyDbContext())
				{
					if(string.IsNullOrWhiteSpace(registering.Email) || string.IsNullOrWhiteSpace(registering.Password) || string.IsNullOrWhiteSpace(registering.ConfirmPassword))
					{
						registering.OutcomeOfCheckRegistration = "SomeNullInput";
						return View("RegisterPage", registering);
					}
					var query = from b in db.ClientsTable
								orderby b.ClientId
								select b;

					var testEmailAddress = new EmailAddressAttribute();
					bool emailIsValid = testEmailAddress.IsValid(registering.Email);

					bool isNewEmail = true;
					foreach(var item in query)
					{
						if(item.Email.Equals(registering.Email))
						{
							isNewEmail = false;
						}
					}

					if(emailIsValid)
					{
						if(isNewEmail)
						{
							if(registering.Password == registering.ConfirmPassword)
							{
								var newClient = new Client
								{
									Email = registering.Email,
									Password = registering.Password
								};
								db.ClientsTable.Add(newClient);
								db.SaveChanges();
								registering.OutcomeOfCheckRegistration = "Passed";
								return View("RegisterPage", registering);
							}
							else
							{
								registering.OutcomeOfCheckRegistration = "Confirm Password need to match Password";
								return View("RegisterPage", registering);
							}
						}
						else
						{
							registering.OutcomeOfCheckRegistration = "Sorry, that username's taken. Try another?";
							return View("RegisterPage", registering);
						}
					}
					else
					{
						registering.OutcomeOfCheckRegistration = "Invalid Email";
						return View("RegisterPage", registering);
					}
				}
			}
			else
				return null;
		}

		public ActionResult ProfileManagementPage()
		{
			return View();
		}

		[HttpPost]
		public ActionResult ManagingProfile(int clientId, ProfileManagementViewModel profileManagmentViewModel)
		{
			if (string.IsNullOrEmpty(profileManagmentViewModel.ClientProfile.Name)
				|| string.IsNullOrEmpty(profileManagmentViewModel.ClientProfile.Address1)
				|| string.IsNullOrEmpty(profileManagmentViewModel.ClientProfile.City)
				|| string.IsNullOrEmpty(profileManagmentViewModel.ClientProfile.State)
				|| string.IsNullOrEmpty(profileManagmentViewModel.ClientProfile.Zipcode))
			{
				var client = new Client { ClientId = clientId };
				var clientProf = new ClientProfile { ClientProfileId = clientId };
				profileManagmentViewModel = new ProfileManagementViewModel
				{
					Client = client,
					ClientProfile = clientProf,
					OutcomeOfValidatingProfiles = "SomeNullInput"
				};
				return View("ProfileManagementPage", profileManagmentViewModel);
			}
			int tempNumber = 0;
			bool zipCodeIsValid = Int32.TryParse(profileManagmentViewModel.ClientProfile.Zipcode, out tempNumber);
			if(!zipCodeIsValid)
			{
				var client = new Client { ClientId = clientId };
				var clientProf = new ClientProfile { ClientProfileId = clientId };
				profileManagmentViewModel = new ProfileManagementViewModel
				{
					Client = client,
					ClientProfile = clientProf,
					OutcomeOfValidatingProfiles = "InvalidZipCode"
				};
				return View("ProfileManagementPage", profileManagmentViewModel);
			}
			using (var db = new MyDbContext())
			{
				ClientProfile temp = db.ClientProfileTable.SingleOrDefault(x => x.ClientProfileId == clientId);
				if (temp != null)
				{
					temp.Name = profileManagmentViewModel.ClientProfile.Name;
					temp.Address1 = profileManagmentViewModel.ClientProfile.Address1;
					temp.Address2 = profileManagmentViewModel.ClientProfile.Address2;
					temp.City = profileManagmentViewModel.ClientProfile.City;
					temp.State = profileManagmentViewModel.ClientProfile.State;
					temp.Zipcode = profileManagmentViewModel.ClientProfile.Zipcode;
					db.SaveChanges();
				}
				else
				{
					var newProfile = new ClientProfile
					{
						ClientProfileId = clientId,
						Name = profileManagmentViewModel.ClientProfile.Name,
						Address1 = profileManagmentViewModel.ClientProfile.Address1,
						Address2 = profileManagmentViewModel.ClientProfile.Address2,
						City = profileManagmentViewModel.ClientProfile.City,
						State = profileManagmentViewModel.ClientProfile.State,
						Zipcode = profileManagmentViewModel.ClientProfile.Zipcode
					};
					db.ClientProfileTable.Add(newProfile);
					db.SaveChanges();
				}

				var fuelQuote = new FuelQuote
				{
					DeliveryAddress = profileManagmentViewModel.ClientProfile.Address1
					+ " " + profileManagmentViewModel.ClientProfile.Address2 + ", " 
					+ profileManagmentViewModel.ClientProfile.City + ", " 
					+ profileManagmentViewModel.ClientProfile.State + ", " 
					+ profileManagmentViewModel.ClientProfile.Zipcode
				};
				var fuelQuoteViewModel = new FuelQuoteViewModel
				{
					ClientId = clientId,
					FuelQuote = fuelQuote,
				};
				return View("FuelQuoteForm", fuelQuoteViewModel);
			}
		}

		public ActionResult FuelQuoteForm()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Pricing(int clientId, FuelQuoteViewModel viewModel)
		{
			if (viewModel.FuelQuote.DeliveryDate == null || viewModel.FuelQuote.GallonRequested == 0)
			{
				viewModel.OutComeOfValidateInput = "SomeNullInput";
				return View("FuelQuoteForm", viewModel);
			}
			using (var db = new MyDbContext())
			{
				var queryClient = from b in db.ClientsTable
							select b;
				Client client = null;
				foreach(var item in queryClient)
				{
					if(item.ClientId == clientId)
					{
						client = item;
					}
				}
				ClientProfile clientProfile = null;
				var queryProfile = from b in db.ClientProfileTable
							select b;
				foreach(var item in queryProfile)
				{
					if(item.Client == client)
					{
						clientProfile = item;
					}
				}
				double currentPricePerGallon = 1.5;
				double locationFactor;
				double rateHistoryFactor = 0;
				double gallonRequestedFactor;
				double companyProfitFactor = 0.1;
				double rateFluctuation;

				// Get locationFactor from state
				if (clientProfile.State == "TX")
					locationFactor = 0.02;
				else
					locationFactor = 0.04;

				// Get rateHistoryFactor by checking in database
				bool ratedBefore = false;
				var queryHistory = from b in db.FuelQuotes
								   select b;
				foreach(var item in queryHistory)
				{
					if (item.Client == client)
						ratedBefore = true;
				}
				if (ratedBefore)
					rateHistoryFactor = 0.01;

				// Get gallonRequestedFactor based on amount of gallon requested
				if (viewModel.FuelQuote.GallonRequested > 1000)
					gallonRequestedFactor = 0.02;
				else
					gallonRequestedFactor = 0.03;

				// Get rateFluctuation from the DeliveryDate (is summer if between May and July)
				int month = Convert.ToInt32(viewModel.FuelQuote.DeliveryDate.Substring(5, 2));
				if (month >= 5 && month <= 7)
					rateFluctuation = 0.04;
				else
					rateFluctuation = 0.03;

				double margin = currentPricePerGallon * (locationFactor - rateHistoryFactor + gallonRequestedFactor + companyProfitFactor + rateFluctuation);
				double price = currentPricePerGallon + margin;
				var newFuelQuote = new FuelQuote
				{
					Client = client,
					DeliveryAddress = viewModel.FuelQuote.DeliveryAddress,
					DeliveryDate = viewModel.FuelQuote.DeliveryDate,
					GallonRequested = viewModel.FuelQuote.GallonRequested,
					SuggestedPrice = price,
					TotalAmountDue = price * viewModel.FuelQuote.GallonRequested
				};
				db.FuelQuotes.Add(newFuelQuote);
				db.SaveChanges();
				viewModel.FuelQuote = newFuelQuote;
				viewModel.ClientId = clientId;
				return View("FuelQuoteForm", viewModel);
			}
		}

		[HttpPost]
		public ActionResult FuelQuoteHistory(int clientId)
		{
			using (var db = new MyDbContext())
			{
				var query = from b in db.FuelQuotes
							orderby b.ClientQuoteId
							select b;
				List<FuelQuote> list = new List<FuelQuote>();
				foreach(var item in query)
				{
					if (item.Client.ClientId == clientId)
						list.Add(item);
				}
				return View(list);
			}
			
		}
	}
}