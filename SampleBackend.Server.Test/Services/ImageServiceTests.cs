using System.Net.Http.Json;
using SampleBackend.Data;
using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using SampleBackend.Data.Models;
using SampleBackend.Server.Services;
using Microsoft.Data.Sqlite;

namespace SampleBackend.Server.Test.Services
{
    public class ImageServiceTests : IDisposable, IAsyncDisposable
    {
        private readonly SampleDbContext _context;
        private readonly SqliteConnection _connection;

        public ImageServiceTests()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            var options = new DbContextOptionsBuilder<SampleDbContext>().UseSqlite(_connection).Options;

            _context = new SampleDbContext(options, "DataSource=:memory:");

            _context.Database.EnsureCreated();
            _context.Images.AddRange(
            new []{
                new Image { Id = 1, Url = "https://api.dicebear.com/8.x/pixel-art/png?seed=1&size=150" },
                new Image { Id = 2, Url = "https://api.dicebear.com/8.x/pixel-art/png?seed=2&size=150" },
                new Image { Id = 3, Url = "https://api.dicebear.com/8.x/pixel-art/png?seed=3&size=150" },
                new Image { Id = 4, Url = "https://api.dicebear.com/8.x/pixel-art/png?seed=4&size=150" },
                new Image { Id = 5, Url = "https://api.dicebear.com/8.x/pixel-art/png?seed=5&size=150" },
            });


        }
        [Fact]
        public async Task DatabaseIsAvailableAndCanBeConnectedTo()
        {
            Assert.True(await _context.Database.CanConnectAsync());
        }

        [Fact]
        public async Task ImageService_GetImageUrl_LastDigit()
        {
            // Arrange
            var imageService = new ImageService(_context);
            var rand = new Random().Next(1, 9);
            var userIdentifier = "12345" + rand;
            // Act
            var result = await imageService.GetImageUrl(userIdentifier);
            // Assert
            if (rand > 5)
            {
                HttpClient client = new HttpClient();
                Image image = await client.GetFromJsonAsync<Image>(
                    $"https://my-json-server.typicode.com/ck-pacificdev/tech-test/images/{rand}");

                Assert.Equal(image.Url, result);
            }
            else
                Assert.Equal($"https://api.dicebear.com/8.x/pixel-art/png?seed={rand}&size=150", result);

        }
        [Fact]
        public async Task ImageService_GetImageUrl_Vowels()
        {
            // Arrange
            var imageService = new ImageService(_context);
            var userIdentifier = "aeiou";
            // Act
            var result = await imageService.GetImageUrl(userIdentifier);
            // Assert
            Assert.Equal("https://api.dicebear.com/8.x/pixel-art/png?seed=vowel&size=150", result);
        }
        [Fact]
        public async Task ImageService_GetImageUrl_NoneAlphaNumeric()
        {
            // Arrange
            var imageService = new ImageService(_context);
            var userIdentifier = "12345@";
            // Act
            var result = await imageService.GetImageUrl(userIdentifier);
            // Assert
            Assert.StartsWith("https://api.dicebear.com/8.x/pixel-art/png?seed=", result);
            Assert.EndsWith("&size=150",result);
        }

        public void Dispose()
        {

            _context.Dispose();
            _connection.Close();
        }

        public async ValueTask DisposeAsync()
        {

            await _context.DisposeAsync();
            await _connection.CloseAsync();
        }
    }
}
