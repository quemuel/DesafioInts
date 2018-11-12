using System.Collections.Generic;

namespace Ints.DesafioInts.Domain.Entities
{
    public class PorteEmpresa
    {
        public PorteEmpresa()
        {
            Clientes = new List<Cliente>();
        }
        public int PorteEmpresaId { get; set; }
        public string Descricao { get; set; }
        public virtual IEnumerable<Cliente> Clientes { get; set; }
    }
}