using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BlazorServerValidation.Shared;
using Microsoft.AspNetCore.Components;

namespace BlazorServerValidation.Client.Pages
{
    public partial class CreateProduct
    {
        public CreateProductCommand Model = new CreateProductCommand();

        [Inject]
        public IHttpService HttpService { get; set; }

        private async Task OnSubmit()
        {
            var result = await HttpService.Post<Result<Guid>>("api/products", Model, CancellationToken.None);

            if (result.Success)
            {
                ShowMessage("Dodano produkt");
            }
            else
            {
                ShowError(result, Model);
            }
        }
    }
}
