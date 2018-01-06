using Lazeez.Model.Infrastructure;

namespace Lazeez.Model.DBModel
{
    public class SourceImage : BaseEntity
    {
        public int ID { get; set; }
        public int SourceID { get; set; }
        public short SourceTypeID { get; set; }
        public string ImagePath { get; set; }
    }
}
