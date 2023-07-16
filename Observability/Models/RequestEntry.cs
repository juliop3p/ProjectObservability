public class RequestEntry
{
    public string RequestType { get; set; }
    public string Path { get; set; }
    public string Headers { get; set; }
    public string Body { get; set; }
    public int? StatusCode { get; set; }
    public string Method { get; set; }
    public string? Query { get; set; }
    public bool IsSuccess { get; set; } = true;
    public long? TimeTaken { get; set; }
    public string? ContentType { get; set; }
}
