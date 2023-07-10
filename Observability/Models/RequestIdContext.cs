public static class RequestIdContext
{
    private static readonly AsyncLocal<string> requestId = new AsyncLocal<string>();

    public static string Current
    {
        get => requestId.Value;
        set => requestId.Value = value;
    }
}
