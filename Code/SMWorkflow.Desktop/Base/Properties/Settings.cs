namespace SMWorkflow.Desktop.Base.Properties
{
    using System.CodeDom.Compiler;
    using System.Configuration;
    using System.Runtime.CompilerServices;

    [CompilerGenerated]
    [GeneratedCode( "Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0" )]
    internal sealed class Settings : ApplicationSettingsBase
    {

        private static Settings defaultInstance;

        public static Settings Default
        {
            get
            {
                return defaultInstance;
            }
        }

        static Settings()
        {
            defaultInstance = ( ( Settings )( Synchronized( new Settings() ) ) );
        }
    }
}
