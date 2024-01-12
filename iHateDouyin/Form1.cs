using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Automation;

namespace iHateDouyin
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            string edgeUrl = GetEdgeUrl();
            textBox1.Text = edgeUrl;
        }
            }
        }
private string GetEdgeUrl()
{
    IntPtr edgeHandle = GetEdgeWindowHandle();

    if (edgeHandle != IntPtr.Zero)
    {
        AutomationElement edgeElement = AutomationElement.FromHandle(edgeHandle);

        if (edgeElement != null)
        {
            AutomationProperty property = AutomationElement.NameProperty;
            object value = edgeElement.GetCurrentPropertyValue(property, true);

            if (value != null)
            {
                return value.ToString();
            }
        }
    }

    return "Unable to retrieve URL";
}

private IntPtr GetEdgeWindowHandle()
{
    Process[] processes = Process.GetProcessesByName("msedge");

    if (processes.Length > 0)
    {
        IntPtr edgeHandle = processes[0].MainWindowHandle;
        return edgeHandle;
    }

    return IntPtr.Zero;
}



        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

