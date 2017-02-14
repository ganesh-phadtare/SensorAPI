using SensorAPI.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SensorAPI.Controllers
{
    [RoutePrefix("view")]
    public class ViewController : ApiController
    {
        private FormRenderService _renderService;
        public ViewController() : this(new FormRenderService()) { }

        public ViewController(FormRenderService service)
        {
            _renderService = service;
        }
        [Route("test/{execType}"), HttpPost]
        public async Task StartUp([FromUri] int execType, [FromBody]Dictionary<string, string> paramSet)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            await _renderService.Fetch(execType, paramSet);

            watch.Stop();
            //_logger.Info(string.Format("Entire Call for {0} took {1}", form, watch.ElapsedMilliseconds));
            //return data;
        }
    }
}
