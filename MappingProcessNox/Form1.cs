using MappingProcessNox.model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Windows.Forms;

namespace MappingProcessNox
{
    public partial class Form1 : Form
    {
        private const string PROCESS_HANDLE = "Nox";
        private const string PROCESS_MEMORY = "NoxVMHandle";


        public Form1()
        {
            InitializeComponent();
        }

        private void buttonReadProcess_Click(object sender, System.EventArgs e)
        {
            Process[] listProcessHandle = Process.GetProcessesByName(PROCESS_HANDLE);
            Process[] listProcessMemory = Process.GetProcessesByName(PROCESS_MEMORY);

            int sizeListProcessDnsPlayer = listProcessHandle.Length;
            int sizeListProcessLdvBoxHeadless = listProcessMemory.Length;

            if (sizeListProcessDnsPlayer == sizeListProcessLdvBoxHeadless)
            {
                List<ProcessMapping> listCombox = new List<ProcessMapping>();

                for (int i = 0; i < sizeListProcessDnsPlayer; i++)
                {
                    Process itemProcessHandle = listProcessHandle[i];

                    string commandProcessHandel = GetCommandLine(itemProcessHandle);
                    commandProcessHandel = cutCommandLineProcessHandle(commandProcessHandel);

                    Process itemProcessMemory = findProcessByCommand(listProcessMemory, commandProcessHandel);
                   
                    if (itemProcessMemory != null)
                    {
                        ProcessMapping itemMap = new ProcessMapping(i, itemProcessHandle, itemProcessMemory);

                        listCombox.Add(itemMap);
                    }
                  
                }

                ProcessDispose(listProcessHandle);
                ProcessDispose(listProcessMemory);

                comboBoxProcess.DisplayMember = "displayComboxProcess";
                comboBoxProcess.ValueMember = "index";
                comboBoxProcess.DataSource = listCombox;

            }

        }


        private void ProcessDispose(Process[] process)
        {
            int size = process.Length;
            for (int i = 0; i < size; i++)
            {
                process[i].Dispose();
            }
        }


        private Process findProcessByCommand(Process[] process, string findCommand)
        {
            int size = process.Length;

            for (int i = 0; i < size; i++)
            {
                string command = GetCommandLine(process[i]);
                string cutcommand = cutCommandLineProcessMemory(command);

                if (findCommand == cutcommand)
                {
                    return process[i];
                }
            }

            return null;
        }

        public string GetCommandLine(Process process)
        {
            if (process == null)
            {
                return "";
            }

            string query =
                    $@"SELECT CommandLine
               FROM Win32_Process
               WHERE ProcessId = {process.Id}";

            using (var searcher = new ManagementObjectSearcher(query))
            using (var collection = searcher.Get())
            {
                var managementObject = collection.Cast<ManagementObject>().FirstOrDefault();

                return managementObject != null ? (string)managementObject["CommandLine"] : "";
            }
        }

        public string cutCommandLineProcessHandle(string commandProcess)
        {
            int word = commandProcess.IndexOf("-clone:");
            word = "-clone:".Length + word;

            string cutword = commandProcess.Substring(word);

            if ("Nox_0".Equals(cutword))
            {
                return "nox";
            }

            return cutword.Trim().ToLower();
        }

        public string cutCommandLineProcessMemory(string commandProcess)
        {
            int wordStart = commandProcess.IndexOf("--comment ");
            wordStart = "--comment ".Length + wordStart;
            int wordEnd = commandProcess.IndexOf(" --startvm") - wordStart;
 
            return commandProcess.Substring(wordStart, wordEnd).Trim().ToLower();
        }

        private void comboBoxProcess_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ProcessMapping itme = comboBoxProcess.SelectedItem as ProcessMapping;

                MessageBox.Show(itme.displayComboxProcess);
            }
            catch (Exception error)
            {
                Console.Error.Write(error.Message);
            }
        }
    }
}
