﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

namespace Elementary_Arithmetic
{
    public partial class FormMain : Form
    {
        static int timerTickCounter = 0;
        static Random randomizer = new Random ();
        static int intAnswer;

        protected override bool ProcessCmdKey (ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Back)
            {
                labelScore.Text = textBoxMinutes.Text;
            }
            return base.ProcessCmdKey (ref msg, keyData);
        }

        public FormMain ()
        {
            InitializeComponent ();
        }

        private void FormMain_Load (object sender, EventArgs e)
        {
            checkBox1.Checked = true;
            checkBox2.Checked = true;
            checkBox3.Checked = true;
            checkBox4.Checked = true;
            radioButton1.Checked = true;
            textBoxMinutes.Enabled = true;
            textBoxMinutes.Focus ();
            textBoxMinutes.Text = "";
            textBoxResult.Enabled = false;
            this.AcceptButton = buttonStart;


            if (!Directory.Exists ("d:\\log"))
            {
                Directory.CreateDirectory ("d:\\log");
            }

            if (File.Exists ("d:\\log\\四则运算测试" + DateTime.Now.Year.ToString () + "-" + DateTime.Now.Month.ToString () + "-" + DateTime.Now.Day.ToString () + ".log"))
            {
                Log ("测试开始!", "d:\\log\\", "四则运算测试" + DateTime.Now.Year.ToString () + "-" + DateTime.Now.Month.ToString () + "-" + DateTime.Now.Day.ToString () + ".log");
            }
            else
            {
                FileStream newLogFile = new FileStream ("d:\\log\\四则运算测试" +
                                                        DateTime.Now.Year.ToString () + "-" +
                                                        DateTime.Now.Month.ToString () + "-" +
                                                        DateTime.Now.Day.ToString () + ".log",
                                                        FileMode.OpenOrCreate);
                newLogFile.Close ();
                Log ("测试开始!", "d:\\log\\", "四则运算测试" + DateTime.Now.Year.ToString () + "-" + DateTime.Now.Month.ToString () + "-" + DateTime.Now.Day.ToString () + ".log");
            }
        }

        private int NewQuiz ()
        {
            int intNumber1, intNumber2,intOperator,intResult;

            
            intResult = -1;
            do
            {
                intOperator = randomizer.Next (4) + 1;
                switch (intOperator)
                {
                    case 1:
                        if (checkBox1.Checked == true)
                        {
                            labelSymbol.Text = "+";
                            do
                            {
                                //随机生成2个1-99的整数
                                intNumber1 = randomizer.Next (99) + 1;
                                intNumber2 = randomizer.Next (99) + 1;
                            } while (intNumber1 + intNumber2 >= 100);   //确保2个整数的和小于100
                            labelNumber1.Text = intNumber1.ToString ();
                            labelNumber2.Text = intNumber2.ToString ();
                            intResult = intNumber1 + intNumber2;
                        }
                        break;
                    case 2:
                        if (checkBox2.Checked == true)
                        {
                            labelSymbol.Text = "-";
                            //随机生成2个1-99的整数
                            intNumber1 = randomizer.Next (99) + 1;
                            intNumber2 = randomizer.Next (99) + 1;
                            if (intNumber1 > intNumber2)
                            {
                                labelNumber1.Text = intNumber1.ToString ();
                                labelNumber2.Text = intNumber2.ToString ();
                                intResult = intNumber1 - intNumber2;
                            }
                            else
                            {
                                labelNumber1.Text = intNumber2.ToString ();
                                labelNumber2.Text = intNumber1.ToString ();
                                intResult = intNumber2 - intNumber1;
                            }
                        }
                        break;
                    case 3:
                        if (checkBox3.Checked == true)
                        {
                            labelSymbol.Text = "x";
                            //随机生成2个1-9的整数
                            intNumber1 = randomizer.Next (9) + 1;
                            intNumber2 = randomizer.Next (9) + 1;
                            labelNumber1.Text = intNumber1.ToString ();
                            labelNumber2.Text = intNumber2.ToString ();
                            intResult = intNumber1 * intNumber2;
                        }
                        break;
                    case 4:
                        if (checkBox4.Checked == true)
                        {
                            labelSymbol.Text = "÷";
                            //随机生成2个1-9的整数
                            intNumber1 = randomizer.Next (9) + 1;
                            intNumber2 = randomizer.Next (9) + 1;
                            labelNumber1.Text = (intNumber1 * intNumber2).ToString();
                            labelNumber2.Text = intNumber2.ToString ();
                            intResult = intNumber1;
                        }
                        break;
                }
            } while (intResult == -1);

            return (intResult);
        }

        static void Log (string logMessage, string logFolder, string logFilename)
        {
            using (StreamWriter w = File.AppendText (logFolder + logFilename))
            {
                w.Write ("\r\n");
                w.Write ("{0} {1}", DateTime.Now.ToLongDateString (), DateTime.Now.ToLongTimeString ());
                w.Write ("  :{0}", logMessage);
            }
        }

        private void timer_Tick (object sender, EventArgs e)
        {
            int intMinutes = 0;
            string strRemainMinutes = "";
            string strRemainSeconds = "";

            int intRemainMinutes;
            int intRemainSeconds;

            bool isNumber = int.TryParse (textBoxMinutes.Text, out intMinutes);

            timerTickCounter = timerTickCounter + 1;

            if (radioButton1.Checked)
            {
                intRemainMinutes = (intMinutes * 60 - timerTickCounter) / 60;
                intRemainSeconds = (intMinutes * 60 - timerTickCounter) % 60;
            }
            else
            {
                intRemainMinutes = timerTickCounter / 60;
                intRemainSeconds = timerTickCounter % 60;
            }

            if (intRemainMinutes < 10)
            {
                strRemainMinutes = "0" + intRemainMinutes.ToString ();
            }
            else
            {
                strRemainMinutes = intRemainMinutes.ToString ();
            }
            if (intRemainSeconds < 10)
            {
                strRemainSeconds = "0" + intRemainSeconds.ToString ();
            }
            else
            {
                strRemainSeconds = intRemainSeconds.ToString ();
            }

            labelTime.Text = strRemainMinutes + ":" + strRemainSeconds;

            if (radioButton1.Checked)
            {
                if (timerTickCounter / 60 == intMinutes)
                {
                    timer_stop ();
                }
            }
            else
            {
                if (labelScore.Text == "0")
                {
                    timer_stop ();
                }
            }
        }

        private void timer_stop ()
        {
            string strArithmetics = "";
            timer.Stop ();
            timerTickCounter = 0;           //停止计时器，并将激活次数清零

            textBoxMinutes.Enabled = true;
            textBoxResult.Enabled = false;
            buttonStart.Enabled = true;
            checkBox1.Enabled = true;
            checkBox2.Enabled = true;
            checkBox3.Enabled = true;
            checkBox4.Enabled = true;
            radioButton1.Enabled = true;
            radioButton2.Enabled = true;
            textBoxMinutes.Focus ();

            if (checkBox1.Checked)
            {
                strArithmetics = strArithmetics + "加";
            }
            if (checkBox2.Checked)
            {
                strArithmetics = strArithmetics + "减";
            }
            if (checkBox3.Checked)
            {
                strArithmetics = strArithmetics + "乘";
            }
            if (checkBox4.Checked)
            {
                strArithmetics = strArithmetics + "除";
            }
            if (radioButton1.Checked)
            {
                Log ("四则运算测试,计时模式(" + strArithmetics + ")完成，用时" + textBoxMinutes.Text + ":00,共计" + labelScore.Text + "题.",
                     "d:\\log\\",
                     "四则运算测试" + DateTime.Now.Year.ToString () + "-" + DateTime.Now.Month.ToString () + "-" + DateTime.Now.Day.ToString () + ".log");
            }
            else
            {
                Log ("四则运算测试,闯关模式(" + strArithmetics + ")完成，用时" + labelTime.Text + ",共计" + textBoxMinutes.Text + "题.",
                     "d:\\log\\",
                     "四则运算测试" + DateTime.Now.Year.ToString () + "-" + DateTime.Now.Month.ToString () + "-" + DateTime.Now.Day.ToString () + ".log");
            }

        }

        private void buttonStart_Click (object sender, EventArgs e)
        {
            checkBox1.Enabled = false;
            checkBox2.Enabled = false;
            checkBox3.Enabled = false;
            checkBox4.Enabled = false;
            radioButton1.Enabled = false;
            radioButton2.Enabled = false;
            textBoxMinutes.Enabled = false;
            textBoxResult.Enabled = true;
            textBoxResult.Text = "";
            textBoxResult.Focus ();

            if (radioButton1.Checked)
            {
                int intMinutes = 0;
                if (int.TryParse (textBoxMinutes.Text, out intMinutes))
                {
                    timer.Interval = 1000;
                    timer.Start ();
                    buttonStart.Enabled = false;
                    intAnswer = NewQuiz ();
                }
            }
            else
            {
                int intTestNumber;
                if (int.TryParse (textBoxMinutes.Text, out intTestNumber))
                {
                    timer.Interval = 1000;
                    timer.Start ();
                    labelScore.Text = intTestNumber.ToString ();
                    buttonStart.Enabled = false;
                    intAnswer = NewQuiz ();

                }
            }
        }

        private void textBoxResult_TextChanged (object sender, EventArgs e)
        {
            int intResult, intScore;
            if (radioButton1.Checked)
            {
                if (int.TryParse (textBoxResult.Text, out intResult))
                {
                    if (intAnswer == intResult)
                    {
                        intScore = System.Convert.ToInt32 (labelScore.Text);
                        intScore = intScore + 1;
                        labelScore.Text = intScore.ToString ();
                        textBoxResult.Text = "";
                        intAnswer = NewQuiz ();
                    }
                }
            }
            else
            {
                if (int.TryParse (textBoxResult.Text, out intResult))
                {
                    if (intAnswer == intResult)
                    {
                        intScore = System.Convert.ToInt32 (labelScore.Text);
                        intScore = intScore - 1;
                        labelScore.Text = intScore.ToString ();
                        textBoxResult.Text = "";
                        intAnswer = NewQuiz ();
                    }
                }
            }
        }

        private void textBoxMinutes_TextChanged (object sender, EventArgs e)
        {
            Mode_Changed ();
        }

        private void radioButton2_CheckedChanged (object sender, EventArgs e)
        {
            Mode_Changed ();
        }

        private void radioButton1_CheckedChanged (object sender, EventArgs e)
        {
            Mode_Changed();
        }

        private void Mode_Changed ()
        {
            if (radioButton1.Checked == true)
            {
                int intMinutes = 0;
                if (int.TryParse (textBoxMinutes.Text, out intMinutes))
                {
                    labelTime.Text = intMinutes.ToString () + ":00";
                    labelMinutes.Text = "分钟";
                    labelScore.Text = "0";
                }
            }
            else
            {
                labelTime.Text = "00:00";
                labelMinutes.Text = "题";
                labelScore.Text = textBoxMinutes.Text;
            }
        }
    }
}
