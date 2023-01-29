using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI.Web.ViewModel.Enterprise
{
    public class UpdateEnterpriseViewModel
    {
        [Required(ErrorMessage = "Campo obrigatório")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string FantasyName { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Sector { get; set; }
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

        internal void ConvertToEntity(Domain.Entities.Enterprise entity)
        {
            entity.Id = this.Id;
            entity.City = this.City;
            entity.Address = this.Address;
            entity.FantasyName = this.FantasyName;
            entity.Sector = this.Sector;
            entity.State = this.State;
            entity.Created_at = DateTime.Now;
            entity.Password = this.Password;
        }
    }
}
