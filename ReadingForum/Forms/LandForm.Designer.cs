namespace ReadingForum
{
    partial class LandForm
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
            this.ACC_Label = new System.Windows.Forms.Label();
            this.PSW_Label = new System.Windows.Forms.Label();
            this.Acc_textBox = new System.Windows.Forms.TextBox();
            this.Psw_textBox = new System.Windows.Forms.TextBox();
            this.Login_btn = new System.Windows.Forms.Button();
            this.Reg_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ACC_Label
            // 
            this.ACC_Label.AutoSize = true;
            this.ACC_Label.Font = new System.Drawing.Font("方正粗黑宋简体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ACC_Label.Location = new System.Drawing.Point(127, 118);
            this.ACC_Label.Name = "ACC_Label";
            this.ACC_Label.Size = new System.Drawing.Size(112, 25);
            this.ACC_Label.TabIndex = 0;
            this.ACC_Label.Text = "手机号码：";
            // 
            // PSW_Label
            // 
            this.PSW_Label.AutoSize = true;
            this.PSW_Label.Font = new System.Drawing.Font("方正粗黑宋简体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PSW_Label.Location = new System.Drawing.Point(167, 200);
            this.PSW_Label.Name = "PSW_Label";
            this.PSW_Label.Size = new System.Drawing.Size(72, 25);
            this.PSW_Label.TabIndex = 0;
            this.PSW_Label.Text = "密码：";
            // 
            // Acc_textBox
            // 
            this.Acc_textBox.Font = new System.Drawing.Font("方正粗黑宋简体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Acc_textBox.Location = new System.Drawing.Point(246, 115);
            this.Acc_textBox.Name = "Acc_textBox";
            this.Acc_textBox.Size = new System.Drawing.Size(198, 32);
            this.Acc_textBox.TabIndex = 1;
            // 
            // Psw_textBox
            // 
            this.Psw_textBox.Font = new System.Drawing.Font("方正粗黑宋简体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Psw_textBox.Location = new System.Drawing.Point(246, 203);
            this.Psw_textBox.Name = "Psw_textBox";
            this.Psw_textBox.Size = new System.Drawing.Size(198, 32);
            this.Psw_textBox.TabIndex = 1;
            // 
            // Login_btn
            // 
            this.Login_btn.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Login_btn.Font = new System.Drawing.Font("方正粗黑宋简体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Login_btn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Login_btn.Location = new System.Drawing.Point(162, 315);
            this.Login_btn.Name = "Login_btn";
            this.Login_btn.Size = new System.Drawing.Size(77, 38);
            this.Login_btn.TabIndex = 2;
            this.Login_btn.Text = "登录";
            this.Login_btn.UseVisualStyleBackColor = false;
            this.Login_btn.Click += new System.EventHandler(this.Login_btn_Click);
            // 
            // Reg_btn
            // 
            this.Reg_btn.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Reg_btn.Font = new System.Drawing.Font("方正粗黑宋简体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Reg_btn.Location = new System.Drawing.Point(384, 315);
            this.Reg_btn.Name = "Reg_btn";
            this.Reg_btn.Size = new System.Drawing.Size(77, 38);
            this.Reg_btn.TabIndex = 2;
            this.Reg_btn.Text = "注册";
            this.Reg_btn.UseVisualStyleBackColor = false;
            this.Reg_btn.Click += new System.EventHandler(this.Reg_btn_Click);
            // 
            // LandForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 450);
            this.Controls.Add(this.Reg_btn);
            this.Controls.Add(this.Login_btn);
            this.Controls.Add(this.Psw_textBox);
            this.Controls.Add(this.Acc_textBox);
            this.Controls.Add(this.PSW_Label);
            this.Controls.Add(this.ACC_Label);
            this.Name = "LandForm";
            this.Text = "LandForm";
            this.Load += new System.EventHandler(this.LandForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ACC_Label;
        private System.Windows.Forms.Label PSW_Label;
        private System.Windows.Forms.TextBox Acc_textBox;
        private System.Windows.Forms.TextBox Psw_textBox;
        private System.Windows.Forms.Button Login_btn;
        private System.Windows.Forms.Button Reg_btn;
    }
}