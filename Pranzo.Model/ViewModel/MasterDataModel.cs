using System;
using Lazeez.Helper;
using Lazeez.Model.DBModel;
using Lazeez.Resource.Shared;
using System.Web.Mvc;

namespace Lazeez.Model.ViewModel
{
    public class MasterDataModel
    {
        public string Header { get; set; }
        
        public string TableName { get; set; }
        public bool DisplayDescription { get; set; }
        public bool DisplayParent { get; set; }
        public string parentTitle { get; set; }
        public int MasterDataId { get; set; }

        private MasterData _masterDataVm;
        public MasterData MasterDataVm
        {
            get { return _masterDataVm; }
            set
            {
                _masterDataVm = value;
                Header = _masterDataVm.ID > 0 ? Resources.Res_Edit : Resources.Res_Add;
            }
        }

        public SelectList ListParent { get; set; }
    }

    public class MasterDataSearch
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
       
    }

    public class MasterDataSearchParams : JQueryDataTablesModel
    {
        public int MasterDataId { get; set; }
        public string Name { get; set; }
    }
}