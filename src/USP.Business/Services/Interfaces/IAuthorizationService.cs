using System;
using Umbraco.Core.Models;
using USP.Business.Models.ContentModels;

namespace USP.Business.Services.Interfaces
{
    public interface IAuthorizationService
    {
        IMember RegisterUmbracoUser(PageRegistration registrationViewModel, bool isApproved);
        string SetMemberResetPasswordToken(IMember member, DateTime expiryTime);
    }
}
