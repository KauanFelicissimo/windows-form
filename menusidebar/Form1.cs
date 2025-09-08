using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace menusidebar
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        bool menuExpand = true;

        private void menuTransition_Tick(object sender, EventArgs e)
        {
            if (menuExpand == false)
            {
                menuContainer.Width += 10;
                if (menuContainer.Width >= 165)
                {
                    menuTransition.Stop();
                    menuExpand = true;
                }
            }
            else
            {
                menuContainer.Width -= 10;
                if (menuContainer.Width <=0)
                {
                    menuTransition.Stop();
                    menuExpand = false;
                }
            }

        }

        private void menu_Click(object sender, EventArgs e)
        {
            menuTransition.Start();
        }

        private void menuContainer_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
