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
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;

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
            var buscar = HttpContext.Request.Params.Get("buscar");

            string[] porteEmpresasInformacoes = new string[3] { "porte-empresa-grande", "porte-empresa-media", "porte-empresa-pequena" };

            List<ClienteViewModel> clientes = new List<ClienteViewModel>();

            if (!string.IsNullOrEmpty(buscar) && Array.Exists(porteEmpresasInformacoes, p => p == buscar))
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                string query = "SELECT ClienteId, Nome, PorteEmpresaId FROM dbo.Cliente;";
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
                            clienteViewModel.PorteEmpresaViewModel = _porteEmpresaAppService.ObterPorId(Convert.ToInt32(reader["PorteEmpresaId"]));
                            clientes.Add(clienteViewModel);
                        }
                    }
                    finally
                    {
                        // Always call Close when done reading.
                        reader.Close();
                    }
                }
            } else if(!string.IsNullOrEmpty(buscar))
            {                
                clientes.Add(_cadastroAppService.ObterPorNome(buscar));
            } else
            {
                clientes = _cadastroAppService.ObterTodos().OrderBy(c => c.Nome).ToList();                
            }

            return View(clientes);
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
            ViewBag.PorteEmpresaId = new SelectList((from porteEmpresaViewModel in _porteEmpresaAppService.ObterTodos()
                select new
                {
                    PorteEmpresaId = porteEmpresaViewModel.PorteEmpresaId,
                    Descricao = porteEmpresaViewModel.Descricao
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
            ViewBag.PorteEmpresaId = new SelectList((from porteEmpresaViewModel in _porteEmpresaAppService.ObterTodos()
                select new
                {
                    PorteEmpresaId = porteEmpresaViewModel.PorteEmpresaId,
                    Descricao = porteEmpresaViewModel.Descricao
                }), "PorteEmpresaId", "Descricao", clienteViewModel.PorteEmpresaId);
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
