﻿namespace Orchard.ContentManagement.Handlers {
    public class ContentHandlerBase : IContentHandler {
        public virtual void Activating(ActivatingContentContext context) {}
        public virtual void Activated(ActivatedContentContext context) {}
        public virtual void Initializing(InitializingContentContext context) {}
        public virtual void Creating(CreateContentContext context) {}
        public virtual void Created(CreateContentContext context) {}
        public virtual void Saving(SaveContentContext context) {}
        public virtual void Saved(SaveContentContext context) {}
        public virtual void Loading(LoadContentContext context) {}
        public virtual void Loaded(LoadContentContext context) {}
        public virtual void Versioning(VersionContentContext context) {}
        public virtual void Versioned(VersionContentContext context) {}
        public virtual void Publishing(PublishContentContext context) {}
        public virtual void Published(PublishContentContext context) {}
        public virtual void Removing(RemoveContentContext context) {}
        public virtual void Removed(RemoveContentContext context) {}
        public virtual void Indexing(IndexContentContext context) {}
        public virtual void Indexed(IndexContentContext context) {}

        public virtual void GetContentItemMetadata(GetContentItemMetadataContext context) {}
        public virtual void BuildDisplay(BuildDisplayContext context) {}
        public virtual void BuildEditor(BuildEditorContext context) {}
        public virtual void UpdateEditor(UpdateEditorContext context) {}
    }
}