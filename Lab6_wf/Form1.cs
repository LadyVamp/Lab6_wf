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
                //textStatus.AppendText("Площадь сектора(с++)(180 градусов)=" + sp.AreaAngleCpp(180).ToString());
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
