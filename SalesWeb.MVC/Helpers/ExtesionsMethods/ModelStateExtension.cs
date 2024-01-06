using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SalesWeb.MVC.Helpers.ExtesionsMethods
{
    public static class ModelStateExtension
    {
        public static void AddExceptionErrors(this ModelStateDictionary modelState)
        {
            var modelErros = modelState.Where(e => e.Value.Errors.Any(a => a.Exception is not null));

            modelErros.ToList().ForEach(e => e.Value.Errors.ToList().ForEach(er => modelState.AddModelError(e.Key, er.Exception.Message)));
        }
    }
}
