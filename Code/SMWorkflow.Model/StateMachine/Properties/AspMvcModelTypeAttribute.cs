namespace SMWorkflow.Model.StateMachine.Properties
{
    using System;

    [AttributeUsage(AttributeTargets.Parameter)]
	public sealed class AspMvcModelTypeAttribute : Attribute
	{
		public AspMvcModelTypeAttribute()
		{
		}
	}
}