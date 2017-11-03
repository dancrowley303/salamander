using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.defrobo.salamander
{
    public class ExecutionEventArgs : EventArgs
    {
        public string RawExectionMessage { get; }
        public Execution[] Executions { get; }

        public ExecutionEventArgs(string rawExecutionMessage)
        {
            this.RawExectionMessage = rawExecutionMessage;
            this.Executions = JsonConvert.DeserializeObject<List<Execution>>(rawExecutionMessage).ToArray();
        }
    }
}
