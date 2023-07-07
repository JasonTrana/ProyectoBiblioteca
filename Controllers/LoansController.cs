using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BibliotecaVirtual.Data;
using BibliotecaVirtual.Models;
using BibliotecaVirtual.Permisos;

namespace BibliotecaVirtual.Controllers
{
    public class LoansController : Controller
    {
        private BibliotecaContext db = new BibliotecaContext();

        // GET: Loans
        public ActionResult Index()
        {
            var loans = db.Loans.Include(l => l.Book).Include(l => l.User);
            return View(loans.ToList());
        }

        [PermisoRol(Rol.Bibliotecario)]
        // GET: Loans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan loan = db.Loans.Find(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            return View(loan);
        }

        // GET: Loans/Create
        public ActionResult Create()
        {
            ViewBag.Book_id = new SelectList(db.Books, "Id_book", "Title");
            ViewBag.User_id = new SelectList(db.Users, "Id_user", "Name");
            return View();
        }

        // POST: Loans/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_loan,Book_id,User_id,LoanDate,ExpectedReturnDate,ActualReturnDate")] Loan loan)
        {
            if (ModelState.IsValid)
            {
                db.Loans.Add(loan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Book_id = new SelectList(db.Books, "Id_book", "Title", loan.Book_id);
            ViewBag.User_id = new SelectList(db.Users, "Id_user", "Name", loan.User_id);
            return View(loan);
        }

        // GET: Loans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan loan = db.Loans.Find(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            ViewBag.Book_id = new SelectList(db.Books, "Id_book", "Title", loan.Book_id);
            ViewBag.User_id = new SelectList(db.Users, "Id_user", "Name", loan.User_id);
            return View(loan);
        }

        // POST: Loans/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_loan,Book_id,User_id,LoanDate,ExpectedReturnDate,ActualReturnDate")] Loan loan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Book_id = new SelectList(db.Books, "Id_book", "Title", loan.Book_id);
            ViewBag.User_id = new SelectList(db.Users, "Id_user", "Name", loan.User_id);
            return View(loan);
        }

        // GET: Loans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan loan = db.Loans.Find(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            return View(loan);
        }

        // POST: Loans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Loan loan = db.Loans.Find(id);
            db.Loans.Remove(loan);
            db.SaveChanges();
            return RedirectToAction("Index");
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
