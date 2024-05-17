using SampleBackend.Server.Services.Contracts;

namespace SampleBackend.Server.Services
{
    public static class BuilderServiceScopes
    {
        public static IServiceCollection AddHandlers(this IServiceCollection builderService)
        {
            builderService.AddScoped<IImageService, ImageService>();
            return builderService;
        }
    }
}
