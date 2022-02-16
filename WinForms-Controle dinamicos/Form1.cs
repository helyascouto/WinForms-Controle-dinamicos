using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Forms.Button;

namespace WinForms_Controle_dinamicos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //Button btn = new Button();
            //btn.Text = "Clique";
            //btn.Width = 200;
            //btn.Click += Btn_Click;
            //panel1.Controls.Add(btn);
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Test");
        }

        private void btnCriar_Click(object sender, EventArgs e)
        {
            Button btn = new Button();
            btn.Text = txtNome.Text;
            btn.Location = new Point((int)numericUpDownX.Value, (int)numericUpDownY.Value);
            btn.Width = 200;
            btn.Click += Btn_Click;
            panel1.Controls.Add(btn);
        }
    }
}
