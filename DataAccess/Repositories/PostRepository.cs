using Application.Abstractions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly SocialDbContext _ctx;
        public PostRepository(SocialDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<Post> CreatePost(Post postToCreat)
        {
            postToCreat.DateCreated = DateTime.Now;
            postToCreat.LastModified = DateTime.Now;
            _ctx.Posts.Add(postToCreat);
            await _ctx.SaveChangesAsync();
            return postToCreat;
        }

        public async Task DeletePost(int postId)
        {
            var post = await _ctx.Posts
            .FirstOrDefaultAsync(x => x.Id == postId);

            if (post == null) return;
            _ctx.Remove(post);
            await _ctx.SaveChangesAsync();
        }

        public async Task<ICollection<Post>> GetAllPost()
        {
            return await _ctx.Posts.ToListAsync();
        }

        public async Task<Post> GetPostById(int postId)
        {
            return await _ctx.Posts.FirstOrDefaultAsync(x => x.Id == postId);
        }

        public async Task<Post> UpdatePost(string updateContent, int postId)
        {
            var post = await _ctx.Posts.FirstOrDefaultAsync(x => x.Id == postId);
            post.Content = updateContent;
            post.LastModified = DateTime.Now;
            await _ctx.SaveChangesAsync();
            return post;
        }
    }
}
