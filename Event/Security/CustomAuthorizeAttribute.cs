﻿using Microsoft.AspNet.Identity;
using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EventWeb.Security
{
    [AttributeUsage(AttributeTargets.Class| AttributeTargets.Method)]
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {

        
        private string[] allowedroles;
        public CustomAuthorizeAttribute(params string[] Roles)
        {
            this.allowedroles = (string[])Roles.Clone();
        }


        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool superAdmin = false;
            bool isAdmin =false;
            bool isuser = false;

            IserviceAdmin spa = new serviceAdmin();
            IserviceUser spu = new serviceUser();
            IPrincipal user = httpContext.User;
            bool authorize = false;


            string userid = user.Identity.Name;
            Admin _admin = spa.Get(x=>x.mailAdmin==userid);
            User _user = new User();

            if (_admin == null)
            {
               _user = spu.Get(x => x.username == userid);
            }


            if (_admin!=null){
                if (_admin.isSuperAdmin)
                {
                    superAdmin = true;
                }
                else { isAdmin = true; }

            }
            else if(_user!=null)
            {
                isuser = true;
            }




            if (user.Identity.IsAuthenticated)
            {
                
                    
                
               
                if (superAdmin && Roles.Contains("SuperAdmin"))
                {
                    authorize = true;
                }
                if (isAdmin && Roles.Contains("Admin"))
                {
                    authorize = true;
                }
                if (isuser && this.Roles.Contains("User"))
                {
                    authorize = true;
                }
            }

            return authorize;

        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            //{
            //    filterContext.Result = new ViewResult()
            //    {
            //        ViewName = "~/Home/Unauthorized"
            //    };
            //}
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(new
                 RouteValueDictionary(new { controller = "Home", action = "Unauthorized" }));
            }
            else if(Roles.Contains("SuperAdmin")|| Roles.Contains("Admin"))
            {
                filterContext.Result = new RedirectToRouteResult(new
                 RouteValueDictionary(new { controller = "Admin", action = "login" }));
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new
                RouteValueDictionary(new { controller = "User", action = "login" }));
            }
           // filterContext.Result = new HttpUnauthorizedResult();
        }


    }
}