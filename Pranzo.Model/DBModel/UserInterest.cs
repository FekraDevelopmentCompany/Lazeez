using Lazeez.Model.Infrastructure;

namespace Lazeez.Model.DBModel
{
    public class UserInterest : BaseEntity
    {
        public int UserID { get; set; }
        public int FoodTypeID { get; set; }
    }
}
