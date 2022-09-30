using MISA.Web08.HVHuy.API.Attributes;
using MISA.Web08.HVHuy.API.Enums;

namespace MISA.Web08.HVHuy.API.Entities
{
    /// <summary>
    /// Thông tin nhân viên
    /// </summary>
    public class Employee
    {
        [PrimaryKey]
        public Guid EmployeeID { get; set; }

        [IsNotNullOrEmptyAttribute("Mã nhân viên không được để trống")]
        public string EmployeeCode { get; set; }

        [IsNotNullOrEmptyAttribute("Tên nhân viên không được để trống")]
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public Guid DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public string IdentityNumber { get; set; }
        public DateTime IdentityDate { get; set; }
        public string IdentityPlace { get; set; }
        public string PositionName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string TelephoneNumber { get; set; }
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankName { get; set; }
        public string BankBranchName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
