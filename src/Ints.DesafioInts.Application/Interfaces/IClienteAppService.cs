using System;
using System.Collections.Generic;
using Ints.DesafioInts.Application.ViewModels;

namespace Ints.DesafioInts.Application.Interfaces
{
    public interface IClienteAppService : IDisposable
    {
        ClienteViewModel Adicionar(ClienteViewModel clienteViewModel);
        ClienteViewModel ObterPorId(Guid id);
        IEnumerable<ClienteViewModel> ObterTodos();
        ClienteViewModel ObterPorNome(string nome);
        ClienteViewModel Atualizar(ClienteViewModel clienteViewModel);
        void Remover(Guid id);
    }
}