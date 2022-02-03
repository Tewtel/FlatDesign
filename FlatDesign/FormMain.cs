using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace FlatDesign
{
    public partial class FormMain : Form
    {
        //Fields
        private Button currentButton;
        private Random random;
        private int tempIndex;
        private Form activeForm;
        
        //Constructor
        public FormMain()
        {
            InitializeComponent();
            random = new Random();
            this.Text = string.Empty;
            this.ControlBox = false;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }

        //Methods
        private Color SelectThemeColor()
        {
            int index = random.Next(ThemeColor.ColorList.Count);
            while (tempIndex == index)
                index = random.Next(ThemeColor.ColorList.Count);
            string color = ThemeColor.ColorList[index];
            return ColorTranslator.FromHtml(color);
        }
        private void SelectLeftPanelItems(Form childForm, object sender)
        {

            ActivateButton(sender);
            
        }
        private void ActivateButton(object sender)
        {
            
            if(sender != null)
                if(currentButton != sender)
                {
                    DisableButton();
                    Color color = SelectThemeColor();
                    currentButton = (Button)sender;
                    currentButton.BackColor = color;
                    currentButton.ForeColor = Color.White;
                    currentButton.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
                    ThemeColor.PrimaryColor = color;
                    ThemeColor.SecondaryColor = ThemeColor.ChangeColorBrightness(color, -0.3);
                    panelTitle.BackColor = ThemeColor.PrimaryColor;
                    panelLogo.BackColor = ThemeColor.SecondaryColor;
                    btnCloseChildForm.Visible = true;
                }
        }
        private void DisableButton()
        {
            foreach(Control previousBtn in panelMenu.Controls)
                if(previousBtn.GetType() == typeof(Button))
                {
                    Button tempButton = (Button)previousBtn;
                    tempButton.BackColor = Color.FromArgb(50, 52, 77);
                    tempButton.ForeColor = Color.Gainsboro;
                    tempButton.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
                }
        }
        private void OpenChildForm(FormParentOfThisForms childForm, object sender)
        {
            if (activeForm != null)
                activeForm.Close();
            ActivateButton(sender);
            childForm.LoadTheme();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelDesktop.Controls.Add(childForm);
            panelDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lblTitle.Text = childForm.Text;
        }
        private void btnProducts_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ChildForms.FormProducts(), sender);
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ChildForms.FormOrders(), sender);
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ChildForms.FormCustomers(), sender);
        }

        private void btnReporting_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ChildForms.FormReporting(), sender);
        }

        private void btnNotifications_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ChildForms.FormNotifications(), sender);
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ChildForms.FormSetting(), sender);
        }
        private void StandartSetOfForm()
        {
            lblTitle.Text = "HOME";
            panelLogo.BackColor = Color.FromArgb(38,38,59);
            panelTitle.BackColor = Color.FromArgb(0, 135, 137);
            DisableButton();
            currentButton = null;

        }
        private void btnCloseChildForm_Click(object sender, EventArgs e)
        {
            if(activeForm != null)
                activeForm.Close();
            StandartSetOfForm();
            btnCloseChildForm.Visible = false;
        }
        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void panelTitle_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xF012, 0);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMaximizeRestore_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                WindowState = FormWindowState.Maximized;
            else
                WindowState = FormWindowState.Normal;
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}
