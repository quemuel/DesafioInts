using Ints.DesafioInts.Domain.Entities;

namespace Ints.DesafioInts.Domain.Interfaces.Repository
{
    //REPOSITORIO ESPECIALISTA
    public interface IClienteRepository : IRepository<Cliente>
    {
        Cliente ObterPorNome(string nome);
    }
}