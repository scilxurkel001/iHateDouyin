using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace iHateDouyin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string edgeUrl = GetEdgeUrl();
            textBox1.Text = edgeUrl;

            // Check if the URL contains "douyin"
            bool containsDouyin = edgeUrl.Contains("douyin");

            if (containsDouyin)
            {
                // Do something if "douyin" is found in the URL
                MessageBox.Show("douyin is included in the URL!");
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
                    AutomationElement editElement = edgeElement.FindFirst(TreeScope.Subtree,
                        new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Edit));

                    if (editElement != null)
                    {
                        // Get the URL from the Edit element
                        object value = editElement.GetCurrentPropertyValue(ValuePatternIdentifiers.ValueProperty, true);

                        if (value != null)
                        {
                            return value.ToString();
                        }
                    }
                }
            }

            return "Unable to retrieve URL";
        }
        public static string GetChromeUrl(Process process)
        {
            if (process == null)
                throw new ArgumentNullException("process");

            if (process.MainWindowHandle == IntPtr.Zero)
                return null;

            AutomationElement element = AutomationElement.FromHandle(process.MainWindowHandle);
            if (element == null)
                return null;

            AutomationElement edit = element.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Edit));
            return ((ValuePattern)edit.GetCurrentPattern(ValuePattern.Pattern)).Current.Value as string;
        }

        public static string GetInternetExplorerUrl(Process process)
        {
            if (process == null)
                throw new ArgumentNullException("process");

            if (process.MainWindowHandle == IntPtr.Zero)
                return null;

            AutomationElement element = AutomationElement.FromHandle(process.MainWindowHandle);
            if (element == null)
                return null;

            AutomationElement rebar = element.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.ClassNameProperty, "ReBarWindow32"));
            if (rebar == null)
                return null;

            AutomationElement edit = rebar.FindFirst(TreeScope.Subtree, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Edit));

            return ((ValuePattern)edit.GetCurrentPattern(ValuePattern.Pattern)).Current.Value as string;
        }

        public static string GetFirefoxUrl(Process process)
        {
            if (process == null)
                throw new ArgumentNullException("process");

            if (process.MainWindowHandle == IntPtr.Zero)
                return null;

            AutomationElement element = AutomationElement.FromHandle(process.MainWindowHandle);
            if (element == null)
                return null;

            AutomationElement doc = element.FindFirst(TreeScope.Subtree, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Document));
            if (doc == null)
                return null;

            return ((ValuePattern)doc.GetCurrentPattern(ValuePattern.Pattern)).Current.Value as string;
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
    }
}



