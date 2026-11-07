namespace PharmacyCoursework
{
    partial class AuthForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            textBoxLogin = new TextBox();
            textBoxPass = new TextBox();
            label2 = new Label();
            label3 = new Label();
            checkBox1 = new CheckBox();
            button1 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15F);
            label1.Location = new Point(640, 209);
            label1.Name = "label1";
            label1.Size = new Size(105, 41);
            label1.TabIndex = 0;
            label1.Text = "LOGIN";
            // 
            // textBoxLogin
            // 
            textBoxLogin.Location = new Point(573, 349);
            textBoxLogin.Name = "textBoxLogin";
            textBoxLogin.Size = new Size(237, 31);
            textBoxLogin.TabIndex = 1;
            // 
            // textBoxPass
            // 
            textBoxPass.Location = new Point(573, 432);
            textBoxPass.Name = "textBoxPass";
            textBoxPass.PasswordChar = '*';
            textBoxPass.Size = new Size(237, 31);
            textBoxPass.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(573, 404);
            label2.Name = "label2";
            label2.Size = new Size(87, 25);
            label2.TabIndex = 3;
            label2.Text = "Password";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(573, 321);
            label3.Name = "label3";
            label3.Size = new Size(56, 25);
            label3.TabIndex = 4;
            label3.Text = "Login";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(573, 469);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(184, 29);
            checkBox1.TabIndex = 5;
            checkBox1.Text = "Keep me signed in";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(573, 531);
            button1.Name = "button1";
            button1.Size = new Size(237, 34);
            button1.TabIndex = 6;
            button1.Text = "Sign in";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // AuthForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1378, 828);
            Controls.Add(button1);
            Controls.Add(checkBox1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(textBoxPass);
            Controls.Add(textBoxLogin);
            Controls.Add(label1);
            MaximizeBox = false;
            MaximumSize = new Size(1400, 884);
            MinimumSize = new Size(1400, 884);
            Name = "AuthForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Authorise";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBoxLogin;
        private TextBox textBoxPass;
        private Label label2;
        private Label label3;
        private CheckBox checkBox1;
        private Button button1;
    }
}
