//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Always.Sms.Entity.DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class ct_SystemCode
    {
        public int Id { get; set; }
        public string CodeTypeText { get; set; }
        public string CodeType { get; set; }
        public string Code { get; set; }
        public string CodeText { get; set; }
        public string IsLock { get; set; }
        public Nullable<int> Sn { get; set; }
        public Nullable<int> ValueInt { get; set; }
        public string ValueString { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateId { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> LastEditDate { get; set; }
        public Nullable<int> LastEditId { get; set; }
    }
}
