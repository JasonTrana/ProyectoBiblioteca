using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BibliotecaVirtual.Models;

namespace BibliotecaVirtual.Permisos
{
    public class PermisoRolAttribute : ActionFilterAttribute
    {
        private Rol IdUserRol;

        public PermisoRolAttribute(Rol idRol)
        {
            IdUserRol = idRol;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if(HttpContext.Current.Session["Usuario"] != null)
            {
                User usuario = HttpContext.Current.Session["Usuario"] as User;
                if(usuario.Roles != this.IdUserRol)
                {
                    filterContext.Result = new RedirectResult("~/Home/NoAccess");
                }
            }
            else
            {
                filterContext.Result = new RedirectResult("~/Home/NoAccess");
            }

            base.OnActionExecuting(filterContext);
        }
    }
}