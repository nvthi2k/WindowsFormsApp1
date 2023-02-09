using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace WindowsFormsApp1
{
    public partial class frmFood : Form
    {
        private DataSet ds;
        private DataTable dt;
        public frmFood()
        {
            InitializeComponent();
        }

        [Obsolete]
        private void Form1_Load(object sender, EventArgs e)
        {
            loadData(); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = @"C:\Users\theha\source\repos\WindowsFormsApp1\WindowsFormsApp1\xml_files\menu.xml";
            XDocument xDocument = XDocument.Load(path);

            XElement xElement = new XElement("food");
            xElement.SetElementValue("name", txtName.Text);
            xElement.SetElementValue("price", txtPrice.Text);
            xElement.SetElementValue("description", txtDes.Text);
            xElement.SetElementValue("calories", txtCal.Text);

            xDocument.Element("breakfast_menu").Add(xElement);
            xDocument.Save(path);
            loadData();
        }

        [Obsolete]
        private void loadData()
        {
            string path = @"C:\Users\theha\source\repos\WindowsFormsApp1\WindowsFormsApp1\xml_files\menu.xml";
            XDocument xDocument = XDocument.Load(path);

            XmlDataDocument xmlmenu = new XmlDataDocument();
            xmlmenu.DataSet.ReadXml(path);
            ds = xmlmenu.DataSet;
            dt = ds.Tables[0];
            dgvFood.DataSource = xmlmenu.DataSet;
            dgvFood.DataMember = "food";

            var data = xDocument.Descendants("food").
                            Select(o => new
                            {
                                name = o.Element("name").Value,
                                price = o.Element("price").Value,
                                des = o.Element("description").Value,
                                cal = o.Element("calories").Value

                            }).ToList();

            /*var list = xDocument.Descendants("food").
                                        Select(o => new
                                        {
                                            name = o.Element("name").Value,
                                            price = o.Element("price").Value,
                                            des = o.Element("description").Value,
                                            cal = o.Element("calories").Value

                                        }).ToList();*/
            

            int i = 0;
            foreach (var item in data)
            {

                listView1.Items.Add(item.name, 0);
                listView1.Items.Add(item.price, 1);
                listView1.Items.Add(item.des, 2);
                listView1.Items.Add(item.cal, 3);
                listView1.Items[i].SubItems.Add(item.name);
                listView1.Items[i].SubItems.Add(item.price);
                listView1.Items[i].SubItems.Add(item.des);
                listView1.Items[i].SubItems.Add(item.cal);
                /*dgvFood.Rows.Add(item);
                dgvFood.Rows.Add(item.price);
                dgvFood.Rows.Add(item.des);
                dgvFood.Rows.Add(item.cal);*/
                i++;
            }
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        [Obsolete]
        private void button2_Click(object sender, EventArgs e)
        {
            string path = @"C:\Users\theha\source\repos\WindowsFormsApp1\WindowsFormsApp1\xml_files\menu.xml";
            XDocument xDocument = XDocument.Load(path);

            var studentFilter = xDocument.Descendants("food").Where(x => (x.Element("name").Value) == txtName.Text).FirstOrDefault();

                 
           studentFilter.Element("name").Value = txtName.Text;
            studentFilter.Element("price").Value = txtPrice.Text;
            studentFilter.Element("description").Value = txtDes.Text;
            studentFilter.Element("calories").Value = txtCal.Text;

            xDocument.Save(path);
            loadData();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        [Obsolete]
        private void btnXoa_Click(object sender, EventArgs e)
        {
            string path = @"C:\Users\theha\source\repos\WindowsFormsApp1\WindowsFormsApp1\xml_files\menu.xml";
            XDocument xDocument = XDocument.Load(path);
            
            var studentFilter = xDocument.Descendants("food").Where(x => (x.Element("name").Value) == txtName.Text).FirstOrDefault();

            studentFilter.Remove();
            xDocument.Save(path);
            loadData();
        }

        private void dgvFood_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            
;            if (e.Equals(null))
            {
                txtName.Text = "";
            }
            else
            {
                txtName.Text = dt.Rows[row]["name"].ToString();
                txtPrice.Text = dt.Rows[row]["price"].ToString();
                txtDes.Text = dt.Rows[row]["description"].ToString();
                txtCal.Text = dt.Rows[row]["calories"].ToString();
            }
            
        }
    }
}
