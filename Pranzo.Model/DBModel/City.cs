using Lazeez.Model.Infrastructure;

namespace Lazeez.Model.DBModel
{
    public class City : BaseEntity
    {
        public int ID { get; set; }
        public int CountryID { get; set; }
        public string Name { get; set; }
    }
}
