namespace SMWorkflow.Model.StateMachine.Properties
{
    using System;

    [Flags]
	public enum ImplicitUseTargetFlags
	{
		Default = 1,
		Itself = 1,
		Members = 2,
		WithMembers = 3
	}
}