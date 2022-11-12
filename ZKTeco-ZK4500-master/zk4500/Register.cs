using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AxZKFPEngXControl;
using OnlyFingerWeb.Entity;
using SqlSugar;

namespace zk4500
{
    public partial class Register : Form
    {
        AxZKFPEngX ZkFprint = new AxZKFPEngX();
        SqlSugarScope db = SqlSugarConnect.getConnect();
        bool Check;
        MyDBDao dbDao = new MyDBDao();
        TaskEntity nowTask = null;

        public Register()
        {
            InitializeComponent();
        }

        private void InitialAxZkfp()
        {
            ZkFprint.OnImageReceived += zkFprint_OnImageReceived;
            ZkFprint.OnFeatureInfo += zkFprint_OnFeatureInfo;
            //zkFprint.OnFingerTouching 
            //zkFprint.OnFingerLeaving
            ZkFprint.OnEnroll += zkFprint_OnEnroll;

            try
            {
                if(ZkFprint.InitEngine() == 0)
                {
                    ZkFprint.FPEngineVersion = "9";
                    ZkFprint.EnrollCount = 3;
                    showPrompt("Connect Successful");
                }
            }catch(Exception e)
            {
                showPrompt("Error" + e.Message);
            }
        }

        private void zkFprint_OnImageReceived(object sender, IZKFPEngXEvents_OnImageReceivedEvent e)
        {
            Graphics g = fingerImage.CreateGraphics();
            Bitmap bmp = new Bitmap(fingerImage.Width, fingerImage.Height);
            g = Graphics.FromImage(bmp);
            int dc = g.GetHdc().ToInt32();
            ZkFprint.PrintImageAt(dc, 0, 0, bmp.Width, bmp.Height);
            g.Dispose();
            fingerImage.Image = bmp;
        }

        private void zkFprint_OnFeatureInfo(object sender, IZKFPEngXEvents_OnFeatureInfoEvent e)
        {

            String strTemp = string.Empty;
            if (ZkFprint.EnrollIndex != 1)
            {
                if (ZkFprint.IsRegister)
                {
                    if (ZkFprint.EnrollIndex - 1 > 0)
                    {
                        int eindex = ZkFprint.EnrollIndex - 1;
                        strTemp = "请再次扫描..." + eindex;
                    }
                }
            }
            showPrompt(strTemp);
        }

        private void zkFprint_OnEnroll(object sender, IZKFPEngXEvents_OnEnrollEvent e)
        {
            if (e.actionResult)
            {


                string template = ZkFprint.EncodeTemplate1(e.aTemplate);

                String name = nameinput.Text;
                String desc = descInput.Text;
                int gender = 0;
                if (manRadio.Checked)
                {
                    gender = 1;
                }
                
                UserDetail tempUser = new UserDetail();
                tempUser.username = name;
                tempUser.fingertemp = template;
                tempUser.gender = gender;
                tempUser.desc = desc;
                tempUser.createdate = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
                int insertable = dbDao.registerUser(tempUser);

                showPrompt(insertable + "");
            }
            else
            {
                showPrompt("错误，请重新注册");

            }
        }
        private void RegisterLoad(object sender, EventArgs e)
        {
            Controls.Add(ZkFprint);
            InitialAxZkfp();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ZkFprint.CancelEnroll();
            ZkFprint.EnrollCount = 3;
            ZkFprint.BeginEnroll();
            showPrompt("请将手指放在指纹机上");
        }

        private void showPrompt(String prom)
        {
            prompt.Text = prom;
        }

        private void verify_Click(object sender, EventArgs e)
        {
            var listTask = dbDao.getTask(int.Parse(taskId.Text));
            if(listTask.Count == 0)
            {
                showPrompt("此任务未开始或已经结束");
                return;
            }
            nowTask = listTask[0];
            showPrompt("当前正在进行 “" + nowTask.taskName + "” 任务的签到");
            
            ZkFprint.CancelEnroll();
            ZkFprint.OnCapture += zkFprint_OnCapture;
            ZkFprint.BeginCapture();
        }

        private void zkFprint_OnCapture(object sender, IZKFPEngXEvents_OnCaptureEvent e)
        {
            string template = ZkFprint.EncodeTemplate1(e.aTemplate);

            List<UserDetail> list = db.Queryable<UserDetail>().ToList();

            foreach(UserDetail ud in list)
            {
                bool check = ZkFprint.VerFingerFromStr(ref template, ud.fingertemp, false, ref Check);
                if (check)
                {
                    string ret = dbDao.sign(nowTask.id, ud.Id);
                    showPrompt(ret);
                }
            }
            if (prompt.Text.Equals(""))
            {
                showPrompt("没有找到结果");
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void nameinput_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
