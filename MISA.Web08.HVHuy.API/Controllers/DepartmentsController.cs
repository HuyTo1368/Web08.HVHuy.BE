using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web08.HVHuy.API.Entities;
using MySqlConnector;

namespace MISA.Web08.HVHuy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        /// <summary>
        /// Lấy tất cả danh sách phòng ban
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public IActionResult GetAllDepartment()
        {
            
            try
            {
                string connectionString = "Server=localhost; Port = 3306; Database = misa.web08.hvhuy; User Id=root; Password = huyhuy123";
                var mysqlConnection = new MySqlConnection(connectionString);

                // Chuẩn bị câu lênh SQL
                string storedProcedureName = "Proc_department_GetDepartment";

                var departements = mysqlConnection.Query(storedProcedureName, commandType: System.Data.CommandType.StoredProcedure);

                return StatusCode(StatusCodes.Status200OK, departements);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "e001");
            }
        }
    }
}
