using Lazeez.Model.Infrastructure;

namespace Lazeez.Model.DBModel
{
  public  class UserDeviceToken : BaseEntity
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string DeviceToken { get; set; }
    }
}
