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
    
    public partial class t_EmployeeCertificate
    {
        public int Id { get; set; }
        public int EmpId { get; set; }
        public Nullable<System.DateTime> AcquiredDate { get; set; }
        public string CertificateType { get; set; }
        public string Name { get; set; }
        public string Level { get; set; }
        public string AcquiredWay { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        public string EvaluateCompany { get; set; }
        public string CertificateNum { get; set; }
        public string AcquiredCompany { get; set; }
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
