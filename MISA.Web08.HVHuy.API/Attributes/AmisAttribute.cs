namespace MISA.Web08.HVHuy.API.Attributes
{
    /// <summary>
    /// Attribute dùng để xác định một property là khóa chính
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKeyAttribute : Attribute
    {

    }


    [AttributeUsage(AttributeTargets.Property)]
    public class IsNotNullOrEmptyAttribute : Attribute
    {
        #region Filed
        /// <summary>
        /// Lỗi trả về cho client
        /// </summary>
        public string ErrorMessage;

        #endregion

        #region Constructor

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="errorMessage"></param>
        public IsNotNullOrEmptyAttribute(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        #endregion
    }
}
