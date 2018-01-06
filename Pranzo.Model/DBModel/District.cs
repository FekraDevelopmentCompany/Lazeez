using Lazeez.Model.Infrastructure;

namespace Lazeez.Model.DBModel
{
    public class District : BaseEntity
    {
        public int ID { get; set; }
        public int CityID { get; set; }
        public string Name { get; set; }
    }
}
