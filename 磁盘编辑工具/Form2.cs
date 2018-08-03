using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using 文件操作类;

namespace 磁盘编辑工具
{
	public partial class Form2 : Form
	{
		FileHelper FileBin = new FileHelper();
		private FileInfo fi ;
		public listdata in_data;

		public Form2()
		{
			InitializeComponent();
		}

		private void Button1_Click(object sender, EventArgs e)
		{
			in_data.data_size = Convert.ToInt32(textBox4.Text.Substring(2),16);//提取长度并转换为十进制
			in_data.disk_start = Convert.ToInt32(textBox3.Text.Substring(2), 16);
			in_data.file_start = Convert.ToInt32(textBox2.Text.Substring(2), 16);
			in_data.short_name = fi.Name;
			in_data.full_name = fi.FullName;

			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void Button2_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void Form2_Load(object sender, EventArgs e)
		{
			comboBox1.SelectedIndex = 0;

		}

		private void button3_Click(object sender, EventArgs e)
		{
			if (comboBox1.Text == "读取")
			{
				SaveFileDialog filesave =new SaveFileDialog();
				filesave.Title = "新建文件";
				filesave.Filter = "二进制文件（*.bin,*img）|*.bin;*img|所有文件(*.*)|*.*";
				filesave.FilterIndex = 1;//设置默认文件类型显示顺序 
				filesave.RestoreDirectory = true;//保存对话框是否记忆上次打开的目录 
				filesave.FileName = "读出文件";//设置默认的文件名
				if (filesave.ShowDialog() == DialogResult.OK)
				{
					string[] names = filesave.FileNames;

					fi = new FileInfo(names[0]);
					textBox1.Text = fi.Name;

					textBox4.Text = "";
				}

			}
			else if (comboBox1.Text == "写入")
			{
				//打开文件
				OpenFileDialog fileDialog = new OpenFileDialog();
				fileDialog.Multiselect = true;
				fileDialog.Title = "请选择文件";
				fileDialog.Filter = "二进制文件（*.bin,*img）|*.bin;*img|所有文件(*.*)|*.*";
				fileDialog.RestoreDirectory = true;//保存对话框是否记忆上次打开的目录 
				if (fileDialog.ShowDialog() == DialogResult.OK)
				{

					string[] names = fileDialog.FileNames;

					fi = new FileInfo(names[0]);
					textBox1.Text = fi.Name;

					textBox4.Text = "0x" + FileBin.filelen(names[0]).ToString("x");

				}
			}
		}
	}
}
