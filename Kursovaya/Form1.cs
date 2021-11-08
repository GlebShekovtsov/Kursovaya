using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Kursovaya
{
    public partial class Form1 : Form
    {
        
        const int t = 60;
        int zadumano = 0;
        int ostalos = 60;
        int nomer_popitki = 0;
        DBconnection connect = new DBconnection();
        RecordClass record = new RecordClass();

        public Form1()

        {
            InitializeComponent();
            showRecInfo();
            textBox2.Focus();
            if (textBox2.Text == "")
            {
                textBox1.Enabled = false;
            }
            button1.Enabled = false;
            label2.Text = "";
            toolStripStatusLabel1.Text = "У вас осталось: " + Convert.ToString(t) + " сек";
            toolStripStatusLabel2.Text = " Попыток: 0 ";
            timer1.Enabled = true;
            timer1.Interval = 1000;
            progressBar1.Maximum = t;
            progressBar1.Value = t;
            progressBar1.Step = 1;
            Random n = new Random();
            zadumano = n.Next(100);

        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            ostalos--;
            progressBar1.Value--;
            toolStripStatusLabel1.Text = "У вас осталось: " + Convert.ToString(ostalos) + " сек";
            if (ostalos == 0)
            {
                timer1.Enabled = false;
                textBox1.Enabled = false;
                progressBar1.Enabled = false;
                label2.Text = "Увы, время истекло...";
            }
        }
     


        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals((char)13))
            {
                try
                {
                    listBox1.Items.Add(textBox1.Text);
                    if (Convert.ToInt16(textBox1.Text) == zadumano)
                    {
                        timer1.Enabled = false;
                        textBox1.Enabled = false;
                        label2.Text = "Вы угадали!, задумывалось число " + Convert.ToString(zadumano);
                        button1.Enabled = true;
                    };
                    if (Convert.ToInt16(textBox1.Text) > zadumano) label2.Text = "Задуманное число меньше";
                    if (Convert.ToInt16(textBox1.Text) < zadumano) label2.Text = "Задуманное число больше";
                }
                catch { label2.Text = "Некорректные входные данные!"; }
                nomer_popitki++;
                toolStripStatusLabel2.Text = " Попыток: " + Convert.ToString(nomer_popitki);
                textBox1.Text = "";
                textBox1.Focus();
            }
        }
        bool verify()
        {
            if ((textBox2.Text == ""))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void showRecInfo()
        {
            dataGridView1.DataSource = record.getreclist();
            DataGridViewColumn Column = new DataGridViewColumn();
            Column = dataGridView1.Columns[2];
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox2.Text;
            string tryes = Convert.ToString(nomer_popitki);


            if (verify())
            {
                try
                {

                    if (record.insertrec(name, tryes))
                    {
                        MessageBox.Show("Новые данные успешно добавлены", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Введите своё имя для записи рекорда", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox1.Enabled = false;
            }
            else
            {
                textBox1.Enabled = true;
            }
        }
    }
}
