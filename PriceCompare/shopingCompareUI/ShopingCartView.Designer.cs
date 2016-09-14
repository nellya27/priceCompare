namespace shopingCompareUI
{
    partial class ShopingCartView
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
            this.productList = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.TotalCostLabel = new System.Windows.Forms.Label();
            this.totalPrice = new System.Windows.Forms.Button();
            this.clearAllButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // productList
            // 
            this.productList.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.productList.FormattingEnabled = true;
            this.productList.ItemHeight = 16;
            this.productList.Location = new System.Drawing.Point(12, 128);
            this.productList.Name = "productList";
            this.productList.Size = new System.Drawing.Size(459, 276);
            this.productList.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel1.Controls.Add(this.TotalCostLabel);
            this.panel1.Controls.Add(this.totalPrice);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.clearAllButton);
            this.panel1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(483, 101);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(325, 66);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(151, 26);
            this.label1.TabIndex = 5;
            this.label1.Text = "סה\"כ לתשלום:";
            this.label1.Visible = false;
            // 
            // TotalCostLabel
            // 
            this.TotalCostLabel.AutoSize = true;
            this.TotalCostLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.TotalCostLabel.Location = new System.Drawing.Point(94, 66);
            this.TotalCostLabel.Name = "TotalCostLabel";
            this.TotalCostLabel.Size = new System.Drawing.Size(0, 26);
            this.TotalCostLabel.TabIndex = 5;
            // 
            // totalPrice
            // 
            this.totalPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.totalPrice.Image = global::shopingCompareUI.Properties.Resources.Check_48;
            this.totalPrice.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.totalPrice.Location = new System.Drawing.Point(3, 12);
            this.totalPrice.Name = "totalPrice";
            this.totalPrice.Size = new System.Drawing.Size(138, 50);
            this.totalPrice.TabIndex = 2;
            this.totalPrice.Text = "סה\"כ";
            this.totalPrice.UseVisualStyleBackColor = true;
            this.totalPrice.Click += new System.EventHandler(this.totalPrice_Click);
            // 
            // clearAllButton
            // 
            this.clearAllButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.clearAllButton.Image = global::shopingCompareUI.Properties.Resources.Return_Purchase_48;
            this.clearAllButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.clearAllButton.Location = new System.Drawing.Point(147, 12);
            this.clearAllButton.Name = "clearAllButton";
            this.clearAllButton.Size = new System.Drawing.Size(139, 49);
            this.clearAllButton.TabIndex = 3;
            this.clearAllButton.Text = "הסר הכל";
            this.clearAllButton.UseVisualStyleBackColor = true;
            this.clearAllButton.Click += new System.EventHandler(this.clearAllButton_Click);
            // 
            // ShopingCartView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 461);
            this.Controls.Add(this.productList);
            this.Controls.Add(this.panel1);
            this.Name = "ShopingCartView";
            this.Text = "עגלת מוצרים";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox productList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button totalPrice;
        private System.Windows.Forms.Button clearAllButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label TotalCostLabel;
    }
}