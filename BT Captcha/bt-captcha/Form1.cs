using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;


namespace bt_captcha
{
    public partial class Form1 : Form
    {
        string strNamedPath = @"D:\img\named\";
        string strFile;

        Stack<string> newFiles = new Stack<string>(Directory.GetFiles(@"D:\img\new"));
        Stack<string> namedFiles = new Stack<string>(Directory.GetFiles(@"D:\img\named"));
        Stack<string> testFiles = new Stack<string>(Directory.GetFiles(@"D:\img\test"));

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //handleNewFiles();
            tweak();
        }

        private void tweak()
        {
            strFile = namedFiles.Pop();
            pb1.Load(strFile);
            Bitmap b = new Bitmap(pb1.Image);
            string f = Application.StartupPath + "\\captcha.dat";
            Captcha c = new Captcha(f, b);
            c.solve();
            pb2.Image = c.getBW();
            pb3.Image = c.getSmooth();
            pb4.Image = c.getClean(); 

        }

        private void handleNewFiles()
        {
            strFile = newFiles.Pop();
            pb1.Load(strFile);
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                File.Move(strFile, strNamedPath + textBox1.Text + ".png");
                handleNewFiles();
                textBox1.Text = "";
            }
            if (e.KeyCode == Keys.Delete)
            { 
                File.Delete(strFile);
                handleNewFiles();
                textBox1.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tweak();
        }
    }
}
