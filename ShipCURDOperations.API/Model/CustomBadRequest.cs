
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;
namespace ShipCURDOperations.API.Model

{
    public class CustomBadRequest

    {
        public List<string> errors{get;}
        public bool succeeded{get;}

        public CustomBadRequest(ModelStateDictionary modelState)

        {
            List<string> modelErrors = modelState.SelectMany(x => x.Value.Errors)
                 .Select(x => x.ErrorMessage).ToList();
           errors=modelErrors;
           succeeded=false;

        
        }
        
    }
}