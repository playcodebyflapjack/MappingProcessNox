using MappingProcessNox.model;
using MappingProcessNox.service;
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
        private const int GET_IN_LINE = 7;
        private const string GET_BY_IP = "127.0.0.1";

        private ShellADBCLI serviceADB;
        private CreateProcess serviceCreateProcess = new CreateProcess();
        private ProcessMapping itemProcessMapSelect;
        private AdbPortDevice itemAdbPortDevice;

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
                    string cutCommandHandle = cutCommandLineProcessHandle(commandProcessHandel);

                    Process itemProcessMemory = findProcessByCommand(listProcessMemory, cutCommandHandle);

                    if (itemProcessMemory != null)
                    {
                        ProcessMapping itemMap = new ProcessMapping(i, itemProcessHandle, itemProcessMemory);

                        itemMap.commandRunByHandleProcess = commandProcessHandel;

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
            int title = commandProcess.IndexOf("-title:");

            string cutword = String.Empty;

            if (title == -1)
            {
                cutword = commandProcess.Substring(word);
            }
            else
            {
                int wordEnd = title - word;
                cutword = commandProcess.Substring(word, wordEnd);
                cutword = cutword.Trim();
            }


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

        private async void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
            {
                return;
            }

            if (this.serviceADB == null)
            {
                if (findFileAdb.ShowDialog() == DialogResult.OK)
                {
                    string fullFilePathADB = findFileAdb.FileName;
                    this.serviceADB = new ShellADBCLI(fullFilePathADB);
                }
            }

            if (this.serviceADB == null)
            {
                checkBox1.Checked = false;
                return;
            }

            string[] listOfDevices = await serviceADB.GetListOfIp();
            List<AdbPortDevice> listComboboxDevices = new List<AdbPortDevice>();

            for (int i = 0; i < listOfDevices.Length; i++)
            {
                AdbPortDevice itemAdbPortDevice = new AdbPortDevice(i, listOfDevices[i]);
                listComboboxDevices.Add(itemAdbPortDevice);
            }

            serviceADB.listOfDevice = listComboboxDevices;

            comboBoxDevices.DisplayMember = "displayComboxProcess";
            comboBoxDevices.ValueMember = "displayComboxProcess";
            comboBoxDevices.DataSource = listComboboxDevices;

        }

        private async void comboBoxDevices_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (itemProcessMapSelect == null)
                {
                    return;
                }

                string ipMapping = await serviceADB.GetListOfIpAndPortByProcessId(
                   GET_BY_IP,
                   itemProcessMapSelect.memoryProcessId.ToString(),
                   GET_IN_LINE);

                if (!String.IsNullOrEmpty(ipMapping))
                {
                    comboBoxDevices.SelectedValue = ipMapping;

                    this.itemAdbPortDevice = comboBoxDevices.SelectedItem as AdbPortDevice;
                }
            }
            catch (Exception error)
            {
                Console.Error.Write(error.Message);
            }
        }

        private void buttonChangeTitle_Click(object sender, EventArgs e)
        {
            this.itemProcessMapSelect = comboBoxProcess.SelectedItem as ProcessMapping;

            int clodeProcessId = this.itemProcessMapSelect.handleProcessId;

            serviceCreateProcess.closeProcess(clodeProcessId);

            string pathFoderNox = this.serviceADB.pathFileAdb;

            serviceCreateProcess.createProcessChangeTitle(
                textBoxChangeTitle.Text,
                this.itemProcessMapSelect,
                 pathFoderNox
                );

            buttonReadProcess_Click(sender, e);
        }

        private void comboBoxProcess_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                // Filter to ADB
                this.itemProcessMapSelect = comboBoxProcess.SelectedItem as ProcessMapping;
                checkBox1_CheckedChanged(sender, e);
                comboBoxDevices_SelectionChangeCommitted(sender, e);
            }
        }
    }
}
