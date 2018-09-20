using Core.Domain.Entities;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    [Authorize(Roles ="Administrator")]
    public class ConfigController : Controller
    {
        readonly IAccountService _service;
        public ConfigController(IAccountService configService)
        {
            _service = configService;
        }
        // GET: Config
        public ActionResult Index()
        {
           
            return View( );
        }
    }
}