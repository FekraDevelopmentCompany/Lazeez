using Lazeez.Model.Infrastructure;

namespace Lazeez.Model.DBModel
{
    public class Country : BaseEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
