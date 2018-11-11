using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Ints.DesafioInts.Application.Interfaces;
using Ints.DesafioInts.Application.Services;
using Ints.DesafioInts.Application.ViewModels;
using System.Data.SqlClient;
using AutoMapper;
using Ints.DesafioInts.Domain.Entities;

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

        public ActionResult Index(string buscaNome = null, string buscaTipo = null)
        {
            List<ClienteViewModel> clientes = new List<ClienteViewModel>();

            if (!string.IsNullOrEmpty(buscaNome))
            {
                clientes.Add(_cadastroAppService.ObterPorNome(buscaNome)); 
            }
            else if (!string.IsNullOrEmpty(buscaTipo))
            {
                ConexaoNativa(clientes, buscaTipo);
            }
            else
            {
                clientes = _cadastroAppService.ObterTodos().OrderBy(c => c.Nome).ToList();
            }

            return View(clientes);
        }

        private void ConexaoNativa(List<ClienteViewModel> clientes, string buscaTipo)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"]
                .ConnectionString;
            string query = "SELECT ClienteId, Nome, PorteEmpresaId FROM dbo.Cliente WHERE PorteEmpresaId = " + buscaTipo;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        ClienteViewModel clienteViewModel = new ClienteViewModel();
                        clienteViewModel.ClienteId = Guid.Parse(reader["ClienteId"].ToString());
                        clienteViewModel.Nome = reader["Nome"].ToString();
                        clienteViewModel.PorteEmpresaId = Convert.ToInt32(reader["PorteEmpresaId"]);
                        clienteViewModel.PorteEmpresa = Mapper.Map<PorteEmpresa>(_porteEmpresaAppService.ObterPorId(Convert.ToInt32(reader["PorteEmpresaId"])));
                        clientes.Add(clienteViewModel);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
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
            ViewBag.PorteEmpresaId = new SelectList(_porteEmpresaAppService.ObterTodos(), "PorteEmpresaId", "Descricao");
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
            ViewBag.PorteEmpresaId = new SelectList(_porteEmpresaAppService.ObterTodos(), "PorteEmpresaId", "Descricao", clienteViewModel.PorteEmpresaId);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void DeleteClienteAjax(Guid id)
        {
            _cadastroAppService.Remover(id);
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
