using System;

namespace Netnr.Login
{
    public class Required : Attribute
    {
        public string Message { get; set; }
    }
}
