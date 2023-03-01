using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Coroutine
{
    public partial class Form1 : Form
    {
        private int _count;
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            this.StartCoroutine(Test());
        }
        
        private IEnumerator Test()
        {
            yield return new WaitForSeconds(0.1f);
            metroLabel1.Text = "开始次数" + (++_count);
            
            yield return null;
            yield return Test2();
            yield return Test2();
            yield return Test2();

            metroLabel1.Text = "结束";
        }

        private IEnumerator Test2()
        {
            yield return new WaitForSeconds(0.1f);
            metroLabel1.Text = "次数" + (++_count);
        }
    }
}