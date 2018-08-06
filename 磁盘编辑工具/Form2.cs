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
		private FileHelper FileBin = new FileHelper();
		//private FileInfo fi;
		private listdata in_data = new listdata();

		public Form2()
		{
			InitializeComponent();
		}

		private void Button1_Click(object sender, EventArgs e)
		{
			if (textBox1.Text != "")
			{
				in_data.data_size = Convert.ToInt32(numericUpDown3.Value);//提取长度并转换为十进制
				in_data.disk_start = Convert.ToInt32(numericUpDown2.Value);
				in_data.file_start = Convert.ToInt32(numericUpDown1.Value);
				//in_data.short_name = fi.Name;
				//in_data.full_name = fi.FullName;
				in_data.operating = comboBox1.Text;

				DialogResult = DialogResult.OK;
			}
			else
			{
				DialogResult = DialogResult.Cancel;
			}
			Close();
		}

		private void Button2_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		public listdata get_data()
		{
			return in_data;
		}

		private void Form2_Load(object sender, EventArgs e)
		{
			comboBox1.SelectedIndex = 0;
			comboBox2.SelectedIndex = 0;
			comboBox3.SelectedIndex = 0;
			comboBox4.SelectedIndex = 0;

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

					FileInfo fi = new FileInfo(names[0]);
					textBox1.Text = fi.Name;

					in_data.short_name = fi.Name;
					in_data.full_name = fi.FullName;

					numericUpDown3.Value = 0;
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

					FileInfo fi = new FileInfo(names[0]);
					textBox1.Text = fi.Name;

					in_data.short_name = fi.Name;
					in_data.full_name = fi.FullName;

					numericUpDown3.Value = FileBin.filelen(names[0]);
				}
			}
		}

		private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
		{
			if(comboBox2.Text == "HEX")
				numericUpDown1.Hexadecimal = true;
			else
				numericUpDown1.Hexadecimal = false;
		}

		private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBox3.Text == "HEX")
				numericUpDown2.Hexadecimal = true;
			else
				numericUpDown2.Hexadecimal = false;
		}

		private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBox4.Text == "HEX")
				numericUpDown3.Hexadecimal = true;
			else
				numericUpDown3.Hexadecimal = false;
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			in_data.short_name = "";
			in_data.full_name = "";
			textBox1.Text = "";
			numericUpDown3.Value = 0;
		}

	}
}
