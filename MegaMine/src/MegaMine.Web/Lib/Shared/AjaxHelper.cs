﻿using MegaMine.Core.Context;
using MegaMine.Core.Exception;
using MegaMine.Services.Widget.Domain;
using MegaMine.Services.Widget.Models;
using MegaMine.Web.Models.Shared;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace MegaMine.Web.Lib.Shared
{
    public static class AjaxHelper
    {
        //public static AjaxModel<T> Get<T>(Func<string, T> action, string message = "") where T : class
        //{
        //    AjaxModel<T> ajax;

        //    try
        //    {

        //        T model = action(null);

        //        ajax = new AjaxModel<T>() { Result = AjaxResult.Success, Model = model, Message = message };
        //    }
        //    catch (Exception exp)
        //    {
        //        ajax = new AjaxModel<T>() { Result = AjaxResult.Exception, Model = null, Message = exp.GetBaseException().Message };
        //    }

        //    return ajax;
        //}

        //public static AjaxModel<T> Save<T>(Action<string> action, string message) where T : class
        //{
        //    AjaxModel<T> ajax;

        //    try
        //    {

        //        action(null);

        //        ajax = new AjaxModel<T>() { Result = AjaxResult.Success, Model = null, Message = message };
        //    }
        //    catch (Exception exp)
        //    {
        //        ajax = new AjaxModel<T>() { Result = AjaxResult.Exception, Model = null, Message = exp.GetBaseException().Message };
        //    }

        //    return ajax;
        //}

        //public static AjaxModel<T> SaveGet<T>(Func<string, T> action, string message) where T : class
        //{
        //    return Get(action, message);
        //}

        public static async Task<AjaxModel<T>> GetAsync<T>(Func<string, Task<T>> action, string message = "") where T : class
        {
            AjaxModel<T> ajax;

            try
            {

                T model = await action(null);
                ajax = new AjaxModel<T>() { Result = AjaxResult.Success, Model = model, Message = message };
            }
            catch (Exception exp)
            {
                ajax = new AjaxModel<T>() { Result = AjaxResult.Exception, Model = null, Message = exp.GetBaseException().Message };
            }

            return ajax;
        }

        public static async Task<AjaxModel<NTModel>> SaveAsync(Func<string, Task> action, string message) 
        {
            AjaxModel<NTModel> ajax;

            try
            {
                await action(null);

                ajax = new AjaxModel<NTModel>() { Result = AjaxResult.Success, Model = null, Message = message };
            }
            catch (NTException exp)
            {
                ajax = new AjaxModel<NTModel>() { Result = AjaxResult.ValidationException, Model = new NTModel() { Data = exp.Model }, Message = exp.Message };
            }
            catch (Exception exp)
            {
                ajax = new AjaxModel<NTModel>() { Result = AjaxResult.Exception, Model = null, Message = exp.GetBaseException().Message };
            }

            return ajax;
        }

        public static async Task<AjaxModel<T>> SaveGetAsync<T>(Func<string, Task<T>> action, string message) where T : class
        {
            return await GetAsync(action, message);
        }

        public static async Task<DashboardModel> DashboardGet(Func<int, Task<object>> widgetData)
        {
            int? dashboardPageId = NTContext.Context.DashboardPageId;
            if (dashboardPageId != null)
            {
                WidgetDomain widgetDomain = NTContext.HttpContext.RequestServices.GetRequiredService<WidgetDomain>();
                DashboardModel dashboard = await widgetDomain.DashboardGet(dashboardPageId.Value);
                foreach(WidgetModel widget in dashboard.Widgets)
                {
                    widget.Chart.Data = await widgetData(widget.WidgetId);
                }
                //await widgetData(1);
                return dashboard;
            }
            else
            {
                return null;
            }
        }   
    }
}
