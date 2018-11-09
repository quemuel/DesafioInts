using System;
using System.Collections.Generic;
using AutoMapper;
using Ints.DesafioInts.Application.Interfaces;
using Ints.DesafioInts.Application.ViewModels;
using Ints.DesafioInts.Domain.Entities;
using Ints.DesafioInts.Infra.Data.Repository;

namespace Ints.DesafioInts.Application.Services
{
    public class PorteEmpresaAppService : IPorteEmpresaAppService
    {
        private readonly PorteEmpresaRepository _porteEmpresaRepository;

        public PorteEmpresaAppService()
        {
            _porteEmpresaRepository = new PorteEmpresaRepository();
        }

        public void Dispose()
        {
            _porteEmpresaRepository.Dispose();
            GC.SuppressFinalize(this);
        }

        public PorteEmpresaViewModel Adicionar(PorteEmpresaViewModel porteEmpresaViewModel)
        {
            var porteEmpresa = Mapper.Map<PorteEmpresa>(porteEmpresaViewModel);
            return Mapper.Map<PorteEmpresaViewModel>(_porteEmpresaRepository.Adicionar(porteEmpresa));
        }

        public PorteEmpresaViewModel ObterPorId(int id)
        {
            return Mapper.Map<PorteEmpresaViewModel>(_porteEmpresaRepository.ObterPorId(id));
        }

        public IEnumerable<PorteEmpresaViewModel> ObterTodos()
        {
            return Mapper.Map<IEnumerable<PorteEmpresaViewModel>>(_porteEmpresaRepository.ObterTodos());
        }

        public PorteEmpresaViewModel ObterPorDescricao(string descricao)
        {
            return Mapper.Map<PorteEmpresaViewModel>(_porteEmpresaRepository.ObterPorDescricao(descricao));
        }

        public PorteEmpresaViewModel Atualizar(PorteEmpresaViewModel porteEmpresaViewModel)
        {
            var porteEmpresa = Mapper.Map<PorteEmpresa>(porteEmpresaViewModel);
            return Mapper.Map<PorteEmpresaViewModel>(_porteEmpresaRepository.Atualizar(porteEmpresa));
        }

        public void Remover(int id)
        {
            _porteEmpresaRepository.Remover(id);
        }
    }
}