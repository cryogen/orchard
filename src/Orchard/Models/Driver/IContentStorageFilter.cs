namespace Orchard.Models.Driver {
    public interface IContentStorageFilter : IContentFilter {
        void Activated(ActivatedContentContext context);
        void Creating(CreateContentContext context);
        void Created(CreateContentContext context);
        void Loading(LoadContentContext context);
        void Loaded(LoadContentContext context);        
    }
}