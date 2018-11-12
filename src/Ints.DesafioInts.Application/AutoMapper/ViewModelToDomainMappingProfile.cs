using AutoMapper;
using Ints.DesafioInts.Application.ViewModels;
using Ints.DesafioInts.Domain.Entities;

namespace Ints.DesafioInts.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        protected void Configure()
        {
            CreateMap<Cliente, ClienteViewModel>();
            CreateMap<PorteEmpresa, PorteEmpresaViewModel>();
        }
    }
}