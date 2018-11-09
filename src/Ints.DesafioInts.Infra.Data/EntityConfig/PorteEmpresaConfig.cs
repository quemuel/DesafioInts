using System.Data.Entity.ModelConfiguration;
using Ints.DesafioInts.Domain.Entities;

namespace Ints.DesafioInts.Infra.Data.EntityConfig
{
    public class PorteEmpresaConfig : EntityTypeConfiguration<PorteEmpresa>
    {
        public PorteEmpresaConfig()
        {
            HasKey(pe => pe.PorteEmpresaId);

            Property(pe => pe.Descricao)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(100);
        }
    }
}