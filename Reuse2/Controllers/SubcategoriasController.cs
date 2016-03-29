using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Reuse2.Models;
using System.Web.Services;
using System.Web.Helpers;

namespace Reuse2.Controllers
{
    public class SubcategoriasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public JsonResult getSubcategoriasByCategoria(int catId)
        {
            var list = db.Subcategorias.Where(s => s.categoriaID == catId).ToList();
            return Json(new { list = list });
        }

        // GET: Subcategorias
        public ActionResult Index()
        {
            var subcategorias = db.Subcategorias.Include(s => s.categoria);
            return View(subcategorias.ToList());
        }

        // GET: Subcategorias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subcategoria subcategoria = db.Subcategorias.Find(id);
            if (subcategoria == null)
            {
                return HttpNotFound();
            }
            return View(subcategoria);
        }

        // GET: Subcategorias/Create
        public ActionResult Create()
        {
            ViewBag.categoriaID = new SelectList(db.Categorias, "categoriaID", "titulo");
            return View();
        }

        // POST: Subcategorias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "subCategoriaID,categoriaID,titulo")] Subcategoria subcategoria)
        {
            if (ModelState.IsValid)
            {
                db.Subcategorias.Add(subcategoria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.categoriaID = new SelectList(db.Categorias, "categoriaID", "titulo", subcategoria.categoriaID);
            return View(subcategoria);
        }

        // GET: Subcategorias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subcategoria subcategoria = db.Subcategorias.Find(id);
            if (subcategoria == null)
            {
                return HttpNotFound();
            }
            ViewBag.categoriaID = new SelectList(db.Categorias, "categoriaID", "titulo", subcategoria.categoriaID);
            return View(subcategoria);
        }

        // POST: Subcategorias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "subCategoriaID,categoriaID,titulo")] Subcategoria subcategoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subcategoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.categoriaID = new SelectList(db.Categorias, "categoriaID", "titulo", subcategoria.categoriaID);
            return View(subcategoria);
        }

        // GET: Subcategorias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subcategoria subcategoria = db.Subcategorias.Find(id);
            if (subcategoria == null)
            {
                return HttpNotFound();
            }
            return View(subcategoria);
        }

        // POST: Subcategorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Subcategoria subcategoria = db.Subcategorias.Find(id);
            db.Subcategorias.Remove(subcategoria);
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
