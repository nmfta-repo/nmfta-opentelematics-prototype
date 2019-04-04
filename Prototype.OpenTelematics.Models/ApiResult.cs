namespace Prototype.OpenTelematics.Models
{
    public class ApiResult
    {
        public ResultStatus Status { get; set; }
        public int ErrorCode { get; set; }
        public string Message { get; set; }
    }
}