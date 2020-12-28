namespace specialStr
{
    partial class QDDataViewForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.whatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重新刷新当前节点数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.开启编辑界面ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tree = new System.Windows.Forms.TreeView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.类型 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.属性 = new System.Windows.Forms.Label();
            this.nodeName = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.scViewer = new System.Windows.Forms.SplitContainer();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scViewer)).BeginInit();
            this.scViewer.Panel1.SuspendLayout();
            this.scViewer.Panel2.SuspendLayout();
            this.scViewer.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.whatToolStripMenuItem,
            this.编辑ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(835, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // whatToolStripMenuItem
            // 
            this.whatToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开文件ToolStripMenuItem,
            this.optinToolStripMenuItem,
            this.重新刷新当前节点数据ToolStripMenuItem,
            this.保存ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.whatToolStripMenuItem.Name = "whatToolStripMenuItem";
            this.whatToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.whatToolStripMenuItem.Text = "操作";
            // 
            // 打开文件ToolStripMenuItem
            // 
            this.打开文件ToolStripMenuItem.Name = "打开文件ToolStripMenuItem";
            this.打开文件ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.打开文件ToolStripMenuItem.Text = "打开文件";
            this.打开文件ToolStripMenuItem.Click += new System.EventHandler(this.打开文件ToolStripMenuItem_Click);
            // 
            // optinToolStripMenuItem
            // 
            this.optinToolStripMenuItem.Name = "optinToolStripMenuItem";
            this.optinToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.optinToolStripMenuItem.Text = "重新刷新所有数据";
            this.optinToolStripMenuItem.Click += new System.EventHandler(this.optinToolStripMenuItem_Click);
            // 
            // 重新刷新当前节点数据ToolStripMenuItem
            // 
            this.重新刷新当前节点数据ToolStripMenuItem.Name = "重新刷新当前节点数据ToolStripMenuItem";
            this.重新刷新当前节点数据ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.重新刷新当前节点数据ToolStripMenuItem.Text = "重新刷新当前节点数据";
            this.重新刷新当前节点数据ToolStripMenuItem.Click += new System.EventHandler(this.重新刷新当前节点数据ToolStripMenuItem_Click);
            // 
            // 保存ToolStripMenuItem
            // 
            this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
            this.保存ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.保存ToolStripMenuItem.Text = "保存";
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            // 
            // 编辑ToolStripMenuItem
            // 
            this.编辑ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.开启编辑界面ToolStripMenuItem});
            this.编辑ToolStripMenuItem.Name = "编辑ToolStripMenuItem";
            this.编辑ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.编辑ToolStripMenuItem.Text = "编辑";
            // 
            // 开启编辑界面ToolStripMenuItem
            // 
            this.开启编辑界面ToolStripMenuItem.Name = "开启编辑界面ToolStripMenuItem";
            this.开启编辑界面ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.开启编辑界面ToolStripMenuItem.Text = "开启编辑界面";
            this.开启编辑界面ToolStripMenuItem.Click += new System.EventHandler(this.开启编辑界面ToolStripMenuItem_Click);
            // 
            // tree
            // 
            this.tree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tree.Location = new System.Drawing.Point(0, 0);
            this.tree.Name = "tree";
            this.tree.Size = new System.Drawing.Size(144, 454);
            this.tree.TabIndex = 1;
            this.tree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column4,
            this.Column1,
            this.Column2,
            this.类型});
            this.dataGridView1.Location = new System.Drawing.Point(3, 36);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(657, 415);
            this.dataGridView1.TabIndex = 3;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Index";
            this.Column4.Name = "Column4";
            this.Column4.Width = 60;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "名字";
            this.Column1.Name = "Column1";
            this.Column1.Width = 150;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "值";
            this.Column2.Name = "Column2";
            this.Column2.Width = 300;
            // 
            // 类型
            // 
            this.类型.HeaderText = "类型";
            this.类型.Name = "类型";
            // 
            // 属性
            // 
            this.属性.AutoSize = true;
            this.属性.Location = new System.Drawing.Point(3, 14);
            this.属性.Name = "属性";
            this.属性.Size = new System.Drawing.Size(65, 12);
            this.属性.TabIndex = 4;
            this.属性.Text = "节点名称：";
            // 
            // nodeName
            // 
            this.nodeName.Location = new System.Drawing.Point(69, 9);
            this.nodeName.Name = "nodeName";
            this.nodeName.Size = new System.Drawing.Size(100, 21);
            this.nodeName.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(585, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "保存修改";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // scViewer
            // 
            this.scViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scViewer.Location = new System.Drawing.Point(12, 28);
            this.scViewer.Name = "scViewer";
            // 
            // scViewer.Panel1
            // 
            this.scViewer.Panel1.Controls.Add(this.tree);
            // 
            // scViewer.Panel2
            // 
            this.scViewer.Panel2.Controls.Add(this.dataGridView1);
            this.scViewer.Panel2.Controls.Add(this.button1);
            this.scViewer.Panel2.Controls.Add(this.nodeName);
            this.scViewer.Panel2.Controls.Add(this.属性);
            this.scViewer.Size = new System.Drawing.Size(811, 454);
            this.scViewer.SplitterDistance = 144;
            this.scViewer.TabIndex = 7;
            // 
            // QDDataViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(835, 494);
            this.Controls.Add(this.scViewer);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "QDDataViewForm";
            this.Text = "QDataViewer";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.scViewer.Panel1.ResumeLayout(false);
            this.scViewer.Panel2.ResumeLayout(false);
            this.scViewer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scViewer)).EndInit();
            this.scViewer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem whatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optinToolStripMenuItem;
        private System.Windows.Forms.TreeView tree;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label 属性;
        private System.Windows.Forms.ToolStripMenuItem 编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 开启编辑界面ToolStripMenuItem;
        private System.Windows.Forms.TextBox nodeName;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem 重新刷新当前节点数据ToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewComboBoxColumn 类型;
        private System.Windows.Forms.ToolStripMenuItem 打开文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.SplitContainer scViewer;
    }
}