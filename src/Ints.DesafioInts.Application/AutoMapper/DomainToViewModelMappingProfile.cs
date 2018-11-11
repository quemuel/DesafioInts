using AutoMapper;
using Ints.DesafioInts.Application.ViewModels;
using Ints.DesafioInts.Domain.Entities;

namespace Ints.DesafioInts.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        protected void Configure()
        {
            CreateMap<Cliente, ClienteViewModel>().ForMember(cvm => cvm.PorteEmpresaViewModel, cvm => cvm.MapFrom(c => c.PorteEmpresa));
            CreateMap<PorteEmpresa, PorteEmpresaViewModel>();
        }
    }
}