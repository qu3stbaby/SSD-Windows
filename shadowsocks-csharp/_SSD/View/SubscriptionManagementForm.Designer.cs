namespace Shadowsocks.View
{
    partial class SubscriptionManagementForm
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
            this.ListBox_subscription = new System.Windows.Forms.ListBox();
            this.TextBox_url = new System.Windows.Forms.TextBox();
            this.Button_add = new System.Windows.Forms.Button();
            this.Label_url = new System.Windows.Forms.Label();
            this.Label_name = new System.Windows.Forms.Label();
            this.TextBox_name = new System.Windows.Forms.TextBox();
            this.Button_delete = new System.Windows.Forms.Button();
            this.Button_save = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ListBox_subscription
            // 
            this.ListBox_subscription.FormattingEnabled = true;
            this.ListBox_subscription.ItemHeight = 25;
            this.ListBox_subscription.Location = new System.Drawing.Point(12, 12);
            this.ListBox_subscription.Name = "ListBox_subscription";
            this.ListBox_subscription.Size = new System.Drawing.Size(263, 254);
            this.ListBox_subscription.TabIndex = 0;
            this.ListBox_subscription.SelectedIndexChanged += new System.EventHandler(this.SubscriptionSelected);
            // 
            // TextBox_url
            // 
            this.TextBox_url.Location = new System.Drawing.Point(466, 12);
            this.TextBox_url.Name = "TextBox_url";
            this.TextBox_url.Size = new System.Drawing.Size(376, 31);
            this.TextBox_url.TabIndex = 1;
            // 
            // Button_add
            // 
            this.Button_add.Location = new System.Drawing.Point(466, 109);
            this.Button_add.Name = "Button_add";
            this.Button_add.Size = new System.Drawing.Size(100, 46);
            this.Button_add.TabIndex = 2;
            this.Button_add.Text = "&Add";
            this.Button_add.UseVisualStyleBackColor = true;
            this.Button_add.Click += new System.EventHandler(this.AddSubscription);
            // 
            // Label_url
            // 
            this.Label_url.AutoSize = true;
            this.Label_url.Location = new System.Drawing.Point(281, 15);
            this.Label_url.Name = "Label_url";
            this.Label_url.Size = new System.Drawing.Size(179, 25);
            this.Label_url.TabIndex = 3;
            this.Label_url.Text = "Subscription URL";
            // 
            // Label_name
            // 
            this.Label_name.AutoSize = true;
            this.Label_name.Location = new System.Drawing.Point(281, 64);
            this.Label_name.Name = "Label_name";
            this.Label_name.Size = new System.Drawing.Size(137, 25);
            this.Label_name.TabIndex = 5;
            this.Label_name.Text = "Airport Name";
            // 
            // TextBox_name
            // 
            this.TextBox_name.ForeColor = System.Drawing.Color.Gray;
            this.TextBox_name.Location = new System.Drawing.Point(466, 61);
            this.TextBox_name.Name = "TextBox_name";
            this.TextBox_name.Size = new System.Drawing.Size(376, 31);
            this.TextBox_name.TabIndex = 6;
            this.TextBox_name.Text = "(Auto)";
            this.TextBox_name.Enter += new System.EventHandler(this.NameEntered);
            this.TextBox_name.Leave += new System.EventHandler(this.NameLeaved);
            // 
            // Button_delete
            // 
            this.Button_delete.Location = new System.Drawing.Point(742, 109);
            this.Button_delete.Name = "Button_delete";
            this.Button_delete.Size = new System.Drawing.Size(100, 46);
            this.Button_delete.TabIndex = 7;
            this.Button_delete.Text = "&Delete";
            this.Button_delete.UseVisualStyleBackColor = true;
            this.Button_delete.Click += new System.EventHandler(this.DeleteSubscription);
            // 
            // Button_save
            // 
            this.Button_save.Enabled = false;
            this.Button_save.Location = new System.Drawing.Point(604, 109);
            this.Button_save.Name = "Button_save";
            this.Button_save.Size = new System.Drawing.Size(100, 46);
            this.Button_save.TabIndex = 8;
            this.Button_save.Text = "&Save";
            this.Button_save.UseVisualStyleBackColor = true;
            this.Button_save.Click += new System.EventHandler(this.SaveSubscription);
            // 
            // SubscriptionManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 278);
            this.Controls.Add(this.TextBox_name);
            this.Controls.Add(this.Label_name);
            this.Controls.Add(this.Label_url);
            this.Controls.Add(this.TextBox_url);
            this.Controls.Add(this.ListBox_subscription);
            this.Controls.Add(this.Button_save);
            this.Controls.Add(this.Button_delete);
            this.Controls.Add(this.Button_add);
            this.MaximizeBox = false;
            this.Name = "SubscriptionManagementForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Subscription Management";
            this.Load += new System.EventHandler(this.LoadSubscriptionManage);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox ListBox_subscription;
        private System.Windows.Forms.TextBox TextBox_url;
        private System.Windows.Forms.Button Button_add;
        private System.Windows.Forms.Label Label_url;
        private System.Windows.Forms.Label Label_name;
        private System.Windows.Forms.TextBox TextBox_name;
        private System.Windows.Forms.Button Button_delete;
        private System.Windows.Forms.Button Button_save;
    }
}