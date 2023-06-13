using System;
using System.Diagnostics;

namespace MappingProcessNox.model
{
    public class ProcessMapping
    {
        public int index;
        private int handleProcessId;
        private string handleProcessName;
        private IntPtr handleProcessHandle;

        private int memoryProcessId;
        private string memoryProcessName;
        private IntPtr memoryProcessHandle;

        public string displayComboxProcess
        {
            get
            {
                return handleProcessName + " " + "(" + handleProcessId + ")" + ":" + "(" + memoryProcessId + ")";
            }
        }



        public ProcessMapping(int index,
                                Process itemHandleProcess,
                                Process itemMemoryProcess)
        {

            this.index = index;
            this.handleProcessId = itemHandleProcess.Id;
            this.handleProcessName = itemHandleProcess.ProcessName;
            this.handleProcessHandle = itemHandleProcess.Handle;

            this.memoryProcessId = itemMemoryProcess.Id;
            this.memoryProcessName = itemMemoryProcess.ProcessName;
            this.memoryProcessHandle = itemMemoryProcess.Handle;
        }


    }
}
