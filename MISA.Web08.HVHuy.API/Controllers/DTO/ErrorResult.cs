using MISA.Web08.HVHuy.API.Enums;

namespace MISA.Web08.HVHuy.API.Controllers.DTO
{
    public class ErrorResult
    {
        public AmisErrorCode ErrorCode { get; set; }
        public string DevMsg { get; set; }
        public string UserMsg { get; set; }
        public string MoreInfo { get; set; }
        public string TraceId { get; set; }

        public ErrorResult(AmisErrorCode errorCode, string devMsg, string userMsg, string moreInfo, string traceId)
        {
            ErrorCode = errorCode;
            DevMsg = devMsg;
            UserMsg = userMsg;
            MoreInfo = moreInfo;
            TraceId = traceId;
        }
    }
}