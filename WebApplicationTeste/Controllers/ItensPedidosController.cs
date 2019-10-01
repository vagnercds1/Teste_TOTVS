using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CamadaDAL;
using CamadaModel;
using WebApplicationTeste; 

namespace WebApplicationTeste.Controllers
{
    public class ItensPedidosController : Controller
    {
        private ContextoEF db = new ContextoEF();

        // GET: ItensPedidos
        public ActionResult Index()
        {
            int PedidoId = (Int32)Session["PedidoId"];
            List<ItensPedidos> itensPedidos = db.ItensPedidos.Include(i => i.Pedidos).Include(i => i.Produtos).Where(a=>a.PedidosId == PedidoId).ToList();

            Pedidos pedidos = db.Pedidos.Include(p => p.Clientes).Include(p => p.Usuarios).Where(a=>a.Id == PedidoId).FirstOrDefault();
             
            ViewBag.NumeroPedido = pedidos.NumeroPedido;
            ViewBag.Cliente = pedidos.Clientes.Nome;
            ViewBag.DataEntrega = pedidos.DataEntrega.ToString("dd/MM/yyyy");
            ViewBag.TotalPedido = new ItensPedidosDAL().RetornaTotalPorPedido(PedidoId);


          //  ViewBag.ProdutosId = new SelectList(db.Produtos, "Id", "Descricao");

             
            return View(itensPedidos);
        }

        // GET: ItensPedidos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItensPedidos itensPedidos = db.ItensPedidos.Find(id);
            if (itensPedidos == null)
            {
                return HttpNotFound();
            }
            return View(itensPedidos);
        }

        // GET: ItensPedidos/Create
        public ActionResult Create()
        {
            int PedidoId = (Int32)Session["PedidoId"];

            Pedidos pedidos = db.Pedidos.Include(p => p.Clientes).Include(p => p.Usuarios).Where(a => a.Id == PedidoId).FirstOrDefault();

            ViewBag.NumeroPedido = pedidos.NumeroPedido;
            ViewBag.Cliente = pedidos.Clientes.Nome;
            ViewBag.DataEntrega = pedidos.DataEntrega.ToString("dd/MM/yyyy");

            ViewBag.PedidosId = new SelectList(db.Pedidos, "Id", "Id");
            ViewBag.ProdutosId = new SelectList(db.Produtos, "Id", "Descricao");
            return View();
        }

        // POST: ItensPedidos/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Quantidade,ValorTotal,PedidosId,ProdutosId")] ItensPedidos itensPedidos)
        {
            int PedidoId = (Int32)Session["PedidoId"];
            if (ModelState.IsValid)
            {
                Produtos prod = db.Produtos.Find(itensPedidos.ProdutosId);

               
                itensPedidos.PedidosId = (Int32)Session["PedidoId"];


                itensPedidos.ValorUnit = prod.Valor;
                itensPedidos.ValorTotal = (itensPedidos.Quantidade * prod.Valor);
                db.ItensPedidos.Add(itensPedidos);
                db.SaveChanges();
                 
            } 
             
            return RedirectToAction("Index", "ItensPedidos"); 
        }

        // GET: ItensPedidos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItensPedidos itensPedidos = db.ItensPedidos.Find(id);
            if (itensPedidos == null)
            {
                return HttpNotFound();
            }
            ViewBag.PedidosId = new SelectList(db.Pedidos, "Id", "Id", itensPedidos.PedidosId);
            ViewBag.ProdutosId = new SelectList(db.Produtos, "Id", "Descricao", itensPedidos.ProdutosId);
            return View(itensPedidos);
        }

        // POST: ItensPedidos/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Quantidade,ValorTotal,PedidosId,ProdutosId")] ItensPedidos itensPedidos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itensPedidos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PedidosId = new SelectList(db.Pedidos, "Id", "Id", itensPedidos.PedidosId);
            ViewBag.ProdutosId = new SelectList(db.Produtos, "Id", "Descricao", itensPedidos.ProdutosId);
            return View(itensPedidos);
        }

        // GET: ItensPedidos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItensPedidos itensPedidos = db.ItensPedidos.Find(id);
            if (itensPedidos == null)
            {
                return HttpNotFound();
            }
            return View(itensPedidos);
        }

        // POST: ItensPedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ItensPedidos itensPedidos = db.ItensPedidos.Find(id);
            db.ItensPedidos.Remove(itensPedidos);
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
