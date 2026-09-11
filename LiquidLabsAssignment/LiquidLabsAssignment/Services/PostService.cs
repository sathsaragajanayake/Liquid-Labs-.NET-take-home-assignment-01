using LiquidLabsAssignment.Data;
using LiquidLabsAssignment.Models;

namespace LiquidLabsAssignment.Services
{
    public class PostService
    {
        private readonly PostRepository _postRepository;
        private readonly HttpClient _httpClient;

        public PostService(PostRepository postRepository, HttpClient httpClient)
        {
            _postRepository = postRepository;
            _httpClient = httpClient;
        }

        public async Task<List<Post>> GetPostsAsync()
        {
            var posts =  await _postRepository.GetPostsAsync();

            if (posts == null || posts.Count == 0)
            {
                var response = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/posts");
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

        public async Task<Post?> GetPostByIdAsync(int id)
        {
            var posts = await _postRepository.GetPostByIdAsync(id);

            if(posts == null)
            {
                var response = await _httpClient.GetAsync($"https://jsonplaceholder.typicode.com/posts/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                response.EnsureSuccessStatusCode();
                var postFromApi = await response.Content.ReadFromJsonAsync<Post>();
                if (postFromApi != null)
                {
                    await _postRepository.AddPostAsync(postFromApi);
                    return postFromApi;
                }
                return null;
            }
            return posts;
        }
    }
}
