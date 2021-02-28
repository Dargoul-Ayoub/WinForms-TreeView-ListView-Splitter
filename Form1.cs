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

namespace _6._12.TreeView__ListView_et_Splitter
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            

            ImageList imageList = new ImageList();
            imageList.Images.Add(Image.FromFile(@"C:\Users\Devman\Downloads\folder.png"));
            imageList.Images.Add(Image.FromFile(@"C:\Users\Devman\Downloads\multimedia.png"));
            treeView1.ImageList = imageList;
            listView1.SmallImageList = imageList;
            //  DirectoryInfo info = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)); if i want to get SubDirectory of Descktop it will take a long time to get ready
            DirectoryInfo info = new DirectoryInfo(@"C:\Users\Devman\Desktop\Package\GTA Sanandreas");
            if (Directory.Exists(info.FullName))
            {
                TreeNode DirectoryNode = new TreeNode(info.Name, 0, 0);
                treeView1.Nodes.Add(DirectoryNode);
                LoadsubDirectory(info, DirectoryNode);
            }
            else MessageBox.Show("This path doesn't exists");

           
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            listView1.Items.Clear();            
            string sub_directory = @"C:\Users\Devman\Desktop\Package\"+treeView1.SelectedNode.FullPath;
            DirectoryInfo SubDir = new DirectoryInfo(sub_directory);
            LoadFiles(SubDir);
        }

        private void LoadsubDirectory(DirectoryInfo path, TreeNode treeNode)
        {

            string[] subdirectoryEntries = Directory.GetDirectories(path.FullName);
        
                // loop into subDirectory
                foreach (string subdirctory in subdirectoryEntries)
                {
                    DirectoryInfo Sub_Di = new DirectoryInfo(subdirctory);
                    TreeNode DirectoryNode = treeNode.Nodes.Add(Sub_Di.Name);
                    LoadsubDirectory(Sub_Di, DirectoryNode);


                }
        }



        private void LoadFiles(DirectoryInfo dir)
        {
            listView1.View = View.List;
            listView1.Columns.Add("None", listView1.Width / 2);
            // i have to creat a coloum even if i made the HeaderStyle as a none else that the listView will not show anything
            listView1.HeaderStyle = ColumnHeaderStyle.None;// i made this one for that will not show HeaderColoumn

            string[] Files = Directory.GetFiles(dir.FullName, "*.*");
            if (Files.Length > 0)
            {

                // Loop through them to see files  
                foreach (string file in Files)
                {
                        FileInfo fi = new FileInfo(file);
                        ListViewItem item1 = new ListViewItem(fi.Name, 1);
                        listView1.Items.Add(item1);
                }
            }
        }
    }
}
