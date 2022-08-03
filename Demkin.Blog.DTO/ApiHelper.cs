namespace Demkin.Blog.DTO
{
    public class ApiHelper
    {
        /// <summary>
        /// 成功返回
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static ApiResponse<T> Success<T>(T obj)
        {
            return new ApiResponse<T>()
            {
                Code = "00000",
                Msg = "success",
                Data = obj
            };
        }

        public static ApiResponse<string> Failed(string code, string errMsg)
        {
            return new ApiResponse<string>
            {
                Code = code,
                Msg = errMsg,
            };
        }

        public static ApiResponse<T> Failed<T>(string code, string errMsg, T obj)
        {
            return new ApiResponse<T>
            {
                Code = code,
                Msg = errMsg,
                Data = obj
            };
        }
    }
}