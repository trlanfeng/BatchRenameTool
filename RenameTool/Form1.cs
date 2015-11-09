using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RenameTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string[] fileNameLists;
        string[] newFileNameLists;
        FileInfo[] FI;
        string path;

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (Directory.Exists(textBox1.Text))
            {
                path = textBox1.Text;
                loadFileList();
            }
        }

        private void loadFileList()
        {
            DirectoryInfo DI = new DirectoryInfo(path);
            FI = DI.GetFiles();
            fileNameLists = new string[FI.Length];
            newFileNameLists = new string[FI.Length];
            for (int i = 0; i < fileNameLists.Length; i++)
            {
                fileNameLists[i] = FI[i].Name;
                newFileNameLists[i] = FI[i].Name;
            }
            updateListView();
        }

        void updateListView()
        {
            listView1.Items.Clear();
            for (int i = 0; i < fileNameLists.Length; i++)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = fileNameLists[i].Replace(path + "\\", "");
                if (checkBox1.Checked)
                {
                    newFileNameLists[i] = textBox4.Text + (i + 1).ToString() + textBox5.Text + FI[i].Extension;
                    lvi.SubItems.Add(newFileNameLists[i]);
                }
                else
                {
                    if (textBox2.Text.Length != 0)
                    {
                        string newName = "";
                        newName = fileNameLists[i].Replace(textBox2.Text, textBox3.Text);
                        lvi.SubItems.Add(newName);
                    }
                }
                listView1.Items.Add(lvi);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            updateListView();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            updateListView();
        }

        void renameAll()
        {
            button3.Enabled = false;
            button3.Text = "进行中";
            for (int i = 0; i < FI.Length; i++)
            {
                FI[i].MoveTo(Path.Combine(FI[i].DirectoryName + "\\" + newFileNameLists[i]));
            }
            button3.Text = "开始";
            MessageBox.Show("修改完成。");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            renameAll();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            updateListView();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            updateListView();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            updateListView();
        }
    }
}
