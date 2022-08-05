namespace Demkin.Blog.DTO.Auth
{
    public class TokenDetailDto
    {
        /// <summary>
        /// token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 有效期至
        /// </summary>
        public string ExpirationDate { get; set; }

        /// <summary>
        /// token类型
        /// </summary>
        public string TokenType { get; set; }
    }
}