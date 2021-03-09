using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class ValidateStage<T> : PipelineStage<T>
        where T : class, new()
    {
        public override Task Process(T model)
        {
            Type type = typeof(T);

            foreach (var prop in type.GetProperties())
            {
                foreach (var error in from attr in prop.GetCustomAttributes(false).OfType<ValidationAttribute>()
                                     where !attr.IsValid(prop.GetValue(model))
                                     select attr.ErrorMessage)
                {
                    LogError(error);
                }
            }

            return Task.CompletedTask;
        }
    }
}
