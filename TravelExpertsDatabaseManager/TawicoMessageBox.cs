﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TravelExpertsData;

namespace TravelExpertsDatabaseManager
{
    public partial class TawicoMessageBox : Form
    {
        public TawicoMessageBox()
        {
            InitializeComponent();
        }

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

        private bool confirmYes = false;
        private bool confirmOk = false;
        private bool confirmNo = false;
        private bool confirmCancel = false;

        public DialogResult Show(string text, Color foreColour, bool confirm = false,bool cancel = false)
        {
            TawicoLabel.Text = text;
            TawicoLabel.ForeColor = foreColour;

            if (!confirm && !cancel)
            {
                TawicoButtonCancel.Visible = false;
                TawicoButtonConfirm.Visible = true;
                TawicoButtonConfirm.Text = "OK";
                TawicoButtonCancel.Text = "";

                confirmOk = true;
            }

            if (confirm && !cancel)
            {
                TawicoButtonCancel.Visible = true;
                TawicoButtonConfirm.Visible = true;
                TawicoButtonConfirm.Text = "Yes";
                TawicoButtonCancel.Text = "No";

                confirmYes = true;
                confirmNo = true;
            }

            return this.ShowDialog();
        }

        private void TawicoButtonConfirm_Click(object sender, EventArgs e)
        {
            if (confirmOk)
            {
                DialogResult = DialogResult.OK;
            }

            if(confirmYes)
            {
                DialogResult = DialogResult.Yes;
            }
        }

        private void TawicoButtonCancel_Click(object sender, EventArgs e)
        {
            if (confirmCancel)
            {
                DialogResult = DialogResult.Cancel;
            }

            if(confirmNo)
            {
                DialogResult = DialogResult.No;
            }
        }
    }
}