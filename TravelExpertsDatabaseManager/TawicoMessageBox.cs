using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TravelExpertsData;

/*
 * Purpose: Tawico Message Box Form Class loads and runs custom message box whenever called
 * Replaced all message boxes with custom message box
 * Author: Tawico 
 * Date: September 18, 2019
 * 
 * */

namespace TravelExpertsDatabaseManager
{
    public partial class TawicoMessageBox : Form
    {
        public TawicoMessageBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// When form loads set styling on various components
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TawicoMessageBox_Load(object sender, EventArgs e)
        {
            //set back color of popup dialog
            this.BackColor = Color.Azure;

            //grab all buttons on the form
            var buttons = HelperMethods.GetAll(this, typeof(Button));

            //grab all labels on the form
            var labels = HelperMethods.GetAll(this, typeof(Label));

            //set the BackColor of each button 
            foreach (var b in buttons)
            {
                b.BackColor = Color.GhostWhite;
            }

            //Style the font of each label on the form
            foreach (var l in labels)
            {
                l.Font = new Font("Arial", (float)8.25);
                l.ForeColor = Color.RoyalBlue;
            }
        }

        //Private boolean class variables
        //Used to determine the type of message box by setting dialog result
        //Used to configure button text
        private bool confirmYes = false;
        private bool confirmOk = false;
        private bool confirmNo = false;
        private bool confirmCancel = false;

        /// <summary>
        /// Custom Show method; used to open Tawico message box and display appropriate info/confirmations
        /// </summary>
        /// <param name="text">The string message to display in message box</param>
        /// <param name="foreColour">default color for messages</param>
        /// <param name="confirm">boolean used to configure buttons displayed</param>
        /// <param name="cancel">boolean used to configure buttons displayed</param>
        /// <returns></returns>
        public DialogResult Show(string text, Color foreColour, bool confirm = false,bool cancel = false)
        {
            TawicoLabel.Text = text;//set the label text to input parameter text
            TawicoLabel.ForeColor = foreColour;//set the label text color to input parameter

            //default mode for message box; OK confirmation button
            //shows and configures buttons according to method input parameters
            if (!confirm && !cancel)
            {
                TawicoButtonCancel.Visible = false;
                TawicoButtonConfirm.Visible = true;
                TawicoButtonConfirm.Text = "OK";
                TawicoButtonCancel.Text = "";

                //set class variable; used for determining what dialog result to return
                confirmOk = true;
            }

            //alternate mode for message box; YesNo confirmation buttons
            //shows and configures buttons according to method input parameters
            if (confirm && !cancel)
            {
                TawicoButtonCancel.Visible = true;
                TawicoButtonConfirm.Visible = true;
                TawicoButtonConfirm.Text = "Yes";
                TawicoButtonCancel.Text = "No";

                //set class variables; used for determining what dialog result to return
                confirmYes = true;
                confirmNo = true;
            }

            return this.ShowDialog();//shows a for with the configuration specified by input parameters
        }

        /// <summary>
        /// Confirmation button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TawicoButtonConfirm_Click(object sender, EventArgs e)
        {
            //Sets dialog result based on button config on click
            if (confirmOk)
            {
                DialogResult = DialogResult.OK;
            }

            //Sets dialog result based on button config on click
            if (confirmYes)
            {
                DialogResult = DialogResult.Yes;
            }
        }

        /// <summary>
        /// Cancel button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TawicoButtonCancel_Click(object sender, EventArgs e)
        {
            //Sets dialog result based on button config on click
            if (confirmCancel)
            {
                DialogResult = DialogResult.Cancel;
            }

            //Sets dialog result based on button config on click
            if (confirmNo)
            {
                DialogResult = DialogResult.No;
            }
        }
    }
}
