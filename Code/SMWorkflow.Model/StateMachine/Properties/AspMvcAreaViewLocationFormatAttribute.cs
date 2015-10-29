namespace SMWorkflow.Model.StateMachine.Properties
{
    using System;

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple=true)]
	public sealed class AspMvcAreaViewLocationFormatAttribute : Attribute
	{
		public AspMvcAreaViewLocationFormatAttribute(string format)
		{
		}
	}
}