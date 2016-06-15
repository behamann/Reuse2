using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Reuse2.Models;
using PagedList;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Reuse2.Controllers
{
    public class AnunciosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Anuncios
        public ActionResult Index(string tipo, string categoria, string cidade, string busca, string currentFilter, int? page)
        {
            var cidadeBusca = String.Empty;
            if (cidade != null)
            {
                cidadeBusca = cidade.Split('-')[0].Trim();
            }         
            if (busca != null)
            {
                page = 1;
            }
            else
            {
                busca = currentFilter;
            }
            ViewBag.CurrentFilter = busca;
            var anuncios = db.Anuncios
                .Include(a => a.categoria)
                .Include(a => a.pessoa);
            if ((string)Session["tipo"] != tipo && tipo != null)
            {
                Session["tipo"] = tipo;
            }
            if ((string)Session["categoria"] != categoria && categoria != null)
            {
                Session["categoria"] = categoria;
            }
            if ((string)Session["cidade"] != cidadeBusca && cidadeBusca != null)
            {
                Session["cidade"] = cidadeBusca;
            }
            var cat = (string)Session["categoria"];
            var tip = (string)Session["tipo"];
            var cid = (string)Session["cidade"];

            if (!String.IsNullOrEmpty(busca))
            {
                anuncios = db.Anuncios.Where(s => s.titulo.Contains(busca) || s.descricao.Contains(busca));
            }

            if (cat != null && cat != "Todos")
            {
                anuncios = anuncios.Where(a => a.categoria.titulo == cat);
            }

            if (tip == null)
            {
                tip = "Ofertas";
            }

            if (cid != "")
            {
                anuncios = anuncios.Where(a => a.pessoa.cidade == cid);
            }
            anuncios = anuncios
                .Where(a => a.tipo == tip)
                .Where(a => a.ativo == true)
                .OrderBy(a => a.titulo);
            
            ViewBag.tipo = tip;
            ViewBag.cat = cat;
            ViewBag.cidade = cidadeBusca;

            int pageSize = 9;
            int pageNumber = (page ?? 1);
            return View(anuncios.ToPagedList(pageNumber, pageSize));
        }

        // GET: Anuncios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anuncio anuncio = db.Anuncios.Find(id);
            if (anuncio == null || (User.Identity.Name != "brunoha" && anuncio.ativo == false) )
            {
                return HttpNotFound();
            }
            return View(anuncio);
        }

        // GET: Anuncios/Create
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.categoriaID = new SelectList(db.Categorias, "categoriaID", "titulo");
                ViewBag.pessoaID = new SelectList(db.Users, "Id", "UserName");
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account", new { message = "notAuthenticated" });
            }
        }

        // POST: Anuncios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "anuncioID,pessoaID,categoriaID,subCategoria,condicao,titulo,descricao,tipo,video,ativo")] Anuncio anuncio)
        {
            if (ModelState.IsValid)
            {
                List<Imagem> fileDetails = new List<Imagem>();
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = System.IO.Path.GetFileName(file.FileName);
                        Imagem fileDetail = new Imagem()
                        {
                            FileName = fileName,
                            Extension = System.IO.Path.GetExtension(fileName),
                            Id = Guid.NewGuid()
                        };
                        fileDetails.Add(fileDetail);

                        var path = System.IO.Path.Combine(Server.MapPath("~/Content/img/Anuncios/"), fileDetail.Id + fileDetail.Extension);
                        file.SaveAs(path);
                    }
                }

                anuncio.imagens = fileDetails;

                var id = db.Users.Where(u => u.UserName == User.Identity.Name).First().Id;
                anuncio.ativo = false;
                anuncio.dataCriacao = DateTime.Now;
                anuncio.pessoaID = id;
                anuncio.status = "Aberto";
                db.Anuncios.Add(anuncio);
                db.SaveChanges();
                await EmailService.sendNovoAnuncioMessage(anuncio.pessoa.UserName, anuncio.pessoa.Email, anuncio.anuncioID);
                return RedirectToAction("Index", new { Message = "adCreated" });
            }

            ViewBag.categoriaID = new SelectList(db.Categorias, "categoriaID", "titulo", anuncio.categoriaID);
            ViewBag.pessoaID = new SelectList(db.Users, "Id", "UserName", anuncio.pessoaID);
            return View(anuncio);
        }

        // GET: Anuncios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anuncio anuncio = db.Anuncios.Include(a => a.imagens).SingleOrDefault(a => a.anuncioID == id);
            if (anuncio == null)
            {
                return HttpNotFound();
            }
            ViewBag.categoriaID = new SelectList(db.Categorias, "categoriaID", "titulo", anuncio.categoriaID);
            ViewBag.pessoaID = new SelectList(db.Users, "Id", "UserName", anuncio.pessoaID);
            return View(anuncio);
        }

        // POST: Anuncios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "anuncioID,pessoaID,categoriaID,subCategoria,condicao,titulo,descricao,tipo,video,ativo,dataCriacao,status,interessadoID")] Anuncio anuncio)
        {
            if (ModelState.IsValid)
            {
                //New Files
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = System.IO.Path.GetFileName(file.FileName);
                        Imagem fileDetail = new Imagem()
                        {
                            FileName = fileName,
                            Extension = System.IO.Path.GetExtension(fileName),
                            Id = Guid.NewGuid(),
                            AnuncioId = anuncio.anuncioID
                        };
                        var path = System.IO.Path.Combine(Server.MapPath("~/Content/img/Anuncios/"), fileDetail.Id + fileDetail.Extension);
                        file.SaveAs(path);

                        db.Entry(fileDetail).State = EntityState.Added;
                    }
                }
                db.Entry(anuncio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Manage", new { Message = "adEdited" });
            }
            ViewBag.categoriaID = new SelectList(db.Categorias, "categoriaID", "titulo", anuncio.categoriaID);
            ViewBag.pessoaID = new SelectList(db.Users, "Id", "UserName", anuncio.pessoaID);
            return View(anuncio);
        }

        // GET: Anuncios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anuncio anuncio = db.Anuncios.Find(id);
            if (anuncio == null)
            {
                return HttpNotFound();
            }
            return View(anuncio);
        }

        // POST: Anuncios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Anuncio anuncio = db.Anuncios.Find(id);
            db.Anuncios.Remove(anuncio);
            db.SaveChanges();
            return RedirectToAction("Index", "Manage", new { Message = "adDeleted" });
        }

        public ActionResult Exchange(int anuncioID, string origem)
        {
            if (origem == null)
            {
                return RedirectToAction("Login", "Account");
            }            
            var anuncio = db.Anuncios.Find(anuncioID);
            if (anuncio.pessoa.UserName == User.Identity.Name)
            {
                return RedirectToAction("Index", new { Message = "sameUser" });
            }
            var user = db.Users.Where(u => u.UserName == origem).First();
            if(db.Interesses.Where(i => i.anuncioID == anuncio.anuncioID).Where(i => i.userID == user.Id).Count() >= 1)
            {
                return RedirectToAction("Details/"+anuncioID, new { Message = "interestAlreadyCreated" });
            }
            var interesse = new Interesse();
            interesse.anuncioID = anuncio.anuncioID;
            interesse.userID = user.Id;
            interesse.texto = (anuncio.tipo == "Ofertas" ? "texto oferta" : "texto pedido");
            interesse.aceito = null;
            db.Interesses.Add(interesse);
            anuncio.status = "Com interessados";
            db.Entry(anuncio).State = EntityState.Modified;
            db.SaveChanges();
            EmailService.sendNovoInteresseMessage(anuncio.pessoa.UserName, User.Identity.Name, anuncio.titulo);
            return RedirectToAction("Index", new { Message = "interestCreated" });
        }

        public ActionResult Interests(int? page, int anuncioID)
        {
            var user = db.Anuncios.Find(anuncioID).pessoa;
            if (user.UserName != User.Identity.Name)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //var interesses = db.Interesses.Where(i => i.anuncioID == anuncioID).ToList();
            var interesses = Interesse.getInteressesByAnuncio(anuncioID).Where(i => i.aceito == null).ToList();
            if(interesses.Count == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(interesses.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult ConfirmarTroca(int anuncioID, string userID)
        {
            var anuncio = db.Anuncios.Find(anuncioID);
            var user = anuncio.pessoa;
            if (user.UserName != User.Identity.Name)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var interesses = Interesse.getInteressesByAnuncio(anuncioID).ToList();
            foreach (var item in interesses)
            {
                var interesse = db.Interesses.Where(i => i.userID == item.userID).Where(i => i.anuncioID == anuncioID).First();
                if (item.userID == userID)
                {
                    interesse.aceito = true;
                }
                else
                {
                    interesse.aceito = false;
                }
                db.Entry(interesse).State = EntityState.Modified;
            }
            anuncio.ativo = false;
            anuncio.status = "Terminado";
            db.Entry(anuncio).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", new { Message = "tradeConfirmed" });
        }

        [HttpPost]
        public JsonResult DeleteFile(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Result = "Error" });
            }
            try
            {
                Guid guid = new Guid(id);
                Imagem imagem = db.Imagens.Find(guid);
                if (imagem == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Json(new { Result = "Error" });
                }

                //Remove from database
                db.Imagens.Remove(imagem);
                db.SaveChanges();

                //Delete file from the file system
                var path = System.IO.Path.Combine(Server.MapPath("~/App_Data/Upload/"), imagem.Id + imagem.Extension);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        // GET: Anuncios/Edit/5
        public ActionResult Ativar(int? id)
        {
            if (id == null && User.Identity.Name != "brunoha")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anuncio anuncio = db.Anuncios.Include(a => a.imagens).SingleOrDefault(a => a.anuncioID == id);
            if (anuncio == null)
            {
                return HttpNotFound();
            }
            anuncio.ativo = true;
            db.Entry(anuncio).State = EntityState.Modified;
            db.SaveChanges();
            return Json("Ativado com sucesso", JsonRequestBehavior.AllowGet);

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
