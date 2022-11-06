namespace PI.Domain.Entities
{
    public class Enterprise : EntityBase
    {
        public string Cnpj { get; set; }
        public string SocialReason { get; set; }
        public string FantasyName { get; set; }
        public string Password { get; set; }
        public string Sector { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string NumberOfLocation { get; set; }

        #region Relationships
        public ICollection<MachineCategory> EnterpriseMachineCategories { get; set; }
        public IEnumerable<Machine> Machines { get; set; }
        public EnterpriseStatus EnterpriseStatus { get; set; }

        #endregion
    }
}