using System;

namespace com.defrobo.salamander
{
    interface IExecutionAlerter
    {
        event EventHandler<ExecutionEventArgs> Created;
    }
}
