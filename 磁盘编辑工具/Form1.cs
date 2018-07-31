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
using SDcard;
using 文件操作类;

namespace 磁盘编辑工具
{
	public partial class Form1 : Form
	{
        SDUtils cipan = new SDUtils();
        FileHelper FileBin = new FileHelper();
        private bool DiskOpen = false;//磁盘状态，标志磁盘是否打开
        string tergetDisk = "";//目标磁盘
		public Form1()
		{
			InitializeComponent();
            comboBox1.SelectedIndex = 0;//设置默认值
			Get_info();
		}

		private void label1_Click(object sender, EventArgs e)
		{

		}

		private void button2_Click(object sender, EventArgs e)
		{
			//打开文件
            
			OpenFileDialog fileDialog = new OpenFileDialog();
			fileDialog.Multiselect = true;
			fileDialog.Title = "请选择文件";
			fileDialog.Filter = "二进制文件（*.bin,*img）|*.bin;*img|所有文件(*.*)|*.*";
			if (fileDialog.ShowDialog() == DialogResult.OK)
			{
                
				string[] names = fileDialog.FileNames;
				textBox4.Text = names[0];//显示文件名
                
                textBox2.Text ="0x"+ FileBin.filelen(names[0]).ToString("x");
                //foreach (string file in names)
                //{
                //    MessageBox.Show("已选择文件:" + file, "选择文件提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //}
                FileInfo fi = new FileInfo(names[0]);
                
			}
		}

        //向磁盘写入数据
		private void button3_Click(object sender, EventArgs e)
		{
			//写入磁盘
            //long filelen = 0;//文件长度
            byte[] WriteByte = new byte[512];
            string filename =textBox4.Text;
            uint fileStartAddr = Convert.ToUInt32(textBox3.Text,16);
            uint diskStartAddr = Convert.ToUInt32(textBox1.Text,16);
            uint filelen = Convert.ToUInt32(textBox2.Text, 16);
            if (filelen > 0)
            {
                if(MessageBox.Show(this, "确定要写入文件？此操作无法撤销！", "提示信息：", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    for (uint SurplusLen = 0; SurplusLen < filelen; SurplusLen += 512)
                    {
                        WriteByte = FileBin.BinRead(filename, SurplusLen + fileStartAddr, 512);//读取数据
                        cipan.WriteSector(WriteByte, SurplusLen / 512 + diskStartAddr);//将数据写入流
                        cipan.Refresh();//将当前流中的数据写入磁盘
                        progressBar1.Step = (int)(SurplusLen * 100 / filelen );
                        progressBar1.PerformStep();
                    }
                    MessageBox.Show(this, "文件写入完成", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
            }
            else
            {
                MessageBox.Show(this,"文件无内容或不存在","提示信息", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            } 
		}

		private void Get_info()
		{
			long lsum,ldr;
			StringBuilder mStringBuilder = new StringBuilder();
			foreach (DriveInfo drive in DriveInfo.GetDrives())
			{
				//判断是否是固定磁盘  
				if (drive.DriveType == DriveType.Removable)
				{
                    //comboBox1.Items.Add(drive.Name);//添加盘符
					lsum = drive.TotalSize / 1024;//KB,磁盘总大小
					ldr = drive.TotalFreeSpace / 1024;//剩余大小
                    label6.Text += "\n\r" + drive.Name + ": 总空间=" + lsum.ToString() + " 剩余空间=" + ldr.ToString();
				}
			}
		}

        //打开磁盘
        private void button1_Click(object sender, EventArgs e)
        {
            tergetDisk = comboBox1.Text;
            //Get_info();
            if (DiskOpen == false)
            {
                if (cipan.OpenDisk(tergetDisk))
                {
                    button1.Text = "磁盘" + comboBox1.Text;
                    DiskOpen = true;
                }
                else
                {
                    MessageBox.Show(this, "打开磁盘失败", "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                cipan.Close();
                button1.Text = "打开磁盘";
                DiskOpen = false;
            }
            
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            //退出提示，真的要退出，才允许退出 
            if (MessageBox.Show(this, "你真的要退出吗？", "提示信息：", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                e.Cancel = false;//Cancel为false，说明不取消窗口关闭操作，所以窗口关闭
            }
            else
            {
                cipan.Close();//关闭磁盘
                e.Cancel = true;
            }
        }

	}

	
}
