using System.ComponentModel;

namespace Demkin.Blog.DTO
{
    /// <summary>
    /// Ali错误码规范,五位组成
    /// 1. 1开头代表用户端错误
    /// 2. 2开头代表当前系统异常
    /// 3. 3开头代表第三方服务接口异常
    /// 4. 若无法确定具体异常，选择宏观错误码
    /// </summary>
    public enum ApiErrorCode
    {
        #region 成功执行

        /// <summary>
        /// 成功
        /// </summary>
        [Description("00000")]
        Success,

        #endregion 成功执行

        #region 客户端错误

        /// <summary>
        /// 客户端一级宏观错误
        /// </summary>
        [Description("A0001")]
        Client_Error,

        #region 注册

        /// <summary>
        /// 注册二级宏观错误
        /// </summary>
        [Description("A0100")]
        Client_Register,

        /// <summary>
        /// 用户未同意隐私协议
        /// </summary>
        [Description("A0101")]
        Client_Register_NoAgreement,

        /// <summary>
        /// 注册受到限制
        /// </summary>
        [Description("A0102")]
        Client_Register_Limited,

        /// <summary>
        /// 用户名校验失败
        /// </summary>
        [Description("A0110")]
        Client_Register_AccountVerifyFailed,

        /// <summary>
        /// 用户名已存在
        /// </summary>
        [Description("A0111")]
        Client_Register_AccountHasExist,

        /// <summary>
        /// 用户名包含敏感词
        /// </summary>
        [Description("A0112")]
        Client_Register_AccountSensitiveWord,

        /// <summary>
        /// 用户名包含特殊字符
        /// </summary>
        [Description("A0113")]
        Client_Register_AccountSpecificSymbol,

        /// <summary>
        /// 密码校验失败
        /// </summary>
        [Description("A0120")]
        Client_Register_PwdVerifyFailed,

        /// <summary>
        /// 密码长度不够
        /// </summary>
        [Description("A0121")]
        Client_Register_PwdLengthShort,

        /// <summary>
        /// 密码强度不够
        /// </summary>
        [Description("A0122")]
        Client_Register_PwdStrongShort,

        /// <summary>
        /// 校验码输入错误
        /// </summary>
        [Description("A0130")]
        Client_Register_VerifyCodeErr,

        /// <summary>
        /// 短信校验码输入错误
        /// </summary>
        [Description("A0131")]
        Client_Register_MsgVerifyCodeErr,

        /// <summary>
        /// 邮件校验码输入错误
        /// </summary>
        [Description("A0132")]
        Client_Register_EmailVerifyCodeErr,

        /// <summary>
        /// 用户证件异常
        /// </summary>
        [Description("A0140")]
        Client_Register_Certificate,

        /// <summary>
        /// 用户基本信息校验失败
        /// </summary>
        [Description("A0150")]
        Client_Register_UserInfo,

        /// <summary>
        /// 手机格式校验失败
        /// </summary>
        [Description("A0151")]
        Client_Register_UserPhone,

        #endregion 注册

        #region 登录

        /// <summary>
        /// 登录二级宏观错误码
        /// </summary>
        [Description("A0200")]
        Client_Login,

        /// <summary>
        /// 用户账户不存在
        /// </summary>
        [Description("A0201")]
        Client_Login_AccountNotExist,

        /// <summary>
        /// 账户不存在
        /// </summary>
        [Description("A0202")]
        Client_Login_AccountFreeze,

        /// <summary>
        /// 密码错误
        /// </summary>
        [Description("A0210")]
        Client_Login_PwdErr,

        /// <summary>
        /// 密码错误超过限制
        /// </summary>
        [Description("A0211")]
        Client_Login_PwdCountLimit,

        /// <summary>
        /// 身份验证失败
        /// </summary>
        [Description("A0220")]
        Client_Login_IdentityVerifyFailed,

        /// <summary>
        /// 登录已过期
        /// </summary>
        [Description("A0230")]
        Client_Login_TokenHasExpired,

        /// <summary>
        /// 登录验证码错误
        /// </summary>
        [Description("A0240")]
        Client_Login_VerifyCodeErr,

        #endregion 登录

        #region 权限

        /// <summary>
        /// 访问权限二级宏观错误码
        /// </summary>
        [Description("A0300")]
        Client_Auth,

        /// <summary>
        /// 访问未授权
        /// </summary>
        [Description("A0301")]
        Client_Auth_UnAuthorized,

        /// <summary>
        /// 因访问对象隐私设置被拦截
        /// </summary>
        [Description("A0310")]
        Client_Auth_IsLimited,

        /// <summary>
        /// 授权已过期
        /// </summary>
        [Description("A0311")]
        Client_Auth_Expired,

        /// <summary>
        /// 用户账户不存在
        /// </summary>
        [Description("A0312")]
        Client_Auth_NoPermission,

        #endregion 权限

        #region 用户请求参数

        /// <summary>
        /// 请求参数二级宏观错误码
        /// </summary>
        [Description("A0400")]
        Client_RequestParam,

        /// <summary>
        /// 请求必填参数为空
        /// </summary>
        [Description("A0410")]
        Client_RequestParam_Null,

        /// <summary>
        /// 请求参数值超出允许的范围
        /// </summary>
        [Description("A0420")]
        Client_RequestParam_OutOfRange,

        /// <summary>
        /// 参数格式不匹配
        /// </summary>
        [Description("A0421")]
        Client_RequestParam_FormatNotMatch,

        /// <summary>
        /// 用户输入内容非法
        /// </summary>
        [Description("A0430")]
        Client_RequestParam_NotFit,

        /// <summary>
        /// 操作异常
        /// </summary>
        [Description("A0440")]
        Client_RequestParam_OperateErr,

        /// <summary>
        /// 支付超时
        /// </summary>
        [Description("A0441")]
        Client_RequestParam_PayTimeout,

        #endregion 用户请求参数

        #region 请求服务

        /// <summary>
        /// 请求服务二级宏观错误码
        /// </summary>
        [Description("A0500")]
        Client_RequestServer,

        /// <summary>
        /// 请求次数超出限制
        /// </summary>
        [Description("A0501")]
        Client_RequestServer_CountLimit,

        #endregion 请求服务

        #region 资源异常

        /// <summary>
        /// 资源异常二级宏观错误码
        /// </summary>
        [Description("A0600")]
        Client_Resource,

        /// <summary>
        /// 账户余额不足
        /// </summary>
        [Description("A0601")]
        Client_Resource_BalanceNotEnough,

        #endregion 资源异常

        #region 上传文件

        /// <summary>
        /// 上传文件二级宏观错误码
        /// </summary>
        [Description("A0700")]
        Client_UploadFile,

        /// <summary>
        /// 上传文件类型不匹配
        /// </summary>
        [Description("A0701")]
        Client_UploadFile_TypeNotMatch,

        /// <summary>
        /// 上传文件太大
        /// </summary>
        [Description("A0702")]
        Client_UploadFile_FileSizeTooLarge,

        #endregion 上传文件

        #region 当前版本

        /// <summary>
        /// 当前版本异常二级宏观错误码
        /// </summary>
        [Description("A0800")]
        Client_Version,

        #endregion 当前版本

        #region 隐私未授权

        /// <summary>
        /// 隐私未授权二级宏观错误码
        /// </summary>
        [Description("A0900")]
        Client_Privacy,

        /// <summary>
        /// 隐私未签署
        /// </summary>
        [Description("A0901")]
        Client_Privacy_NotSigned,

        /// <summary>
        /// 摄像头未授权
        /// </summary>
        [Description("A0902")]
        Client_Privacy_Camera,

        #endregion 隐私未授权

        #region 设备异常

        /// <summary>
        /// 设备异常二级宏观错误码
        /// </summary>
        [Description("A1000")]
        Client_Equipment,

        /// <summary>
        /// 相机异常
        /// </summary>
        [Description("A1001")]
        Client_Equipment_Camera,

        #endregion 设备异常

        #endregion 客户端错误

        #region 服务端错误

        /// <summary>
        /// 系统执行出错，一级宏观错误
        /// </summary>
        [Description("B0001")]
        Server_Error,

        #region 执行超时

        /// <summary>
        /// 执行超时，二级宏观错误
        /// </summary>
        [Description("B0100")]
        Server_ExecuteTimeout,

        #endregion 执行超时

        #region 容灾功能

        /// <summary>
        /// 容灾功能被触发，二级宏观错误
        /// </summary>
        [Description("B0200")]
        Server_DisasterTrigger,

        /// <summary>
        /// 系统限流
        /// </summary>
        [Description("B0210")]
        Server_DisasterTrigger_CurrentLimit,

        #endregion 容灾功能

        #region 资源

        /// <summary>
        /// 资源异常，二级宏观错误
        /// </summary>
        [Description("B0300")]
        Server_Resource,

        /// <summary>
        /// 资源耗尽
        /// </summary>
        [Description("B0310")]
        Server_Resource_Deplete,

        #endregion 资源

        #endregion 服务端错误

        #region 第三方错误

        /// <summary>
        /// 第三方出错
        /// </summary>
        [Description("C0001")]
        ThirdParty_Error,

        #region 中间件服务

        /// <summary>
        /// 中间件,一级宏观错误
        /// </summary>
        [Description("C0100")]
        ThirdParty_Middleware,

        #endregion 中间件服务

        #region RPC服务

        /// <summary>
        /// RPC服务,二级宏观错误
        /// </summary>
        [Description("C0110")]
        ThirdParty_RPC,

        /// <summary>
        /// RPC服务未找到
        /// </summary>
        [Description("C0111")]
        ThirdParty_RPC_NotFound,

        #endregion RPC服务

        #region 第三方执行超时

        /// <summary>
        /// 第三方系统执行超时,二级宏观错误
        /// </summary>
        [Description("C0200")]
        ThirdParty_ExecuteTimeout,

        /// <summary>
        /// RPC 执行超时
        /// </summary>
        [Description("C0210")]
        ThirdParty_ExecuteTimeout_RPC,

        #endregion 第三方执行超时

        #region 数据库服务

        /// <summary>
        /// 数据库服务出错,二级宏观错误
        /// </summary>
        [Description("C0300")]
        ThirdParty_DataBase,

        /// <summary>
        /// 表不存在
        /// </summary>
        [Description("C0311")]
        ThirdParty_DataBase_TableNotExist,

        /// <summary>
        /// 列不存在
        /// </summary>
        [Description("C0312")]
        ThirdParty_DataBase_ColumnNotExist,

        #endregion 数据库服务

        #endregion 第三方错误
    }

    public class ApiErrorCodeDto
    {
        public string ErrorCode { get; set; }

        public string ErrorDescription { get; set; }
    }
}