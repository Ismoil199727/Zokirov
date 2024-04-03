using System;
using System.Drawing;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Zokirov
{
    public partial class Uy_hisoboti : Form
    {
        public Uy_hisoboti()
        {
            InitializeComponent();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            
        }

        private void ID_MouseUp(object sender, MouseEventArgs e)
        {
            ID.Clear();
            ID.ForeColor = Color.Black;
        }

        private void Kirim_tasdiqlash_Click(object sender, EventArgs e)
        {
            try
            {
                if (Kirim_izoh.Text != "" && Kirim_summa.Text != "")
                {
                    double.Parse(Kirim_summa.Text);
                    string connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=database.accdb;Persist Security Info=False;";
                    OleDbConnection con = new OleDbConnection(connectionString);
                    con.Open();
                    string insertDataQuery = $"INSERT INTO Kirim (Izoh, Summa, Vaqt) VALUES ('{Kirim_izoh.Text}', '{Kirim_summa.Text}', '{Kirim_vaqt.Text}')";
                    OleDbCommand command = new OleDbCommand(insertDataQuery, con);
                    command.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Ma'lumot kiritildi", "Xabar");
                    Kirim_izoh.Text = "";
                    Kirim_summa.Text = "";
                    restart(true);
                }
                
            }
            catch(Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }
        private void yangiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Kirim_izoh.Select();
            restart(true);
            restart(false);
        }
        public void restart(bool text) // true - kirim
        {
            if(text)
            {
                double s = 0;
                try
                {
                    kirim_ruyhat.Rows.Clear();
                    string connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=database.accdb;Persist Security Info=False;";
                    OleDbConnection con = new OleDbConnection(connectionString);
                    con.Open();
                    string queryCommand = "SELECT * FROM Kirim";
                    OleDbCommand command = new OleDbCommand(queryCommand, con);
                    OleDbDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        s += double.Parse(reader["Summa"].ToString());
                        kirim_ruyhat.Rows.Add(false, reader["ID"], reader["Izoh"], reader["Summa"], reader["Vaqt"]);
                    }
                    Kirim_umumiy.Text = s.ToString();
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }
            }
            else
            {
                double s = 0;
                try
                {
                    Chiqim_ruyhat.Rows.Clear();
                    string connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=database.accdb;Persist Security Info=False;";
                    OleDbConnection con = new OleDbConnection(connectionString);
                    con.Open();
                    string queryCommand = "SELECT * FROM Chiqim";
                    OleDbCommand command = new OleDbCommand(queryCommand, con);
                    OleDbDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        s += double.Parse(reader["Summa"].ToString());
                        Chiqim_ruyhat.Rows.Add(false, reader["ID"], reader["Izoh"], reader["Summa"], reader["Vaqt"]);
                    }
                    Chiqim_umumiy.Text = s.ToString();
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }
            }
        }

        private void Vaqt_buyicha_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            restart(true);
            restart(false);
        }

        private void Chiqim_tasdiqlash_Click(object sender, EventArgs e)
        {
            try
            {
                if (Chiqim_izoh.Text != "" && Chiqim_summa.Text != "")
                {
                    double.Parse(Chiqim_summa.Text);
                    string connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=database.accdb;Persist Security Info=False;";
                    OleDbConnection con = new OleDbConnection(connectionString);
                    con.Open();
                    string insertDataQuery = $"INSERT INTO Chiqim (Izoh, Summa, Vaqt) VALUES ('{Chiqim_izoh.Text}', '{Chiqim_summa.Text}', '{Chiqim_vaqt.Text}')";
                    OleDbCommand command = new OleDbCommand(insertDataQuery, con);
                    command.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Ma'lumot kiritildi", "Xabar");
                    Chiqim_izoh.Text = "";
                    Chiqim_summa.Text = "";
                    restart(false);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Haqiqatdan ham o'chirasizmi?", "Surov", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string k = "", c = "";
                foreach (DataGridViewRow item in kirim_ruyhat.Rows)
                {
                    if (Convert.ToBoolean(item.Cells["Kirim"].Value))
                    {
                        k += Convert.ToString(item.Cells["_id"].Value)+",";
                    }
                }
                foreach (DataGridViewRow item in Chiqim_ruyhat.Rows)
                {

                    if (Convert.ToBoolean(item.Cells["Chiqim"].Value))
                    {
                        c += Convert.ToString(item.Cells["_id_"].Value) + ",";
                    }
                }
                if (c.Length != 0)
                {
                    string connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=database.accdb;Persist Security Info=False;";
                    OleDbConnection con = new OleDbConnection(connectionString);
                    string queryString = $"DELETE FROM Chiqim WHERE ID IN ({c.Remove(c.Length - 1, 1)});";
                    con.Open();
                    OleDbCommand command = new OleDbCommand(queryString, con);
                    command.ExecuteNonQuery();
                    con.Close();
                    //MessageBox.Show("O'chirildi");
                }
                if(k.Length != 0)
                {
                    string connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=database.accdb;Persist Security Info=False;";
                    OleDbConnection con = new OleDbConnection(connectionString);
                    string queryString = $"DELETE FROM Kirim WHERE ID IN ({k.Remove(k.Length - 1, 1)}); ";
                    con.Open();
                    OleDbCommand command = new OleDbCommand(queryString, con);
                    command.ExecuteNonQuery();
                    con.Close();
                    //MessageBox.Show("O'chirildi");
                }
                restart(true);
                restart(false);
            }

            
        }

        private void Chiqim_ruyhat_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {

            try
            {
                kirim_ruyhat.Rows.Clear();
                string connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=database.accdb;Persist Security Info=False;";
                OleDbConnection con = new OleDbConnection(connectionString);
                con.Open();
                string queryCommand = "SELECT * FROM Kirim";
                OleDbCommand command = new OleDbCommand(queryCommand, con);
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["ID"].ToString().ToLower().Contains(ID.Text.ToLower()) || reader["Izoh"].ToString().ToLower().Contains(ID.Text.ToLower()) || reader["Summa"].ToString().ToLower().Contains(ID.Text.ToLower()) || reader["Vaqt"].ToString().ToLower().Contains(ID.Text.ToLower()))
                        kirim_ruyhat.Rows.Add(false, reader["ID"], reader["Izoh"], reader["Summa"], reader["Vaqt"]);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }

            try
            {
                Chiqim_ruyhat.Rows.Clear();
                string connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=database.accdb;Persist Security Info=False;";
                OleDbConnection con = new OleDbConnection(connectionString);
                con.Open();
                string queryCommand = "SELECT * FROM Chiqim";
                OleDbCommand command = new OleDbCommand(queryCommand, con);
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["ID"].ToString().Contains(ID.Text) || reader["Izoh"].ToString().Contains(ID.Text) || reader["Summa"].ToString().Contains(ID.Text) || reader["Vaqt"].ToString().Contains(ID.Text))
                        Chiqim_ruyhat.Rows.Add(false, reader["ID"], reader["Izoh"], reader["Summa"], reader["Vaqt"]);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }

        }

        private void chiqimniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Haqiqatdan ham chiqim jadvali tozalansinmi?", "Surov", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=database.accdb;Persist Security Info=False;";
                OleDbConnection con = new OleDbConnection(connectionString);
                string queryString = $"DELETE FROM Chiqim WHERE ID > -1;";
                con.Open();
                OleDbCommand command = new OleDbCommand(queryString, con);
                command.ExecuteNonQuery();
                con.Close();
                restart(false);
            }
        }

        private void ID_Enter(object sender, EventArgs e)
        {
            
        }

        private void kirimniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Haqiqatdan ham kirim jadvali tozalansinmi?", "Surov", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=database.accdb;Persist Security Info=False;";
                OleDbConnection con = new OleDbConnection(connectionString);
                string queryString = $"DELETE FROM Kirim WHERE ID > -1;";
                con.Open();
                OleDbCommand command = new OleDbCommand(queryString, con);
                command.ExecuteNonQuery();
                con.Close();
                restart(true);
            }
        }

        private void hammasiniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Haqiqatdan ham barcha jadvallar tozalansinmi?", "Surov", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=database.accdb;Persist Security Info=False;";
                OleDbConnection con = new OleDbConnection(connectionString);
                string k = $"DELETE FROM Kirim WHERE ID > -1;";
                string c = $"DELETE FROM Chiqim WHERE ID > -1;";
                con.Open();
                OleDbCommand command1 = new OleDbCommand(k, con);
                OleDbCommand command2 = new OleDbCommand(c, con);
                command1.ExecuteNonQuery();
                command2.ExecuteNonQuery();
                con.Close();
                restart(false);
                restart(true);
            }
        }

        private void umumiyHisobotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Hisobingiz: {double.Parse(Kirim_umumiy.Text) - double.Parse(Chiqim_umumiy.Text)}");
        }

        private void dasturchiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string text = "Ism: Ismoil\n" +
                          "Familya: Xasraqulov\n" +
                          "Tug'ilgan yil: 1997\n" +
                          "Viloyat: Toshkent\n" +
                          "Talim dargoh: O'zbekiston Milliy Universiteti\n" +
                          "Telifon raqam: +998934429779";
            MessageBox.Show(text, "Info");
        }

        private void dasturToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string text = "Bu dastur kurs ishi qilib tayorlandi.\n" +
                          "Uydagi hisob kitoblarni yengillashtirish uchun hizmat qiladi.\n" +
                          "Ma'lumotlar Microsoft Access fayliga saqlanadi.\n" +
                          "Dastur (29.04.2024 20:40)da tuliq tugatildi";
            MessageBox.Show(text, "Info");
        }
    }
}
