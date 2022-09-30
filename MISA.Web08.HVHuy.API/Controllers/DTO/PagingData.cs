using MISA.Web08.HVHuy.API.Entities;

namespace MISA.Web08.HVHuy.API.Controllers.DTO
{
    public class PagingData
    {
        /// <summary>
        /// Danh sách nhân viên
        /// </summary>
        public List<Employee> Data { get; set; }

        /// <summary>
        /// Tổng số bản ghi thỏa mãn điều kiện
        /// </summary>
        public int TotalCount { get; set; }
    }
}
