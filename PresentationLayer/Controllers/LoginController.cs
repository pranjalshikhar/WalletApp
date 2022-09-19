using DataAccessLayer.Models;
using DataAccessLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    public class LoginController : Controller
    {
        private readonly WalletAppContext _walletAppContext;
        WalletServices _walletServices;

        public LoginController(WalletAppContext walletAppContext)
        {
            _walletAppContext = walletAppContext;
            _walletServices = new WalletServices(_walletAppContext);
        }

        public IActionResult LoginHome()
        {
            ViewBag.amount = _walletServices.ViewBalance(HttpContext.Session.GetString("userEmail"));
            return View();
        }
    }
}
