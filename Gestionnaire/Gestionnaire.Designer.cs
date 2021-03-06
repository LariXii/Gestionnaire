﻿using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Gestionnaire
{
    partial class Gestionnaire
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Mon coffre fort");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Gestionnaire));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.col_Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Username = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_URL = new System.Windows.Forms.DataGridViewLinkColumn();
            this.col_password = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fichierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rechercheParURLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbtnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtn_addEntry = new System.Windows.Forms.ToolStripButton();
            this.tsbtn_deleteEntry = new System.Windows.Forms.ToolStripButton();
            this.tsBtnUpdateEntry = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize) (this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.BackColor = System.Drawing.Color.PapayaWhip;
            this.treeView1.Location = new System.Drawing.Point(12, 73);
            this.treeView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.treeView1.Name = "treeView1";
            treeNode4.Name = "node_root";
            treeNode4.Text = "Mon coffre fort";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {treeNode4});
            this.treeView1.Size = new System.Drawing.Size(187, 365);
            this.treeView1.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.PapayaWhip;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {this.col_Title, this.col_Username, this.col_URL, this.col_password});
            this.dataGridView1.Location = new System.Drawing.Point(205, 73);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(583, 366);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellFormatting += new DataGridViewCellFormattingEventHandler(dataGridView1_CellFormatting);
            // 
            // col_Title
            // 
            this.col_Title.DataPropertyName = "Name";
            this.col_Title.HeaderText = "Titre";
            this.col_Title.Name = "col_Title";
            this.col_Title.ReadOnly = true;
            // 
            // col_Username
            // 
            this.col_Username.DataPropertyName = "UserName";
            this.col_Username.FillWeight = 150F;
            this.col_Username.HeaderText = "Nom d\'utilisateur";
            this.col_Username.Name = "col_Username";
            this.col_Username.ReadOnly = true;
            // 
            // col_URL
            // 
            this.col_URL.DataPropertyName = "Url";
            this.col_URL.HeaderText = "URL";
            this.col_URL.Name = "col_URL";
            this.col_URL.ReadOnly = true;
            this.col_URL.TrackVisitedState = false;
            this.col_URL.VisitedLinkColor = System.Drawing.Color.Blue;
            // 
            // col_password
            // 
            this.col_password.DataPropertyName = "Password";
            this.col_password.HeaderText = "Mot de passe";
            this.col_password.Name = "col_password";
            this.col_password.ReadOnly = true;
            this.col_password.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.col_password.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.fichierToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fichierToolStripMenuItem
            // 
            this.fichierToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {this.rechercheParURLToolStripMenuItem, this.quitterToolStripMenuItem});
            this.fichierToolStripMenuItem.Name = "fichierToolStripMenuItem";
            this.fichierToolStripMenuItem.Size = new System.Drawing.Size(64, 24);
            this.fichierToolStripMenuItem.Text = "Fichier";
            // 
            // rechercheParURLToolStripMenuItem
            // 
            this.rechercheParURLToolStripMenuItem.Name = "rechercheParURLToolStripMenuItem";
            this.rechercheParURLToolStripMenuItem.Size = new System.Drawing.Size(202, 24);
            this.rechercheParURLToolStripMenuItem.Text = "Recherche par URL";
            this.rechercheParURLToolStripMenuItem.Click += new System.EventHandler(this.rechercheParURLToolStripMenuItem_Click);
            // 
            // quitterToolStripMenuItem
            // 
            this.quitterToolStripMenuItem.Name = "quitterToolStripMenuItem";
            this.quitterToolStripMenuItem.Size = new System.Drawing.Size(202, 24);
            this.quitterToolStripMenuItem.Text = "Quitter";
            this.quitterToolStripMenuItem.Click += new System.EventHandler(this.quitterToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.tsbtnSave, this.toolStripSeparator1, this.tsbtn_addEntry, this.tsbtn_deleteEntry, this.tsBtnUpdateEntry});
            this.toolStrip1.Location = new System.Drawing.Point(0, 28);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbtnSave
            // 
            this.tsbtnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnSave.Image = ((System.Drawing.Image) (resources.GetObject("tsbtnSave.Image")));
            this.tsbtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSave.Name = "tsbtnSave";
            this.tsbtnSave.Size = new System.Drawing.Size(23, 22);
            this.tsbtnSave.Text = "tsbtnSave";
            this.tsbtnSave.Click += new EventHandler(tsbtnSave_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbtn_addEntry
            // 
            this.tsbtn_addEntry.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtn_addEntry.Image = ((System.Drawing.Image) (resources.GetObject("tsbtn_addEntry.Image")));
            this.tsbtn_addEntry.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_addEntry.Name = "tsbtn_addEntry";
            this.tsbtn_addEntry.Size = new System.Drawing.Size(23, 22);
            this.tsbtn_addEntry.Text = "Ajouter une entrée";
            this.tsbtn_addEntry.Click += new System.EventHandler(this.tsbtn_addEntry_Click);
            // 
            // tsbtn_deleteEntry
            // 
            this.tsbtn_deleteEntry.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtn_deleteEntry.Image = ((System.Drawing.Image) (resources.GetObject("tsbtn_deleteEntry.Image")));
            this.tsbtn_deleteEntry.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_deleteEntry.Name = "tsbtn_deleteEntry";
            this.tsbtn_deleteEntry.Size = new System.Drawing.Size(23, 22);
            this.tsbtn_deleteEntry.Text = "Supprimer une entrée";
            this.tsbtn_deleteEntry.Click += new System.EventHandler(this.tsbtn_deleteEntry_Click);
            // 
            // tsBtnUpdateEntry
            // 
            this.tsBtnUpdateEntry.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnUpdateEntry.Image = ((System.Drawing.Image) (resources.GetObject("tsBtnUpdateEntry.Image")));
            this.tsBtnUpdateEntry.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnUpdateEntry.Name = "tsBtnUpdateEntry";
            this.tsBtnUpdateEntry.Size = new System.Drawing.Size(23, 22);
            this.tsBtnUpdateEntry.Text = "Modifier une entrée";
            this.tsBtnUpdateEntry.Click += new System.EventHandler(this.tsBtnUpdateEntry_Click);
            // 
            // Gestionnaire
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Moccasin;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Gestionnaire";
            this.Text = "Gestionnaire";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Gestionnaire_FormClosing);
            ((System.ComponentModel.ISupportInitialize) (this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        
        private System.Windows.Forms.ToolStripMenuItem quitterToolStripMenuItem;

        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;

        private System.Windows.Forms.ToolStripButton tsbtnSave;

        private System.Windows.Forms.ToolStripButton tsBtnUpdateEntry;

        private System.Windows.Forms.ToolStripMenuItem rechercheParURLToolStripMenuItem;

        private System.Windows.Forms.ToolStripButton tsbtn_addEntry;
        private System.Windows.Forms.ToolStripButton tsbtn_deleteEntry;

        private System.Windows.Forms.ToolStrip toolStrip1;

        private System.Windows.Forms.ToolStripMenuItem fichierToolStripMenuItem;

        private System.Windows.Forms.MenuStrip menuStrip1;

        private System.Windows.Forms.DataGridViewTextBoxColumn col_password;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_Title;
        private System.Windows.Forms.DataGridViewLinkColumn col_URL;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_Username;

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TreeView treeView1;

        #endregion
    }
}