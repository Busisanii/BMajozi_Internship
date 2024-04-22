using BMajozi.Data;
using BMajozi.Models;
using BMajozi.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BMajozi.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppbbContext context;

        public HomeController(AppbbContext _context)
        {
            context = _context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(User user, string movie, string eat, string TV, string radio, string Pasta, string Pap, string Other, string Pizza)
        {
            try
            {
                if (ModelState.IsValid && movie != null && movie != null && eat != null && TV != null && radio != null)
                {
                    if (Pap == null && Pasta == null && Pizza == null && Other == null) throw new Exception("Choose at least one food");
                    context.User.Add(user);
                    context.SaveChanges();

                    user = context.User.Find(context.User.ToArray().Length); //last inserted user

                    //inistantiating list and adding the list rating into an array  
                    List<Activity> activities = new List<Activity>();
                    activities.Add(new Activity() { Rating = int.Parse(movie), Type = "Movie", UserID = user.ID });
                    activities.Add(new Activity() { Rating = int.Parse(eat), Type = "Eat", UserID = user.ID });
                    activities.Add(new Activity() { Rating = int.Parse(radio), Type = "TV", UserID = user.ID });
                    activities.Add(new Activity() { Rating = int.Parse(TV), Type = "RADIO", UserID = user.ID });

                    //inistantiating list and adding the list of food to the array
                    List<Food> foods = new List<Food>();
                    if (Pasta != null) foods.Add(new Food() { Type = "Pasta", UserId = user.ID });
                    if (Pizza != null) foods.Add(new Food() { Type = "Pizza", UserId = user.ID });
                    if (Pap != null) foods.Add(new Food() { Type = "Pap", UserId = user.ID });
                    if (Other != null) foods.Add(new Food() { Type = "Other", UserId = user.ID });


                    Array.ForEach(activities.ToArray(), activity =>
                    {
                        context.Activity.Add(activity);
                        context.SaveChanges();
                    });

                    Array.ForEach(foods.ToArray(), food =>
                    {
                        context.Food.Add(food);
                        context.SaveChanges();
                    });

                    return RedirectToAction("Results");
                }
                else
                {
                    ModelState.AddModelError("", "Please fill all the fields and check atleast one checkbox for Food");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"{ex.Message}");
            }
            return View();
        }
        //All calculations
        public IActionResult Results()
        {
            Summary summary = new Summary();
            summary.Count = context.User.Count();
            summary.AverageAge = context.User.Select(u => DateTime.Now.Year - u.DoB.Year).Average();
            summary.MaxAge = context.User.Select(u => DateTime.Now.Year - u.DoB.Year).Max();
            summary.MinAge = context.User.Select(u => DateTime.Now.Year - u.DoB.Year).Min();
            summary.PizzaLikersPercent = Math.Round((double)context.Food.Where(f => f.Type == "Pizza").ToList().Count /
                (double)context.Food.Count() * 100,2);
            summary.PizzaLikersPercent = context.Food.Where(f => f.Type == "Pizza").ToList().Count / context.Food.Count() * 100;
            summary.PapNWLikersPercent = context.Food.Where(f => f.Type == "Pap").ToList().Count / context.Food.Count() * 100;
            summary.PastaLikersPercent = context.Food.Where(f => f.Type == "Pasta").ToList().Count / context.Food.Count() * 100;

            summary.PPLWatchingTV = context.Activity.Where(a => a.Type == "TV").ToList().Count;
            summary.PPLListiningRadio = context.Activity.Where(a => a.Type == "RADIO").ToList().Count;
            summary.PPLEating = context.Activity.Where(a => a.Type == "Eat").ToList().Count;
            summary.PPLWatchingMovie = context.Activity.Where(a => a.Type == "Movie").ToList().Count;
            return View(summary);
        }
    }
}
