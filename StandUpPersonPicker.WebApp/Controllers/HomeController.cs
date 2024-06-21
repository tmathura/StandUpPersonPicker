using Microsoft.AspNetCore.Mvc;
using StandUpPersonPicker.Core.Interfaces;
using StandUpPersonPicker.Domain.Models;
using StandUpPersonPicker.WebApp.Models;
using System.Diagnostics;

namespace StandUpPersonPicker.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPersonBl _personBl;

        public HomeController(IPersonBl personBl)
        {
            _personBl = personBl;
        }

        public IActionResult Index()
        {
            var viewModel = new HomeViewModel
            {
                People = new List<Person>()
            };

            var cookieValue = Request.Cookies["SUPP-People"];

            if (!string.IsNullOrEmpty(cookieValue))
            {
                var people = cookieValue.Split(',');

                foreach (var person in people)
                {
                    viewModel.People.Add(new Person { Name = person, IsSelected = true});
                }
            }

            return View(viewModel);
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Start(HomeViewModel viewModel)
        {

            if (viewModel.SelectedPeople == null || viewModel.SelectedPeople.Count == 0)
            {
                ModelState.AddModelError("people-count-error", "No one added to start the stand up!");
                
                return View("Index", viewModel);
            }

            // Save SelectedPeople to cookies
            var cookieOptions = new CookieOptions
            {
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.Now.AddDays(30)
            };
            Response.Cookies.Append("SUPP-People", viewModel.AllPeopleNames, cookieOptions);

            var result = await _personBl.CreateCharacterPersonPairs(viewModel.SelectedPeople);

            var model = new StandUpViewModel
            {
                StandUpPeople = new List<StandUpPerson>()
            };

            foreach (var keyValuePair in result)
            {
                model.StandUpPeople.Add(new StandUpPerson
                {
                    Name = keyValuePair.Value,
                    CharacterName = keyValuePair.Key.Name,
                    CharacterImageUrl = keyValuePair.Key.Image
                });
            }
            
            return View("StandUp", model);
        }
    }
}
