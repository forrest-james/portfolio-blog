using Markdig;
using Markdig.Renderers;
using Microsoft.AspNetCore.Html;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Application.Common.Services
{
    public class MarkdownService
    {
        public static MarkdownPipeline Pipeline;
        public static string TagBlackList = "script|iframe|object|embed|form";
        public static Action<MarkdownPipelineBuilder> ConfigurePipelineBuilder { get; set; }

        private readonly bool _usePragmaLines;
        private readonly bool _forceReload;
        private readonly Action<MarkdownPipelineBuilder> _configuration;

        public MarkdownService(bool usePragmaLines = false,
            bool forceReload = false,
            Action<MarkdownPipelineBuilder> configuration = null)
        {
            _usePragmaLines = usePragmaLines;

            if (forceReload || Pipeline == null)
            {
                if (configuration == null && ConfigurePipelineBuilder != null)
                    configuration = ConfigurePipelineBuilder;

                var builder = CreatePipelineBuilder(configuration);
                Pipeline = builder.Build();
            }
        }

        public static HtmlString ParseHtml(string markdown) => new HtmlString(Parse(markdown));

        public static string SanitizeHtml(string html)
        {
            if (string.IsNullOrEmpty(html))
                return html;

            return Regex.Replace(html, $@"(<({TagBlackList})\b[^<]*(?:(?!<\/({TagBlackList}))<[^<]*)*<\/({TagBlackList})>)",
                "",
                RegexOptions.IgnoreCase | RegexOptions.Multiline);
        }

        public static string Parse(string markdown)
        {
            if (string.IsNullOrEmpty(markdown))
                return string.Empty;

            string html;
            using(var htmlWriter = new StringWriter())
            {
                var renderer = CreateRenderer(htmlWriter);
                Markdown.Convert(markdown, renderer, Pipeline);
                html = SanitizeHtml(htmlWriter.ToString());
            }

            return html;
        }

        public virtual MarkdownPipelineBuilder CreatePipelineBuilder(Action<MarkdownPipelineBuilder> configuration)
        {
            MarkdownPipelineBuilder builder = null;
            builder = new MarkdownPipelineBuilder();

            if (configuration == null)
            {
                builder = builder
                    .UseEmphasisExtras()
                    .UsePipeTables()
                    .UseGridTables()
                    .UseFooters()
                    .UseFootnotes()
                    .UseCitations()
                    .UseAutoLinks()
                    .UseAbbreviations()
                    .UseMediaLinks()
                    .UseListExtras()
                    .UseTaskLists()
                    .UseGenericAttributes();
            }
            else
                configuration.Invoke(builder);

            if (_usePragmaLines)
                builder = builder.UsePragmaLines();

            return builder;
        }

        protected static IMarkdownRenderer CreateRenderer(TextWriter writer) => new HtmlRenderer(writer);
    }    
}