using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AltCreate
{
    public partial class Form1 : Form
    {
        private String apiUrl = "https://api.datamuse.com/words?ml=";
        String loadedText = String.Empty;
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            loadedText = richTextBox2.Text;
            label4.Text = "Loaded text: " + loadedText;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this is where shit goes down
            String replacedText = loadedText;
            foreach (String replaceWord in richTextBox1.Text.Split('\n'))
            {
                String wordToReplaceWith = replaceWord.Replace(' ', '+');
                String json = new WebClient().DownloadString(apiUrl + wordToReplaceWith);
                JArray a = JArray.Parse(json);
                JObject o = JObject.Parse(a[new Random().Next(a.Count - 1)].ToString());
                wordToReplaceWith = o.GetValue("word").ToString();

                replacedText = replacedText.Replace(replaceWord, wordToReplaceWith);
            }
            richTextBox2.Text = replacedText;
        }
    }
}