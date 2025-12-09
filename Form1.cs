using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Формы
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Scale(new SizeF(1.0f, 1.0f)); // Принудительный масштаб 100%
            this.AutoScaleMode = AutoScaleMode.None;
            this.AutoScaleDimensions = new SizeF(96F, 96F); // Стандартные 96 DPI
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 newForm = new Form2();
            newForm.Show();
        }
    }
}
