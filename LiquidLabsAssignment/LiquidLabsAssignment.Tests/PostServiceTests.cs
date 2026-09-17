using LiquidLabsAssignment.Data;
using LiquidLabsAssignment.Models;
using LiquidLabsAssignment.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using System.ComponentModel.DataAnnotations;

namespace LiquidLabsAssignment.Tests
{
    public class PostServiceTests
    {
        [Fact]
        public async Task GetPostsAsync_ReturnsPostsFromDB_WhenDBIsNotEmpty()
        {
            var postsFromDb = new List<Post>
            {
                new Post { Id = 1, UserId = 1, Title = "Post 1", Body = "Body 1" }
            };

            var repositoryMock = new Mock<IPostRepository>();
            repositoryMock.Setup(repo => repo.GetPostsAsync()).ReturnsAsync(postsFromDb);

            var httpClient= new HttpClient();

            var configuration = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string>
            {
                ["ExternalApi:BaseUrl"] = "https://jsonplaceholder.typicode.com/"
            }).Build();

            var service = new PostService(repositoryMock.Object, httpClient, configuration);

            var result = await service.GetPostsAsync();

            Assert.Single(result);
            Assert.Equal(1, result[0].Id);
            Assert.Equal("Post 1", result[0].Title);

            repositoryMock.Verify(repo => repo.GetPostsAsync(), Times.Once);

        }
    }
}
