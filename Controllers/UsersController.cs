using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BibliotecaVirtual.Data;
using BibliotecaVirtual.Models;
using BibliotecaVirtual.Permisos;

namespace BibliotecaVirtual.Controllers
{
    [PermisoRol(Rol.Bibliotecario)]
    public class UsersController : Controller
    {
        private BibliotecaContext db = new BibliotecaContext();

        // GET: Users
        //[Authorize(Roles = "Bibliotecario")]
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_user,Name,LastName,Address,Phone,Email,Password,Roles")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_user,Name,LastName,Address,Phone,Email,Password,Roles")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Mi metodos
        public ActionResult Login()
        {
            return View();
        }

        //Ver lo del login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            if (user.Email == null)
            {
                ViewData["correo"] = "Ingrese el correo"; 
                return View();
            }

            if (user.Password == null)
            {
                ViewData["clave"] = "Ingrese la contraseña"; 
                return View();
            }

            var _user = ValidarUser(user.Email, user.Password);
            if(_user != null)
            {
                FormsAuthentication.SetAuthCookie(_user.Email, false);
                Session["User"] = _user;
                ViewData["User"] = _user.Name + " " + _user.LastName;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["User"] = "";
                return View();
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user)
        {

            if(user.Password != user.ConfirmPassword)
            {
                ViewData["Mensaje"] = "Las contraseñas no coinciden";
                return View();
            }

            db.Users.Add(user);
            db.SaveChanges();

            return RedirectToAction("Index","Home");
        }

        public ActionResult CloseSession()
        {
            FormsAuthentication.SignOut();
            Session["User"] = null;
            return RedirectToAction("Login","Users");
        }

        private User ValidarUser(string email, string password)
        {
            var user = db.Users.Where(u => u.Email == email && u.Password == password).FirstOrDefault();
            return user;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
