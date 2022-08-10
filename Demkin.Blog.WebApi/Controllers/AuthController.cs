using Demkin.Blog.DTO;
using Demkin.Blog.DTO.Auth;
using Demkin.Blog.Entity;
using Demkin.Blog.IService;
using Demkin.Blog.Utils.ClassExtension;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Demkin.Blog.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IUserRoleRelationService _userRoleRelationService;

        public AuthController(ILogger<AuthController> logger, IUserRoleRelationService userRoleRelationService)
        {
            _logger = logger;
            _userRoleRelationService = userRoleRelationService;
        }

        /// <summary>
        /// 给用户
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResponse<string>> AddRoleToUser([FromBody] UserRoleRelationInsertDto entityDto)
        {
            var entity = new UserRoleRelation
            {
                UserId = entityDto.UserId,
                RoleId = entityDto.RoleId,
            };

            var result = await _userRoleRelationService.AddAsync(entity);
            if (result == null)
            {
                return ApiHelper.Failed(ApiErrorCode.Server_Error.GetDescription(), "添加失败");
            }
            return ApiHelper.Success();
        }
    }
}