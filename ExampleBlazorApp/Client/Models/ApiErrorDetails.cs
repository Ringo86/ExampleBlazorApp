namespace ExampleBlazorApp.Client.Models
{
    public class ApiErrorDetails
    {
        public Uri Type { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
        public string TraceId { get; set; }
        public Dictionary<string,IEnumerable<string>> Errors { get; set; }
    }
}
/*
 {
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "traceId": "00-504eb0712e31cb5d682d6294b72c71d7-0c32f81ff15afd4e-00",
  "errors": {
    "$": [
      "The input does not contain any JSON tokens. Expected the input to start with a valid JSON token, when isFinalBlock is true. Path: $ | LineNumber: 0 | BytePositionInLine: 0."
    ],
    "getRequest": [
      "The getRequest field is required."
    ]
  }
}
 */