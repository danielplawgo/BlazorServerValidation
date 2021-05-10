using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorServerValidation.Client.Shared;
using BlazorServerValidation.Shared;
using Microsoft.AspNetCore.Components;

namespace BlazorServerValidation.Client.Pages
{
    public class BasePage : ComponentBase
    {
        protected FormValidator FormValidator;

        protected void ShowError(Result result, object model = null)
        {
            FormValidator?.ShowError(result, model);
        }

        protected void ShowMessage(string message)
        {
            FormValidator?.ShowMessage(message);
        }
    }
}
