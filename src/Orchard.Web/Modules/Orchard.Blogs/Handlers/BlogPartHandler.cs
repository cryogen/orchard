using JetBrains.Annotations;
using Orchard.Blogs.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;

namespace Orchard.Blogs.Handlers {
    [UsedImplicitly]
    public class BlogPartHandler : ContentHandler {
        public BlogPartHandler(IRepository<BlogPartRecord> repository) {
            Filters.Add(StorageFilter.For(repository));

            OnGetDisplayShape<BlogPart>((context, blog) => {
                                            context.Shape.Description = blog.Description;
                                            context.Shape.PostCount = blog.PostCount;
                                        });
        }
    }
}