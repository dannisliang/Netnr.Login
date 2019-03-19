using Newtonsoft.Json.Linq;

namespace Netnr.Login
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class Gitee_User_ResultEntity
    {
        public int id { get; set; }
        public string login { get; set; }
        public string name { get; set; }
        public string avatar_url { get; set; }
        public string url { get; set; }
        public string html_url { get; set; }
        public string followers_url { get; set; }
        public string following_url { get; set; }
        public string gists_url { get; set; }
        public string starred_url { get; set; }
        public string subscriptions_url { get; set; }
        public string organizations_url { get; set; }
        public string repos_url { get; set; }
        public string events_url { get; set; }
        public string received_events_url { get; set; }
        public string type { get; set; }
        public bool site_admin { get; set; }
        public string blog { get; set; }
        public string weibo { get; set; }
        public string bio { get; set; }
        public int public_repos { get; set; }
        public int public_gists { get; set; }
        public int followers { get; set; }
        public int following { get; set; }
        public int stared { get; set; }
        public int watched { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string private_token { get; set; }
        public int total_repos { get; set; }
        public int owned_repos { get; set; }
        public int total_private_repos { get; set; }
        public int owned_private_repos { get; set; }
        public int private_gists { get; set; }
        public JObject address { get; set; }
    }
}