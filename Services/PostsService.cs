using System.Collections.Generic;
using System.Linq;

using mvc_album_browser.Abstract;
using mvc_album_browser.Entities;
using mvc_album_browser.Models;

namespace mvc_album_browser.Services
{
    public class PostsService
    {
        private readonly IRepository<post> _repository;
        public PostsService(IRepository<post> repository)
        {
            _repository = repository;
        }
        public IEnumerable<Post> GetAllPosts()
        {
            return _repository.GetAll().Select(p => _getPostModelFromEntity(p));
        }
        public IEnumerable<Post> GetAllPostsByUserId(int userId)
        {
            return _repository.Where(p => p.userId == userId).Select(p => _getPostModelFromEntity(p));
        }
        public Post GetFirstPostByUserId(int userId)
        {
            var post = _repository.FirstOrDefault(p => p.userId == userId);

            return _getPostModelFromEntity(post);
        }
        private Post _getPostModelFromEntity(post post)
        {
            return post.MapTo<post, Post>();

            // return new Post
            // {
            //     Id = post.id,
            //     UserId = post.userId,
            //     Title = post.title,
            //     Body = post.body
            // };
        }
    }
}