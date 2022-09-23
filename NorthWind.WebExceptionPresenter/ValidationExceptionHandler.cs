using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.WebExceptionPresenter
{
    public class ValidationExceptionHandler:ExceptionHandlerBase, IExceptionHandler
    {
        public Task Handle(ExceptionContext context)
        {
            var Exception = context.Exception as ValidationException;

            StringBuilder builder = new StringBuilder();
            
            foreach (var item in Exception.Errors)
            {
                builder.AppendLine(
                    string.Format("Propiedad: {0}. Error: {1}",
                    item.PropertyName, item.ErrorMessage));
            }

            return SetResult(context, StatusCodes.Status400BadRequest,
                "Error en los datos de entrada", builder.ToString());

        }
    }
}
