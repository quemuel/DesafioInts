using System.Linq;
using Ints.DesafioInts.Domain.Entities;
using Ints.DesafioInts.Domain.Interfaces.Repository;

namespace Ints.DesafioInts.Infra.Data.Repository
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public Cliente ObterPorNome(string nome)
        {
            return Buscar(c => c.Nome.Contains(nome)).FirstOrDefault();
        }
    }
}