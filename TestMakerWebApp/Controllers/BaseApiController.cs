﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestMaker.Data.Context;

namespace TestMakerWebApp.Controllers
{
    [Route("api/[controller]")]
    public class BaseApiController : Controller
    {
        protected ApplicationDbContext DbContext { get; private set; }
        protected JsonSerializerSettings JsonSettings { get; private set; }
        

        public BaseApiController(ApplicationDbContext context)
        {
            DbContext = context;

            JsonSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
        }
    }
}
