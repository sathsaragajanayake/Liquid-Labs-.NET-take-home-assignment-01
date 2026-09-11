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
            return await _postRepository.GetPostsAsync();
        }

        public async Task<Post?> GetPostByIdAsync(int id)
        {
            return await _postRepository.GetPostByIdAsync(id);
        }
    }
}
