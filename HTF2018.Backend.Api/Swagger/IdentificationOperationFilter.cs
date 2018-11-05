using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Reflection;
using HTF2018.Backend.Api.Attributes;

namespace HTF2018.Backend.Api.Swagger
{
    public class IdentificationOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            Attribute attribute = context.MethodInfo.GetCustomAttribute(typeof(HtfIdentificationAttribute));
            if (attribute == null) { return; }

            if (operation.Parameters == null) { operation.Parameters = new List<IParameter>(); }
            operation.Parameters.Add(new NonBodyParameter
            {
                Name = "htf-identification",
                Description = "HTF Team Identification",
                In = "header",
                Type = "string",
                Required = true
            });
        }
    }
}