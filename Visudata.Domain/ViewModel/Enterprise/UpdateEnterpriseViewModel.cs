using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI.Domain.ViewModel.Enterpriqse
{
    public class UpdateEnterpriseViewModel
    {
        [Required(ErrorMessage = "Campo obrigatório")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string FantasyName { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Scctor { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string State { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string City { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "As senhas devem ser iguais !")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string ConfirmPassword { get; set; }
    }
}
