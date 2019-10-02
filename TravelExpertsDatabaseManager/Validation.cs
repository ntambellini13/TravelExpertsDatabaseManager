using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * Purpose: Class for validating textbox data
 * Author: Tawico
 * Date: September 18, 2019
 * 
 * */

namespace TravelExpertsDatabaseManager
{
    /// <summary>
    /// Contains methods to validate textboxes in windows forms
    /// </summary>
    public static class Validation
    {

        /// <summary>
        /// Tries to parse integer value in textbox and checks if its a valid non-negative whole number.
        /// </summary>
        /// <param name="textbox">The textbox field to try and parse</param>
        /// <returns>Is the value in the textbox a valid non-negative integer?</returns>
        public static bool IsValidNonNegativeInteger(TextBox textbox)
        {
            int value; // Stores value on try Parse
            // If parse is successful, test if value is negative. If unsuccessful, return false.
            if (int.TryParse(textbox.Text.Trim(), out value))
            {
                if (value < 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks that textbox contains a positive integer
        /// </summary>
        /// <param name="textbox">Textbox to check</param>
        /// <returns>Is the value in the textbox a valid positive integer?</returns>
        public static bool IsValidPositiveInteger(TextBox textbox)
        {
            int value; // Stores value on try Parse
            // If parse is successful, test if value is not positive. If unsuccessful, return false.
            if (int.TryParse(textbox.Text.Trim(), out value))
            {
                if (value <= 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks that the textbox has a year thats greater than or equal to the current year
        /// </summary>
        /// <param name="textbox">Textbox to check</param>
        /// <returns>True/false</returns>
        public static bool IsGreaterThanOrEqualToCurrentYear(TextBox textbox)
        {
            int year; // Stores year
            if (int.TryParse(textbox.Text.Trim(), out year))
            {
                if (year >= DateTime.Now.Year) // Checks that the year is equal to or greater than current year
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if textbox contains a valid positive decimal
        /// </summary>
        /// <param name="textbox">Textbox</param>
        /// <returns>True/false</returns>
        public static bool isValidPositiveDecimal(TextBox textbox)
        {
            decimal value;
            if(decimal.TryParse(textbox.Text.Trim(),out value))
            {
                return value > 0;
            }
            return false;
        }

        /// <summary>
        /// Checks that first textbox with decimal value is less than second textbox with decimal vlaue
        /// </summary>
        /// <param name="first">First textbox with valid decimal value</param>
        /// <param name="second">Second textbox with valid decimal values</param>
        /// <returns>True/false</returns>
        public static bool isFirstDecimalLessThanSecondDecimal(TextBox first, TextBox second)
        {
            if(isValidPositiveDecimal(first) && isValidPositiveDecimal(second))
            {
                return (decimal.Parse(first.Text.Trim()) < decimal.Parse(second.Text.Trim()));                
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks that the first date is before the second date
        /// </summary>
        /// <param name="firstDate">First date</param>
        /// <param name="secondDate">Second date</param>
        /// <returns>True/false</returns>
        public static bool IsFutureDateGreaterThanPastDate(DateTimePicker firstDate, DateTimePicker secondDate)
        {
            if(firstDate.Value == null || secondDate.Value == null)
            {
                return false;
            }
            return firstDate.Value < secondDate.Value;
        }

        /// <summary>
        /// Checks that the textbox is not empty
        /// </summary>
        /// <param name="textbox">Textbox to check</param>
        /// <returns>True or false</returns>
        public static bool IsNotEmptyOrNull(TextBox textbox)
        {
            return !String.IsNullOrEmpty(textbox.Text.Trim());
        }

        /// <summary>
        /// Checks if a valid email was entered
        /// </summary>
        /// <param name="textBox">Textbox to check</param>
        /// <returns>True/False</returns>
        public static bool IsValidEmail(TextBox textBox)
        {
            return Regex.IsMatch(textBox.Text.Trim(), @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Checks if a valid  url is entered
        /// </summary>
        /// <param name="textBox">Textbox to check</param>
        /// <returns>True/false</returns>
        public static bool IsValidURL(TextBox textBox)
        {
            return Regex.IsMatch(textBox.Text.Trim(), @"(https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|www\.[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9]+\.[^\s]{2,}|www\.[a-zA-Z0-9]+\.[^\s]{2,})", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Colors the textbox red if invalid, black if vallid
        /// </summary>
        /// <param name="textBox">Textbox to check</param>
        /// <param name="validationMethod">Method to validate textbox</param>
        /// <returns>True/False if valid/invalid</returns>
        public static bool ColorTextBoxValidation(TextBox textBox, Func<TextBox, bool> validationMethod)
        {
            if (validationMethod(textBox))
            {
                textBox.ForeColor = Color.Black;
                return true;
            }
            else
            {
                textBox.ForeColor = Color.Red;
                return false;
            }
        }
    }
}
