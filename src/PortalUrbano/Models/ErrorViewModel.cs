namespace portal_urbano.Models
{
    public class ErrorViewModel
    {
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
