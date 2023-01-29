using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace PI.Web.ViewModel.Enterprise

{
    public class EnterpriseProfileViewModel
    {
        public int Id { get; set; }
        public string FantasyName { get; set; }
        public string Sector { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

        internal void GetDataFromEntity(Domain.Entities.Enterprise entity)
        {
            this.Id = entity.Id;
            this.FantasyName = entity.FantasyName;
            this.Sector = entity.Sector;
            this.State = entity.State;
            this.City = entity.City;
            this.Address = entity.Address;
        }
    }
}
