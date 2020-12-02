using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace parser.Fitlers
{
    public class UploadExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private string _message = "check your csv format with the guideline. You could be upload an incorrect file";
        private List<string> _messageList = new List<string>() { "Rubric metadata is required before rubric content"};

        public override void OnException(ExceptionContext context)
        {
            if (_messageList.Contains(context.Exception.Message)) _message = context.Exception.Message;
            var result = new ViewResult();
            result.ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary()) { { "Message", _message } };
            result.ViewName = "Failure";
            context.Result = result;
            context.ExceptionHandled = true;
        }
    }
}
