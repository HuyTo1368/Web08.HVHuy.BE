﻿namespace MISA.Web08.HVHuy.API.Entities
{
    public class Department
    {
        public Guid DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
