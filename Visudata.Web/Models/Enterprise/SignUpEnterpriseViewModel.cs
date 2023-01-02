using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI.Web.ViewModel.Enterprise
{
    public class SignUpEnterpriseViewModel
    {
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Cnpj { get; set; }
    }
}
