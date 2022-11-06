namespace PI.Application.ViewModel.UserSupport;

public class AddUserSupportViewModel
{
    public string RepresentativeEmailAddress { get; set; }
    public int ProblemCategoryId { get; set; }
    public string ProblemDescription { get; set; }
    public DateTime Created_at = DateTime.Now;
}