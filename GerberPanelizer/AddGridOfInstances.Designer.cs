namespace GerberCombinerBuilder
{
    partial class AddGridOfInstances
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.countXTextBox = new System.Windows.Forms.NumericUpDown();
            this.countYTextBox = new System.Windows.Forms.NumericUpDown();
            this.spacingXTextBox = new System.Windows.Forms.TextBox();
            this.spacingYTextBox = new System.Windows.Forms.TextBox();
            this.startYTextBox = new System.Windows.Forms.TextBox();
            this.startXTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.countXTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.countYTextBox)).BeginInit();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(219, 100);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 5;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(300, 100);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 6;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Spacing X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(191, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Spacing Y";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Count X";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(191, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Count Y";
            // 
            // countXTextBox
            // 
            this.countXTextBox.Location = new System.Drawing.Point(69, 65);
            this.countXTextBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.countXTextBox.Name = "countXTextBox";
            this.countXTextBox.Size = new System.Drawing.Size(120, 20);
            this.countXTextBox.TabIndex = 4;
            this.countXTextBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.countXTextBox.ValueChanged += new System.EventHandler(this.countXTextBox_ValueChanged);
            // 
            // countYTextBox
            // 
            this.countYTextBox.Location = new System.Drawing.Point(253, 65);
            this.countYTextBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.countYTextBox.Name = "countYTextBox";
            this.countYTextBox.Size = new System.Drawing.Size(120, 20);
            this.countYTextBox.TabIndex = 5;
            this.countYTextBox.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.countYTextBox.ValueChanged += new System.EventHandler(this.countXTextBox_ValueChanged);
            // 
            // spacingXTextBox
            // 
            this.spacingXTextBox.Location = new System.Drawing.Point(69, 39);
            this.spacingXTextBox.Name = "spacingXTextBox";
            this.spacingXTextBox.Size = new System.Drawing.Size(120, 20);
            this.spacingXTextBox.TabIndex = 2;
            this.spacingXTextBox.Text = "0mm";
            this.spacingXTextBox.TextChanged += new System.EventHandler(this.spacingXTextBox_TextChanged);
            this.spacingXTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.spacingXTextBox_Validating);
            // 
            // spacingYTextBox
            // 
            this.spacingYTextBox.Location = new System.Drawing.Point(253, 39);
            this.spacingYTextBox.Name = "spacingYTextBox";
            this.spacingYTextBox.Size = new System.Drawing.Size(120, 20);
            this.spacingYTextBox.TabIndex = 3;
            this.spacingYTextBox.Text = "0mm";
            this.spacingYTextBox.TextChanged += new System.EventHandler(this.spacingXTextBox_TextChanged);
            this.spacingYTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.spacingXTextBox_Validating);
            // 
            // startYTextBox
            // 
            this.startYTextBox.Location = new System.Drawing.Point(253, 12);
            this.startYTextBox.Name = "startYTextBox";
            this.startYTextBox.Size = new System.Drawing.Size(120, 20);
            this.startYTextBox.TabIndex = 1;
            this.startYTextBox.Text = "0mm";
            // 
            // startXTextBox
            // 
            this.startXTextBox.Location = new System.Drawing.Point(69, 12);
            this.startXTextBox.Name = "startXTextBox";
            this.startXTextBox.Size = new System.Drawing.Size(120, 20);
            this.startXTextBox.TabIndex = 0;
            this.startXTextBox.Text = "0mm";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(191, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Start Y";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Start X";
            // 
            // AddGridOfInstances
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ClientSize = new System.Drawing.Size(387, 135);
            this.Controls.Add(this.startYTextBox);
            this.Controls.Add(this.startXTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.spacingYTextBox);
            this.Controls.Add(this.spacingXTextBox);
            this.Controls.Add(this.countYTextBox);
            this.Controls.Add(this.countXTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddGridOfInstances";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Grid of Instances";
            this.Load += new System.EventHandler(this.AddGridOfInstances_Load);
            ((System.ComponentModel.ISupportInitialize)(this.countXTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.countYTextBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown countXTextBox;
        private System.Windows.Forms.NumericUpDown countYTextBox;
        private System.Windows.Forms.TextBox spacingXTextBox;
        private System.Windows.Forms.TextBox spacingYTextBox;
        private System.Windows.Forms.TextBox startYTextBox;
        private System.Windows.Forms.TextBox startXTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}