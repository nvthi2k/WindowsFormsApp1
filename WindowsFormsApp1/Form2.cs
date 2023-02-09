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
    public partial class Form2 : Form
    {
        private DataTable dt;
        private DataSet ds;
        public Form2()
        {
            InitializeComponent();
        }

        [Obsolete]
        private void Form2_Load(object sender, EventArgs e)
        {
            string path = @"C:\Users\theha\source\repos\WindowsFormsApp1\WindowsFormsApp1\xml_files\books.xml";
            XDocument xDocument = XDocument.Load(path);

            XmlDataDocument xmlmenu = new XmlDataDocument();
            xmlmenu.DataSet.ReadXml(path);
            ds = xmlmenu.DataSet;
            dt = ds.Tables[0];
            
            dataGridView1.DataSource = xmlmenu.DataSet;
            dataGridView1.DataMember = "book";



            var data = xDocument.Descendants("book").
                Select(o => new
                {
                    cate = o.Attribute("category").Value,
                    name = o.Element("title").Value,
                    lang = o.Element("title").Attribute("lang").Value,
                    /*author = xDocument.Descendants("book").
                    Select(y=>
                        y.Elements("author").
                        Select(x=>
                        x.Element("author").Value
                        )
                    ).ToList(),*/
                    author = o.Elements("author").ToList(),
                    /*author = o.Descendants("author").Select(y => new
                    {
                        author = (String)y.Element("author").Value
                    }).ToList(),*/
                    /*author = o.Elements("author").Select(y =>new
                    {
                        authors = y.Element("author").Value
                    }
                        ).ToList(),*/
                    year = o.Element("year").Value,
                    price = o.Element("price").Value
                }).ToList();



            int i = 0;
            foreach (var item in data)
            {
                ListViewItem name = new ListViewItem(item.name);
                ListViewItem cate = new ListViewItem(item.cate);
                ListViewItem lang = new ListViewItem(item.lang);
                List< ListViewItem> author = new List<ListViewItem>();
                
                
                ListViewItem year = new ListViewItem(item.year);
                ListViewItem price = new ListViewItem(item.price);
                /*lvi.Items.Add(item.cate);
                lvi.SubItems.Add(item.lang);
                lvi.SubItems.Add(item.author);
                lvi.SubItems.Add(item.year);
                lvi.SubItems.Add(item.price);*/
               
                listView1.Items.AddRange(new ListViewItem[] {name,cate,lang,year,price });
                foreach (var ind in item.author)
                {
                    String s = ind.ToString();
                    Console.WriteLine(s);
                    s = s.Replace("<author>", "");
                    s = s.Replace("</author>", "");
                    Console.WriteLine(s);

                    author.Add(new ListViewItem(s));
                    listView1.Items.Add(new ListViewItem(s));
                }
                /*listView1.Items.Add(item.cate);
                listView1.Items.Add(item.name);
                listView1.Items.Add(item.lang);
                foreach (var index in item.author)
                {
                    listView1.Items.Add(index.ToString());
                }
                
                listView1.Items.Add(item.year);
                listView1.Items.Add(item.price);*/
                i++;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        
        class Book
        {
            public Book(string cate, string name)
            {
            }

            public Book(string cate, string name, string lang, string[] author, string year, string price)
            {
                this.cate = cate;
                this.name = name;
                this.lang = lang;
                this.author = author;
                this.year = year;
                this.price = price;
            }

            public String cate { set; get; }
            public String name { set; get; }
            public String lang { set; get; }
            public String[] author { set; get; }
            public String year { set; get; }
            public String price { set; get; }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
