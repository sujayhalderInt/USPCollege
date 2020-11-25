using System;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using USP.Business.Constants;
using USP.Business.Models.ContentModels;
using USP.Business.Services.Interfaces;

namespace USP.Business.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private IMemberService MemberService => ApplicationContext.Current.Services.MemberService;

        public IMember RegisterUmbracoUser(PageRegistration model, bool isApproved = true)
        {
            IMember member = MemberService.GetByEmail(model.EmailAddress);

            if (member != null)
            {
                //can't create a user that alread exists
                return member;
            }

            try
            {
                var memberTypeName = SiteConstants.AliasMemberType;
                var memberType = ApplicationContext.Current.Services.MemberTypeService.Get(memberTypeName);
                member = MemberService.CreateMemberWithIdentity(model.EmailAddress, model.EmailAddress, memberType);
                MemberService.Save(member);
                MemberService.SavePassword(member, model.Password);
                return member;
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(AuthorizationService),
                    $"Error creating the Umbraco Member: {model.EmailAddress}", ex);
                return null;
            }
        }

        public string SetMemberResetPasswordToken(IMember member, DateTime expiryTime)
        {
            string expiryToken = expiryTime.ToString("ddMMyyyyHHmmssFFFF");
            member.SetValue("resetPasswordToken", expiryToken);
            MemberService.Save(member);
            return expiryToken;
        }
    }
}
