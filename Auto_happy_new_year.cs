using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Một_cái_tool_nhảm_địt
{
    public partial class Tool_Nham_Dit : Form
    {
        public Tool_Nham_Dit()
        {
            InitializeComponent();
        }

        //DECLARE
        bool canGetName = true;


        private void button1_Click(object sender, EventArgs e)
        {           
            if(txtCookie.Text != "" && txtLinkMess.Text != "" && txtMessInfo.Text != "")
            {
                string doan1 = "javascript:void(function(){ function setCookie(t) { var list = t.split(\"; \"); console.log(list); for (var i = list.length - 1; i >= 0; i--) { var cname = list[i].split(\"=\")[0]; var cvalue = list[i].split(\"=\")[1]; var d = new Date(); d.setTime(d.getTime() + (7*24*60*60*1000)); var expires = \";domain=.facebook.com;expires=\"+ d.toUTCString(); document.cookie = cname + \"=\" + cvalue + \"; \" + expires; } } function hex2a(hex) { var str = ''; for (var i = 0; i < hex.length; i += 2) { var v = parseInt(hex.substr(i, 2), 16); if (v) str += String.fromCharCode(v); } return str; } var cookie ='";
                string doan2 = "'; setCookie(cookie); location.href = 'https://mbasic.facebook.com/me'; })();";
                string cookie = txtCookie.Text;
                string tongket = doan1 + cookie + doan2;
                HtmlDocument doc = web.Document;
                HtmlElement head = doc.GetElementsByTagName("head")[0];
                HtmlElement s = doc.CreateElement("script");
                s.SetAttribute("text", tongket);
                head.AppendChild(s);
            }else
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void web_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if(txtCookie.Text != "" && txtLinkMess.Text != "")
            {
                if (canGetName == true)
                {
                    var ten = web.Document.GetElementsByTagName("title");
                    foreach (HtmlElement get_ten in ten)
                    {
                        btnAuto_Send.Enabled = false;
                        label7.Text = "Đăng nhập thành công: " + get_ten.InnerText.ToString();
                        web.Navigate(txtLinkMess.Text);
                    }
                    canGetName = false;
                }
            }else
            {
                label7.Text = "Vui lòng nhập đầy đủ thông tin";
            }
        }

        private void timerTime_Tick(object sender, EventArgs e)
        {
            try
            {
                lblCurrent.Text = DateTime.Now.ToString("HH:mm:ss");
                if (lblCurrent.Text == txtHenGio.Text)
                {
                    timerSend_mess.Enabled = false;
                    timerSend_mess.Enabled = true;
                }
            }
            catch
            {

            }
        }

        private void timerSend_mess_Tick(object sender, EventArgs e)
        {
            try
            {
                web.Document.GetElementById("composerInput").SetAttribute("value", txtMessInfo.Text);
                var ten = web.Document.GetElementsByTagName("input");
                foreach (HtmlElement get_ten in ten)
                {
                    if(get_ten.GetAttribute("name") == "send")
                    {
                        timerSend_mess.Enabled = false;
                        get_ten.InvokeMember("click");
                        MessageBox.Show("Tui đã gửi tin nhắn thành công, tui off nhé!\nBái bai <3");
                        this.Close();
                    }
                }
            }
            catch
            {

            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            btn_Start.Enabled = false;
            timerTime.Enabled = true;
        }

        private void txtLinkMess_TextChanged(object sender, EventArgs e)
        {
            txtLinkMess.Text = txtLinkMess.Text.Replace("www", "mbasic");
        }
    }
}
