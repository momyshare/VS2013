using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using HtmlAgilityPack;


namespace crawler1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            //richTextBox1.Text =  GetWebText(textBox1.Text);
            for (int i = 1; i < 100; i++)
            {
                string str = GetWebText("http://mihanblog.com/post/"+i.ToString());
                string fi = "a href=\"/post/" + i.ToString() + "\">";
                int j = str.IndexOf(fi);
                string title = str.Substring(j + fi.Length + 3, 500);
                title = title.Substring(0, title.IndexOf("</"));
                richTextBox1.Text += title + "\n";
                
            }
            
        }
        private static string GetWebText(string url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.UserAgent = "A .NET Web Crawler";
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            string htmlText = reader.ReadToEnd();
            return htmlText;
        }
        int page = 1;
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                while (true)
                {
                    richTextBox1.Text += "------------page:"+page+"\n";
                    //asd
                    HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                    string htmlString = GetWebText(textBox1.Text + "/page/" + page++.ToString()); ;
                    document.LoadHtml(htmlString);
                    HtmlNodeCollection collection = document.DocumentNode.SelectNodes("//div[@id='posttitle']");
                    foreach (HtmlNode link in collection)
                    {
                        string target = link.ChildNodes[0].ChildNodes[0].InnerHtml; //link.Attributes["href"].Value;
                        richTextBox1.Text += target + "\n";
                    } 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
}
