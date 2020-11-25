using System.ComponentModel;

namespace USP.Business.Enums
{
    public enum EmailTokens
    {
        [Description("[ReceiverFullname]")] ReceiverFullname,

        [Description("[SiteUrl]")] SiteUrl,

        [Description("[SenderFullname]")] SenderFullname,

        [Description("[Telephone]")] Telephone,

        [Description("[Address1]")] Address1,

        [Description("[Address2]")] Address2,

        [Description("[City]")] City,

        [Description("[PostCode]")] PostCode,

        [Description("[Country]")] Country,

        [Description("[ForgotPasswordLink]")] ForgotPasswordLink,

        [Description("[EmailAddress]")] EmailAddress,

        [Description("[AccountId]")] AccountId,

        [Description("[ResultURL]")] ResultUrl,

        [Description("[Details]")] Details,
    }
}