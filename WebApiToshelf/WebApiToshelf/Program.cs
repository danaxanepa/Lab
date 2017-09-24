using System;
using System.Web.Http;
using Microsoft.Owin.Hosting;
using Owin;
using Topshelf;

namespace WebApiToshelf
{
    class Program
    {
        private class OwinConfiguration
        {
            public void Configuration(IAppBuilder app)
            {
                var config = new HttpConfiguration();
                config.Formatters.Remove(config.Formatters.XmlFormatter);
                config.Formatters.Add(config.Formatters.JsonFormatter);
                config.MapHttpAttributeRoutes();
                app.UseWebApi(config);
            }
        }

        private class WebAppWrapper 
        {
            private IDisposable app;
            public void Start(int port) => app = WebApp.Start<OwinConfiguration>($"http://localhost:{port}");
            public void Stop() => app.Dispose();
        }

        public static int Main()
        {
            var exitCode = Topshelf.HostFactory.Run(x =>
            {
                x.Service<WebAppWrapper>(s =>
                {
                    s.ConstructUsing(() => new WebAppWrapper());
                    s.WhenStarted(h => h.Start(7603));
                    s.WhenStopped(h => h.Stop());
                });
                x.RunAsLocalSystem();
            });
            return (int)exitCode;
        }
    }

    public class HelloWorldController : ApiController
    {
        [Route("")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok("Hello World");
        }

        [Route("Dump")]
        [HttpGet]
        public IHttpActionResult Dump()
        {
            return Ok(new Data() { A = "Hello", B = "World" });
        }

        public struct Data
        {
            public string A { get; set; }
            public string B { get; set; }
        }
    }
}
