namespace MISA.Web08.HVHuy.API.Enums
{
    /// <summary>
    /// Danh sách mã lỗi khi gọi API
    /// </summary>
    public enum AmisErrorCode
    {
        //Lỗi Exception
        Exception = 1,

        //lỗi trùng mã
        Duplicate = 2,

        //lỗi trống mã
        EmptyCode = 3,

        //Lỗi không thêm mới thành công
        InsertError = 4,

        //Lỗi không cập nhật thành công
        UpdateError = 5,

        //Lỗi xóa không thành công
        DeleteError = 6,

        //lỗi xóa toàn bộ không thành công
        DeleteAllEror = 7,

        // dữ liệu ko hợp lệ
        ValidateError = 8
    }
}
