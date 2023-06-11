
using Microsoft.AspNetCore.Mvc;
using NewProj.Models;

using NewProj.Interface;

namespace NewProj.Controllers
{



    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IName _name;
        

        public HomeController(IConfiguration config, IName name) {
            _configuration = config;
            _name = name;
            }
        public IActionResult Index()
        {
            PeopleViewModel peopleViewModel = new PeopleViewModel();
            peopleViewModel.Lst = _name.GetProductList().ToList();
            return View(peopleViewModel);
        }
        
        public IActionResult Add()
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            return View();
        }


        [HttpPost]
        public IActionResult Add(People person)
        {
            if (person == null)
            {
                return View();
            }
            else
            {
               _name.EditUpdateProduct(person);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(People data)
        {
            _name.EditUpdateProduct(data);
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public IActionResult Del(int id)
        {
            _name.DeleteProductByID(id);
            return RedirectToAction("Index");
        }


    }
}