using System;

namespace com.defrobo.salamander
{
    public interface IExecutionAlerter
    {
        event EventHandler<ExecutionEventArgs> ExecutionCreated;
    }
}
