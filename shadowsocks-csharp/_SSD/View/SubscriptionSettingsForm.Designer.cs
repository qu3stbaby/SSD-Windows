namespace Shadowsocks.View
{
    partial class SubscriptionSettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SubscriptionSettingsForm));
            this.ListBox_subscription = new System.Windows.Forms.ListBox();
            this.TextBox_url = new System.Windows.Forms.TextBox();
            this.Button_add = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
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
            this.ListBox_subscription.SelectedIndexChanged += new System.EventHandler(this.ListBox_subscription_SelectedIndexChanged);
            // 
            // TextBox_url
            // 
            this.TextBox_url.Location = new System.Drawing.Point(443, 12);
            this.TextBox_url.Name = "TextBox_url";
            this.TextBox_url.Size = new System.Drawing.Size(283, 31);
            this.TextBox_url.TabIndex = 1;
            // 
            // Button_add
            // 
            this.Button_add.Location = new System.Drawing.Point(445, 133);
            this.Button_add.Name = "Button_add";
            this.Button_add.Size = new System.Drawing.Size(87, 46);
            this.Button_add.TabIndex = 2;
            this.Button_add.Text = "Add";
            this.Button_add.UseVisualStyleBackColor = true;
            this.Button_add.Click += new System.EventHandler(this.Button_add_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(281, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Subscribe URL";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(281, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 25);
            this.label2.TabIndex = 5;
            this.label2.Text = "Airport Name";
            // 
            // TextBox_name
            // 
            this.TextBox_name.ForeColor = System.Drawing.Color.Gray;
            this.TextBox_name.Location = new System.Drawing.Point(443, 61);
            this.TextBox_name.Name = "TextBox_name";
            this.TextBox_name.Size = new System.Drawing.Size(283, 31);
            this.TextBox_name.TabIndex = 6;
            this.TextBox_name.Text = "(Auto)";
            this.TextBox_name.Enter += new System.EventHandler(this.TextBox_name_Enter);
            this.TextBox_name.Leave += new System.EventHandler(this.TextBox_name_Leave);
            // 
            // Button_delete
            // 
            this.Button_delete.Location = new System.Drawing.Point(639, 133);
            this.Button_delete.Name = "Button_delete";
            this.Button_delete.Size = new System.Drawing.Size(87, 46);
            this.Button_delete.TabIndex = 7;
            this.Button_delete.Text = "Delete";
            this.Button_delete.UseVisualStyleBackColor = true;
            this.Button_delete.Click += new System.EventHandler(this.Button_delete_Click);
            // 
            // Button_save
            // 
            this.Button_save.Enabled = false;
            this.Button_save.Location = new System.Drawing.Point(542, 133);
            this.Button_save.Name = "Button_save";
            this.Button_save.Size = new System.Drawing.Size(87, 46);
            this.Button_save.TabIndex = 8;
            this.Button_save.Text = "Save";
            this.Button_save.UseVisualStyleBackColor = true;
            this.Button_save.Click += new System.EventHandler(this.Button_save_Click);
            // 
            // SubscriptionSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 278);
            this.Controls.Add(this.Button_save);
            this.Controls.Add(this.Button_delete);
            this.Controls.Add(this.TextBox_name);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Button_add);
            this.Controls.Add(this.TextBox_url);
            this.Controls.Add(this.ListBox_subscription);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SubscriptionSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Subscription Settings";
            this.Load += new System.EventHandler(this.SubscriptionManageForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox ListBox_subscription;
        private System.Windows.Forms.TextBox TextBox_url;
        private System.Windows.Forms.Button Button_add;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TextBox_name;
        private System.Windows.Forms.Button Button_delete;
        private System.Windows.Forms.Button Button_save;
    }
}