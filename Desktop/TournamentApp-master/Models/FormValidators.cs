using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TournamentApp.Models
{
    static class FormValidators
    {
        public delegate bool ControlValidator(Control value);

        /// <summary>
        ///  Return an error message if any control is invalid
        /// </summary>
        /// <param name="validators"></param>
        /// <returns>string</returns>
        public static ControlErrorProvider validForm(ToValidate[] validators)
        {
            string errorMessage = null;
            Control control = null;
            foreach (ToValidate validator in validators)
            {
                errorMessage = FormValidators.validControl(validator.Control, validator.ControlValidators, validator.ErrorMessages);
                control = validator.Control;
                if (!(errorMessage == null)) break;
            }
            return errorMessage == null ? null : new ControlErrorProvider(errorMessage, control);
        }

        public static List<ControlErrorProvider> validFormTest(ToValidate[] validators)
        {
            List<ControlErrorProvider> errors = new List<ControlErrorProvider>();

            string errorMessage = null;
            Control control = null;
            bool hasErrors = false;
            foreach (ToValidate validator in validators)
            {
                errorMessage = FormValidators.validControl(validator.Control, validator.ControlValidators, validator.ErrorMessages);
                if (errorMessage != null) hasErrors = true;
                control = validator.Control;
                errors.Add(new ControlErrorProvider(errorMessage, control));
            }
            return !hasErrors ? null : errors;
        }

        public static string validControl(Control control, ControlValidator[] validators, string[] errorMessages)
        {
            if (validators.Length == errorMessages.Length)
            {
                for (int i = 0; i < validators.Length; i++)
                {
                    bool isValid = validators[i](control);
                    if (!isValid) return errorMessages[i];
                }
            }
            return null;
        }

        // Validators
        public static bool hasText(Control control)
        {
            if (control is TextBox)
                return control.Text.Trim().Length > 0;
            throw new Exception("Invalid control for isEmpty Method");
        }

        public static bool isSelected(Control control)
        {
            if (control is ComboBox)
                return ((ComboBox)control).SelectedIndex >= 0;
            else if (control is ComboBox)
                return ((ComboBox)control).SelectedIndex >= 0;

            throw new Exception("Invalid control for isSelected Method");
        }

        public static bool isNumber(Control control)
        {
            if (FormValidators.hasText(control))
                return Validators.isNumber(control.Text);
            return false;
        }
    }
}
