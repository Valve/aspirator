using System;
using System.Collections;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;

namespace KiP.Web.Mvc.Tests.Unit.Infrastructure {
    public static class MvcHelper {
        private const string AppPathModifier = "/$(SESSION)";

        public static HtmlHelper<object> GetHtmlHelper() {
            HttpContextBase httpcontext = GetHttpContext("/", null, null);
            var rt = new RouteCollection();
            rt.Add(new Route("{controller}/{action}/{id}", null) { Defaults = new RouteValueDictionary(new { id = "defaultid" }) });

            var rd = new RouteData();
            rd.Values.Add("controller", "a");
            rd.Values.Add("action", "b");


            var vdd = new ViewDataDictionary();

            var viewContext = new ViewContext {
                HttpContext = httpcontext,
                RouteData = rd,
                ViewData = vdd
            };
            var mockVdc = new Mock<IViewDataContainer>();
            mockVdc.Setup(vdc => vdc.ViewData).Returns(vdd);

            var htmlHelper = new HtmlHelper<object>(viewContext, mockVdc.Object, rt);
            return htmlHelper;
        }

        private static HttpContextBase GetHttpContext(string appPath, string requestPath, string httpMethod) {
            return GetHttpContext(appPath, requestPath, httpMethod, Uri.UriSchemeHttp, -1);
        }

        private static HttpContextBase GetHttpContext(string appPath, string requestPath, string httpMethod, string protocol, int port) {
            var mockHttpContext = new Mock<HttpContextBase>();

            if (!String.IsNullOrEmpty(appPath)) {
                mockHttpContext.Setup(o => o.Request.ApplicationPath).Returns(appPath);
            }
            if (!String.IsNullOrEmpty(requestPath)) {
                mockHttpContext.Setup(o => o.Request.AppRelativeCurrentExecutionFilePath).Returns(requestPath);
            }

            Uri uri = port >= 0 ? new Uri(protocol + "://localhost" + ":" + Convert.ToString(port)) : new Uri(protocol + "://localhost");
            mockHttpContext.Setup(o => o.Request.Url).Returns(uri);

            mockHttpContext.Setup(o => o.Request.PathInfo).Returns(String.Empty);
            if (!String.IsNullOrEmpty(httpMethod)) {
                mockHttpContext.Setup(o => o.Request.HttpMethod).Returns(httpMethod);
            }

            mockHttpContext.Setup(o => o.Session).Returns((HttpSessionStateBase)null);
            mockHttpContext.Setup(o => o.Response.ApplyAppPathModifier(It.IsAny<string>())).Returns<string>(r => AppPathModifier + r);
            mockHttpContext.Setup(o => o.Items).Returns(new Hashtable());
            return mockHttpContext.Object;
        }
    }
}
