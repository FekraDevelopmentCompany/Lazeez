namespace Lazeez.Helper
{
    public class MessageDialog
    {
        public class ButtonType
        {
            public const string Info = "btn-primary";
            public const string Warning = "btn-warning";
            public const string Error = "btn-danger";
        }

        public string Title { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
    }
}