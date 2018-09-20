using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class MyMessageSimpleModel
    {
        public string ShortText { get; set; }
        public string UserName { get; set; }
        public string Avatar { get; set; }
       
        public string DateTime { get; set; }
    }
    public class MyMessageSimpleModelPaging
    {
        public int Total { get; set; }
        public List<MyMessageSimpleModel> MyMessageSimpleModels { get; set; }
    }
}