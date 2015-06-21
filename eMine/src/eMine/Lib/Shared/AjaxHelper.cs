﻿using eMine.Models;
using eMine.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eMine.Lib.Shared
{
    public static class AjaxHelper
    {
        public static AjaxModel<T> Get<T>(Func<string, T> action, string message = "") where T: class
        {
            AjaxModel<T> ajax;

            try
            {
                
                T model = action(null);

                ajax = new AjaxModel<T>() { Result = AjaxResult.Success, Model = model, Message = message };
            }
            catch (Exception exp)
            {
                ajax = new AjaxModel<T>() { Result = AjaxResult.Exception, Model = null, Message = exp.Message };
            }

            return ajax;
        }

        public static AjaxModel<T> Save<T>(Action<string> action, string message) where T : class
        {
            AjaxModel<T> ajax;

            try
            {

                action(null);

                ajax = new AjaxModel<T>() { Result = AjaxResult.Success, Model = null, Message = message };
            }
            catch (Exception exp)
            {
                ajax = new AjaxModel<T>() { Result = AjaxResult.Exception, Model = null, Message = exp.Message };
            }

            return ajax;
        }

        public static AjaxModel<T> SaveGet<T>(Func<string, T> action, string message) where T : class
        {
            return Get(action, message);
        }

    }
}