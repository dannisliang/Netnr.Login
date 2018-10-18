namespace Netnr.Login
{
    /// <summary>
    /// Authorize授权返回
    /// </summary>
    public class MicroSoft_Authorize_ResultEntity
    {
        public string code { get; set; }

        /// <summary>
        /// 如果传递参数，会回传该参数。
        /// </summary>
        public string state { get; set; }
    }
}
