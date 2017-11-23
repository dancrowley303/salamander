using Newtonsoft.Json;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace com.defrobo.salamander
{
    [ProtoContract]
    public class ExecutionEventArgs : EventArgs
    {
        [ProtoMember(1)]
        public Execution[] Executions { get; set;  }

        public ExecutionEventArgs(string rawExecutionMessage)
        {
            this.Executions = JsonConvert.DeserializeObject<List<Execution>>(rawExecutionMessage).ToArray();
        }

        public ExecutionEventArgs(Execution[] executions)
        {
            this.Executions = executions;
        }

        public ExecutionEventArgs()
        {
        }
    }
}
