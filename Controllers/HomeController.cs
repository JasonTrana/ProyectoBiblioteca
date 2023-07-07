
using System.Web.Mvc;

namespace BibliotecaVirtual.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NoAccess()
        {
            ViewBag.Message = "No tiene acceso a esta pagina";
            return View();
        }
    }
}