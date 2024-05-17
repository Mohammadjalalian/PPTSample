using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using SampleBackend.Data;
using SampleBackend.Data.Models;
using SampleBackend.Server.Services.Contracts;
namespace SampleBackend.Server.Services
{
    public class ImageService : IImageService
    {
        private readonly SampleDbContext _context;

        public ImageService(SampleDbContext context)
        {
            _context = context;
        }
        public async Task<string> GetImageUrl(string userIdentifier)
        {
            var lastCharacter = userIdentifier.LastOrDefault().ToString();
            if (int.TryParse(lastCharacter, out var lastDigitOfUserIdentifier))
            {
                if (lastDigitOfUserIdentifier > 5)
                {
                    HttpClient client = new HttpClient();
                    Image image = await client.GetFromJsonAsync<Image>(
                        $"https://my-json-server.typicode.com/ck-pacificdev/tech-test/images/{lastDigitOfUserIdentifier}");
                    return image.Url;
                        
                }
                if (lastDigitOfUserIdentifier > 0)
                {
                    return (await _context.Images.FirstOrDefaultAsync(i => i.Id == lastDigitOfUserIdentifier))?.Url;
                }
                
            }

            if (Regex.IsMatch(userIdentifier, "[aeiou]"))
                return "https://api.dicebear.com/8.x/pixel-art/png?seed=vowel&size=150";
            if (Regex.IsMatch(userIdentifier, @"\W|_"))
                return $"https://api.dicebear.com/8.x/pixel-art/png?seed={new Random().Next(1,5)}&size=150";
            

            
            return $"https://api.dicebear.com/8.x/pixel-art/png?seed=default&size=150";
        }
    }
}
