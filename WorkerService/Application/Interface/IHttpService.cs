﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WorkerService.Application.Interface
{
    public interface IHttpService
    {
        Task<HttpStatusCode> CheckStatusSite(string url);
    }
}
