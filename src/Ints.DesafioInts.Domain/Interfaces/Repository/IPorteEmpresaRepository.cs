using System.Collections.Generic;
using Ints.DesafioInts.Domain.Entities;

namespace Ints.DesafioInts.Domain.Interfaces.Repository
{
    //REPOSITORIO ESPECIALISTA
    public interface IPorteEmpresaRepository : IRepository<PorteEmpresa>
    {
        PorteEmpresa ObterPorDescricao(string descricao);
    }
}