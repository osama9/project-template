using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using ProjectTemplate.Core.Exceptions;
using ProjectTemplate.Core.Resources;
using ProjectTemplate.Web.AppServices;
using ProjectTemplate.Web.Helpers;
using ProjectTemplate.Web.Models;

namespace ProjectTemplate.Web.Controllers
{
    public class BaseController : Controller
    {
        protected IViewRenderService RenderService
            => HttpContext.RequestServices.GetService(typeof(IViewRenderService)) as IViewRenderService;

        protected void SetSuccess(bool isAutoHide = true)
        {
            var alert = new Alert(MessageText.OperationSuccess, Alert.Type.Success, isAutoHide: isAutoHide);

            var alertJson = JsonConvert.SerializeObject(alert);

            TempData[WebConstants.ALERT] = alertJson;
        }

        protected void SetSuccess(JsonResultObject result, bool isAutoHide = true)
        {
            var alert = new Alert(MessageText.OperationSuccess, Alert.Type.Success, isAutoHide: isAutoHide);
            result.Alert = alert;
        }

        protected void SetError(string msg, bool isAutoHide = false)
        {
            var alert = new Alert(msg, Alert.Type.Error, isAutoHide: isAutoHide);
            var alertJson = JsonConvert.SerializeObject(alert);
            TempData[WebConstants.ALERT] = alertJson;
        }

        protected void SetError(Exception ex = null, bool isAutoHide = false)
        {
            var msg = MessageText.OperationFailed;

            if (ex != null)
            {
                msg = GetExceptionError(ex);
            }

            var alert = new Alert(msg, Alert.Type.Error, isAutoHide: isAutoHide);
            var alertJson = JsonConvert.SerializeObject(alert);
            TempData[WebConstants.ALERT] = alertJson;
        }

        protected void SetError(JsonResultObject result, Exception ex = null, bool isAutoHide = false)
        {
            var msg = MessageText.OperationFailed;

            if (ex != null)
            {
                msg = GetExceptionError(ex);
            }

            var alert = new Alert(msg, Alert.Type.Error, isAutoHide: isAutoHide);

            result.Alert = alert;
            result.Success = false;
        }

        protected ActionResult SimpleAjaxAction(Action action, bool setSuccessMessage = true)
        {
            JsonResultObject result = new JsonResultObject();

            try
            {
                action.Invoke();

                if (setSuccessMessage)
                    SetSuccess(result);
            }
            catch (BusinessException ex)
            {
                SetError(result, ex);
                return BadRequest(result);
            }

            return Ok(result);
        }
        
        protected string FormatErrorMessage(List<string> errors)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(MessageText.PleaseFixTheFollowingErrors);

            sb.Append("<ul>");

            foreach (var item in errors)
                sb.AppendLine($"<li>{item}</li>");

            sb.Append("</ul>");

            return sb.ToString();
        }

        protected bool IsAjaxRequest()
        {
            if (Request == null)
                throw new ArgumentNullException(nameof(Request));

            if (Request.Headers != null)
                return Request.Headers["X-Requested-With"] == "XMLHttpRequest";

            return false;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Thread.CurrentPrincipal = context.HttpContext.User;

            base.OnActionExecuting(context);
        }

        private string GetExceptionError(Exception ex)
        {
            if (ex == null)
                return "";

            if (ex is EntityValidationException entityValidationException)
            {
                return FormatErrorMessage(entityValidationException.Errors);
            }

            if (ex is BusinessException businessValidationMessage && businessValidationMessage.Errors?.Count > 0)
            {
                return FormatErrorMessage(businessValidationMessage.Errors);
            }

            return ex.Message;
        }

        public List<string> GetModalStateErrors()
        {
            var result = new List<string>();

            foreach (var modelState in ViewData.ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    result.Add(error.ErrorMessage);
                }
            }

            return result;

        }
    }
}