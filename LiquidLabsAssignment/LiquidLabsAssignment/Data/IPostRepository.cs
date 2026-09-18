using LiquidLabsAssignment.Models;

namespace LiquidLabsAssignment.Data
{
    public interface IPostRepository
    {
        Task<List<Post>> GetPostsAsync();
        Task<Post?> GetPostByIdAsync(int id);
        Task AddPostAsync(Post post);
    }
}
