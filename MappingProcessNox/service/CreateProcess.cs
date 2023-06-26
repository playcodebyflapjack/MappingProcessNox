using MappingProcessNox.model;
using System;
using System.Diagnostics;
using System.IO;

namespace MappingProcessNox.service
{
    class CreateProcess
    {

        public void closeProcess(int processId)
        {
            try
            {
                Process process = Process.GetProcessById(processId);
                process.Kill();
            }
            catch (Exception error)
            {
                Console.Error.Write(error.Message);
            }
        }

        public Process createProcessChangeTitle(string title, ProcessMapping itemProcessMapping, string pathFoderNox)
        {
            string command = itemProcessMapping.commandRunByHandleProcess;
            string workingDirectory = new DirectoryInfo(pathFoderNox).Parent.FullName;

            command = command + " -title:" + title;

            Process noxChangeTitle = new Process();

            noxChangeTitle.StartInfo.FileName = "Nox.exe";
            noxChangeTitle.StartInfo.Arguments = command;
            noxChangeTitle.StartInfo.WorkingDirectory = workingDirectory;

            noxChangeTitle.Start();

            return noxChangeTitle;

        }

    }
}
