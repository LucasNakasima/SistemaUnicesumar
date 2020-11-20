using SitemaUnicesumar.Data;
using SitemaUnicesumar.Models;
using System.Linq;
using System.Web.Mvc;

namespace SitemaUnicesumar.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            ViewBag.SideMenu = false;
            ViewBag.LogoutMenu = false;

            if (Session["userId"] != null)
                return RedirectToAction("Index", "Home");

            var loginViewModel = new LoginViewModel();

            return View(loginViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel viewModel)
        {
            ViewBag.SideMenu = false;

            using (var context = new UnicesumarBdEntities())
            {
                var user = context.ListUser.Where(p => p.UserName == viewModel.UserName && p.Password == viewModel.Password).FirstOrDefault();

                if (user != null)
                {
                    Session["userId"] = user.Id.ToString();
                    Session["userName"] = user.UserName.ToString();

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    viewModel.ErrorMenssage = "Nome de usuário ou senha inválidos";

                    return View(viewModel);
                }
            }
        }

        public ActionResult Logout() 
        {
            Session["userId"] = null;
            Session["userName"] = null;

            return RedirectToAction("Login", "Account");
        }
    }
}