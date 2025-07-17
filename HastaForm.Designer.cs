using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace diyabetapp
{
    partial class HastaForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private ProgressBar progressBarDiyet;
        private ProgressBar progressBarEgzersiz;
        private Label lblDiyetYuzde;
        private Label lblEgzersizYuzde;

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
            this.progressBarDiyet = new ProgressBar();
            this.progressBarEgzersiz = new ProgressBar();
            this.lblDiyetYuzde = new Label();
            this.lblEgzersizYuzde = new Label();
            SuspendLayout();
            // 
            // HastaForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1260, 701);
            Name = "HastaForm";
            Text = "Hasta Takip Sistemi";
            // progressBarDiyet
            this.progressBarDiyet.Location = new Point(700, 60); // Diyet sekmesinde uygun bir yere konumlandır
            this.progressBarDiyet.Size = new Size(200, 25);
            this.progressBarDiyet.Name = "progressBarDiyet";
            // lblDiyetYuzde
            this.lblDiyetYuzde.Location = new Point(910, 60);
            this.lblDiyetYuzde.Size = new Size(100, 25);
            this.lblDiyetYuzde.Name = "lblDiyetYuzde";
            this.lblDiyetYuzde.Text = "Diyet Uyum: %0";
            // progressBarEgzersiz
            this.progressBarEgzersiz.Location = new Point(700, 100); // Egzersiz sekmesinde uygun bir yere konumlandır
            this.progressBarEgzersiz.Size = new Size(200, 25);
            this.progressBarEgzersiz.Name = "progressBarEgzersiz";
            // lblEgzersizYuzde
            this.lblEgzersizYuzde.Location = new Point(910, 100);
            this.lblEgzersizYuzde.Size = new Size(100, 25);
            this.lblEgzersizYuzde.Name = "lblEgzersizYuzde";
            this.lblEgzersizYuzde.Text = "Egzersiz Uyum: %0";
            ResumeLayout(false);
        }

        #endregion
    }
}
