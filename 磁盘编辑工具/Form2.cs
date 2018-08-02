using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 磁盘编辑工具
{
	public partial class Form2 : Form
	{
		public Form2()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		public string get_FileName()
		{
			return textBox1.Text;
		}

		public string get_FileStart()
		{
			return textBox2.Text;
		}

		public string get_DiskStart()
		{
			return textBox3.Text;
		}

		public string get_DataSize()
		{
			return textBox3.Text;
		}
	}
}
