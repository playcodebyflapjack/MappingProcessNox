namespace MappingProcessNox.model
{
    public class AdbPortDevice
    {
        public int index;
        public string device;
        public string ip;
        public string port;

        public string displayComboxProcess
        {
            get
            {
                return device;
            }
        }

        public AdbPortDevice(int index, string device)
        {
            this.index = index;
            this.device = device;

            string[] ipport = device.Split(':');

            if (ipport.Length == 2)
            {
                this.ip = ipport[0];
                this.port = ipport[1];
            }

        }

    }
}
