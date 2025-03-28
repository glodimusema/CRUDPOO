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

namespace CRUDPOO.Formulaires
{
    public partial class frmCatgorie : Form
    {
        public frmCatgorie()
        {
            InitializeComponent();
        }

        clsCategorieProduit cat = new clsCategorieProduit();

        private void button1_Click(object sender, EventArgs e)
        {
            txtId.Text = "";
            txtDesignation.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cat.Nom_categorie = txtDesignation.Text;
            clsGlossiaire.Getinstance().SaveCategorieProduit(cat);
            listeCategorie.DataSource = clsGlossiaire.Getinstance().loadData("tCategorieProduit");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cat.Id = int.Parse(txtId.Text);
            cat.Nom_categorie = txtDesignation.Text;
            clsGlossiaire.Getinstance().updateCategorieProduit(cat);
            listeCategorie.DataSource = clsGlossiaire.Getinstance().loadData("tCategorieProduit");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            clsGlossiaire.Getinstance().SupprimerData("tCategorieProduit", "id", txtId.Text);
            listeCategorie.DataSource = clsGlossiaire.Getinstance().loadData("tCategorieProduit");
        }

        private void frmCatgorie_Load(object sender, EventArgs e)
        {
            listeCategorie.DataSource = clsGlossiaire.Getinstance().loadData("tCategorieProduit");
        }
    }
}
