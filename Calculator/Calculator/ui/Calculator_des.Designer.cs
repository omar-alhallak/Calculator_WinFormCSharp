namespace calculator
{
    partial class Calculator_des
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
            btn1 = new Button();
            btn8 = new Button();
            btn7 = new Button();
            btn6 = new Button();
            btn5 = new Button();
            btn4 = new Button();
            btn3 = new Button();
            btn2 = new Button();
            btn9 = new Button();
            btn0 = new Button();
            btnPlus = new Button();
            btnEquals = new Button();
            btnSub = new Button();
            btnMultiply = new Button();
            btnDevide = new Button();
            btnDot = new Button();
            btnAC = new Button();
            btnDelete = new Button();
            btnPercent = new Button();
            Memory = new Panel();
            lblStorage = new Label();
            P2 = new Panel();
            txtResult = new TextBox();
            Memory.SuspendLayout();
            P2.SuspendLayout();
            SuspendLayout();
            // 
            // btn1
            // 
            btn1.FlatAppearance.BorderSize = 0;
            btn1.ForeColor = Color.FromArgb(23, 32, 32);
            btn1.Location = new Point(3, 180);
            btn1.Name = "btn1";
            btn1.Size = new Size(75, 53);
            btn1.TabIndex = 0;
            btn1.Tag = "1";
            btn1.Text = "1";
            btn1.UseVisualStyleBackColor = true;
            btn1.Click += Digit_Click;
            // 
            // btn8
            // 
            btn8.FlatAppearance.BorderSize = 0;
            btn8.ForeColor = Color.FromArgb(23, 32, 32);
            btn8.Location = new Point(84, 62);
            btn8.Name = "btn8";
            btn8.Size = new Size(75, 53);
            btn8.TabIndex = 1;
            btn8.Tag = "8";
            btn8.Text = "8";
            btn8.UseVisualStyleBackColor = true;
            btn8.Click += Digit_Click;
            // 
            // btn7
            // 
            btn7.FlatAppearance.BorderSize = 0;
            btn7.ForeColor = Color.FromArgb(23, 32, 32);
            btn7.Location = new Point(3, 62);
            btn7.Name = "btn7";
            btn7.Size = new Size(75, 53);
            btn7.TabIndex = 2;
            btn7.Tag = "7";
            btn7.Text = "7";
            btn7.UseVisualStyleBackColor = true;
            btn7.Click += Digit_Click;
            // 
            // btn6
            // 
            btn6.FlatAppearance.BorderSize = 0;
            btn6.ForeColor = Color.FromArgb(23, 32, 32);
            btn6.Location = new Point(166, 121);
            btn6.Name = "btn6";
            btn6.Size = new Size(75, 53);
            btn6.TabIndex = 3;
            btn6.Tag = "6";
            btn6.Text = "6";
            btn6.UseVisualStyleBackColor = true;
            btn6.Click += Digit_Click;
            // 
            // btn5
            // 
            btn5.FlatAppearance.BorderSize = 0;
            btn5.ForeColor = Color.FromArgb(23, 32, 32);
            btn5.Location = new Point(85, 121);
            btn5.Name = "btn5";
            btn5.Size = new Size(75, 53);
            btn5.TabIndex = 4;
            btn5.Tag = "5";
            btn5.Text = "5";
            btn5.UseVisualStyleBackColor = true;
            btn5.Click += Digit_Click;
            // 
            // btn4
            // 
            btn4.FlatAppearance.BorderSize = 0;
            btn4.ForeColor = Color.FromArgb(23, 32, 32);
            btn4.Location = new Point(3, 121);
            btn4.Name = "btn4";
            btn4.Size = new Size(75, 53);
            btn4.TabIndex = 5;
            btn4.Tag = "4";
            btn4.Text = "4";
            btn4.UseVisualStyleBackColor = true;
            btn4.Click += Digit_Click;
            // 
            // btn3
            // 
            btn3.FlatAppearance.BorderSize = 0;
            btn3.ForeColor = Color.FromArgb(23, 32, 32);
            btn3.Location = new Point(166, 180);
            btn3.Name = "btn3";
            btn3.Size = new Size(75, 53);
            btn3.TabIndex = 6;
            btn3.Tag = "3";
            btn3.Text = "3";
            btn3.UseVisualStyleBackColor = true;
            btn3.Click += Digit_Click;
            // 
            // btn2
            // 
            btn2.FlatAppearance.BorderSize = 0;
            btn2.ForeColor = Color.FromArgb(23, 32, 32);
            btn2.Location = new Point(85, 180);
            btn2.Name = "btn2";
            btn2.Size = new Size(75, 53);
            btn2.TabIndex = 7;
            btn2.Tag = "2";
            btn2.Text = "2";
            btn2.UseVisualStyleBackColor = true;
            btn2.Click += Digit_Click;
            // 
            // btn9
            // 
            btn9.FlatAppearance.BorderSize = 0;
            btn9.ForeColor = Color.FromArgb(23, 32, 32);
            btn9.Location = new Point(165, 62);
            btn9.Name = "btn9";
            btn9.Size = new Size(75, 53);
            btn9.TabIndex = 8;
            btn9.Tag = "9";
            btn9.Text = "9";
            btn9.UseVisualStyleBackColor = true;
            btn9.Click += Digit_Click;
            // 
            // btn0
            // 
            btn0.FlatAppearance.BorderSize = 0;
            btn0.ForeColor = Color.FromArgb(23, 32, 32);
            btn0.Location = new Point(85, 239);
            btn0.Name = "btn0";
            btn0.Size = new Size(75, 53);
            btn0.TabIndex = 10;
            btn0.Tag = "0";
            btn0.Text = "0";
            btn0.UseVisualStyleBackColor = true;
            btn0.Click += Digit_Click;
            // 
            // btnPlus
            // 
            btnPlus.FlatAppearance.BorderSize = 0;
            btnPlus.Font = new Font("Gadugi", 27.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnPlus.ForeColor = Color.FromArgb(255, 128, 0);
            btnPlus.Location = new Point(247, 239);
            btnPlus.Name = "btnPlus";
            btnPlus.Size = new Size(75, 53);
            btnPlus.TabIndex = 11;
            btnPlus.Tag = "+";
            btnPlus.Text = "+";
            btnPlus.UseVisualStyleBackColor = true;
            btnPlus.Click += Op_Click;
            // 
            // btnEquals
            // 
            btnEquals.BackColor = Color.FromArgb(255, 128, 0);
            btnEquals.FlatAppearance.BorderColor = Color.FromArgb(255, 128, 0);
            btnEquals.FlatAppearance.BorderSize = 0;
            btnEquals.FlatStyle = FlatStyle.Flat;
            btnEquals.Font = new Font("Gadugi", 27.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnEquals.ForeColor = Color.White;
            btnEquals.Location = new Point(166, 239);
            btnEquals.Margin = new Padding(0);
            btnEquals.Name = "btnEquals";
            btnEquals.Size = new Size(75, 53);
            btnEquals.TabIndex = 12;
            btnEquals.Tag = "=";
            btnEquals.Text = "=";
            btnEquals.UseVisualStyleBackColor = false;
            // 
            // btnSub
            // 
            btnSub.FlatAppearance.BorderSize = 0;
            btnSub.Font = new Font("Gadugi", 36F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSub.ForeColor = Color.FromArgb(255, 128, 0);
            btnSub.Location = new Point(247, 180);
            btnSub.Name = "btnSub";
            btnSub.Size = new Size(75, 53);
            btnSub.TabIndex = 13;
            btnSub.Tag = "-";
            btnSub.Text = "-";
            btnSub.UseVisualStyleBackColor = true;
            btnSub.Click += Op_Click;
            // 
            // btnMultiply
            // 
            btnMultiply.FlatAppearance.BorderSize = 0;
            btnMultiply.Font = new Font("Gadugi", 27.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnMultiply.ForeColor = Color.FromArgb(255, 128, 0);
            btnMultiply.Location = new Point(246, 121);
            btnMultiply.Name = "btnMultiply";
            btnMultiply.Size = new Size(75, 53);
            btnMultiply.TabIndex = 15;
            btnMultiply.Tag = "*";
            btnMultiply.Text = "×";
            btnMultiply.UseVisualStyleBackColor = true;
            btnMultiply.Click += Op_Click;
            // 
            // btnDevide
            // 
            btnDevide.FlatAppearance.BorderSize = 0;
            btnDevide.Font = new Font("Gadugi", 27.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnDevide.ForeColor = Color.FromArgb(255, 128, 0);
            btnDevide.Location = new Point(246, 62);
            btnDevide.Name = "btnDevide";
            btnDevide.Size = new Size(75, 53);
            btnDevide.TabIndex = 16;
            btnDevide.Tag = "/";
            btnDevide.Text = "÷";
            btnDevide.UseVisualStyleBackColor = true;
            btnDevide.Click += Op_Click;
            // 
            // btnDot
            // 
            btnDot.FlatAppearance.BorderSize = 0;
            btnDot.Font = new Font("Gadugi", 27.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnDot.ForeColor = Color.FromArgb(23, 32, 32);
            btnDot.Location = new Point(3, 239);
            btnDot.Name = "btnDot";
            btnDot.Size = new Size(76, 53);
            btnDot.TabIndex = 17;
            btnDot.Tag = ".";
            btnDot.Text = ".";
            btnDot.UseVisualStyleBackColor = true;
            btnDot.Click += btnDot_Click;
            // 
            // btnAC
            // 
            btnAC.BackColor = Color.FromArgb(32, 32, 32);
            btnAC.FlatAppearance.BorderColor = Color.White;
            btnAC.FlatAppearance.BorderSize = 0;
            btnAC.Font = new Font("Gadugi", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnAC.ForeColor = Color.White;
            btnAC.Location = new Point(4, 3);
            btnAC.Name = "btnAC";
            btnAC.Size = new Size(75, 53);
            btnAC.TabIndex = 18;
            btnAC.Text = "AC";
            btnAC.UseVisualStyleBackColor = false;
            btnAC.Click += btnAC_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.FromArgb(32, 32, 32);
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.Location = new Point(85, 3);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(155, 53);
            btnDelete.TabIndex = 20;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnPercent
            // 
            btnPercent.FlatAppearance.BorderSize = 0;
            btnPercent.Font = new Font("Gadugi", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnPercent.ForeColor = Color.FromArgb(255, 128, 0);
            btnPercent.Location = new Point(246, 3);
            btnPercent.Name = "btnPercent";
            btnPercent.Size = new Size(75, 53);
            btnPercent.TabIndex = 21;
            btnPercent.Tag = "%";
            btnPercent.Text = "%";
            btnPercent.UseVisualStyleBackColor = true;
            btnPercent.Click += Op_Click;
            // 
            // Memory
            // 
            Memory.Controls.Add(lblStorage);
            Memory.Location = new Point(29, 0);
            Memory.Name = "Memory";
            Memory.Size = new Size(317, 80);
            Memory.TabIndex = 22;
            // 
            // lblStorage
            // 
            lblStorage.Location = new Point(3, 42);
            lblStorage.Name = "lblStorage";
            lblStorage.Size = new Size(311, 27);
            lblStorage.TabIndex = 24;
            // 
            // P2
            // 
            P2.BackColor = Color.Black;
            P2.Controls.Add(btnPercent);
            P2.Controls.Add(btnDelete);
            P2.Controls.Add(btnMultiply);
            P2.Controls.Add(btn1);
            P2.Controls.Add(btn4);
            P2.Controls.Add(btn3);
            P2.Controls.Add(btn8);
            P2.Controls.Add(btnDevide);
            P2.Controls.Add(btnSub);
            P2.Controls.Add(btn0);
            P2.Controls.Add(btn5);
            P2.Controls.Add(btn7);
            P2.Controls.Add(btn2);
            P2.Controls.Add(btnPlus);
            P2.Controls.Add(btnDot);
            P2.Controls.Add(btnAC);
            P2.Controls.Add(btnEquals);
            P2.Controls.Add(btn9);
            P2.Controls.Add(btn6);
            P2.Location = new Point(25, 130);
            P2.Name = "P2";
            P2.Size = new Size(325, 298);
            P2.TabIndex = 0;
            // 
            // txtResult
            // 
            txtResult.BackColor = Color.FromArgb(32, 32, 32);
            txtResult.Font = new Font("Gadugi", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtResult.ForeColor = SystemColors.Window;
            txtResult.Location = new Point(28, 86);
            txtResult.Multiline = true;
            txtResult.Name = "txtResult";
            txtResult.ReadOnly = true;
            txtResult.Size = new Size(318, 38);
            txtResult.TabIndex = 23;
            txtResult.Text = "0";
            // 
            // Calculator_des
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(32, 32, 32);
            ClientSize = new Size(373, 430);
            Controls.Add(txtResult);
            Controls.Add(P2);
            Controls.Add(Memory);
            Font = new Font("Gadugi", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            KeyPreview = true;
            Margin = new Padding(4);
            MaximumSize = new Size(389, 469);
            MinimumSize = new Size(389, 469);
            Name = "Calculator_des";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Calculator";
            KeyDown += Calculator_des_KeyDown;
            Memory.ResumeLayout(false);
            P2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn1;
        private System.Windows.Forms.Button btn8;
        private System.Windows.Forms.Button btn7;
        private System.Windows.Forms.Button btn6;
        private System.Windows.Forms.Button btn5;
        private System.Windows.Forms.Button btn4;
        private System.Windows.Forms.Button btn3;
        private System.Windows.Forms.Button btn2;
        private System.Windows.Forms.Button btn9;
        private System.Windows.Forms.Button btn0;
        private System.Windows.Forms.Button btnPlus;
        private System.Windows.Forms.Button btnEquals;
        private System.Windows.Forms.Button btnSub;
        private System.Windows.Forms.Button btnMultiply;
        private System.Windows.Forms.Button btnDevide;
        private System.Windows.Forms.Button btnDot;
        private System.Windows.Forms.Button btnAC;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnPercent;
        private System.Windows.Forms.Panel Memory;
        private System.Windows.Forms.Panel P2;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Label lblStorage;
    }
}