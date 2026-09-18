using LiquidLabsAssignment.Data;
using LiquidLabsAssignment.Models;

namespace LiquidLabsAssignment.Services
{
    public class PostService
    {
        private readonly IPostRepository _postRepository;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string _externalApiUrl;

        public PostService(IPostRepository postRepository, HttpClient httpClient, IConfiguration configuration)
        {
            _postRepository = postRepository;
            _httpClient = httpClient;
            _configuration = configuration;

            _externalApiUrl = _configuration["ExternalApi:BaseUrl"]
            ?? throw new InvalidOperationException("ExternalApiUrl is not configured.");
        }

        public async Task<List<Post>> GetPostsAsync()
        {
            var posts =  await _postRepository.GetPostsAsync();

            if (posts == null || posts.Count == 0)
            {
                var response = await _httpClient.GetAsync(_externalApiUrl);
                response.EnsureSuccessStatusCode();
                var postsFromApi = await response.Content.ReadFromJsonAsync<List<Post>>();
                if (postsFromApi != null)
                {
                    foreach (var post in postsFromApi)
                    {
                        await _postRepository.AddPostAsync(post);
                    }
                    return postsFromApi;
                }
                return new List<Post>();
            }
            return posts;
        }

        public async Task<(Post? post, string message)> GetPostByIdAsync(int id)
        {
            var posts = await _postRepository.GetPostByIdAsync(id);

            if(posts == null)
            {
                var response = await _httpClient.GetAsync($"{_externalApiUrl}/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    return (null, "Post not found in DB or API");
                }

                response.EnsureSuccessStatusCode();
                var postFromApi = await response.Content.ReadFromJsonAsync<Post>();
                if (postFromApi != null)
                {
                    await _postRepository.AddPostAsync(postFromApi);
                    return (postFromApi, "Post retrieved from API and saved to DB");
                }
                return (null, "Post not found in API");
            }
            return (posts, "Post retrieved from DB");
        }
    }
}
