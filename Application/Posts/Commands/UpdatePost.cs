using Domain.Models;
using MediatR;

namespace Application.Posts.Commands
{
    public class UpdatePost: IRequest<Post>
    {
        public int Id { get; set; }
        public string? PostContent { get; set; }
    }
}
