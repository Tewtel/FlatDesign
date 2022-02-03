using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlatDesign
{
    public partial class FormParentOfThisForms : Form
    {
        public FormParentOfThisForms()
        {
            InitializeComponent();
        }
        public void LoadTheme()
        {
            foreach (Control btnControl in this.Controls)
                if (btnControl.GetType() == typeof(Button))
                {
                    Button btn = (Button)btnControl;
                    btn.BackColor = ThemeColor.PrimaryColor;
                    btn.ForeColor = Color.White;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;
                }
        }
    }
}
