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

namespace TravelExpertsDatabaseManager
{
    public partial class TawicoMessageBox : Form
    {
        public TawicoMessageBox()
        {
            InitializeComponent();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {

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
    }
}
