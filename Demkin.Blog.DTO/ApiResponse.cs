namespace Demkin.Blog.DTO
{
    public class ApiResponse<T>
    {
        /// <summary>
        /// 返回的状态码
        /// </summary>
        public string Code { get; set; } = "00000";

        /// <summary>
        /// 返回信息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public T Data { get; set; }
    }
}