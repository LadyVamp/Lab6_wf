using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InterSystems.Data.CacheClient;
using InterSystems.Data.CacheTypes;


namespace Lab6_wf
{
    public partial class Form1 : Form
    {
        public CacheConnection CacheConnect;
        public User.SearchPattern sp;
        public User.File f;
        
        public Form1()
        {
            InitializeComponent();
            string ConnStr = "Server = localhost;"
                + " Port = 1972;"
                + " Namespace = User;"
                + " Password = 123;" //был SYS
                + " User ID = _system;";

            try
            {
                CacheConnect = new CacheConnection();
                CacheConnect.ConnectionString = ConnStr;
                CacheConnect.Open();
            }
            catch (Exception eLoad)
            {
                MessageBox.Show("An error has occurred:  " + eLoad.Message);
            } 


        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            User.SearchPattern sp = new User.SearchPattern(CacheConnect);
            sp.regexp = txtRegexp.Text;
            sp.compare = cmbCompare.Text;
            sp.action = cmbAction.Text;
            CacheStatus sc = sp.Save();
            MessageBox.Show("Объект " + sp.regexp + " успешно создан!");
            textStatus.Clear();
            textStatus.AppendText("Результат создания объекта: " + sc.IsOK.ToString()); 

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                txtRegexp.Text = "";
                cmbCompare.Text = "";
                cmbAction.Text = "";
                sp = User.SearchPattern.OpenId(CacheConnect, txtID.Text);
                //Заполнение полей со свойствами
                txtRegexp.Text = sp.regexp.ToString();
                cmbCompare.Text = sp.compare.ToString();
                cmbAction.Text = sp.action.ToString();
                //Вызов методов
                textStatus.Clear();
                textStatus.AppendText("ID=" + sp.Id().ToString());
            }
            catch (Exception eLoad)
            {
                txtID.Text = "";
                textStatus.Clear();
                textStatus.AppendText("Ошибка: " + eLoad.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            sp = User.SearchPattern.OpenId(CacheConnect, txtID.Text);
            sp.regexp = txtRegexp.Text;
            sp.compare = cmbCompare.Text;
            sp.action = cmbAction.Text;
            CacheStatus sc = sp.Save();
            textStatus.Clear();
            textStatus.AppendText("Результат изменения объекта: " + sc.IsOK.ToString());  
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                txtRegexp.Text = "";
                cmbCompare.Text = "";
                cmbAction.Text = "";

                CacheStatus sc = User.SearchPattern.DeleteId(CacheConnect, sp.Id());

                //Вывод сообщения
                textStatus.Clear();
                textStatus.AppendText("Результат удаления объекта: " + sc.IsOK.ToString());
            }
            catch (Exception eLoad)
            {
                txtID.Text = "";
                textStatus.Clear();
                textStatus.AppendText("Ошибка: " + eLoad.Message);
            }


        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            try
            {
                txtFileName.Text = "";
                txtKeywords.Text = "";
                txtSize.Text = "";
                cmbFormat.Text = "";
                txtContent.Text = "";
                f = User.File.OpenId(CacheConnect, txtFileID.Text);

                txtFileName.Text = f.name.ToString();
                txtKeywords.Text = f.keywords.ToString();
                txtSize.Text = f.size.ToString();
                cmbFormat.Text = f.format.ToString();
                txtContent.Text = f.content.ToString();
                //Вызов методов
                textStatus2.Clear();
                textStatus2.AppendText("ID=" + f.Id().ToString() + Environment.NewLine);
                textStatus2.AppendText("calcSize() результат: =" + f.calcSize().ToString() + Environment.NewLine);
                textStatus2.AppendText("calcSize2(5) результат: =" + f.calcSize2(5).ToString());
            }
            catch (Exception eLoad)
            {
                txtID.Text = "";
                textStatus.Clear();
                textStatus.AppendText("Ошибка: " + eLoad.Message);
            }
        }




    }
}
