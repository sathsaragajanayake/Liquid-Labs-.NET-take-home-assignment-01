using LiquidLabsAssignment.Models;
using Microsoft.Data.SqlClient;

namespace LiquidLabsAssignment.Data
{
    public class PostRepository
    {
        private readonly string _connectionString;

        public PostRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<List<Post>> GetPostsAsync()
        {
            var posts = new List<Post>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("SELECT Id, UserId, Title, Body FROM Posts", connection))
                {
                    using (var reader = await command.ExecuteReaderAsync()) //SqlDataReader
                    {
                        while (await reader.ReadAsync())
                        {
                            var post = new Post // read current row
                            {
                                Id = reader.GetInt32(0),  //convert sql row to c#
                                UserId = reader.GetInt32(1),
                                Title = reader.GetString(2),
                                Body = reader.GetString(3)
                            };
                            posts.Add(post);
                        }
                    }
                }
            }
            return posts;
        }

        public async Task<Post?> GetPostByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("SELECT Id, UserId, Title, Body FROM Posts WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id); //parameterized SQL
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Post
                            {
                                Id = reader.GetInt32(0),
                                UserId = reader.GetInt32(1),
                                Title = reader.GetString(2),
                                Body = reader.GetString(3)
                            };
                        }
                    }
                }
            }
            return null;
        }
    }
}
