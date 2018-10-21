# Netnr.Login

----------
## 集成三方登录

- QQ
- 微博
- GitHub
- 淘宝（天猫）
- Microsoft

----------
> 如果该项目对你有帮助，请你为项目Star，谢谢，这是对我精神上的支持，也是能一直坚持下去的动力。

----------
## 安装 (NuGet)
```
Install-Package Netnr.Login
```
> 修改配置信息（密钥、回调等）

----------
## 框架
`.NETStandard 2.0`

`.NETFramework 4.0`

----------
## Code
```csharp
/// <summary>
/// 配置
/// </summary>
private void LoginConfig()
{
    QQConfig.APPID = "101511xxx";
    QQConfig.APPKey = "f26b4af4a9d68bec2bbcbeee443fexxx";
    //回调地址，与申请填写的地址保持一致
    QQConfig.Redirect_Uri = "https://rf2.netnr.com/account/authcallback/qq";

    WeiboConfig.AppKey = "";
    WeiboConfig.AppSecret = "";
    WeiboConfig.Redirect_Uri = "";

    GitHubConfig.ClientID = "";
    GitHubConfig.ClientSecret = "";
    GitHubConfig.Redirect_Uri = "https://rf2.netnr.com/account/authcallback/github";
    //申请的应用名称，非常重要
    GitHubConfig.ApplicationName = "netnrf";

    TaobaoConfig.AppKey = "";
    TaobaoConfig.AppSecret = "";
    TaobaoConfig.Redirect_Uri = "";

    MicroSoftConfig.ClientID = "";
    MicroSoftConfig.ClientSecret = "";
    MicroSoftConfig.Redirect_Uri = "";
}

/// <summary>
/// 授权登录跳转
/// 访问地址：/{controller}/{action}/{id}，如：/account/auth/qq
/// </summary>
/// <returns></returns>
public IActionResult Auth()
{
    //调用配置
    LoginConfig();

    string url = "/account/login";
    string vtype = RouteData.Values["id"]?.ToString().ToLower();
    switch (vtype)
    {
        case "qq":
            url = QQ.AuthorizationHref(new QQ_Authorization_RequestEntity());
            break;
        case "weibo":
            url = Weibo.AuthorizeHref(new Weibo_Authorize_RequestEntity());
            break;
        case "github":
            url = GitHub.AuthorizeHref(new GitHub_Authorize_RequestEntity());
            break;
        case "taobao":
            url = Taobao.AuthorizeHref(new Taobao_Authorize_RequestEntity());
            break;
        case "microsoft":
            url = MicroSoft.AuthorizeHref(new MicroSoft_Authorize_RequestEntity());
            break;
    }
    return Redirect(url);
}

/// <summary>
/// 授权登录回调
/// 访问地址：/{controller}/{action}/{id}，如：/account/authcallback/qq
/// </summary>
/// <param name="code">自动接收授权登录后回调参数code</param>
/// <returns></returns>
public IActionResult AuthCallback(string code)
{
    //调用配置
    LoginConfig();

    //唯一标示
    string openId = string.Empty;
    //登录类型
    string vtype = RouteData.Values["id"]?.ToString().ToLower();
    try
    {
        switch (vtype)
        {
            case "qq":
                {
                    //获取 access_token
                    var accessToken_ResultEntity = QQ.AccessToken(new QQ_AccessToken_RequestEntity()
                    {
                        code = code
                    });

                    //获取 OpendId
                    var openId_ResultEntity = QQ.OpenId(new QQ_OpenId_RequestEntity()
                    {
                        access_token = accessToken_ResultEntity.access_token
                    });

                    //获取 UserInfo
                    var openId_Get_User_Info_ResultEntity = QQ.OpenId_Get_User_Info(new QQ_OpenAPI_RequestEntity()
                    {
                        access_token = accessToken_ResultEntity.access_token,
                        openid = openId_ResultEntity.openid
                    });

                    //身份唯一标识
                    openId = openId_ResultEntity.openid;
                }
                break;
            case "weibo":
                {
                    //获取 access_token
                    var accessToken_ResultEntity = Weibo.AccessToken(new Weibo_AccessToken_RequestEntity()
                    {
                        code = code
                    });

                    //获取 access_token 的授权信息
                    var tokenInfo_ResultEntity = Weibo.GetTokenInfo(new Weibo_GetTokenInfo_RequestEntity()
                    {
                        access_token = accessToken_ResultEntity.access_token
                    });

                    //获取 users/show
                    var userShow_ResultEntity = Weibo.UserShow(new Weibo_UserShow_RequestEntity()
                    {
                        access_token = accessToken_ResultEntity.access_token,
                        uid = Convert.ToInt64(tokenInfo_ResultEntity.uid)
                    });

                    openId = accessToken_ResultEntity.access_token;
                }
                break;
            case "github":
                {
                    //申请的应用名称，非常重要
                    GitHubConfig.ApplicationName = "netnrf";

                    //获取 access_token
                    var accessToken_ResultEntity = GitHub.AccessToken(new GitHub_AccessToken_RequestEntity()
                    {
                        code = code
                    });

                    //获取 user
                    var user_ResultEntity = GitHub.User(new GitHub_User_RequestEntity()
                    {
                        access_token = accessToken_ResultEntity.access_token
                    });

                    openId = user_ResultEntity.id.ToString();
                }
                break;
            case "taobao":
                {
                    //获取 access_token
                    var accessToken_ResultEntity = Taobao.AccessToken(new Taobao_AccessToken_RequestEntity()
                    {
                        code = code
                    });

                    openId = accessToken_ResultEntity.open_uid;
                }
                break;
            case "microsoft":
                {
                    //获取 access_token
                    var accessToken_ResultEntity = MicroSoft.AccessToken(new MicroSoft_AccessToken_RequestEntity()
                    {
                        code = code
                    });

                    //获取 user
                    var user_ResultEntity = MicroSoft.User(new MicroSoft_User_RequestEntity()
                    {
                        access_token = accessToken_ResultEntity.access_token
                    });

                    openId = user_ResultEntity.id.ToString();
                }
                break;
        }
    }
    catch (Exception ex)
    {
        throw new Exception(ex.Message);
    }

    //拿到登录标识
    if (string.IsNullOrWhiteSpace(openId))
    {
        //TO DO
    }

    return View();
}
```