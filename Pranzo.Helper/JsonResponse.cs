namespace Lazeez.Helper
{
    public class JsonResponse
    {
        public enum ResultStatus
        {
            Success = 1,
            Error = 2
        }

        public int ID { get; set; }
        public ResultStatus Status { get; set; }
        public string Message { get; set; }
        public string RedirectToUrl { get; set; }
    }
}