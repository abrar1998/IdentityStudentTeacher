using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task2Identity.Models;

namespace Task2Identity.Controllers
{
    public class StudentController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly DbFile context;

        public StudentController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager, DbFile context)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            this.context = context;
        }
        public IActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterUserModel umodel)
        {
            if(ModelState.IsValid)
            {
                var newuser = new ApplicationUser
                {
                    UserName = umodel.Email,
                    Email = umodel.Email,
                    Name = umodel.Name,
                    
                };

                var result = await userManager.CreateAsync(newuser, umodel.Password);
                var roles = new List<string>() { umodel.SelectedRole };
                await userManager.AddToRolesAsync(newuser, roles);
                if (result.Succeeded)
                {
                   
                    return RedirectToAction("Login", "Student");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLogin login)
        {
            if(ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(login.Email, login.Password, login.RememberMe, false);
                if(!result.Succeeded)
                {
                    ModelState.AddModelError("", "Invalid Credentials");
                }
                if(User.IsInRole("Admin"))
                {
                    return RedirectToAction("AdminDashboard", "Student");
                }
                else if(User.IsInRole("Teacher"))
                {

                    /*var userName = User.Identity.Name;
                    var loginTeachear = userManager.Users.SingleOrDefault(x => x.UserName == userName);
                    ViewBag.data = loginTeachear;*/

                    var user = await userManager.FindByNameAsync(login.Email);
                   /* if(user.Email == 'saquib@gmail.com')
                    {
                        return RedirectToAction("TeacherDashboard", "Student");
                    }*/

                    if (user != null && user.Email.Equals("saquib@gmail.com"))
                    {
                        return RedirectToAction("Saquib", "Student");
                    }
                    else if(user !=null && user.Email.Equals("uzair@gmail.com"))
                    {
                        return RedirectToAction("Uzair", "Student");
                    }






                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.Clear();

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Student");
        }


        [HttpGet]
        public IActionResult RegisterStudent()
        {

            ViewBag.data = userManager.Users.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult RegisterStudent(Student std)
        {
            


                context.Students.Add(std);
                context.SaveChanges();
            return RedirectToAction("Index", "Home");
            
           
        }

        public async Task<IActionResult> Saquib()
        {
             
          /* var  userLogin = await userManager.FindByIdAsync(User.Identity.Name);
           var data = context.Students.Where(x=>x.TeacherId == userLogin.Id).ToList();*/
            return View();
        }

        public IActionResult Uzair()
        {
            return View();
        }

        public IActionResult AdminDashboard()
        {
            return View();
        }

        public IActionResult TeacherDashboard()
        {
            return View();
        }

        [Authorize(Roles= RolesClass.Teacher)]
        public async Task<IActionResult> StudentofSaquib()
        {
            var userLogin = await userManager.FindByNameAsync(User.Identity.Name);
            var data = context.Students.Where(x => x.TeacherId == userLogin.Id).Include(x=>x.Teacher).ToList();
            return View(data);
        }

        //Adding roles

        public string AddRoles()
        {
            roleManager.CreateAsync(new IdentityRole(RolesClass.Admin)).GetAwaiter().GetResult();
            roleManager.CreateAsync(new IdentityRole(RolesClass.Teacher)).GetAwaiter().GetResult();
            return "Roles AddedSuccessFully";
        }


    }
}
