using Markdig;
using Markdig.Prism;

namespace L.Common;
public static class MDHelper
{
    private static readonly MarkdownPipeline MarkdownPipeline = new MarkdownPipelineBuilder()
        .UseAdvancedExtensions()
        .UsePrism()
        .Build();
    public static string ToHtml(string markdown)
    {
        return Markdown.ToHtml(markdown, MarkdownPipeline);
    }
}
