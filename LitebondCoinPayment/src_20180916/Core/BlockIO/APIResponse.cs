using System.Collections.Generic;

namespace Core.BlockIO
{
    public class APIResponse
    {
        public string Status { get; set; }

        public bool IsSuccess
        {
            get
            {
                return Status == "success";
            }
        }

        public Dictionary<string, object> Data { get; set; }
    }
}