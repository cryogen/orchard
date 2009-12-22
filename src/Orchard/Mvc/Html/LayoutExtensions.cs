﻿using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Orchard.Mvc.ViewEngines;
using Orchard.Mvc.ViewModels;
using Orchard.UI.PageTitle;
using Orchard.UI.Resources;
using Orchard.UI.Zones;

namespace Orchard.Mvc.Html {
    public static class LayoutExtensions {
        public static void RenderBody(this HtmlHelper html) {
            LayoutViewContext layoutViewContext = LayoutViewContext.From(html.ViewContext);
            html.ViewContext.HttpContext.Response.Output.Write(layoutViewContext.BodyContent);
        }

        public static MvcHtmlString Body(this HtmlHelper html) {
            LayoutViewContext layoutViewContext = LayoutViewContext.From(html.ViewContext);

            return MvcHtmlString.Create(layoutViewContext.BodyContent);
        }

        public static void AddTitleParts(this HtmlHelper html, params string[] titleParts) {
            html.Resolve<IPageTitleBuilder>().AddTitleParts(titleParts);
        }

        public static MvcHtmlString Title(this HtmlHelper html, params string[] titleParts) {
            IPageTitleBuilder pageTitleBuilder = html.Resolve<IPageTitleBuilder>();

            html.Resolve<IPageTitleBuilder>().AppendTitleParts(titleParts);

            return MvcHtmlString.Create(html.Encode(pageTitleBuilder.GenerateTitle()));
        }

        public static void Zone<TModel>(this HtmlHelper<TModel> html, string zoneName, string partitions) where TModel : BaseViewModel {
            IZoneManager manager = html.Resolve<IZoneManager>();

            manager.Render(html, html.ViewData.Model.Zones, zoneName, partitions);
        }

        public static void Zone<TModel>(this HtmlHelper<TModel> html, string zoneName) where TModel : BaseViewModel {
            html.Zone(zoneName, string.Empty);
        }

        public static void Zone<TModel>(this HtmlHelper<TModel> html, string zoneName, Action action) where TModel : BaseViewModel {
            //TODO: again, IoC could move this AddAction (or similar) method out of the data-bearing object
            html.ViewData.Model.Zones.AddAction(zoneName, x => action());
            html.Zone(zoneName, string.Empty);
        }

        public static void ZoneBody<TModel>(this HtmlHelper<TModel> html, string zoneName) where TModel : BaseViewModel {
            html.Zone(zoneName, () => html.RenderBody());
        }

        public static void RegisterMeta(this HtmlHelper html, string name, string content) {
            html.Resolve<IResourceManager>().RegisterMeta(name, content);
        }

        public static void RegisterStyle(this HtmlHelper html, string fileName) {
            html.Resolve<IResourceManager>().RegisterStyle(fileName, html);
        }

        public static void RegisterScript(this HtmlHelper html, string fileName) {
            html.Resolve<IResourceManager>().RegisterHeadScript(fileName, html);
        }

        public static void RegisterFootScript(this HtmlHelper html, string fileName) {
            html.Resolve<IResourceManager>().RegisterFootScript(fileName, html);
        }


        public static IDisposable Capture(this ViewUserControl control, string name) {
            var writer = LayoutViewContext.From(control.ViewContext).GetNamedContent(name);
            return new HtmlTextWriterScope(control.Writer, writer);
        }

        class HtmlTextWriterScope : IDisposable {
            private readonly HtmlTextWriter _context;
            private readonly TextWriter _writer;

            public HtmlTextWriterScope(HtmlTextWriter context, TextWriter writer) {
                _context = context;
                _writer = _context.InnerWriter;
                _context.InnerWriter = writer;
            }

            public void Dispose() {
                _context.InnerWriter = _writer;
            }
        }

    }
}