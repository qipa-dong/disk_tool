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
            //comboBox1.SelectedIndex = 0;//设置默认值
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			Get_info();
		}

		private void Button2_Click(object sender, EventArgs e)
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
		private void Button3_Click(object sender, EventArgs e)
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
			long lsum, ldr; 
			label6.Text = "磁盘信息\r\n";
			comboBox1.Items.Clear();
			foreach (DriveInfo drive in DriveInfo.GetDrives())
			{
				//判断是否是固定磁盘  
				if (drive.DriveType == DriveType.Fixed)
				{
					lsum = drive.TotalSize / 1024 / 1024;//MB,磁盘总大小
					ldr = drive.TotalFreeSpace / 1024 / 1024;//剩余大小
					label6.Text += drive.Name + " 总空间  = " + lsum.ToString() + " MB" + "\r\n" + "    剩余空间= " + ldr.ToString() + " MB" + "\r\n";
				}
				else if (drive.DriveType == DriveType.Removable)//判断是否是移动磁盘 
				{
					if (drive.IsReady)//磁盘已就绪
					{
						lsum = drive.TotalSize / 1024 / 1024;//MB,磁盘总大小
						ldr = drive.TotalFreeSpace / 1024 / 1024;//剩余大小
						label6.Text += drive.Name + " 总空间  = " + lsum.ToString() + " MB" + "\r\n" + "    剩余空间= " + ldr.ToString() + " MB" + "\r\n";
					}
					else
					{
						label6.Text += drive.Name + " 未知" + "\r\n";
						
					}
				}

				if (drive.IsReady)//磁盘已就绪
					comboBox1.Items.Add(drive.Name + drive.VolumeLabel);
				else
					comboBox1.Items.Add(drive.Name + "未知");
			}
			comboBox1.SelectedIndex = 0;//设置默认值
		}

        //打开磁盘
        private void Button1_Click(object sender, EventArgs e)
        {
            tergetDisk = comboBox1.Text.Substring(0,2);
            //Get_info();
            if (DiskOpen == false)
            {
                if (cipan.OpenDisk(tergetDisk))
                {
                    button1.Text = "磁盘" + comboBox1.Text.Substring(0, 2);
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
				Get_info();//关闭磁盘时刷新磁盘信息
			}
            
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            //退出提示，真的要退出，才允许退出 
            if (MessageBox.Show(this, "是否退出？", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                e.Cancel = false;//Cancel为false，说明不取消窗口关闭操作，所以窗口关闭
            }
            else
            {
                cipan.Close();//关闭磁盘
                e.Cancel = true;
            }
        }

		private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void Button4_Click(object sender, EventArgs e)
		{
			Form2 testDialog = new Form2();
			listView1.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度

			ListViewItem lvi = new ListViewItem();

			//lvi.ImageIndex = 1;     //通过与imageList绑定，显示imageList中第i项图标

			lvi.Text = "1";

			if (testDialog.ShowDialog(this) == DialogResult.OK)//弹出窗口
			{
				lvi.SubItems.Add(testDialog.Get_FileName());
				lvi.SubItems.Add(testDialog.Get_FileStart());
				lvi.SubItems.Add(testDialog.Get_DiskStart());
				lvi.SubItems.Add(testDialog.Get_DataSize());

				listView1.Items.Add(lvi);//列表显示数据
			}
			testDialog.Dispose();

			

			listView1.EndUpdate();  //结束数据处理，UI界面一次性绘制。


			
			
			
		}

		

		//m = listView1.CheckedItems.Count;//或去选中项
	}

	
}
