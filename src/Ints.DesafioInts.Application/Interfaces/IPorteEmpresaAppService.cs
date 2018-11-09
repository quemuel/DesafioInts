using System;
using System.Collections.Generic;
using Ints.DesafioInts.Application.ViewModels;

namespace Ints.DesafioInts.Application.Interfaces
{
    public interface IPorteEmpresaAppService : IDisposable
    {
        PorteEmpresaViewModel Adicionar(PorteEmpresaViewModel porteEmpresaViewModel);
        PorteEmpresaViewModel ObterPorId(int id);
        IEnumerable<PorteEmpresaViewModel> ObterTodos();
        PorteEmpresaViewModel ObterPorDescricao(string descricao);
        PorteEmpresaViewModel Atualizar(PorteEmpresaViewModel porteEmpresaViewModel);
        void Remover(int id);
    }
}