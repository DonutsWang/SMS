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
    
    public partial class t_EmployeeWork
    {
        public int Id { get; set; }
        public int EmpId { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public string Position { get; set; }
        public Nullable<int> ReplaceEmp { get; set; }
        public string IsInstead { get; set; }
        public string InsteadPosition { get; set; }
        public string BelongingCompany { get; set; }
        public string TaxCompany { get; set; }
        public string Department { get; set; }
        public string JobLevel { get; set; }
        public string PerformProject { get; set; }
        public Nullable<int> PP { get; set; }
        public string SchedulingType { get; set; }
        public string ASName { get; set; }
        public string AEName { get; set; }
        public string RAMName { get; set; }
        public string IsClearedAccount { get; set; }
        public Nullable<System.DateTime> LeaveDate { get; set; }
        public string LeaveReason { get; set; }
        public string LeaveReasonNote { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateId { get; set; }
        public Nullable<System.DateTime> LastEditDate { get; set; }
        public Nullable<int> LastEditId { get; set; }
        public Nullable<System.DateTime> AuditDate { get; set; }
        public Nullable<int> AuditId { get; set; }
    }
}