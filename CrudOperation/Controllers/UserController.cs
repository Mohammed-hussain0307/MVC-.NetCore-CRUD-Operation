using CrudOperation.Models;
using CrudOperation.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CrudOperation.Controllers
{
    public class UserController : Controller
    {
        private UserRepository userRepository;

         public UserController()
        {
            userRepository = new UserRepository();
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                 var user = userRepository.GetAllUsers();
            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }

        //get create user
        [HttpGet]
        public IActionResult create()
        {
            return View();
        }

        //post create user
        [HttpPost]
        public IActionResult create(UserModel userModel)
        {
            bool isAdded = false;

            try
            {
                if (ModelState.IsValid) {
                    isAdded = userRepository.CreateUser(userModel);

                    if (isAdded)
                    {
                        TempData["SuccessMessage"] = "User created successfully";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Cant create user detail";
                        return RedirectToAction("Index");
                    }
                }
                return View();
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }
    }
}
