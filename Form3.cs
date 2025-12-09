using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Формы
{
    public partial class Form3: Form
    {
        public Form3()
        {
            InitializeComponent();
            this.Scale(new SizeF(1.0f, 1.0f)); // Принудительный масштаб 100%
            this.AutoScaleMode = AutoScaleMode.None;
            this.AutoScaleDimensions = new SizeF(96F, 96F); // Стандартные 96 DPI
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 1. ПРОВЕРКА ОБЯЗАТЕЛЬНЫХ ПОЛЕЙ
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Введите ваше ФИО!", "Ошибка");
                textBox1.Focus();
                return;
            }

            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Введите ваш телефон!", "Ошибка");
                textBox2.Focus();
                return;
            }

            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Выберите тур!", "Ошибка");
                comboBox1.Focus();
                return;
            }

            if (dateTimePicker1.Text == null)
            {
                MessageBox.Show("Выберите время!", "Ошибка");
                dateTimePicker1.Focus();
                return;
            }

            // 2. СОБИРАЕМ ДАННЫЕ
            string ФИО = textBox1.Text;
            string телефон = textBox2.Text;
            string тур = comboBox1.SelectedItem.ToString();
            string количество = comboBox2.Text;
            string дата = dateTimePicker1.Value.ToString("dd.MM.yyyy");
            string пожелание = textBox4.Text;
            string email = textBox3.Text;

            // 3. ФОРМИРУЕМ СООБЩЕНИЕ
            string сообщение = $"ЗАПИСЬ ПРИНЯТА!\n\n" +
                              $"ФИО: {ФИО}\n" +
                              $"Телефон: {телефон}\n" +
                              $"Тур: {тур}\n" +
                              $"Дата: {дата}\n";

            if (!string.IsNullOrEmpty(email))
                сообщение += $"Email: {email}\n";

            if (!string.IsNullOrEmpty(пожелание))
                сообщение += $"Пожелание: {пожелание}\n";

            сообщение += $"\nМы вам перезвоним для подтверждения!";

            // 4. ПОКАЗЫВАЕМ РЕЗУЛЬТАТ
            MessageBox.Show(сообщение, "Успешная запись!");

            // 5. ОЧИЩАЕМ ФОРМУ (опционально)
            // ОчисткаФормы();

            {
                // Данные из формы
                string ФИО_клиента = textBox1.Text;
                string Телефон = textBox2.Text;
                string Email = textBox3.Text;
                string Тур = comboBox1.Text;
                string Количество = comboBox2.Text;
                string Дата_записи = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                string Пожелание = textBox4.Text;

                // Используем MDB файл
                string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\user\Desktop\учеба\3 курс\анализ ПО\Формы\вся инфа\Формы\bin\Debug\амам.mdb";
                using (OleDbConnection dbConnection = new OleDbConnection(connectionString))
                {
                    try
                    {
                        dbConnection.Open();

                        // Запрос с ТОЧНЫМИ названиями полей из вашей таблицы
                        string query = "INSERT INTO Данные ([ФИО], [Телефон], [Почта], [Тур], [Количество], [Желаемая дата], [Пожелания]) " +
                                       "VALUES (@ФИО, @Телефон, @Почта, @Тур, @Количество, @Желаемая_дата, @Комментарий)";

                        using (OleDbCommand dbCommand = new OleDbCommand(query, dbConnection))
                        {
                            // Параметризованный запрос
                            dbCommand.Parameters.AddWithValue("@ФИО", ФИО_клиента);
                            dbCommand.Parameters.AddWithValue("@Телефон", Телефон);
                            dbCommand.Parameters.AddWithValue("@Почта", Email);
                            dbCommand.Parameters.AddWithValue("@Тур", Тур);
                            dbCommand.Parameters.AddWithValue("@Количество", Количество);
                            dbCommand.Parameters.AddWithValue("@Желаемая_дата", Дата_записи);
                            dbCommand.Parameters.AddWithValue("@Пожелания", Пожелание);

                            if (dbCommand.ExecuteNonQuery() != 1)
                                MessageBox.Show("Ошибка выполнения запроса", "Внимание!");
                            else
                                MessageBox.Show("Данные добавлены!", "Успешно");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка: " + ex.Message, "Ошибка!");
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Вернуться на главную страницу?\nНесохраненные данные будут потеряны.",
                "Подтверждение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close(); // Закрываем форму записи
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
