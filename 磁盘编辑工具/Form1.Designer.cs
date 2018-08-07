namespace 磁盘编辑工具
{
	partial class Form1
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.button1 = new System.Windows.Forms.Button();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.button3 = new System.Windows.Forms.Button();
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.button4 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.button6 = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.button2 = new System.Windows.Forms.Button();
			this.button7 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// comboBox1
			// 
			this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(514, 12);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(89, 20);
			this.comboBox1.TabIndex = 0;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(514, 38);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(89, 28);
			this.button1.TabIndex = 1;
			this.button1.Text = "打开磁盘";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Button1_Click);
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(12, 367);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(170, 24);
			this.progressBar1.TabIndex = 2;
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(514, 140);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(89, 28);
			this.button3.TabIndex = 13;
			this.button3.Text = "执行";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.Button3_Click);
			// 
			// listView1
			// 
			this.listView1.CheckBoxes = true;
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader8,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader7});
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.Location = new System.Drawing.Point(12, 12);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(485, 190);
			this.listView1.TabIndex = 15;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.SelectedIndexChanged += new System.EventHandler(this.ListView1_SelectedIndexChanged);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "序号";
			this.columnHeader1.Width = 50;
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = "操作";
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "文件名";
			this.columnHeader2.Width = 140;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "文件起始地址";
			this.columnHeader3.Width = 90;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "磁盘起始扇区";
			this.columnHeader4.Width = 90;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "数据大小";
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "文件路径";
			this.columnHeader7.Width = 200;
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(514, 72);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(89, 28);
			this.button4.TabIndex = 16;
			this.button4.Text = "添加";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.Button4_Click);
			// 
			// button5
			// 
			this.button5.Location = new System.Drawing.Point(514, 174);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(89, 28);
			this.button5.TabIndex = 17;
			this.button5.Text = "停止";
			this.button5.UseVisualStyleBackColor = true;
			// 
			// button6
			// 
			this.button6.Location = new System.Drawing.Point(514, 106);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(89, 28);
			this.button6.TabIndex = 18;
			this.button6.Text = "移除";
			this.button6.UseVisualStyleBackColor = true;
			this.button6.Click += new System.EventHandler(this.Button6_Click);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(12, 208);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(485, 153);
			this.textBox1.TabIndex = 19;
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(514, 208);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(89, 28);
			this.button2.TabIndex = 20;
			this.button2.Text = "地址查看";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// button7
			// 
			this.button7.Location = new System.Drawing.Point(514, 242);
			this.button7.Name = "button7";
			this.button7.Size = new System.Drawing.Size(89, 28);
			this.button7.TabIndex = 21;
			this.button7.Text = "格式化";
			this.button7.UseVisualStyleBackColor = true;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(615, 417);
			this.Controls.Add(this.button7);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.button6);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.listView1);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.progressBar1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.comboBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button7;
	}
}

