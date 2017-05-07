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
        }
    }
}
