namespace SMWorkflow.Model.StateMachine.Properties
{
    using System;

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple=true)]
	public sealed class AspMvcViewLocationFormatAttribute : Attribute
	{
		public AspMvcViewLocationFormatAttribute(string format)
		{
		}
	}
}