using System.Collections.Generic;
using System.Linq;
using Orchard.Blogs.Models;
using Orchard.Data;
using Orchard.Models;

namespace Orchard.Blogs.Services {
    public class BlogService : IBlogService {
        private readonly IContentManager _contentManager;
        private readonly IRepository<BlogRecord> _repository;

        public BlogService(IContentManager contentManager, IRepository<BlogRecord> repository) {
            _contentManager = contentManager;
            _repository = repository;
        }

        public Blog Get(string slug) {
            BlogRecord record = _repository.Get(br => br.Slug == slug && br.Enabled);
            ContentItem item = _contentManager.Get(record.Id);

            return item != null ? item.As<Blog>() : null;
        }

        public IEnumerable<Blog> Get() {
            IEnumerable<BlogRecord> blogs =_repository.Fetch(br => br.Enabled, br => br.Asc(br2 => br2.Name));

            return blogs.Select(br => _contentManager.Get(br.Id).As<Blog>());
        }

        public Blog CreateBlog(CreateBlogParams parameters) {
            BlogRecord record = new BlogRecord() {Name = parameters.Name, Slug = parameters.Slug, Enabled = parameters.Enabled};

            //TODO: (erikpo) Need an extension method or something for this default behavior
            ContentItem contentItem = _contentManager.New("blog");
            contentItem.As<Blog>().Record = record;
            _contentManager.Create(contentItem);

            return contentItem.As<Blog>();
        }
    }
}