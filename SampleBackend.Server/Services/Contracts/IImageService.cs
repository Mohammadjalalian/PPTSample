namespace SampleBackend.Server.Services.Contracts
{
    public interface IImageService
    {
        Task<string> GetImageUrl(string userIdentifier);
    }
}
