using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


/*
 * Purpose: Class for objects that extend the application's functionality
 * Author: Tawico
 * Date: September 18, 2019
 * 
 * */

namespace TravelExpertsData
{
    public static class HelperMethods
    {
        /// <summary>
        /// Interface used to grab all controls of a certain type from a parent object
        /// </summary>
        /// <param name="control">parent control</param>
        /// <param name="type">type </param>
        /// <returns></returns>
        public static IEnumerable<Control> GetAll(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetAll(ctrl, type))
                                      .Concat(controls)
                                      .Where(c => c.GetType() == type);
        }
    }

    /// <summary>
    /// Form manager Class;
    /// registers and unregisters forms so that back color can be set
    /// Can add additional methods to manipulate more properties of forms
    /// </summary>
    public static class FormManager
    {
        //List holding to hold all forms registered
        private static List<Form> formList = new List<Form>();

        /// <summary>
        /// Method checks if a form is part of the forms list, if not adds it to the list
        /// </summary>
        /// <param name="form"></param>
        public static void registerForm(Form form)
        {
            if (!formList.Contains(form)) formList.Add(form);
        }
        
        /// <summary>
        /// Method checks if a form is part of the forms list, if it is removes it from the list
        /// </summary>
        /// <param name="form"></param>
        public static void unRegisterForm(Form form)
        {
            if (formList.Contains(form)) formList.Remove(form);
        }

        /// <summary>
        /// Method that sets the back color for all forms registered in the form list
        /// </summary>
        /// <param name="backColor"></param>
        public static void setAllBackcolors(Color backColor)
        {
            foreach (Form f in formList) if (f != null) f.BackColor = backColor;
        }
    }
}
