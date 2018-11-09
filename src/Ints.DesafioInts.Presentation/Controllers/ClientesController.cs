using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ints.DesafioInts.Application.Interfaces;
using Ints.DesafioInts.Application.Services;
using Ints.DesafioInts.Application.ViewModels;
using Ints.DesafioInts.Presentation.Models;

namespace Ints.DesafioInts.Presentation.Controllers
{
    public class ClientesController : Controller
    {
        private readonly IClienteAppService _cadastroAppService;
        private readonly IPorteEmpresaAppService _porteEmpresaAppService;

        public ClientesController()
        {
            _cadastroAppService = new ClienteAppService();
            _porteEmpresaAppService = new PorteEmpresaAppService();
        }
        
        public ActionResult Index()
        {
            return View(_cadastroAppService.ObterTodos());
        }
        
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var clienteViewModel = _cadastroAppService.ObterPorId(id.Value);
            if (clienteViewModel == null)
            {
                return HttpNotFound();
            }
            return View(clienteViewModel);
        }
        
        public ActionResult Create()
        {
            ViewBag.PorteEmpresaId = new SelectList((from porteEmpresa in _porteEmpresaAppService.ObterTodos()
                select new
                {
                    PorteEmpresaId = porteEmpresa.PorteEmpresaId,
                    Descricao = porteEmpresa.Descricao
                }), "PorteEmpresaId", "Descricao");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClienteViewModel clienteViewModel)
        {
            if (ModelState.IsValid)
            {
                _cadastroAppService.Adicionar(clienteViewModel);
                return RedirectToAction("Index");
            }

            return View(clienteViewModel);
        }
        
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var clienteViewModel = _cadastroAppService.ObterPorId(id.Value);
            if (clienteViewModel == null)
            {
                return HttpNotFound();
            }
            return View(clienteViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClienteViewModel clienteViewModel)
        {
            if (ModelState.IsValid)
            {
                _cadastroAppService.Atualizar(clienteViewModel);
                return RedirectToAction("Index");
            }
            return View(clienteViewModel);
        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var clienteViewModel = _cadastroAppService.ObterPorId(id.Value);
            if (clienteViewModel == null)
            {
                return HttpNotFound();
            }
            return View(clienteViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            _cadastroAppService.Remover(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _cadastroAppService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
