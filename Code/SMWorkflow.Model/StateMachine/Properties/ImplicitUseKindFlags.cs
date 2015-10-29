namespace SMWorkflow.Model.StateMachine.Properties
{
    using System;

    [Flags]
	public enum ImplicitUseKindFlags
	{
		Access = 1,
		Assign = 2,
		InstantiatedWithFixedConstructorSignature = 4,
		Default = 7,
		InstantiatedNoFixedConstructorSignature = 8
	}
}