using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace specialStr
{

    public partial class QDDataViewForm : Form
    {
        public class CTData
        {
            public long id;
            public QuickData.CQDDataNode refNode;
        }

        QuickData.CQDSystem refData; //数据源

        //FormScript win = null;

        List<CTData> treeDataDict = new List<CTData>();
        

        public void AddRefData(QuickData.CQDSystem data)
        {
            refData = data;
        }

        public QDDataViewForm()
        {
            InitializeComponent();

            this.FormClosing += (x, y) => Application.Exit();
            退出ToolStripMenuItem.Click += (x, y) => Application.Exit();
            保存ToolStripMenuItem.Click += (x, y) => this.button1_Click(x, y);
        }

        public void InitTree()
        {
            if (refData == null)
                return;

            QuickData.CQDDataNode qd_n = refData.FirstDataNode();

            treeDataDict.Clear();

            TreeNode node;

            if (qd_n != null)
                node = this.tree.Nodes.Add("*Root");
            else
                node = this.tree.Nodes.Add("Root");

            InitNodeParam(null);
            InitTreeNode(qd_n, node);  
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.ByMouse)
            {
                if(!(e.Node.Text=="Root"|| e.Node.Text == "*Root"))
                {
                    int id = int.Parse(e.Node.Name);
                    CTData tddata = treeDataDict[id];

                    //if (e.Node.Nodes.Count == 0)
                    {
                        e.Node.Nodes.Clear();

                        tddata.refNode.LoadSelfChild();

                        QuickData.CQDDataNode qd_n = (QuickData.CQDDataNode)tddata.refNode.refChild;

                        InitNodeParam(tddata.refNode);

                        InitTreeNode(qd_n, e.Node); 
                     }

                   // if (win != null)
                     //   win.Init(refData, tddata.refNode);
                }
                else
                {
                   // if (win != null)
                       // win.Init(refData, null);
                }

            }
        }


        private void InitTreeNode(QuickData.CQDDataNode qd_n, TreeNode treeNode)
        {
            TreeNode node = null;
            while (qd_n != null)
            {
                qd_n.LoadSelfChild();

                string name = refData.GetStringByIndex(qd_n.nodeNameIndex);
                if (string.IsNullOrEmpty(name))
                    name = "no name";

                if (qd_n.refChild != null)
                    name = "*" + name;

                node = treeNode.Nodes.Add(name);

                CTData treeData = new CTData();
                treeData.id = treeDataDict.Count;
                treeData.refNode = qd_n;
                treeDataDict.Add(treeData);

                node.Name = treeData.id.ToString();

                qd_n = (QuickData.CQDDataNode)qd_n.refBrother;

            }
        }

        private void InitNodeParam(QuickData.CQDNode refNode )
        {       
            this.dataGridView1.Rows.Clear();

            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.Columns[0].ReadOnly = true;

            if (refNode == null)
                return;

            refNode.ReadyParam();

            this.nodeName.Text = refData.GetStringByIndex(refNode.nodeNameIndex);

            List<QuickData.CQDParam> param = refNode.GetCQParam();

            for (int i = 0; i < param.Count; i++)
            {
                int index = this.dataGridView1.Rows.Add();

                this.dataGridView1.Rows[index].Cells[0].Value = i.ToString();

                string name = refData.GetStringByIndex(param[i].nodeNameStrIndex);
                if (string.IsNullOrEmpty(name))
                    this.dataGridView1.Rows[index].Cells[1].Value = " ";
                else
                    this.dataGridView1.Rows[index].Cells[1].Value = name;

                this.dataGridView1.Rows[index].Cells[2].Value = param[i].ValueToStr();

                DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)this.dataGridView1.Rows[index].Cells[3]; //. = param[i].valueType.ToString();
                cell.Items.Clear();
                cell.Items.Add("Int");
                cell.Items.Add("Float");
                cell.Items.Add("Long");
                cell.Items.Add("Str");
                cell.Items.Add("Char");
                cell.Items.Add("Byte");
                cell.Items.Add("Double");
                cell.Value = param[i].valueType.ToString();
            }
        }



        private void optinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //
            treeDataDict.Clear();
            this.tree.Nodes.Clear();
            InitTree();
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            if (refData == null)
                return;
            try
            {
                //save all change
                TreeNode node = this.tree.SelectedNode;
                if (node != null)
                {
                    bool change = false;

                    int id = int.Parse(node.Name);
                    CTData tddata = treeDataDict[id];

                    //check and save name
                    //string editNodeName = this.nodeName.Text;
                    //if (editNodeName != refData.GetStringByIndex(tddata.refNode.nodeNameIndex))
                    //{
                    //    tddata.refNode.nodeNameIndex = refData.AddString(editNodeName);
                    //    change = true;
                    //    node.Text = editNodeName;
                    //}

                    //check and save param
                    List<QuickData.CQDParam> param = tddata.refNode.GetCQParam();

                    for (int i = 0; i < this.dataGridView1.RowCount; i++)
                    {
                        int index = int.Parse((string)dataGridView1.Rows[i].Cells[0].Value);
                        string paramName = (string)dataGridView1.Rows[i].Cells[1].Value;
                        string value = (string)dataGridView1.Rows[i].Cells[2].Value;
                        string valuetype = (string)dataGridView1.Rows[i].Cells[3].Value;

                        //param Name
                        if (paramName != refData.GetStringByIndex(param[index].nodeNameStrIndex))
                        {
                            param[index].nodeNameStrIndex = refData.AddString(paramName);
                        }

                        //param type
                        QuickData.eQDValueType paramValueType = QuickData.CQDParam.GetValueType(valuetype);
                        if (paramValueType != param[index].valueType)
                        {
                            QuickData.CQDParam p = refData.CreateParamByType(paramValueType);
                            p.nodeNameStrIndex = param[index].nodeNameStrIndex;
                            param[index] = p;
                        }

                        //param value
                        param[index].ReadValueFromStr(value);

                    }

                    //更新到实体数据
                    //if (change)
                    {
                        tddata.refNode.ReadParam();

                    }
                }
            }
            catch(Exception ex)
            {
                
            }




        }

        public void ScriptEditerFormThread()
        {

            //win.ShowDialog();
        }

        private void 重新刷新当前节点数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = this.tree.SelectedNode;
            if(node!=null)
            {
                if(node.Text=="Root"||node.Text=="*Root")
                {
                    treeDataDict.Clear();
                    this.tree.Nodes.Clear();
                    InitTree();
                }
                else
                {
                    int id = int.Parse(node.Name);

                    CTData tddata = treeDataDict[id];

                    tddata.refNode.LoadSelfChild();

                    QuickData.CQDDataNode qd_n = (QuickData.CQDDataNode)tddata.refNode.refChild;

                    node.Nodes.Clear();

                    InitTreeNode(qd_n, node);

                    InitNodeParam(tddata.refNode);
                }
              
            }
        }

        private void 开启编辑界面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (win == null)
              //  win = new FormScript();

            TreeNode node = this.tree.SelectedNode;
            if (node != null && node.Name != "Root" && node.Name != "*Root" && !string.IsNullOrEmpty(node.Name))
            {
                int id = int.Parse(node.Name);
                CTData tddata = treeDataDict[id];
                //win.Init(refData, tddata.refNode);
            }
           // else
                //win.Init(refData, null);

           // Thread buffmsgThread = new Thread(new ThreadStart(ScriptEditerFormThread));

           // buffmsgThread.SetApartmentState(ApartmentState.STA);

           // buffmsgThread.Start();
        }

        private void 打开文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var filePath = SelectFile();
            if (filePath != null)
            {
                refData?.UpdateSave();
                refData?.Close();

                treeDataDict.Clear();
                this.tree.Nodes.Clear();

                QuickData.CQDSystem dataSys = new QuickData.CQDSystem();
                dataSys.Init(filePath);
                dataSys.Load();

                refData = dataSys;
                this.InitTree();
            }
        }

        private string SelectFile()
        {
            string filePath = null;

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.InitialDirectory = Environment.CurrentDirectory;

            var res = dialog.ShowDialog();
            if (res == DialogResult.Yes || res == DialogResult.OK)
                filePath = dialog.FileName;

            return filePath;
        }
    }
}
