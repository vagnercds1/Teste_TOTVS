using CamadaDAL;
using CamadaModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplicationTeste; 

namespace WebApplicationTeste.Controllers
{
    public class PedidosController : Controller
    {
        private ContextoEF db = new ContextoEF();

        // GET: Pedidos
        public ActionResult Index()
        {
            var pedidos = db.Pedidos.Include(p => p.Clientes).Include(p => p.Usuarios);
            return View(pedidos.ToList());
        }

        // GET: Pedidos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Session["PedidoId"] = id;

            return RedirectToAction("Index", "ItensPedidos");
        }

        // GET: Pedidos/Create
        public ActionResult Create()
        { 
            ViewBag.ClientesId = new SelectList(db.Clientes, "Id", "Nome");
            ViewBag.UsuariosId = new SelectList(db.Usuarios, "Id", "Nome"); 

            var max = new PedidosDAL().RetornaNumeroPedido();
            ViewBag.NumeroPedido = max.ToString();

            return View();
        }

        // POST: Pedidos/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ClientesId,UsuariosId,NumeroPedido,DataEntrega")] Pedidos pedidos)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PedidosDAL dal = new PedidosDAL();
                    pedidos.NumeroPedido = dal.RetornaNumeroPedido();

                    int idInserido= dal.GravarPedido(pedidos);

                    Session["PedidoId"] = idInserido;

                    return RedirectToAction("Index","ItensPedidos");
                }

                ViewBag.Id = new SelectList(db.Clientes, "Id", "Nome", pedidos.Id);
                ViewBag.Id = new SelectList(db.Usuarios, "Id", "Nome", pedidos.Id);
            }
            catch (Exception ex)
            { 

            }
           
            return View(pedidos);
        }

        // GET: Pedidos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Session["PedidoId"] = id;

            return RedirectToAction("Index", "ItensPedidos"); 
        }

        // POST: Pedidos/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ClientesId,UsuariosId,NumeroPedido,DataEntrega")] Pedidos pedidos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pedidos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.Clientes, "Id", "Nome", pedidos.Id);
            ViewBag.Id = new SelectList(db.Usuarios, "Id", "Nome", pedidos.Id);
            return View(pedidos);
        }

        // GET: Pedidos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedidos pedidos = db.Pedidos.Find(id);
            if (pedidos == null)
            {
                return HttpNotFound();
            }
            return View(pedidos);
        }

        // POST: Pedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pedidos pedidos = db.Pedidos.Find(id);
            db.Pedidos.Remove(pedidos);
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
