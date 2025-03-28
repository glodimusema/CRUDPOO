using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CRUDPOO.Classes;
using CRUDPOO.Rapports;
using DevExpress.XtraReports.UI;

namespace CRUDPOO.Formulaires
{
    public partial class frmClient : Form
    {
        public frmClient()
        {
            InitializeComponent();
        }

        clsClient cl = new clsClient();

        private void button1_Click(object sender, EventArgs e)
        {
            txtId.Text = "";
            txtNoms.Text = "";
            txtAdresse.Text = "";
            txtContact.Text = "";
        }

        private void frmClient_Load(object sender, EventArgs e)
        {
           listeClient.DataSource = clsGlossiaire.Getinstance().loadData("tClient");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cl.Noms = txtNoms.Text;
            cl.Adresse = txtAdresse.Text;
            cl.Contact = txtContact.Text;
            clsGlossiaire.Getinstance().SaveClient(cl);
            listeClient.DataSource = clsGlossiaire.Getinstance().loadData("tClient");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cl.Id = int.Parse(txtId.Text);
            cl.Noms = txtNoms.Text;
            cl.Adresse = txtAdresse.Text;
            cl.Contact = txtContact.Text;
            clsGlossiaire.Getinstance().updateClient(cl);
            listeClient.DataSource = clsGlossiaire.Getinstance().loadData("tClient");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            clsGlossiaire.Getinstance().SupprimerData("tClient", "id", txtId.Text);
            listeClient.DataSource = clsGlossiaire.Getinstance().loadData("tClient");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                rptListeClient rpt = new rptListeClient();
                rpt.DataSource = clsGlossiaire.Getinstance().get_Report_All("tClient");
                using (ReportPrintTool printTool = new ReportPrintTool(rpt))
                {
                    printTool.ShowPreviewDialog();
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }

        }
    }
}
