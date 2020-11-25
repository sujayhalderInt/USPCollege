namespace USP.Business.Services.Interfaces
{
    public interface IFileTypeService
    {
        string GetFileType(string extension);
        void ResetCache();
    }
}