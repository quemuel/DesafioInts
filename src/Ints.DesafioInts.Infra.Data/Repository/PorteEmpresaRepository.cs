using System.Collections.Generic;
using System.Linq;
using Ints.DesafioInts.Domain.Entities;
using Ints.DesafioInts.Domain.Interfaces.Repository;

namespace Ints.DesafioInts.Infra.Data.Repository
{
    public class PorteEmpresaRepository : Repository<PorteEmpresa>, IPorteEmpresaRepository
    {
        public PorteEmpresa ObterPorDescricao(string descricao)
        {
            return Buscar(c => c.Descricao == descricao).FirstOrDefault();
        }
    }
}