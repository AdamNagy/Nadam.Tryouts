using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DemoCli;

public class CompanyInsights
{
    /// <summary>
    /// Gets or sets the DUNS number of the company.
    /// </summary>
    [JsonPropertyName("duns_number")]
    public int DunsNumber { get; set; }

    /// <summary>
    /// Gets or sets the name of the company.
    /// </summary>
    [JsonPropertyName("company_name")]
    public string? CompanyName { get; set; }

    [JsonPropertyName("articles")]
    public List<Article>? Articles { get; set; }

    /// <summary>
    /// Gets or sets the generated date and time of the insight.
    /// </summary>
    [JsonPropertyName("generated_at")]
    public DateTimeOffset? GeneratedAt { get; set; }

    /// <summary>
    /// Gets or sets the status of a article.
    /// </summary>
    public CompanyInsightStatus Status { get; set; }
}

/*
 *             {
                "title": "Apple's next-generation 'CarPlay Ultra' is finally here",
                "summary": "Apple's next-generation CarPlay experience, called \"CarPlay Ultra,\" will start arriving in Aston Martin cars in the next few weeks, at least five months later than initially expected.",
                "reasoning": "This development signifies Apple's expansion in the automotive tech sector, potentially impacting market positioning and industry trends.",
                "url": "https://machash.com/appleinsider/389471/apples-next-generation-carplay-ultra-finally/",
                "publisher": "Mac Hash News",
                "published_at": "2025-05-15T12:36:00+00:00"
            }
 */
public class Article
{
    /// <summary>
    /// Gets or sets the title of the article.
    /// </summary>
    [Required]
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the summary of the article.
    /// </summary>
    [JsonPropertyName("summary")]
    public string? Summary { get; set; }

    /// <summary>
    /// Gets or sets the reasoning behind the article.
    /// </summary>
    [JsonPropertyName("reasoning")]
    public string? Reasoning { get; set; }

    /// <summary>
    /// Gets or sets the URL of the article.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    /// <summary>
    /// Gets or sets the publisher of the article.
    /// </summary>
    [JsonPropertyName("publisher")]
    public string? Publisher { get; set; }

    /// <summary>
    /// Gets or sets the published date of the article.
    /// </summary>
    [JsonPropertyName("published_at")]
    public DateTimeOffset? PublishedAt { get; set; }
}

[JsonConverter(typeof(JsonStringEnumConverter<CompanyInsightStatus>))]
public enum CompanyInsightStatus
{
    /// <summary>
    /// Successful insights.
    /// </summary>
    Success,

    /// <summary>
    /// Not daily insight.
    /// </summary>
    [Display(Name = "NOTECENT")]
    NoRecent,

    /// <summary>
    /// Not relevant insight.
    /// </summary>
    [Display(Name = "NORELEVANT")]
    NoRelevant,

    /// <summary>
    /// Insight without data.
    /// </summary>
    [Display(Name = "NODATA")]
    NoData,

    /// <summary>
    /// Insight under processing.
    /// </summary>
    [Display(Name = "PROCESSING")]
    Processing,

    /// <summary>
    /// Unregistered companys insight.
    /// </summary>
    [Display(Name = "UNREGISTERED")]
    Unregistered,
}