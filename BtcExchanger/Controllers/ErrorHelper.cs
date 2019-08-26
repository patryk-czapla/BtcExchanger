using System.Dynamic;
using System.Collections.Generic;

namespace BtcExchanger.Controllers
{
    public static class ErrorHelper
    {
        public static dynamic GenerateAnErrorMessage(string errorClass, string errorMessage){
                
            dynamic error_message = new ExpandoObject();
            var dictionary_second = (IDictionary<string, object>)error_message;
            dictionary_second.Add(errorClass, errorMessage);

            dynamic errors = new ExpandoObject();
            var dictionary = (IDictionary<string, object>)errors;
            dictionary.Add("errors",error_message);

            return errors;
        }
    }
}