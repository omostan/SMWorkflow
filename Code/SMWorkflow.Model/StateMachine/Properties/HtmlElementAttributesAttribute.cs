namespace SMWorkflow.Model.StateMachine.Properties
{
    using System;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, Inherited=true)]
	public sealed class HtmlElementAttributesAttribute : Attribute
	{
		[NotNull]
		public string Name
		{
			get;
			private set;
		}

		public HtmlElementAttributesAttribute()
		{
		}

		public HtmlElementAttributesAttribute([NotNull] string name)
		{
			this.Name = name;
		}
	}
}