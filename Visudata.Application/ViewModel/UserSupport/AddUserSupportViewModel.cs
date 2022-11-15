namespace PI.Application.ViewModel.UserSupport;

public class AddUserSupportViewModel
{
    public int EnterpriseId { get; set; }
    public string NameOfEnterprise { get; set; }
    public string RepresentativeEmailAddress { get; set; }
    public int ProblemCategoryId { get; set; }
    public string ProblemDescription { get; set; }
    public DateTime Created_at = DateTime.Now;
}