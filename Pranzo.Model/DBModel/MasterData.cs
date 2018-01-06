using Lazeez.Model.Infrastructure;
using System.ComponentModel.DataAnnotations;
using Lazeez.Resource.Shared;
using System.Web.Mvc;

namespace Lazeez.Model.DBModel
{
    public class MasterData : BaseEntity
    {
        public int ID { get; set; }
        
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "Val_RequiredField")]
        public string Name { get; set; }

        public string Description { get; set; }
        public string parentId { get; set; }
       
    }
}
