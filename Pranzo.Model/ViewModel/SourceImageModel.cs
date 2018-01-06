using Lazeez.Helper;

namespace Lazeez.Model.ViewModel
{
    public class SourceImageModel
    {
        public int ID { get; set; }
        public int SourceID { get; set; }
        public string Url { get; set; }
        public string Caption { get; set; }
        public long Size { get; set; }
        public Enums.SourceType SourceType { get; set; }
    }
}