using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Ints.DesafioInts.Domain.Entities;

namespace Ints.DesafioInts.Application.ViewModels
{
    public class PorteEmpresaViewModel
    {
        public PorteEmpresaViewModel()
        {
            Clientes = new List<ClienteViewModel>();
        }

        [Key]
        public int PorteEmpresaId { get; set; }

        [Required(ErrorMessage = "Preencha o campo Descrição")]
        [MaxLength(100, ErrorMessage = "Máximo {0} caracteres")]
        [MinLength(2, ErrorMessage = "Mínimo {0} caracteres")]
        [DisplayName("Descrição")]
        public string Descricao { get; set; }

        public virtual IEnumerable<ClienteViewModel> Clientes { get; set; }
    }
}