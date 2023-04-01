using Application.Abstractions;
using Application.Posts.Commands;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Posts.CommandHandlers
{
    public class DeletePostHandler : IRequestHandler<DeletePost>
    {
        private readonly IPostRepository _postRepo;
        public DeletePostHandler(IPostRepository postRepository)
        {
            _postRepo = postRepository;
        }

        public async Task Handle(DeletePost request, CancellationToken cancellationToken)
        {
            await _postRepo.DeletePost(request.PostId);
        }
    }
}
