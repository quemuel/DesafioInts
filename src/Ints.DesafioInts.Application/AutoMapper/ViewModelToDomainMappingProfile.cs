using AutoMapper;
using Ints.DesafioInts.Application.ViewModels;
using Ints.DesafioInts.Domain.Entities;

namespace Ints.DesafioInts.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        protected void Configure()
        {
            CreateMap<ClienteViewModel, Cliente>().ForMember(cvm => cvm.PorteEmpresa, cvm => cvm.MapFrom(c => c.PorteEmpresaViewModel));
            CreateMap<PorteEmpresaViewModel, PorteEmpresa>();
        }
    }
}