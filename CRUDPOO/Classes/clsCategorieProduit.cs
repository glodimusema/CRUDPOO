using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDPOO.Classes
{
    class clsCategorieProduit
    {
        int id;
        string nom_categorie;

        public int Id { get => id; set => id = value; }
        public string Nom_categorie { get => nom_categorie; set => nom_categorie = value; }
    }
}
