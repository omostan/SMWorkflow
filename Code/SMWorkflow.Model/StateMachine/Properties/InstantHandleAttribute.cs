namespace SMWorkflow.Model.StateMachine.Properties
{
    using System;

    [AttributeUsage(AttributeTargets.Parameter, Inherited=true)]
	public sealed class InstantHandleAttribute : Attribute
	{
		public InstantHandleAttribute()
		{
		}
	}
}