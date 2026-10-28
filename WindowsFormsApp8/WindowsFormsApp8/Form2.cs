using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace WindowsFormsApp8
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        public void listele()
        {
            MySqlConnection baglanti = new MySqlConnection("server=localhost;Database=stokkayıt;Uid=root;pwd=root");
            baglanti.Open();
            MySqlCommand komut = new MySqlCommand("select * from urunler", baglanti);
            DataSet ds = new DataSet();
            MySqlDataAdapter kopru = new MySqlDataAdapter(komut);
            kopru.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        public void temizle()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }
        public void arama(string kelime)
        {
            MySqlConnection baglanti = new MySqlConnection("Server=localhost;Database=stokkayıt;Uid=root;pwd=root");
            baglanti.Open();
            if (baglanti.State != ConnectionState.Open)
            {
                baglanti.Open();
            }
            MySqlCommand ara = new MySqlCommand("select * from urunler where barkod like '" + textBox5.Text + "%'", baglanti);
            DataSet ds = new DataSet();
            MySqlDataAdapter kopru = new MySqlDataAdapter(ara);
            kopru.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            baglanti.Close();

        }

        private void Form2_Load(object sender, EventArgs e)
        {

            listele();
            MySqlConnection baglanti = new MySqlConnection("server=localhost;Database=stokkayıt;Uid=root;pwd=root");
            baglanti.Open();
            MySqlCommand getir = new MySqlCommand("select tur from urunler", baglanti);
            MySqlDataReader okuyucu = getir.ExecuteReader();
            while (okuyucu.Read())
            {
                comboBox2.Items.Add(okuyucu["tur"].ToString());
            }
            baglanti.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Refresh();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells["barkod"].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells["urunadi"].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells["adet"].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells["fiyat"].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells["renk"].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells["tur"].Value.ToString();
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells["kayittarihi"].Value.ToString();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            MySqlConnection baglanti = new MySqlConnection("server=localhost;Database=stokkayıt;Uid=root;pwd=root");
            baglanti.Open();
            if (baglanti.State != ConnectionState.Open)
            {
                baglanti.Open();
            }
            MySqlCommand sil = new MySqlCommand("delete from urunler where urunadi=@ad", baglanti);
            sil.Parameters.AddWithValue("@ad", dataGridView1.CurrentRow.Cells["urunadi"].Value.ToString());
            sil.ExecuteNonQuery();
            MessageBox.Show("Kaydınız başarıyla silindi");
            listele();
            baglanti.Close();
            temizle();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnection baglanti = new MySqlConnection("Server=localhost;Database=stokkayıt;Uid=root;pwd=root");
            baglanti.Open();
            if (baglanti.State != ConnectionState.Open)
            {
                baglanti.Open();
            }
            if (comboBox3.SelectedIndex == 1)
            {
                MySqlCommand ara = new MySqlCommand("select * from urunler where barkod=@bno", baglanti);
                ara.Parameters.AddWithValue("@bno", textBox5.Text);
                DataSet ds = new DataSet();
                MySqlDataAdapter kopru = new MySqlDataAdapter(ara);
                kopru.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                baglanti.Close();
            }
            if (comboBox3.SelectedIndex == 2)
            {
                MySqlCommand ara = new MySqlCommand("select * from urunler where urunadi=@ad", baglanti);
                ara.Parameters.AddWithValue("@ad", textBox5.Text);
                DataSet ds = new DataSet();
                MySqlDataAdapter kopru = new MySqlDataAdapter(ara);
                kopru.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                baglanti.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MySqlConnection baglanti = new MySqlConnection("server=localhost;Database=stokkayıt;Uid=root;pwd=root");
            baglanti.Open();
            if (baglanti.State != ConnectionState.Open)
            {
                baglanti.Open();
            }
            MySqlCommand ekle = new MySqlCommand("insert into urunler(barkod,urunadi,adet,fiyat,renk,tur,kayittarihi)values(@bno,@ad,@adet,@fiyat,@renk,@tur,@ktarih)", baglanti);
            ekle.Parameters.AddWithValue("@bno", textBox1.Text);
            ekle.Parameters.AddWithValue("@ad", textBox2.Text);
            ekle.Parameters.AddWithValue("@adet", textBox3.Text);
            ekle.Parameters.AddWithValue("@fiyat", textBox4.Text);
            ekle.Parameters.AddWithValue("@renk", comboBox1.Text);
            ekle.Parameters.AddWithValue("@tur", comboBox2.Text);
            ekle.Parameters.AddWithValue("@ktarih", dateTimePicker1.Value);
            ekle.ExecuteNonQuery();
            listele();
            baglanti.Close();
            temizle();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            MySqlConnection baglanti = new MySqlConnection("Server=localhost;Database=stokkayıt;Uid=root;pwd=root");
            baglanti.Open();
            if (baglanti.State != ConnectionState.Open)
            {
                baglanti.Open();
            }
            MySqlCommand guncelle = new MySqlCommand("update urunler set urunadi=@ad,adet=@adet,fiyat=@fiyat,renk=@renk,tur=@tur,kayittarihi=@ktarih where barkod=@bno", baglanti);
            guncelle.Parameters.AddWithValue("@bno", textBox1.Text);
            guncelle.Parameters.AddWithValue("@ad", textBox2.Text);
            guncelle.Parameters.AddWithValue("@adet", textBox3.Text);
            guncelle.Parameters.AddWithValue("@fiyat", textBox4.Text);
            guncelle.Parameters.AddWithValue("@renk", comboBox1.Text);
            guncelle.Parameters.AddWithValue("@tur", comboBox2.Text);
            guncelle.Parameters.AddWithValue("@ktarih", dateTimePicker1.Value);
            guncelle.ExecuteNonQuery();
            MessageBox.Show("Kaydınız güncellenmiştir");
            listele();
            baglanti.Close();
            temizle();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            MySqlConnection baglanti = new MySqlConnection("Server=localhost;Database=stokkayıt;Uid=root;pwd=root");
            baglanti.Open();
            MySqlCommand komut = new MySqlCommand("Select * from urunler where barkod=@bno", baglanti);
            komut.Parameters.AddWithValue("@bno", textBox1.Text);
            MySqlDataReader oku = komut.ExecuteReader();
            if (oku.HasRows == true)
            {
                MessageBox.Show("Böyle barkod numarası bulunmaktadır");
            }
            else
            {
                baglanti.Close();
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            arama(textBox5.Text);
        }
    }
}
