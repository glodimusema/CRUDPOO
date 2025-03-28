using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDPOO.Classes
{
    class clsVente
    {
        int id;
        int refClient;
        int refProduit;
        double qte_vente;
        double pu_vente;
        DateTime date_vente;

        public int Id { get => id; set => id = value; }
        public int RefClient { get => refClient; set => refClient = value; }
        public int RefProduit { get => refProduit; set => refProduit = value; }
        public double Qte_vente { get => qte_vente; set => qte_vente = value; }
        public double Pu_vente { get => pu_vente; set => pu_vente = value; }
        public DateTime Date_vente { get => date_vente; set => date_vente = value; }
    }
}
