namespace SMWorkflow.Model.StateMachine.Properties
{
    using System;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple=false, Inherited=true)]
	public sealed class MeansImplicitUseAttribute : Attribute
	{
		[UsedImplicitly]
		public ImplicitUseTargetFlags TargetFlags
		{
			get;
			private set;
		}

		[UsedImplicitly]
		public ImplicitUseKindFlags UseKindFlags
		{
			get;
			private set;
		}

		public MeansImplicitUseAttribute() : this(ImplicitUseKindFlags.Default, ImplicitUseTargetFlags.Default)
		{
		}

		public MeansImplicitUseAttribute(ImplicitUseKindFlags useKindFlags) : this(useKindFlags, ImplicitUseTargetFlags.Default)
		{
		}

		public MeansImplicitUseAttribute(ImplicitUseTargetFlags targetFlags) : this(ImplicitUseKindFlags.Default, targetFlags)
		{
		}

		public MeansImplicitUseAttribute(ImplicitUseKindFlags useKindFlags, ImplicitUseTargetFlags targetFlags)
		{
			this.UseKindFlags = useKindFlags;
			this.TargetFlags = targetFlags;
		}
	}
}