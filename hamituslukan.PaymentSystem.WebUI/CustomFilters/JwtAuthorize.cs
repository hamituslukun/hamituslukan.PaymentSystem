using hamituslukan.PaymentSystem.Dto.Concrete;
using hamituslukan.PaymentSystem.WebUI.Builders.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace hamituslukan.PaymentSystem.WebUI.CustomFilters
{
    public class JwtAuthorize : ActionFilterAttribute
    {
        public string Roles { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var _configuration = context.HttpContext.RequestServices.GetService<IConfiguration>();

            if (JwtAuthorizeHelper.CheckToken(context, out string token))
            {
                var response = JwtAuthorizeHelper.GetActiveUser(_configuration, token);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    JwtAuthorizeHelper.CheckUserRole(JwtAuthorizeHelper.GetActiveUser(response), Roles, context);
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    context.HttpContext.Session.Remove("token");
                }
                else
                {
                    context.HttpContext.Session.Remove("token");
                    var statusCode = response.StatusCode.ToString();
                    context.Result = new RedirectToActionResult("Login", "Account", new { code = statusCode });
                }
            }
        }
    }

    public class JwtAuthorizeHelper
    {
        public static bool CheckToken(ActionExecutingContext context, out string token)
        {
            token = context.HttpContext.Session.GetString("token");

            if (!string.IsNullOrEmpty(token))
                return true;

            context.Result = new RedirectToActionResult("Login", "Account", null);

            return false;
        }

        public static HttpResponseMessage GetActiveUser(IConfiguration _configuration, string token)
        {
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return httpClient.GetAsync($"{ _configuration.GetValue<string>("Api") }/Auth/CurrentUser").Result;
        }

        public static ApplicationUserDto GetActiveUser(HttpResponseMessage responseMessage)
        {
            return JsonConvert.DeserializeObject<ApplicationUserDto>(responseMessage.Content.ReadAsStringAsync().Result);
        }

        public static void CheckUserRole(ApplicationUserDto activeUser, string roles, ActionExecutingContext context)
        {
            Status status = null;
            if (!string.IsNullOrWhiteSpace(roles))
            {
                if (roles.Contains(","))
                {
                    StatusBuilderDirector director = new StatusBuilderDirector(new MultiRoleStatusBuilder());
                    status = director.GenerateStatus(activeUser, roles);
                }
                else
                {
                    StatusBuilderDirector director = new StatusBuilderDirector(new SingleRoleStatusBuilder());
                    status = director.GenerateStatus(activeUser, roles);
                }

                CheckStatus(status, context);
            }
        }

        private static void CheckStatus(Status status, ActionExecutingContext context)
        {
            if (!status.AccessStatus)
            {
                context.Result = new RedirectToActionResult("AccessDenied", "Account", null);
                return;
            }
        }
    }
}