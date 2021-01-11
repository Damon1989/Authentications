using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Ocelot.Middleware;
using Ocelot.Multiplexer;

namespace OcelotDemo.Aggregator
{
    public class LeaderInfoAdvancedAggregator:IDefinedAggregator
    {
        public async Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
        {
            var results=new List<string>();

            var contentBuilder=new StringBuilder();
            contentBuilder.Append("{");
            foreach (var down in responses)
            {
                var content= new StreamReader(down.Response.Body).ReadToEnd();
                results.Add($"\"{Guid.NewGuid()}\":{content}");
            }

            //来自leader的声音
            results.Add($"\"{Guid.NewGuid()}\":{{comment:\"我是leader，我组织了他们两个进行调查\"}}");
            contentBuilder.Append(string.Join(",", results));
            contentBuilder.Append("}");

            var stringContent = new StringContent(contentBuilder.ToString())
            {
                Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            };
            var headers = responses.SelectMany(x => x.Response.Headers).ToList();
            return new DownstreamResponse(stringContent,HttpStatusCode.OK,new List<Header>(), "123");
        }
    }
}
