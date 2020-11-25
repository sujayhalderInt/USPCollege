namespace USP.Business.Services.Interfaces
{
    public interface IRecaptchaService
    {
        bool VerifyRecaptcha(string captchaResponse, string secretKey);
    }
}
