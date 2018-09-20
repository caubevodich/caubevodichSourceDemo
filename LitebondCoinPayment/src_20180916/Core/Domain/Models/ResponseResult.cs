using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Core.Domain.Models
{
    public class ResponseResult
    {
        [JsonProperty("data")]
        public dynamic Data { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("success")]
        public bool IsSuccess
        {
            get
            {
                return !Errors.Any();
            }
        }

        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonIgnore]
        public ICollection<string> Errors { get; set; } = new List<string>();
    }
}
