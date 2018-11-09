using System.Net;
using System.Web.Mvc;
using Ints.DesafioInts.Application.Interfaces;
using Ints.DesafioInts.Application.Services;
using Ints.DesafioInts.Application.ViewModels;

namespace Ints.DesafioInts.Presentation.Controllers
{
    public class PorteEmpresasController : Controller
    {
        private readonly IPorteEmpresaAppService _porteEmpresaAppService;

        public PorteEmpresasController()
        {
            _porteEmpresaAppService = new PorteEmpresaAppService();
        }
        
        public ActionResult Index()
        {
            return View(_porteEmpresaAppService.ObterTodos());
        }
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var porteEmpresaViewModel = _porteEmpresaAppService.ObterPorId(id.Value);
            if (porteEmpresaViewModel == null)
            {
                return HttpNotFound();
            }
            return View(porteEmpresaViewModel);
        }
        
        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PorteEmpresaViewModel porteEmpresaViewModel)
        {
            if (ModelState.IsValid)
            {
                _porteEmpresaAppService.Adicionar(porteEmpresaViewModel);
                return RedirectToAction("Index");
            }

            return View(porteEmpresaViewModel);
        }
        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var porteEmpresaViewModel = _porteEmpresaAppService.ObterPorId(id.Value);
            if (porteEmpresaViewModel == null)
            {
                return HttpNotFound();
            }
            return View(porteEmpresaViewModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PorteEmpresaViewModel porteEmpresaViewModel)
        {
            if (ModelState.IsValid)
            {
                _porteEmpresaAppService.Atualizar(porteEmpresaViewModel);
                return RedirectToAction("Index");
            }
            return View(porteEmpresaViewModel);
        }
        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var porteEmpresaViewModel = _porteEmpresaAppService.ObterPorId(id.Value);
            if (porteEmpresaViewModel == null)
            {
                return HttpNotFound();
            }
            return View(porteEmpresaViewModel);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _porteEmpresaAppService.Remover(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _porteEmpresaAppService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
