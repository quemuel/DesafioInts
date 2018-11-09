using System;

namespace Ints.DesafioInts.Domain.Entities
{
    public class Cliente
    {
        public Cliente()
        {
            ClienteId = Guid.NewGuid();
        }
        public Guid ClienteId { get; set; }
        public string Nome { get; set; }
        public int PorteEmpresaId { get; set; }
        public virtual PorteEmpresa PorteEmpresa { get; set; }
    }
}