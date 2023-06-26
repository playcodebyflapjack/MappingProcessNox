using CliWrap;
using MappingProcessNox.model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MappingProcessNox.service
{
    class ShellADBCLI
    {
        public readonly string pathFileAdb;

        public List<AdbPortDevice> listOfDevice;

        private ShellADBCLI()
        {

        }


        public ShellADBCLI(string pathFileAdb)
        {
            this.pathFileAdb = pathFileAdb;

            CheckFile();
        }


        private void CheckFile()
        {
            if (!(File.Exists(this.pathFileAdb)))
            {
                throw new FileNotFoundException("Check Path File " + this.pathFileAdb);
            }
        }

        public async Task<string[]> GetListOfIp()
        {
            string[] commandparameter = new string[] { "devices" };
            StringBuilder stdOutBuffer = new StringBuilder();

            await Cli.Wrap(this.pathFileAdb)
             .WithArguments(commandparameter)
             .WithStandardOutputPipe(PipeTarget.ToStringBuilder(stdOutBuffer))
             .ExecuteAsync();

            return FilterIp(stdOutBuffer.ToString());

        }

        private string[] FilterIp(string word)
        {
            List<string> result = new List<string>();
            Regex pattern = new Regex(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}:\d{1,5}\b");

            if (!String.IsNullOrEmpty(word.ToString()))
            {
                string[] lines = word.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                int size = lines.Length;

                for (int i = 0; i < size; i++)
                {
                    MatchCollection m = pattern.Matches(lines[i]);

                    if (m.Count > 0)
                    {
                        result.Add(m[0].Value);
                    }
                }
            }
            return result.ToArray();
        }

        public async Task<string> GetListOfIpAndPortByProcessId(string ip, string processId, int getLine)
        {
            StringBuilder stdOutBuffer = new StringBuilder();

            await Cli.Wrap("cmd.exe")
             .WithArguments($"/C netstat -aon | findstr " + processId)
             .WithStandardOutputPipe(PipeTarget.ToStringBuilder(stdOutBuffer))
             .ExecuteAsync();

            string[] lines = stdOutBuffer.ToString().Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            int sizeLines = lines != null ? lines.Length : 0;

            if (sizeLines > 0)
            {
                string line = lines[getLine];
                string substring = line.Substring(line.IndexOf(ip + ":"));
                string[] row = substring.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string ipMapping = row[0];

                return ipMapping;
            }

            return String.Empty;
        }


        public AdbPortDevice getItemAdbPortDeviceByIp(string ip)
        {
            int size = listOfDevice != null ? listOfDevice.Count : 0;

            for (int i = 0; i < size; i++)
            {
                AdbPortDevice item = listOfDevice[i];

                if (item.device.Equals(ip))
                {
                    return item;
                }
            }

            return null;
        }

    }
}
