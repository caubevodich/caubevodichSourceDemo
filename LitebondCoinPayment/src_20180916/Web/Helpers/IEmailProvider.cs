using System.Collections.Generic;

namespace WebUI.Helpers
{
    public interface IEmailProvider
    {
        bool Send(string bodyTemplate, Dictionary<string, string> paras);
    }
}