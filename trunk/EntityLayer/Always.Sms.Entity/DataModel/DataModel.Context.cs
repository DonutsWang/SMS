﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Database : DbContext
    {
        public Database()
            : base("name=Database")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<ct_Account> ct_Account { get; set; }
        public DbSet<ct_AccountRegion> ct_AccountRegion { get; set; }
        public DbSet<ct_ArchiveLog> ct_ArchiveLog { get; set; }
        public DbSet<ct_Log> ct_Log { get; set; }
        public DbSet<ct_Position> ct_Position { get; set; }
        public DbSet<ct_PositionControlLimit> ct_PositionControlLimit { get; set; }
        public DbSet<ct_PositionIndicators> ct_PositionIndicators { get; set; }
        public DbSet<ct_Region> ct_Region { get; set; }
        public DbSet<ct_SysAccountRole> ct_SysAccountRole { get; set; }
        public DbSet<ct_SysModule> ct_SysModule { get; set; }
        public DbSet<ct_SysRole> ct_SysRole { get; set; }
        public DbSet<ct_SysRoleModule> ct_SysRoleModule { get; set; }
        public DbSet<ct_SysRoleModuleAuthority> ct_SysRoleModuleAuthority { get; set; }
        public DbSet<ct_SystemCode> ct_SystemCode { get; set; }
        public DbSet<t_EmployeeAccount> t_EmployeeAccount { get; set; }
        public DbSet<t_EmployeeAction> t_EmployeeAction { get; set; }
        public DbSet<t_EmployeeCertificate> t_EmployeeCertificate { get; set; }
        public DbSet<t_EmployeeClient> t_EmployeeClient { get; set; }
        public DbSet<t_EmployeeContact> t_EmployeeContact { get; set; }
        public DbSet<t_EmployeeContract> t_EmployeeContract { get; set; }
        public DbSet<t_EmployeeEducation> t_EmployeeEducation { get; set; }
        public DbSet<t_EmployeeFieldChanges> t_EmployeeFieldChanges { get; set; }
        public DbSet<t_EmployeeFinancial> t_EmployeeFinancial { get; set; }
        public DbSet<t_EmployeeInf> t_EmployeeInf { get; set; }
        public DbSet<t_EmployeeJob> t_EmployeeJob { get; set; }
        public DbSet<t_EmployeeMaterial> t_EmployeeMaterial { get; set; }
        public DbSet<t_EmployeeSalaryArchive> t_EmployeeSalaryArchive { get; set; }
        public DbSet<t_EmployeeSalaryCalculation> t_EmployeeSalaryCalculation { get; set; }
        public DbSet<t_EmployeeSalaryStructure> t_EmployeeSalaryStructure { get; set; }
        public DbSet<t_EmployeeTransaction> t_EmployeeTransaction { get; set; }
        public DbSet<t_EmployeeVacation> t_EmployeeVacation { get; set; }
        public DbSet<t_EmployeeWelfare> t_EmployeeWelfare { get; set; }
        public DbSet<t_EmployeeWelfareAdjust> t_EmployeeWelfareAdjust { get; set; }
        public DbSet<t_EmployeeWork> t_EmployeeWork { get; set; }
        public DbSet<t_PersDetailArchive> t_PersDetailArchive { get; set; }
        public DbSet<t_PersDetailCalculation> t_PersDetailCalculation { get; set; }
        public DbSet<t_PersLeaveArchive> t_PersLeaveArchive { get; set; }
        public DbSet<t_PersLeaveCalculation> t_PersLeaveCalculation { get; set; }
        public DbSet<t_PersTotalArchive> t_PersTotalArchive { get; set; }
        public DbSet<t_PersTotalCalculation> t_PersTotalCalculation { get; set; }
        public DbSet<t_SalaryCityMin> t_SalaryCityMin { get; set; }
        public DbSet<t_SalaryKpiBonus> t_SalaryKpiBonus { get; set; }
        public DbSet<t_SalaryKpiBonusCG1> t_SalaryKpiBonusCG1 { get; set; }
        public DbSet<t_SalaryStucture> t_SalaryStucture { get; set; }
        public DbSet<t_StoresInf> t_StoresInf { get; set; }
        public DbSet<t_StoresLog> t_StoresLog { get; set; }
        public DbSet<t_WelfareInsurance> t_WelfareInsurance { get; set; }
    }
}
