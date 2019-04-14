using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GerberLibrary.Core;

namespace GerberCombinerBuilder
{
    public partial class AddGridOfInstances : Form
    {
        public AddGridOfInstances()
        {
            InitializeComponent();
        }

        public int CountX
        {
            get { return int.Parse(countXTextBox.Text); }
            set { countXTextBox.Text = value.ToString(); }
        }
        public int CountY
        {
            get { return int.Parse(countYTextBox.Text); }
            set { countYTextBox.Text = value.ToString(); }
        }

        public double StartXMM
        {
            get { return UnitsNet.Length.Parse(startXTextBox.Text).Millimeters; }
            set { startXTextBox.Text = UnitsNet.Length.FromMillimeters(value).ToString(); }
        }
        public double StartYMM
        {
            get { return UnitsNet.Length.Parse(startYTextBox.Text).Millimeters; }
            set { startYTextBox.Text = UnitsNet.Length.FromMillimeters(value).ToString(); }
        }
        public double SpacingXMM
        {
            get { return UnitsNet.Length.Parse(spacingXTextBox.Text).Millimeters; }
            set { spacingXTextBox.Text = UnitsNet.Length.FromMillimeters(value).ToString(); }
        }
        public double SpacingYMM
        {
            get { return UnitsNet.Length.Parse(spacingYTextBox.Text).Millimeters; }
            set { spacingYTextBox.Text = UnitsNet.Length.FromMillimeters(value).ToString(); }
        }

        private void spacingXTextBox_TextChanged(object sender, EventArgs e)
        {
            this.ValidateInput();
        }

        private void countXTextBox_ValueChanged(object sender, EventArgs e)
        {
            this.ValidateInput();
        }

        private void ValidateInput()
        {
            okButton.Enabled = (UnitsNet.Length.TryParse(spacingXTextBox.Text, out var spacingX) &&
                                UnitsNet.Length.TryParse(spacingYTextBox.Text, out var spacingY) &&
                                int.TryParse(countXTextBox.Text, out var countX) &&
                                int.TryParse(countYTextBox.Text, out var countY));
        }

        private void AddGridOfInstances_Load(object sender, EventArgs e)
        {
            spacingXTextBox.Focus();
            this.Activate();
        }

        private void spacingXTextBox_Validating(object sender, CancelEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null)
            {
                return;
            }

            if (int.TryParse(textBox.Text, out var x))
            {
                textBox.Text = UnitsNet.Length.FromMillimeters(x).ToString();
            }
        }
    }
}
