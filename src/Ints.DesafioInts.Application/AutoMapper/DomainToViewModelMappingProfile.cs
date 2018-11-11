using AutoMapper;
using Ints.DesafioInts.Application.ViewModels;
using Ints.DesafioInts.Domain.Entities;

namespace Ints.DesafioInts.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        protected void Configure()
        {
            CreateMap<Cliente, ClienteViewModel>();
            CreateMap<PorteEmpresa, PorteEmpresaViewModel>();
        }
    }
}