using Calculator.Calculator.UI.Tools;
using Calculator.Calculator.UI.Tools.HistoryHelper;

namespace Calculator.Calculator.UI.Forms
{
    partial class Calculator_des : Form
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
            components = new System.ComponentModel.Container();
            txtStorage = new RichTextBox();
            txtResult = new RichTextBox();
            label1 = new Label();
            btn6 = new RoundedButton();
            btn9 = new RoundedButton();
            btnEquals = new RoundedButton();
            btnDot = new RoundedButton();
            btnPlus = new RoundedButton();
            btn2 = new RoundedButton();
            btn7 = new RoundedButton();
            btn5 = new RoundedButton();
            btn0 = new RoundedButton();
            btnSub = new RoundedButton();
            btnDevide = new RoundedButton();
            btn8 = new RoundedButton();
            btn3 = new RoundedButton();
            btn4 = new RoundedButton();
            btn1 = new RoundedButton();
            btnMultiply = new RoundedButton();
            btnPercent = new RoundedButton();
            btnBackSpace = new RoundedButton();
            btnC = new RoundedButton();
            btnCE = new RoundedButton();
            btnHistory = new RoundedButton();
            MessageForHistory = new ToolTip(components);
            historyView = new HistoryView();
            SuspendLayout();
            // 
            // txtStorage
            // 
            txtStorage.BackColor = Color.Black;
            txtStorage.BorderStyle = BorderStyle.None;
            txtStorage.Font = new Font("Segoe UI", 12F);
            txtStorage.ForeColor = SystemColors.Window;
            txtStorage.Location = new Point(26, 36);
            txtStorage.Margin = new Padding(4);
            txtStorage.MaximumSize = new Size(560, 866);
            txtStorage.Multiline = false;
            txtStorage.Name = "txtStorage";
            txtStorage.ReadOnly = true;
            txtStorage.ScrollBars = RichTextBoxScrollBars.Horizontal;
            txtStorage.Size = new Size(323, 39);
            txtStorage.TabIndex = 999;
            txtStorage.TabStop = false;
            txtStorage.Text = "";
            txtStorage.WordWrap = false;
            // 
            // txtResult
            // 
            txtResult.BackColor = Color.FromArgb(32, 32, 32);
            txtResult.Font = new Font("Segoe UI", 18F);
            txtResult.ForeColor = SystemColors.Window;
            txtResult.Location = new Point(26, 83);
            txtResult.Margin = new Padding(4);
            txtResult.Multiline = false;
            txtResult.Name = "txtResult";
            txtResult.ReadOnly = true;
            txtResult.ScrollBars = RichTextBoxScrollBars.None;
            txtResult.Size = new Size(323, 39);
            txtResult.TabIndex = 999;
            txtResult.TabStop = false;
            txtResult.Text = "0";
            txtResult.WordWrap = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F);
            label1.ForeColor = Color.Transparent;
            label1.Location = new Point(-4, 86);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(30, 32);
            label1.TabIndex = 999;
            label1.Text = "=";
            // 
            // btn6
            // 
            btn6.BackColor = Color.FromArgb(64, 64, 64);
            btn6.FlatStyle = FlatStyle.Flat;
            btn6.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold);
            btn6.ForeColor = Color.White;
            btn6.Location = new Point(191, 268);
            btn6.Margin = new Padding(8);
            btn6.Name = "btn6";
            btn6.Padding = new Padding(8);
            btn6.Roundness = 0.65F;
            btn6.Size = new Size(75, 63);
            btn6.TabIndex = 11;
            btn6.TabStop = false;
            btn6.Tag = "6";
            btn6.Text = "6";
            btn6.UseVisualStyleBackColor = false;
            btn6.Click += Digit_Click;
            // 
            // btn9
            // 
            btn9.BackColor = Color.FromArgb(64, 64, 64);
            btn9.FlatStyle = FlatStyle.Flat;
            btn9.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold);
            btn9.ForeColor = Color.White;
            btn9.Location = new Point(191, 199);
            btn9.Margin = new Padding(8);
            btn9.Name = "btn9";
            btn9.Padding = new Padding(8);
            btn9.Roundness = 0.65F;
            btn9.Size = new Size(75, 63);
            btn9.TabIndex = 7;
            btn9.TabStop = false;
            btn9.Tag = "9";
            btn9.Text = "9";
            btn9.UseVisualStyleBackColor = false;
            btn9.Click += Digit_Click;
            // 
            // btnEquals
            // 
            btnEquals.BackColor = Color.DarkOrange;
            btnEquals.FlatStyle = FlatStyle.Flat;
            btnEquals.Font = new Font("Segoe UI", 27.75F);
            btnEquals.ForeColor = Color.White;
            btnEquals.Location = new Point(191, 404);
            btnEquals.Margin = new Padding(0);
            btnEquals.Name = "btnEquals";
            btnEquals.Roundness = 0.65F;
            btnEquals.Size = new Size(75, 63);
            btnEquals.TabIndex = 19;
            btnEquals.TabStop = false;
            btnEquals.Tag = "=";
            btnEquals.Text = "=";
            btnEquals.UseVisualStyleBackColor = false;
            btnEquals.Click += btnEquals_Click;
            // 
            // btnDot
            // 
            btnDot.BackColor = Color.FromArgb(64, 64, 64);
            btnDot.FlatStyle = FlatStyle.Flat;
            btnDot.Font = new Font("Segoe UI", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDot.ForeColor = Color.White;
            btnDot.Location = new Point(25, 404);
            btnDot.Margin = new Padding(8);
            btnDot.Name = "btnDot";
            btnDot.Padding = new Padding(8, 4, 4, 0);
            btnDot.Roundness = 0.65F;
            btnDot.Size = new Size(75, 63);
            btnDot.TabIndex = 17;
            btnDot.TabStop = false;
            btnDot.Tag = ".";
            btnDot.Text = ".";
            btnDot.UseVisualStyleBackColor = false;
            btnDot.Click += btnDot_Click;
            // 
            // btnPlus
            // 
            btnPlus.BackColor = Color.FromArgb(64, 64, 64);
            btnPlus.FlatStyle = FlatStyle.Flat;
            btnPlus.Font = new Font("Segoe UI", 27.75F);
            btnPlus.ForeColor = Color.DarkOrange;
            btnPlus.Location = new Point(274, 404);
            btnPlus.Margin = new Padding(8, 0, 8, 0);
            btnPlus.Name = "btnPlus";
            btnPlus.Padding = new Padding(8, 4, 8, 0);
            btnPlus.Roundness = 0.65F;
            btnPlus.Size = new Size(75, 63);
            btnPlus.TabIndex = 20;
            btnPlus.TabStop = false;
            btnPlus.Tag = "+";
            btnPlus.Text = "+";
            btnPlus.UseVisualStyleBackColor = false;
            btnPlus.Click += Op_Click;
            // 
            // btn2
            // 
            btn2.BackColor = Color.FromArgb(64, 64, 64);
            btn2.FlatStyle = FlatStyle.Flat;
            btn2.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold);
            btn2.ForeColor = Color.White;
            btn2.Location = new Point(108, 336);
            btn2.Margin = new Padding(8);
            btn2.Name = "btn2";
            btn2.Padding = new Padding(8);
            btn2.Roundness = 0.65F;
            btn2.Size = new Size(75, 63);
            btn2.TabIndex = 14;
            btn2.TabStop = false;
            btn2.Tag = "2";
            btn2.Text = "2";
            btn2.UseVisualStyleBackColor = false;
            btn2.Click += Digit_Click;
            // 
            // btn7
            // 
            btn7.BackColor = Color.FromArgb(64, 64, 64);
            btn7.FlatStyle = FlatStyle.Flat;
            btn7.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold);
            btn7.ForeColor = Color.White;
            btn7.Location = new Point(25, 199);
            btn7.Margin = new Padding(8);
            btn7.Name = "btn7";
            btn7.Padding = new Padding(8);
            btn7.Roundness = 0.65F;
            btn7.Size = new Size(75, 63);
            btn7.TabIndex = 5;
            btn7.TabStop = false;
            btn7.Tag = "7";
            btn7.Text = "7";
            btn7.UseVisualStyleBackColor = false;
            btn7.Click += Digit_Click;
            // 
            // btn5
            // 
            btn5.BackColor = Color.FromArgb(64, 64, 64);
            btn5.FlatStyle = FlatStyle.Flat;
            btn5.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold);
            btn5.ForeColor = Color.White;
            btn5.Location = new Point(108, 268);
            btn5.Margin = new Padding(8);
            btn5.Name = "btn5";
            btn5.Padding = new Padding(8);
            btn5.Roundness = 0.65F;
            btn5.Size = new Size(75, 63);
            btn5.TabIndex = 10;
            btn5.TabStop = false;
            btn5.Tag = "5";
            btn5.Text = "5";
            btn5.UseVisualStyleBackColor = false;
            btn5.Click += Digit_Click;
            // 
            // btn0
            // 
            btn0.BackColor = Color.FromArgb(64, 64, 64);
            btn0.FlatStyle = FlatStyle.Flat;
            btn0.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold);
            btn0.ForeColor = Color.White;
            btn0.Location = new Point(108, 404);
            btn0.Margin = new Padding(8);
            btn0.Name = "btn0";
            btn0.Padding = new Padding(8);
            btn0.Roundness = 0.65F;
            btn0.Size = new Size(75, 63);
            btn0.TabIndex = 18;
            btn0.TabStop = false;
            btn0.Tag = "0";
            btn0.Text = "0";
            btn0.UseVisualStyleBackColor = false;
            btn0.Click += Digit_Click;
            // 
            // btnSub
            // 
            btnSub.BackColor = Color.FromArgb(64, 64, 64);
            btnSub.FlatStyle = FlatStyle.Flat;
            btnSub.Font = new Font("Segoe UI", 27.75F);
            btnSub.ForeColor = Color.DarkOrange;
            btnSub.Location = new Point(274, 336);
            btnSub.Margin = new Padding(8, 0, 8, 0);
            btnSub.Name = "btnSub";
            btnSub.Padding = new Padding(8, 4, 8, 0);
            btnSub.Roundness = 0.65F;
            btnSub.Size = new Size(75, 63);
            btnSub.TabIndex = 16;
            btnSub.TabStop = false;
            btnSub.Tag = "-";
            btnSub.Text = "−";
            btnSub.UseVisualStyleBackColor = false;
            btnSub.Click += Op_Click;
            // 
            // btnDevide
            // 
            btnDevide.BackColor = Color.FromArgb(64, 64, 64);
            btnDevide.FlatStyle = FlatStyle.Flat;
            btnDevide.Font = new Font("Segoe UI", 27.75F);
            btnDevide.ForeColor = Color.DarkOrange;
            btnDevide.Location = new Point(274, 199);
            btnDevide.Margin = new Padding(8, 0, 8, 0);
            btnDevide.Name = "btnDevide";
            btnDevide.Padding = new Padding(8, 4, 8, 0);
            btnDevide.Roundness = 0.65F;
            btnDevide.Size = new Size(75, 63);
            btnDevide.TabIndex = 8;
            btnDevide.TabStop = false;
            btnDevide.Tag = "/";
            btnDevide.Text = "÷";
            btnDevide.UseVisualStyleBackColor = false;
            btnDevide.Click += Op_Click;
            // 
            // btn8
            // 
            btn8.BackColor = Color.FromArgb(64, 64, 64);
            btn8.FlatStyle = FlatStyle.Flat;
            btn8.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold);
            btn8.ForeColor = Color.White;
            btn8.Location = new Point(108, 199);
            btn8.Margin = new Padding(8);
            btn8.Name = "btn8";
            btn8.Padding = new Padding(8);
            btn8.Roundness = 0.65F;
            btn8.Size = new Size(75, 63);
            btn8.TabIndex = 6;
            btn8.TabStop = false;
            btn8.Tag = "8";
            btn8.Text = "8";
            btn8.UseVisualStyleBackColor = false;
            btn8.Click += Digit_Click;
            // 
            // btn3
            // 
            btn3.BackColor = Color.FromArgb(64, 64, 64);
            btn3.FlatStyle = FlatStyle.Flat;
            btn3.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold);
            btn3.ForeColor = Color.White;
            btn3.Location = new Point(191, 336);
            btn3.Margin = new Padding(8);
            btn3.Name = "btn3";
            btn3.Padding = new Padding(8);
            btn3.Roundness = 0.65F;
            btn3.Size = new Size(75, 63);
            btn3.TabIndex = 15;
            btn3.TabStop = false;
            btn3.Tag = "3";
            btn3.Text = "3";
            btn3.UseVisualStyleBackColor = false;
            btn3.Click += Digit_Click;
            // 
            // btn4
            // 
            btn4.BackColor = Color.FromArgb(64, 64, 64);
            btn4.FlatStyle = FlatStyle.Flat;
            btn4.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold);
            btn4.ForeColor = Color.White;
            btn4.Location = new Point(25, 268);
            btn4.Margin = new Padding(8);
            btn4.Name = "btn4";
            btn4.Padding = new Padding(8);
            btn4.Roundness = 0.65F;
            btn4.Size = new Size(75, 63);
            btn4.TabIndex = 9;
            btn4.TabStop = false;
            btn4.Tag = "4";
            btn4.Text = "4";
            btn4.UseVisualStyleBackColor = false;
            btn4.Click += Digit_Click;
            // 
            // btn1
            // 
            btn1.BackColor = Color.FromArgb(64, 64, 64);
            btn1.FlatStyle = FlatStyle.Flat;
            btn1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold);
            btn1.ForeColor = Color.White;
            btn1.Location = new Point(25, 336);
            btn1.Margin = new Padding(8);
            btn1.Name = "btn1";
            btn1.Padding = new Padding(8);
            btn1.Roundness = 0.65F;
            btn1.Size = new Size(75, 63);
            btn1.TabIndex = 13;
            btn1.TabStop = false;
            btn1.Tag = "1";
            btn1.Text = "1";
            btn1.UseVisualStyleBackColor = false;
            btn1.Click += Digit_Click;
            // 
            // btnMultiply
            // 
            btnMultiply.BackColor = Color.FromArgb(64, 64, 64);
            btnMultiply.FlatStyle = FlatStyle.Flat;
            btnMultiply.Font = new Font("Segoe UI", 27.75F);
            btnMultiply.ForeColor = Color.DarkOrange;
            btnMultiply.Location = new Point(274, 268);
            btnMultiply.Margin = new Padding(8, 0, 8, 0);
            btnMultiply.Name = "btnMultiply";
            btnMultiply.Padding = new Padding(8, 4, 8, 0);
            btnMultiply.Roundness = 0.65F;
            btnMultiply.Size = new Size(75, 63);
            btnMultiply.TabIndex = 12;
            btnMultiply.TabStop = false;
            btnMultiply.Tag = "*";
            btnMultiply.Text = "×";
            btnMultiply.UseVisualStyleBackColor = false;
            btnMultiply.Click += Op_Click;
            // 
            // btnPercent
            // 
            btnPercent.BackColor = Color.FromArgb(64, 64, 64);
            btnPercent.FlatStyle = FlatStyle.Flat;
            btnPercent.Font = new Font("Segoe UI", 27.75F);
            btnPercent.ForeColor = Color.DarkOrange;
            btnPercent.Location = new Point(274, 130);
            btnPercent.Margin = new Padding(8, 0, 8, 0);
            btnPercent.Name = "btnPercent";
            btnPercent.Padding = new Padding(8, 4, 8, 0);
            btnPercent.Roundness = 0.65F;
            btnPercent.Size = new Size(75, 63);
            btnPercent.TabIndex = 4;
            btnPercent.TabStop = false;
            btnPercent.Tag = "%";
            btnPercent.Text = "%";
            btnPercent.UseVisualStyleBackColor = false;
            btnPercent.Click += btnPercent_Click;
            // 
            // btnBackSpace
            // 
            btnBackSpace.BackColor = Color.FromArgb(60, 60, 60);
            btnBackSpace.FlatStyle = FlatStyle.Flat;
            btnBackSpace.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            btnBackSpace.ForeColor = Color.DarkOrange;
            btnBackSpace.Location = new Point(191, 130);
            btnBackSpace.Margin = new Padding(4);
            btnBackSpace.Name = "btnBackSpace";
            btnBackSpace.Padding = new Padding(8, 4, 8, 50);
            btnBackSpace.Roundness = 0.65F;
            btnBackSpace.Size = new Size(75, 63);
            btnBackSpace.TabIndex = 3;
            btnBackSpace.TabStop = false;
            btnBackSpace.Text = "⟵";
            btnBackSpace.TextAlign = ContentAlignment.TopCenter;
            btnBackSpace.UseVisualStyleBackColor = false;
            btnBackSpace.Click += btnBackSpace_Click;
            // 
            // btnC
            // 
            btnC.BackColor = Color.FromArgb(60, 60, 60);
            btnC.FlatStyle = FlatStyle.Flat;
            btnC.Font = new Font("Segoe UI", 21.75F);
            btnC.ForeColor = Color.DarkOrange;
            btnC.Location = new Point(25, 130);
            btnC.Name = "btnC";
            btnC.Roundness = 0.65F;
            btnC.Size = new Size(75, 63);
            btnC.TabIndex = 1;
            btnC.TabStop = false;
            btnC.Text = "C";
            btnC.UseVisualStyleBackColor = false;
            btnC.Click += btnAC_Click;
            // 
            // btnCE
            // 
            btnCE.BackColor = Color.FromArgb(60, 60, 60);
            btnCE.FlatStyle = FlatStyle.Flat;
            btnCE.Font = new Font("Segoe UI", 21.75F);
            btnCE.ForeColor = Color.DarkOrange;
            btnCE.Location = new Point(107, 130);
            btnCE.Margin = new Padding(4);
            btnCE.Name = "btnCE";
            btnCE.Roundness = 0.65F;
            btnCE.Size = new Size(75, 63);
            btnCE.TabIndex = 2;
            btnCE.TabStop = false;
            btnCE.Text = "CE";
            btnCE.UseVisualStyleBackColor = false;
            btnCE.Click += btnCE_Click;
            // 
            // btnHistory
            // 
            btnHistory.BackColor = Color.Black;
            btnHistory.FlatStyle = FlatStyle.Flat;
            btnHistory.Font = new Font("Segoe UI", 21.75F);
            btnHistory.ForeColor = Color.DarkOrange;
            btnHistory.Location = new Point(321, 2);
            btnHistory.Name = "btnHistory";
            btnHistory.Roundness = 0.65F;
            btnHistory.Size = new Size(40, 37);
            btnHistory.TabIndex = 1000;
            btnHistory.TabStop = false;
            btnHistory.Text = "🕒";
            btnHistory.UseVisualStyleBackColor = false;
            btnHistory.Click += btnHistory_Click;
            // 
            // MessageForHistory
            // 
            MessageForHistory.AutoPopDelay = 4000;
            MessageForHistory.BackColor = Color.Black;
            MessageForHistory.ForeColor = Color.White;
            MessageForHistory.InitialDelay = 300;
            MessageForHistory.IsBalloon = true;
            MessageForHistory.ReshowDelay = 100;
            MessageForHistory.ToolTipIcon = ToolTipIcon.Info;
            // 
            // historyView
            // 
            historyView.BackColor = Color.Black;
            historyView.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            historyView.Location = new Point(0, 0);
            historyView.Margin = new Padding(5);
            historyView.Name = "historyView";
            historyView.Size = new Size(364, 355);
            historyView.TabIndex = 0;
            // 
            // Calculator_des
            // 
            AutoScaleDimensions = new SizeF(13F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(360, 471);
            Controls.Add(btnHistory);
            Controls.Add(btnC);
            Controls.Add(btnBackSpace);
            Controls.Add(btnCE);
            Controls.Add(label1);
            Controls.Add(btnPercent);
            Controls.Add(txtResult);
            Controls.Add(btnMultiply);
            Controls.Add(txtStorage);
            Controls.Add(btn1);
            Controls.Add(btn4);
            Controls.Add(btn3);
            Controls.Add(btn8);
            Controls.Add(btn6);
            Controls.Add(btnDevide);
            Controls.Add(btn9);
            Controls.Add(btnSub);
            Controls.Add(btnEquals);
            Controls.Add(btn0);
            Controls.Add(btnDot);
            Controls.Add(btn5);
            Controls.Add(btnPlus);
            Controls.Add(btn7);
            Controls.Add(btn2);
            Font = new Font("Segoe UI", 15.75F, FontStyle.Bold);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            KeyPreview = true;
            Margin = new Padding(6, 4, 6, 4);
            MaximizeBox = false;
            Name = "Calculator_des";
            Padding = new Padding(8);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Calculator";
            Load += Calculator_des_Load;
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
        private RichTextBox txtResult;
        private Label label1;
        private RichTextBox txtStorage;
        private RoundedButton btn6;
        private RoundedButton btn9;
        private RoundedButton btnEquals;
        private RoundedButton btnDot;
        private RoundedButton btnPlus;
        private RoundedButton btn2;
        private RoundedButton btn7;
        private RoundedButton btn5;
        private RoundedButton btn0;
        private RoundedButton btnSub;
        private RoundedButton btnDevide;
        private RoundedButton btn8;
        private RoundedButton btn3;
        private RoundedButton btn4;
        private RoundedButton btn1;
        private RoundedButton btnMultiply;
        private RoundedButton btnPercent;
        private RoundedButton btnBackSpace;
        private RoundedButton btnC;
        private RoundedButton btnCE;
        private RoundedButton btnHistory;
        private ToolTip MessageForHistory;
        private HistoryView historyView;
    }
}