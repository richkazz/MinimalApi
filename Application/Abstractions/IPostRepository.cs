using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions
{
    public interface IPostRepository
    {
        Task<ICollection<Post>> GetAllPost();
        Task<Post> GetPostById(int postId);
        Task<Post> CreatePost(Post postToCreat);
        Task<Post> UpdatePost(string updateContent, int postId);
        Task DeletePost(int postId);
    }
}
