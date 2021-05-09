using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace Manager_DPStuff
{
    public partial class Form1 : Form
    {
        DataClass myClass = new DataClass();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cbDeliveryType.DataSource = myClass.GetListItems("select name from DeliveryType");
        }

        bool validForm()
        {
            return true;
        }

        void clearForm()
        {

        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (!validForm()) { return; }

            string query = $"insert into contacts(phone, email) values " +
                $"('{mtbPhone.Text}', '{tbEmail.Text}')";
            myClass.executeQuery(query);

            query = $"insert into client(firstName, lastName, patronymic, idContacts) values " +
                $"('{tbFName.Text}', '{tbLName.Text}', '{tbPatron.Text}', " +
                $"(select id from contacts where phone='{mtbPhone.Text}' and email='{tbEmail.Text}'))";
            myClass.executeQuery(query);

            query = $"insert into orderInfo(addressOrder, dateOrder, priorityOrder, idClient, idDeliveryType, idOrderStatus) values " +
                $"('{tbAddress.Text}', '{DateTime.Now}', 10, (select id from Client where firstName='{tbFName.Text}' and lastName='{tbLName.Text}' and patronymic='{tbPatron.Text}'), " +
                $"(select id from DeliveryType where name='{cbDeliveryType.SelectedItem}'), 1)";
            myClass.executeQuery(query);

            clearForm();
            MessageBox.Show("Order wast confirmed", "Succes");
        }
    }
}
