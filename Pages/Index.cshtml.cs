using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Day3.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public string schema;
        public string body;
        public string queryString;
        public string path;
        public string host;
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            if (HttpContext.Items.TryGetValue("Schema", out var requestSchema))
            {
                schema = (string)requestSchema;
            }if (HttpContext.Items.TryGetValue("Body", out var requestBody))
            {
                body = (string)requestBody;
            }if (HttpContext.Items.TryGetValue("QueryString", out var requestQueryString))
            {
                queryString = (string)requestQueryString;
            }if (HttpContext.Items.TryGetValue("Path", out var requestPath))
            {
                path = (string)requestPath;
            }if (HttpContext.Items.TryGetValue("Host", out var requestHost))
            {
                host = (string)requestHost;
            }

            using (StreamWriter writetext = new StreamWriter("siu.txt"))
            {
                writetext.WriteLine($"Path: {path}");
                writetext.WriteLine($"Schema: {schema}");
                writetext.WriteLine($"Body: {body}");
                writetext.WriteLine($"Host: {host}");
                writetext.WriteLine($"QueryString: {queryString}");
            }
        }

    }
}
