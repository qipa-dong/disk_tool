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
		private FileHelper FileBin = new FileHelper();
        private bool DiskOpen = false;//磁盘状态，标志磁盘是否打开
        string tergetDisk = "";//目标磁盘
		int list_num = 0;

		public Form1()
		{
			InitializeComponent();
            //comboBox1.SelectedIndex = 0;//设置默认值
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			Get_info();
		}

        //向磁盘写入数据
		private void Button3_Click(object sender, EventArgs e)
		{
			byte[] WriteByte = new byte[512];
			for (uint i = 0; i < 512; i++ )
			{
				WriteByte[i] = 0xFF;
			}
			listdata execute_data = new listdata();

			//if (DiskOpen == false)
			//{
			//	MessageBox.Show(this, "磁盘未打开", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Question);
			//	return;
			//}

			if (MessageBox.Show(this, "确定要执行操作？此操作无法撤销！", "提示信息：", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
			{
				button3.Text = "执行";
				return;
			}

			button3.Text = "停止";

			for (int i = 0; i < listView1.CheckedItems.Count; i++)//遍历整个列表
			{
				if (listView1.CheckedItems[i].Checked)//判断是否复选
				{
					execute_data.full_name = listView1.Items[i].SubItems[6].Text;
					execute_data.operating = listView1.Items[i].SubItems[1].Text;
					execute_data.file_start = Convert.ToUInt32(listView1.Items[i].SubItems[3].Text);
					execute_data.disk_start = Convert.ToUInt32(listView1.Items[i].SubItems[4].Text);
					execute_data.data_size = Convert.ToUInt32(listView1.Items[i].SubItems[5].Text);

					//写入磁盘
					if (execute_data.operating == "写入" || execute_data.operating == "擦除")
					{
						if (execute_data.data_size == 0 && execute_data.operating == "写入")
						{
							MessageBox.Show(this, "文件无内容或不存在", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Question);
							continue;
						}

						for (uint SurplusLen = 0; SurplusLen < execute_data.data_size; SurplusLen += 512)
						{
							if (execute_data.operating == "写入")
							{
								//读取数据
								WriteByte = FileBin.BinRead(execute_data.full_name, SurplusLen + execute_data.file_start, 512);
							}
							//将数据写入流
							cipan.WriteSector(WriteByte, SurplusLen / 512 + execute_data.disk_start);

							//将当前流中的数据写入磁盘
							cipan.Refresh();

							//更新进度条
							progressBar1.Step = (int)(SurplusLen * 100 / execute_data.data_size);
							progressBar1.PerformStep();
						}
						MessageBox.Show(this, "文件写入完成", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Question);

					}
					else if (execute_data.operating == "读取")
					{
						/*读取数据*/
						for (uint SurplusLen = 0; SurplusLen < execute_data.data_size; SurplusLen += 512)
						{
							//读取磁盘数据
							WriteByte = cipan.ReadSector(SurplusLen / 512 + execute_data.disk_start);

							//写入文件
							FileBin.Write(execute_data.full_name, SurplusLen / 512 + execute_data.disk_start, WriteByte);

							//更新进度条
							progressBar1.Step = (int)(SurplusLen * 100 / execute_data.data_size);
							progressBar1.PerformStep();
						}
						MessageBox.Show(this, "磁盘读取完成", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Question);
						
					}
				}
			}
			button3.Text = "执行";
		}

		private void Get_info()
		{
			long lsum, ldr;
			string disk_log ="磁盘信息\r\n";

			//清空显示log
			log("");

			comboBox1.Items.Clear();
			foreach (DriveInfo drive in DriveInfo.GetDrives())
			{
				//判断是否是固定磁盘  
				if (drive.DriveType == DriveType.Fixed)
				{
					lsum = drive.TotalSize / 1024 / 1024;//MB,磁盘总大小
					ldr = drive.TotalFreeSpace / 1024 / 1024;//剩余大小
					disk_log += drive.Name + " 总空间  = " + lsum.ToString("n") + " MB" + "    剩余空间= " + ldr.ToString("n") + " MB" + "\r\n";
				}
				else if (drive.DriveType == DriveType.Removable)//判断是否是移动磁盘 
				{
					if (drive.IsReady)//磁盘已就绪
					{
						lsum = drive.TotalSize / 1024 / 1024;//MB,磁盘总大小
						ldr = drive.TotalFreeSpace / 1024 / 1024;//剩余大小
						disk_log += drive.Name + " 总空间  = " + lsum.ToString("n") + " MB" + "    剩余空间= " + ldr.ToString("n") + " MB" + "\r\n";
					}
					else
					{
						disk_log += drive.Name + " 未知" + "\r\n";
						
					}
				}

				if (drive.IsReady)//磁盘已就绪
					comboBox1.Items.Add(drive.Name + drive.VolumeLabel);
				else
					comboBox1.Items.Add(drive.Name + "未知");
			}
			log(disk_log);
			comboBox1.SelectedIndex = 0;//设置默认值
		}

		//打印log信息
		private void log(string data)
		{
			if(data == "")
			{
				textBox1.Text = "";
			}

			textBox1.Text += data;
		}

        //打开磁盘
        private void Button1_Click(object sender, EventArgs e)
        {
            tergetDisk = comboBox1.Text.Substring(0,2);
            if (DiskOpen == false)//磁盘未打开
            {
                if (cipan.OpenDisk(tergetDisk))
                {
                    button1.Text = "磁盘" + comboBox1.Text.Substring(0, 2);
					button2.Enabled = true;
					button3.Enabled = true;
					button7.Enabled = true;
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
				button2.Enabled = false;
				button3.Enabled = false;
				button7.Enabled = false;
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
				if (DiskOpen == true)
				{
					cipan.Close();//关闭磁盘
				}

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

			

			if (testDialog.ShowDialog(this) == DialogResult.OK)//弹出窗口
			{
				listdata data = new listdata();
				data = testDialog.get_data();

				list_num++;
				lvi.Text = list_num.ToString();

				lvi.SubItems.Add(data.operating);
				lvi.SubItems.Add(data.short_name);
				lvi.SubItems.Add(data.file_start.ToString());
				lvi.SubItems.Add(data.disk_start.ToString());
				lvi.SubItems.Add(data.data_size.ToString());
				lvi.SubItems.Add(data.full_name);
				lvi.Checked = true;

				listView1.Items.Add(lvi);//列表显示数据
			}
			testDialog.Dispose();

			listView1.EndUpdate();  //结束数据处理，UI界面一次性绘制。
		}

		private void Button6_Click(object sender, EventArgs e)
		{
			string data = listView1.FocusedItem.SubItems[1].Text;//获取选中行的第一列的数据
			string data1 = listView1.Items[0].SubItems[1].Text;//获取第0项第一列的数据
			int data2 = listView1.Items.Count;//获取总项数
		}



		//m = listView1.CheckedItems.Count;//或去选中项
	}

	public struct listdata
	{
		public string full_name;//文件全名（路径）
		public string short_name;//短文件名
		public string operating;//操作
		public uint file_start;//文件起始地址
		public uint disk_start;//磁盘起始地址
		public uint data_size;//数据大小
	}
	
}
