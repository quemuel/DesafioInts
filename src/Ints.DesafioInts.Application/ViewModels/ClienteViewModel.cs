using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Ints.DesafioInts.Domain.Entities;

namespace Ints.DesafioInts.Application.ViewModels
{
    public class ClienteViewModel
    {
        public ClienteViewModel()
        {
            ClienteId = Guid.NewGuid();
        }
        [Key]
        public Guid ClienteId { get; set; }

        [Required(ErrorMessage = "Preencha o campo Nome")]
        [MaxLength(150, ErrorMessage = "Máximo {0} caracteres")]
        [MinLength(2, ErrorMessage = "Mínimo {0} caracteres")]
        [DisplayName("Nome")]
        public string Nome { get; set; }

        [ScaffoldColumn(false)]
        [DisplayName("Porte da Empresa")]
        public int PorteEmpresaId { get; set; }

        public virtual PorteEmpresa PorteEmpresa { get; set; }
    }
}