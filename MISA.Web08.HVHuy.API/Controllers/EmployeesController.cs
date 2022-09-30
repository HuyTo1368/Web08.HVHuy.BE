using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web08.HVHuy.API.Attributes;
using MISA.Web08.HVHuy.API.Controllers.DTO;
using MISA.Web08.HVHuy.API.Entities;
using MISA.Web08.HVHuy.API.Enums;
using MISA.Web08.HVHuy.API.Properties;
using MySqlConnector;


namespace MISA.Web08.HVHuy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        /// <summary>
        /// Lấy thông tin một nhân viên bằng id
        /// </summary>
        /// <param name="employeeid"></param>
        /// <returns></returns>
        [HttpGet("{employeeid}")]
        public IActionResult GetEmployeeByID([FromRoute] Guid employeeID)
        {

            try
            {
                //Khởi tạo kết nối với MySQl
                string connectionString = "Server=localhost; Port = 3306; Database = misa.web08.hvhuy; User Id=root; Password = huyhuy123";
                var mysqlConnection = new MySqlConnection(connectionString);

                string storeProdureName = "Proc_employee_InformationEmployee";

                //CHuẩn bị tham số đầu vào cho câu lệnh MySQL
                var parameters = new DynamicParameters();
                parameters.Add("EmployeeID", employeeID);

                //Thực hiện gọi vào DB
                var numberOfAffectedRows = mysqlConnection.QueryFirstOrDefault(storeProdureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                return StatusCode(StatusCodes.Status200OK, numberOfAffectedRows);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                    AmisErrorCode.Exception,
                    Resource.DevMsg_ExceptionFailed,
                    Resource.UserMsg_ExceptionFailed,
                    Resource.MoreInfo_ExceptionFailed,
                    HttpContext.TraceIdentifier));
            }

        }

        /// <summary>
        /// API thêm mới một nhân viên
        /// </summary>
        [HttpPost]
        public IActionResult InsertEmployee([FromBody] Employee employee)
        {
            try
            {
                // Validate dữ liệu đầu vào
                var properties = typeof(Employee).GetProperties();
                var validateFailures = new List<string>();
                foreach (var property in properties)
                {
                    string propertyName = property.Name;  
                    var propertyValue = property.GetValue(employee);
                    var isNotNullOrEmptyAttribute = (IsNotNullOrEmptyAttribute?)Attribute.GetCustomAttribute(property, typeof(IsNotNullOrEmptyAttribute));
                    if (isNotNullOrEmptyAttribute != null && string.IsNullOrEmpty(propertyValue?.ToString()))
                    {
                        validateFailures.Add(isNotNullOrEmptyAttribute.ErrorMessage);
                    }

                    if (validateFailures.Count > 0)
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult(
                        AmisErrorCode.ValidateError,
                        Resource.DevMsg_ValidateFailed,
                        Resource.UserMsg_ValidateFailed,
                        Resource.MoreInfo_ValidateFailed,
                        HttpContext.TraceIdentifier));
                    }
                }

                string connectionString = "Server=localhost; Port = 3306; Database = misa.web08.hvhuy; User Id=root; Password = huyhuy123";
                var mysqlConnection = new MySqlConnection(connectionString);

                var storedProcedureName = "Proc_employee_InsertEmployee";

                var parameters = new DynamicParameters();
                var employeeID = Guid.NewGuid();

                parameters.Add("EmployeeID", employeeID);
                parameters.Add("EmployeeCode", employee.EmployeeCode);
                parameters.Add("FullName", employee.FullName);
                parameters.Add("DateOfBirth", employee.DateOfBirth);
                parameters.Add("Gender", employee.Gender);
                parameters.Add("DepartmentID", employee.DepartmentID);
                parameters.Add("DepartmentName", employee.DepartmentName);
                parameters.Add("IdentityNumber", employee.IdentityNumber);
                parameters.Add("IdentityDate", employee.IdentityDate);
                parameters.Add("IdentityPlace", employee.IdentityPlace);
                parameters.Add("PositionName", employee.PositionName);
                parameters.Add("Address", employee.Address);
                parameters.Add("PhoneNumber", employee.PhoneNumber);
                parameters.Add("TelephoneNumber", employee.TelephoneNumber);
                parameters.Add("Email", employee.Email);
                parameters.Add("BankAccountNumber", employee.BankAccountNumber);
                parameters.Add("BankName", employee.BankName);
                parameters.Add("BankBranchName", employee.BankBranchName);
                parameters.Add("CreatedBy", employee.CreatedBy);
                parameters.Add("CreatedDate", DateTime.Now);
                parameters.Add("ModifiedBy", employee.ModifiedDate);
                parameters.Add("ModifiedDate", DateTime.Now);

                var numberOfAffectedRows = mysqlConnection.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                if (numberOfAffectedRows > 0)
                {
                    return StatusCode(StatusCodes.Status201Created, employeeID);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    (
                        AmisErrorCode.InsertError,
                        Resource.DevMsg_InsertFailed,
                        Resource.UserMsg_InsertFailed,
                        Resource.MoreInfo_InsertFailed,
                        HttpContext.TraceIdentifier
                    ));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                   (
                       AmisErrorCode.Exception,
                       Resource.DevMsg_ExceptionFailed,
                       Resource.UserMsg_ExceptionFailed,
                       Resource.MoreInfo_ExceptionFailed,
                       HttpContext.TraceIdentifier
                   ));
            }
        }

        /// <summary>
        /// API sửa thông tin nhân viên
        /// </summary>
        [HttpPut("{employeeid}")]
        public IActionResult UpdateEmployee([FromRoute] Guid employeeID, [FromBody] Employee employee)
        {
            try
            {
                // Validate dữ liệu đầu vào
                var properties = typeof(Employee).GetProperties();
                var validateFailures = new List<string>();
                foreach (var property in properties)
                {
                    string propertyName = property.Name;
                    var propertyValue = property.GetValue(employee);
                    var isNotNullOrEmptyAttribute = (IsNotNullOrEmptyAttribute?)Attribute.GetCustomAttribute(property, typeof(IsNotNullOrEmptyAttribute));
                    if (isNotNullOrEmptyAttribute != null && string.IsNullOrEmpty(propertyValue?.ToString()))
                    {
                        validateFailures.Add(isNotNullOrEmptyAttribute.ErrorMessage);
                    }

                    if (validateFailures.Count > 0)
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult(
                        AmisErrorCode.ValidateError,
                        Resource.DevMsg_ValidateFailed,
                        Resource.UserMsg_ValidateFailed,
                        Resource.MoreInfo_ValidateFailed,
                        HttpContext.TraceIdentifier));
                    }
                }



                string connectionString = "Server=localhost; Port = 3306; Database = misa.web08.hvhuy; User Id=root; Password = huyhuy123";
                var mysqlConnection = new MySqlConnection(connectionString);

                var storedProcedureName = "Proc_employee_InsertEmployee";

                var parameters = new DynamicParameters();

                parameters.Add("EmployeeID", employeeID);
                parameters.Add("EmployeeCode", employee.EmployeeCode);
                parameters.Add("FullName", employee.FullName);
                parameters.Add("DateOfBirth", employee.DateOfBirth);
                parameters.Add("Gender", employee.Gender);
                parameters.Add("DepartmentID", employee.DepartmentID);
                parameters.Add("DepartmentName", employee.DepartmentName);
                parameters.Add("IdentityNumber", employee.IdentityNumber);
                parameters.Add("IdentityDate", employee.IdentityDate);
                parameters.Add("IdentityPlace", employee.IdentityPlace);
                parameters.Add("PositionName", employee.PositionName);
                parameters.Add("Address", employee.Address);
                parameters.Add("PhoneNumber", employee.PhoneNumber);
                parameters.Add("TelephoneNumber", employee.TelephoneNumber);
                parameters.Add("Email", employee.Email);
                parameters.Add("BankAccountNumber", employee.BankAccountNumber);
                parameters.Add("BankName", employee.BankName);
                parameters.Add("BankBranchName", employee.BankBranchName);
                parameters.Add("CreatedBy", employee.CreatedBy);
                parameters.Add("CreatedDate", DateTime.Now);
                parameters.Add("ModifiedBy", employee.ModifiedDate);
                parameters.Add("ModifiedDate", DateTime.Now);

                var numberOfAffectedRows = mysqlConnection.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                if (numberOfAffectedRows > 0)
                {
                    return StatusCode(StatusCodes.Status201Created, employeeID);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    (
                        AmisErrorCode.UpdateError,
                        Resource.DevMsg_UpdateFailed,
                        Resource.UserMsg_UpdateFailed,
                        Resource.MoreInfo_UpdateFailed,
                        HttpContext.TraceIdentifier
                    ));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                   (
                       AmisErrorCode.Exception,
                       Resource.DevMsg_ExceptionFailed,
                       Resource.UserMsg_ExceptionFailed,
                       Resource.MoreInfo_ExceptionFailed,
                       HttpContext.TraceIdentifier
                   ));
            }
        }

        /// <summary>
        /// Xóa thông tin một nhân viên
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult DeleteEmployee([FromRoute] Guid employeeID)
        {
            try
            {
                string connectionString = "Server=localhost; Port = 3306; Database = misa.web08.hvhuy; User Id=root; Password = huyhuy123";
                var mysqlConnection = new MySqlConnection(connectionString);

                var storedProcedureName = "Proc_employee_DeleteEmployee";

                var parameters = new DynamicParameters();
                parameters.Add("EmployeeID", employeeID);

                var numberOfAffectedRows = mysqlConnection.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                if (numberOfAffectedRows > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, employeeID);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult(
                        AmisErrorCode.DeleteError,
                        Resource.DevMsg_DeleteFailed,
                        Resource.UserMsg_DeleteFailed,
                        Resource.MoreInfo_DeleteFailed,
                        HttpContext.TraceIdentifier));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                   (
                       AmisErrorCode.Exception,
                       Resource.DevMsg_ExceptionFailed,
                       Resource.UserMsg_ExceptionFailed,
                       Resource.MoreInfo_ExceptionFailed,
                       HttpContext.TraceIdentifier
                   ));
            }
        }
    }
}
