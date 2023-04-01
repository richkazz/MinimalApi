using Application.Posts.Commands;
using Application.Posts.Queries;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Hosting;
using MinimalApi.Abstractions;
using MinimalApi.Filiters;

namespace MinimalApi.EndpointDefinitions
{
    public class PostEndpointDefinition : IEndpointDefinition
    {
        public void RegisterEndpoints(WebApplication app)
        {
            var post = app.MapGroup("/api/posts");
            post.MapGet("/{id}", GetPostById)
                .WithName("GetPostById");
            post.MapPost("/", CreatePost)
                .AddEndpointFilter<PostValidationFiliters>();
            post.MapGet("/", GetAllPosts);
            post.MapPut("/{id}", UpdatePost)
                .AddEndpointFilter<PostValidationFiliters>();
            post.MapDelete("/{id}", DeletePost);
        }

        private async Task<IResult> GetPostById(IMediator mediator, int Id)
        {
            var getPost = new GetPostById { PostId = Id };
            var post = await mediator.Send(getPost);
            return TypedResults.Ok(post);
        }

        private async Task<IResult> CreatePost(IMediator mediator, Post post)
        {
            var creatPost = new CreatePost { PostContent = post.Content };
            var createdPost = await mediator.Send(creatPost);
            return Results.CreatedAtRoute("GetPostById", new { createdPost.Id }, createdPost);
        }

         private async Task<IResult> GetAllPosts(IMediator mediator)
         {
            var getCommand = new GetAllPosts();
            var posts = await mediator.Send(getCommand);
            return TypedResults.Ok(posts);
         }

        private async Task<IResult> UpdatePost(IMediator mediator, Post post, int id)
        {
            var updatePost = new UpdatePost { Id = id, PostContent = post.Content };
            var updetedPost = await mediator.Send(updatePost);
            return TypedResults.Ok(updetedPost);
        }

        private async Task<IResult> DeletePost(IMediator mediator, int id)
        {
            var deletePost = new DeletePost { PostId = id };
            await mediator.Send(deletePost);
            return TypedResults.NoContent();
        }


    }
}
