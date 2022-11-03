﻿using PI.Application.ViewModel.Enterprise;

namespace PI.Application.Intefaces;

public interface IEnterpriseService
{
    Task<bool> Login(EnterpriseLoginViewModel model);
    Task<bool> SignUp(CreateEnterpriseViewModel model);
    Task<bool> Update(UpdateEnterpriseViewModel model);
    Task<bool> Remove(int enterpriseId);
    Task<EnterpriseProfileViewModel> GetEnterpriseForProfileById(int enterpriseId);

    //TODO : Create enterprise profile view and update. 

}