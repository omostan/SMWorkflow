namespace SMWorkflow.Model.StateMachine.Properties
{
    using System;

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple=true)]
	public sealed class AspMvcAreaPartialViewLocationFormatAttribute : Attribute
	{
		public AspMvcAreaPartialViewLocationFormatAttribute(string format)
		{
		}
	}
}