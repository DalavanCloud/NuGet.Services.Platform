﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using NuGet.Services.Http;
using NuGet.Services.ServiceModel;
using Owin;

namespace NuGet.Services.Test.Echo
{
    public class EchoService : NuGetHttpService
    {
        private static readonly PathString _path = new PathString("/echo");
        public override PathString BasePath
        {
            get { return _path; }
        }

        public EchoService(ServiceName name, ServiceHost host) : base(name, host) { }

        protected override async Task OnRun()
        {
            await Host.WhenShutdown();
        }

        public override IEnumerable<EventSource> GetEventSources()
        {
            yield return EchoServiceEventSource.Log;
        }

        protected override void Configure(IAppBuilder app)
        {
            app.Use(async (ctx, next) =>
            {
                var message = ctx.Request.Query.Get("message");
                if(String.IsNullOrEmpty(message))
                {
                    message = "Put something in the 'message' query string parameter and I'll repeat it!";
                }
                else
                {
                    EchoServiceEventSource.Log.Echoing(message);
                }
                ctx.Response.ContentType = "text/plain";
                await ctx.Response.WriteAsync(message);
            });
        }
    }

    [EventSource(Name = "Outercurve-NuGet-Services-Echo")]
    public class EchoServiceEventSource : EventSource
    {
        public static readonly EchoServiceEventSource Log = new EchoServiceEventSource();
        private EchoServiceEventSource() { }

        [Event(eventId: 1, Message="Echoing '{0}'")]
        public void Echoing(string message) { WriteEvent(1, message); }
    }
}
