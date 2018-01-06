﻿using System.Collections.Generic;

namespace Lazeez.Helper
{
    public sealed class JQueryDataTables<T> where T : class
    {
        public int iTotalRecords { get; set; }
        public int iTotalDisplayRecords { get; set; }
        public List<T> aaData { get; set; }
    }

    public abstract class JQueryDataTablesModel
    {
        /// <summary>
        /// Request sequence number sent by DataTable,
        /// same value must be returned in response
        /// </summary>       
        public string sEcho { get; set; }

        /// <summary>
        /// Text used for filtering
        /// </summary>
        public string sSearch { get; set; }

        /// <summary>
        /// Number of records that should be shown in table
        /// </summary>
        public int iDisplayLength { get; set; }

        /// <summary>
        /// First record that should be shown(used for paging)
        /// </summary>
        public int iDisplayStart { get; set; }

        /// <summary>
        /// Number of columns in table
        /// </summary>
        public int iColumns { get; set; }

        /// <summary>
        /// Number of columns that are used in sorting
        /// </summary>
        public int iSortingCols { get; set; }

        /// <summary>
        /// Comma separated list of column names
        /// </summary>
        public string sColumns { get; set; }

        /// <summary>
        /// Sort column
        /// </summary>
        public int iSortCol_0 { get; set; }

        /// <summary>
        /// Asc or Desc
        /// </summary>
        public string sSortDir_0 { get; set; }

        public string OrderBy
        {
            get
            {
                string col = sColumns.Split(',')[iSortCol_0];
                return (col + " " + sSortDir_0);
            }
        }
    }

    public abstract class JQueryDataTablesExtension
    {
        public int Checkbox { get; set; }
    }
}