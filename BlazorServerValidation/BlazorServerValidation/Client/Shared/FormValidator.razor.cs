using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorServerValidation.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorServerValidation.Client.Shared
{
    public partial class FormValidator
    {
        [CascadingParameter]
        public EditContext CurrentEditContext { get; set; }

        protected ValidationMessageStore ValidationMessageStore { get; set; }

        public string Message { get; set; }

        public MessageType Type { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (this.CurrentEditContext == null)
            {
                throw new InvalidOperationException();
            }

            ValidationMessageStore = new ValidationMessageStore(CurrentEditContext);
        }

        public void ShowError(Result result, object model)
        {
            ClearMessages(false);

            if (result.Success == false)
            {
                ShowMessage("Błąd w formularzu", MessageType.Error);

                ValidationMessageStore.Clear();

                foreach (var error in result.Errors.Where(e => string.IsNullOrEmpty(e.PropertyName) == false))
                {
                    var fieldIdentifier = new FieldIdentifier(model, error.PropertyName);
                    ValidationMessageStore.Add(fieldIdentifier, error.Message);
                }
            }

            CurrentEditContext.NotifyValidationStateChanged();
        }

        public void ShowMessage(string message)
        {
            ShowMessage(message, MessageType.Success);
        }

        public void ShowMessage(string message, MessageType type)
        {
            if (type == MessageType.Success)
            {
                ClearMessages(true);
            }

            Message = message;
            Type = type;

            StateHasChanged();
        }

        public void ClearMessages(bool clearValidationMessages)
        {
            if (clearValidationMessages)
            {
                ValidationMessageStore.Clear();

                CurrentEditContext.NotifyValidationStateChanged();
            }

            Message = string.Empty;

            StateHasChanged();
        }

        public enum MessageType
        {
            Success,
            Error
        }
    }
}
