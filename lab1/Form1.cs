using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace lab1
{
    public partial class Form1 : Form
    {
        private PCComponentCollection collection;
        private string dataFilePath = "components.dat";

        private TextBox txtName;
        private TextBox txtSerial;
        private TextBox txtManufacturer;
        private TextBox txtCountry;
        private TextBox txtPrice;
        private TextBox txtSearch;
        private ListBox lstData;

        public Form1()
        {
            InitializeComponent();
            collection = new PCComponentCollection();
            InitializeUI();
        }

        private void InitializeUI()
        {
            this.Text = "Облік комплектуючих ПК";
            this.Size = new Size(650, 450);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblName = new Label { Text = "Назва:", Location = new Point(20, 20), Width = 100 };
            txtName = new TextBox { Location = new Point(130, 20), Width = 150 };

            Label lblSerial = new Label { Text = "Серійний номер:", Location = new Point(20, 50), Width = 100 };
            txtSerial = new TextBox { Location = new Point(130, 50), Width = 150 };

            Label lblManufacturer = new Label { Text = "Виробник:", Location = new Point(20, 80), Width = 100 };
            txtManufacturer = new TextBox { Location = new Point(130, 80), Width = 150 };

            Label lblCountry = new Label { Text = "Країна:", Location = new Point(20, 110), Width = 100 };
            txtCountry = new TextBox { Location = new Point(130, 110), Width = 150 };

            Label lblPrice = new Label { Text = "Ціна:", Location = new Point(20, 140), Width = 100 };
            txtPrice = new TextBox { Location = new Point(130, 140), Width = 150 };

            Button btnAdd = new Button { Text = "Додати", Location = new Point(20, 180), Width = 260 };
            btnAdd.Click += BtnAdd_Click;

            Button btnSave = new Button { Text = "Серіалізувати", Location = new Point(20, 220), Width = 125 };
            btnSave.Click += BtnSave_Click;

            Button btnLoad = new Button { Text = "Десеріалізувати", Location = new Point(155, 220), Width = 125 };
            btnLoad.Click += BtnLoad_Click;

            txtSearch = new TextBox { Location = new Point(310, 20), Width = 300 };

            Button btnSearchName = new Button { Text = "Пошук за назвою", Location = new Point(310, 50), Width = 145 };
            btnSearchName.Click += BtnSearchName_Click;

            Button btnSearchCountry = new Button { Text = "Пошук за країною", Location = new Point(465, 50), Width = 145 };
            btnSearchCountry.Click += BtnSearchCountry_Click;

            Button btnShowAll = new Button { Text = "Показати всі", Location = new Point(310, 80), Width = 300 };
            btnShowAll.Click += BtnShowAll_Click;

            lstData = new ListBox { Location = new Point(310, 120), Width = 300, Height = 260 };

            this.Controls.Add(lblName);
            this.Controls.Add(txtName);
            this.Controls.Add(lblSerial);
            this.Controls.Add(txtSerial);
            this.Controls.Add(lblManufacturer);
            this.Controls.Add(txtManufacturer);
            this.Controls.Add(lblCountry);
            this.Controls.Add(txtCountry);
            this.Controls.Add(lblPrice);
            this.Controls.Add(txtPrice);
            this.Controls.Add(btnAdd);
            this.Controls.Add(btnSave);
            this.Controls.Add(btnLoad);
            this.Controls.Add(txtSearch);
            this.Controls.Add(btnSearchName);
            this.Controls.Add(btnSearchCountry);
            this.Controls.Add(btnShowAll);
            this.Controls.Add(lstData);
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtName.Text;
                string serial = txtSerial.Text;
                string manufacturer = txtManufacturer.Text;
                string country = txtCountry.Text;
                double price = Convert.ToDouble(txtPrice.Text);

                PCComponent newComponent = new PCComponent(name, serial, manufacturer, country, price);
                collection.Add(newComponent);

                MessageBox.Show("Комплектуючу успішно додано!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputs();
                RefreshList(collection.GetAll());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                collection.Serialize(dataFilePath);
                MessageBox.Show("Дані успішно збережено у файл.", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                collection.Deserialize(dataFilePath);
                RefreshList(collection.GetAll());
                MessageBox.Show("Дані успішно завантажено.", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSearchName_Click(object sender, EventArgs e)
        {
            string query = txtSearch.Text;
            if (string.IsNullOrWhiteSpace(query)) return;
            RefreshList(collection.FindByName(query));
        }

        private void BtnSearchCountry_Click(object sender, EventArgs e)
        {
            string query = txtSearch.Text;
            if (string.IsNullOrWhiteSpace(query)) return;
            RefreshList(collection.FindByCountry(query));
        }

        private void BtnShowAll_Click(object sender, EventArgs e)
        {
            RefreshList(collection.GetAll());
        }

        private void RefreshList(List<PCComponent> items)
        {
            lstData.Items.Clear();
            foreach (var item in items)
            {
                lstData.Items.Add(item.ToString());
            }
        }

        private void ClearInputs()
        {
            txtName.Clear();
            txtSerial.Clear();
            txtManufacturer.Clear();
            txtCountry.Clear();
            txtPrice.Clear();
        }
    }
}