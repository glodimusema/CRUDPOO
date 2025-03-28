using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUDPOO.Classes
{
    class clsGlossiaire
    {
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataAdapter dt = null;
        SqlDataReader dr = null;
        public static clsGlossiaire _glos = null;

        public static clsGlossiaire Getinstance()
        {
            if (_glos == null)
                _glos = new clsGlossiaire();
            return _glos;
        }

        public void InnitialiseConnexion()
        {
            try
            {
                con = new SqlConnection(clsConnexion.chemin);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private static void setParameter(SqlCommand cmd, string name, DbType type, int length, object paramValue)
        {
            IDbDataParameter a = cmd.CreateParameter();
            a.ParameterName = name;
            a.Size = length;
            a.DbType = type;

            if (paramValue == null)
            {
                if (!a.IsNullable)
                {
                    a.DbType = DbType.String;
                }
                a.Value = DBNull.Value;
            }
            else
                a.Value = paramValue;
            cmd.Parameters.Add(a);
        }

        public void loadCombo(string nomTable, string nomchamp, System.Windows.Forms.ComboBox comb1)
        {
            InnitialiseConnexion();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            DataTable table = new DataTable();
            dt = new SqlDataAdapter("SELECT " + nomchamp + " FROM " + nomTable + "", con);
            try
            {
                DataTable dt1 = new DataTable();
                dt.Fill(dt1);

                foreach (DataRow dr in dt1.Rows)
                {
                    comb1.Items.Add(dr["nomEcol"]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur" + ex);
            }

            con.Close();
        }

        public string getcode_Combo(string nomTable, string nomChampId, string nomChamp, string valeur)
        {
            string IdData = "";
            try
            {
                InnitialiseConnexion();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("select " + nomChampId + " from " + nomTable + " where " + nomChamp + "=@a", con);
                cmd.Parameters.AddWithValue("@a", valeur);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    IdData = (dr[nomChampId].ToString());
                }
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return IdData;
        }

        public DataSet get_Report_All(string nomTable)
        {
            DataSet dst;
            try
            {
                InnitialiseConnexion();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("SELECT * FROM " + nomTable + "", con);
                dt = new SqlDataAdapter(cmd);
                dst = new DataSet();
                dt.Fill(dst, nomTable);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dt.Dispose(); con.Close();
            }
            return dst;
        }
        public DataSet get_Report_Trier(string nomTable, string nomchamp, DateTime val1, DateTime val2)
        {
            DataSet dst;
            try
            {
                InnitialiseConnexion();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("SELECT * FROM " + nomTable + " WHERE " + nomchamp + " between @date1 and @date2 ", con);
                setParameter(cmd, "@date1", DbType.DateTime, 30, val1);
                setParameter(cmd, "@date2", DbType.DateTime, 30, val2);
                dt = new SqlDataAdapter(cmd);
                dst = new DataSet();
                dt.Fill(dst, nomTable);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dt.Dispose(); con.Close();
            }
            return dst;
        }

        public DataSet get_Report_One(string nomTable, string nomchamp, string val1)
        {
            DataSet dst;
            try
            {
                InnitialiseConnexion();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new SqlCommand("SELECT * FROM " + nomTable + " WHERE " + nomchamp + " = @val1 ", con);
                setParameter(cmd, "@val1", DbType.String, 30, val1);
                dt = new SqlDataAdapter(cmd);
                dst = new DataSet();
                dt.Fill(dst, nomTable);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dt.Dispose(); con.Close();
            }
            return dst;
        }
    



    public DataTable loadData(string nomTable)
        {
           InnitialiseConnexion();
            if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            DataTable table = new DataTable();
            dt = new SqlDataAdapter("select * from " + nomTable + "", con);
            dt.Fill(table);
            con.Close();

            return table;
        }

        public void SupprimerData(string nomTable, string nomChamp, string value)
        {
            con = new SqlConnection(clsConnexion.chemin);
            con.Open();
            cmd = new SqlCommand("delete from " + nomTable + " where " + nomChamp + "=@id", con);
            cmd.Parameters.AddWithValue("@id", value);
            cmd.ExecuteNonQuery();
            con.Close();
        }


        public void SaveClient(clsClient cl)
        {
            InnitialiseConnexion();
            con.Open();
            cmd = new SqlCommand("insert into tClient (noms,adresse,contact) values (@noms,@adresse,@contact)", con);
            cmd.Parameters.AddWithValue("@noms", cl.Noms);
            cmd.Parameters.AddWithValue("@adresse", cl.Adresse);
            cmd.Parameters.AddWithValue("@contact", cl.Contact);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void updateClient(clsClient cl)
        {
            InnitialiseConnexion();
            con.Open();
            cmd = new SqlCommand("update tClient set noms=@noms,adresse=@adresse,contact=@contact where id=@id", con);
            cmd.Parameters.AddWithValue("@id", cl.Id);
            cmd.Parameters.AddWithValue("@noms", cl.Noms);
            cmd.Parameters.AddWithValue("@adresse", cl.Adresse);
            cmd.Parameters.AddWithValue("@contact", cl.Contact);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void SaveCategorieProduit(clsCategorieProduit cat)
        {
            InnitialiseConnexion();
            con.Open();
            cmd = new SqlCommand("insert into tCategorieProduit (nom_categorie) values (@nom_categorie)", con);
            cmd.Parameters.AddWithValue("@nom_categorie", cat.Nom_categorie);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void updateCategorieProduit(clsCategorieProduit cat)
        {
            InnitialiseConnexion();
            con.Open();
            cmd = new SqlCommand("update tCategorieProduit set nom_categorie=@nom_categorie where id=@txtId", con);
            cmd.Parameters.AddWithValue("@nom_categorie", cat.Nom_categorie);
            cmd.Parameters.AddWithValue("@txtId", cat.Id);
            cmd.ExecuteNonQuery();
            con.Close();
        }


        public void SaveProduit(clsProduit pro)
        {
            InnitialiseConnexion();
            con.Open();
            cmd = new SqlCommand("insert into tProduit (nom_produit,prix_unitaire,qte,refCategorie) values (@nom_produit,@prix_unitaire,@qte,@refCategorie)", con);
            cmd.Parameters.AddWithValue("@nom_produit", pro.Nom_produit);
            cmd.Parameters.AddWithValue("@prix_unitaire", pro.Prix_unitaire);
            cmd.Parameters.AddWithValue("@qte", pro.Qte);
            cmd.Parameters.AddWithValue("@refCategorie", pro.RefCategorie);
            cmd.ExecuteNonQuery();
            con.Close();
        }


        public void updateProduit(clsProduit pro)
        {
            InnitialiseConnexion();
            con.Open();
            cmd = new SqlCommand("update tProduit set nom_produit=@nom_produit,prix_unitaire=@prix_unitaire,qte=@qte,refCategorie=@refCategorie where id=@id", con);
            cmd.Parameters.AddWithValue("@id", pro.Id);
            cmd.Parameters.AddWithValue("@nom_produit", pro.Nom_produit);
            cmd.Parameters.AddWithValue("@prix_unitaire", pro.Prix_unitaire);
            cmd.Parameters.AddWithValue("@qte", pro.Qte);
            cmd.Parameters.AddWithValue("@refCategorie", pro.RefCategorie);
            cmd.ExecuteNonQuery();
            con.Close();
        }


        public void SaveVente(clsVente vente)
        {
            InnitialiseConnexion();
            con.Open();
            cmd = new SqlCommand("insert into tVente (refClient,refProduit,qte_vente,pu_vente,date_vente) values (@refClient,@refProduit,@qte_vente,@pu_vente,@date_vente)", con);
            cmd.Parameters.AddWithValue("@refClient", vente.RefClient);
            cmd.Parameters.AddWithValue("@refProduit", vente.RefProduit);
            cmd.Parameters.AddWithValue("@qte_vente", vente.Qte_vente);
            cmd.Parameters.AddWithValue("@pu_vente", vente.Pu_vente);
            cmd.Parameters.AddWithValue("@date_vente", vente.Date_vente);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void updateVente(clsVente vente)
        {
            InnitialiseConnexion();
            con.Open();
            cmd = new SqlCommand("update tVente set refClient=@refClient,refProduit=@refProduit,qte_vente=@qte_vente,pu_vente=@pu_vente,date_vente=@date_vente where id=@id)", con);
            cmd.Parameters.AddWithValue("@id", vente.Id);
            cmd.Parameters.AddWithValue("@refClient", vente.RefClient);
            cmd.Parameters.AddWithValue("@refProduit", vente.RefProduit);
            cmd.Parameters.AddWithValue("@qte_vente", vente.Qte_vente);
            cmd.Parameters.AddWithValue("@pu_vente", vente.Pu_vente);
            cmd.Parameters.AddWithValue("@date_vente", vente.Date_vente);
            cmd.ExecuteNonQuery();
            con.Close();
        }

    }
}
