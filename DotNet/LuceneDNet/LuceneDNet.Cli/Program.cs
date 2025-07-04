using DemoCli;
using Lucene.Net.Util;
using LuceneDNet.Domain.SearchIndex;
using LuceneDNet.Domain.WebContent;
using LuceneDNet.Domain.WebMirror;
using System.Text.Json;


// var jsonString = "{\r\n        \"duns_number\": 60704780,\r\n        \"company_name\": \"Apple\",\r\n        \"generated_at\": \"2025-04-20T08:59:21.546060\",\r\n        \r\n        \"status\": \"SUCCESS\"\r\n    }";
var jsonString = "[    {\r\n        \"duns_number\": 60704780,\r\n        \"company_name\": \"Apple\",\r\n        \"generated_at\": \"2025-05-20T09:14:53.076707\",\r\n        \"articles\": [\r\n            {\r\n                \"title\": \"Apple's next-generation 'CarPlay Ultra' is finally here\",\r\n                \"summary\": \"Apple's next-generation CarPlay experience, called \\\"CarPlay Ultra,\\\" will start arriving in Aston Martin cars in the next few weeks, at least five months later than initially expected.\",\r\n                \"reasoning\": \"This development signifies Apple's expansion in the automotive tech sector, potentially impacting market positioning and industry trends.\",\r\n                \"url\": \"https://machash.com/appleinsider/389471/apples-next-generation-carplay-ultra-finally/\",\r\n                \"publisher\": \"Mac Hash News\",\r\n                \"published_at\": \"2025-05-15T12:36:00+00:00\"\r\n            },\r\n            {\r\n                \"title\": \"Apple readies feature that lets Vision Pro users scroll with their eyes\",\r\n                \"summary\": \"Apple is preparing to introduce a feature for Vision Pro that allows users to scroll using their eyes. The company is also set to unveil major upgrades to iOS, iPadOS, and macOS at an upcoming event.\",\r\n                \"reasoning\": \"This innovation in user interface technology could significantly impact Apple's competitive edge in the AR/VR market.\",\r\n                \"url\": \"https://www.businesstimes.com.sg/companies-markets/telcos-media-tech/apple-readies-feature-lets-vision-pro-users-scroll-their-eyes\",\r\n                \"publisher\": \"Business Times Singapore - Companies & Markets\",\r\n                \"published_at\": \"2025-05-14T23:05:10+00:00\"\r\n            },\r\n            {\r\n                \"title\": \"iPhone 18 Pro Models Again Rumored to Feature Under-Screen Face ID\",\r\n                \"summary\": \"Counterpoint Research vice president Ross Young reports that iPhone 18 Pro models are expected to feature under-screen Face ID technology, with only a small hole for the front camera remaining visible. This suggests a significant design change for Apple's flagship devices.\",\r\n                \"reasoning\": \"This potential design innovation could influence future smartphone trends and Apple's market positioning in the high-end segment.\",\r\n                \"url\": \"https://www.macrumors.com/2025/05/13/iphone-18-pro-under-screen-face-id-again-rumored/\",\r\n                \"publisher\": \"MacRumors - Mac News and Rumors Front Page\",\r\n                \"published_at\": \"2025-05-14T02:26:34.935911+00:00\"\r\n            }\r\n        ],\r\n        \"status\": \"SUCCESS\"\r\n    }]";


var article = JsonSerializer.Deserialize<IEnumerable<CompanyInsights>>(jsonString);

if (article.First().Status != CompanyInsightStatus.Success)
{
    Console.WriteLine("shit");
}
else
{
    Console.WriteLine("good");

}

var dataRoot = "C:\\webindex";
var webContentIndex = new WebContentIndex(Path.Combine(dataRoot, "contentindex"));
var searchIndexWriter = new SearchIndexWriter(Path.Combine(dataRoot, "searchindex"), LuceneVersion.LUCENE_48);

var webScanner = new WebScanner(webContentIndex, searchIndexWriter, new HttpClient());

string command = Console.ReadLine() ?? "exit";

while (command != "exit")
{
    if (command.StartsWith("index") || command.StartsWith("idx"))
    {
        var splitted = command.Split(' ');
        var entryUrl = splitted.Length > 1 ? splitted[1] : "https://www.uktights.com";

        await webScanner.ScanDomain(entryUrl);

        Console.WriteLine($"Done scanning {entryUrl}");
    }
    else if (command == "reindex" || command == "re")
    {
        await webScanner.ReIndex();
    }
    else
    {
        searchIndexWriter.OpenWrite();
        var indexReader = searchIndexWriter.GetReader();
        var searchIndexReader = new SearchIndexReader(indexReader);
        var res = searchIndexReader.Search(command!);

        foreach (var item in res)
        {
            Console.WriteLine(item.Get("imgAlt"));
            Console.WriteLine(item.Get("imgSrc"));
        }
    }

    command = Console.ReadLine() ?? "exit";
}


// Specify the compatibility version we want
// const LuceneVersion luceneVersion = LuceneVersion.LUCENE_48;

//Open the Directory using a Lucene Directory class
//string indexName = "example_index";
//string indexPath = Path.Combine(Environment.CurrentDirectory, indexName);

//using LuceneDirectory indexDir = FSDirectory.Open(indexPath);

////Create an analyzer to process the text 
//Analyzer standardAnalyzer = new StandardAnalyzer(luceneVersion);

////Create an index writer
//IndexWriterConfig indexConfig = new IndexWriterConfig(luceneVersion, standardAnalyzer);
//indexConfig.OpenMode = OpenMode.CREATE;                             // create/overwrite index
//IndexWriter writer = new IndexWriter(indexDir, indexConfig);

////Add three documents to the index
//Document doc = new Document();
//doc.Add(new TextField("title", "The Apache Software Foundation - The world's largest open source foundation.", Field.Store.YES));
//doc.Add(new StringField("domain", "www.apache.org/", Field.Store.YES));
//writer.AddDocument(doc);

//doc = new Document();
//doc.Add(new TextField("title", "Powerful open source search library for .NET", Field.Store.YES));
//doc.Add(new StringField("domain", "lucenenet.apache.org", Field.Store.YES));
//writer.AddDocument(doc);

//doc = new Document();
//doc.Add(new TextField("title", "Unique gifts made by small businesses in North Carolina.", Field.Store.YES));
//doc.Add(new StringField("domain", "www.giftoasis.com", Field.Store.YES));
//writer.AddDocument(doc);

////Flush and commit the index data to the directory
//writer.Commit();

//using DirectoryReader reader = writer.GetReader(applyAllDeletes: true);
//IndexSearcher searcher = new IndexSearcher(reader);

//Query query = new TermQuery(new Term("domain", "lucenenet.apache.org"));
//TopDocs topDocs = searcher.Search(query, n: 2);         //indicate we want the first 2 results

//int numMatchingDocs = topDocs.TotalHits;
//Document resultDoc = searcher.Doc(topDocs.ScoreDocs[0].Doc);  //read back first doc from results (ie 0 offset)
//string title = resultDoc.Get("title");

//Console.WriteLine($"Matching results: {topDocs.TotalHits}");
//Console.WriteLine($"Title of first result: {title}");
