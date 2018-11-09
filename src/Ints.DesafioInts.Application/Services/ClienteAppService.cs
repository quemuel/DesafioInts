using System;
using System.Collections.Generic;
using AutoMapper;
using Ints.DesafioInts.Application.Interfaces;
using Ints.DesafioInts.Application.ViewModels;
using Ints.DesafioInts.Domain.Entities;
using Ints.DesafioInts.Domain.Interfaces.Repository;
using Ints.DesafioInts.Infra.Data.Repository;

namespace Ints.DesafioInts.Application.Services
{
    public class ClienteAppService : IClienteAppService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteAppService()
        {
            _clienteRepository = new ClienteRepository();
        }

        public void Dispose()
        {
            _clienteRepository.Dispose();
            GC.SuppressFinalize(this);
        }

        public ClienteViewModel Adicionar(ClienteViewModel clienteViewModel)
        {
            var cliente = Mapper.Map<Cliente>(clienteViewModel);
            return Mapper.Map<ClienteViewModel>(_clienteRepository.Adicionar(cliente));
        }

        public ClienteViewModel ObterPorId(Guid id)
        {
            return Mapper.Map<ClienteViewModel>(_clienteRepository.ObterPorId(id));
        }

        public IEnumerable<ClienteViewModel> ObterTodos()
        {
            return Mapper.Map<IEnumerable<ClienteViewModel>>(_clienteRepository.ObterTodos());
        }

        public ClienteViewModel ObterPorNome(string nome)
        {
            return Mapper.Map<ClienteViewModel>(_clienteRepository.ObterPorNome(nome));
        }

        public ClienteViewModel Atualizar(ClienteViewModel clienteViewModel)
        {
            var cliente = Mapper.Map<Cliente>(clienteViewModel);
            return Mapper.Map<ClienteViewModel>(_clienteRepository.Atualizar(cliente));
        }

        public void Remover(Guid id)
        {
            _clienteRepository.Remover(id);
        }
    }
}