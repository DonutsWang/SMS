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
    
    public partial class t_EmployeeAction
    {
        public int Id { get; set; }
        public Nullable<int> EmpId { get; set; }
        public string ActType { get; set; }
        public string ActName { get; set; }
        public string ActBefore { get; set; }
        public string ActAfter { get; set; }
        public Nullable<int> Sn { get; set; }
        public string Note { get; set; }
        public string IsDispose { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<int> CreateId { get; set; }
        public string Creator { get; set; }
        public Nullable<System.DateTime> LastEditDate { get; set; }
        public Nullable<int> LastEditId { get; set; }
        public Nullable<System.DateTime> AuditDate { get; set; }
        public Nullable<int> AuditId { get; set; }
    }
}
