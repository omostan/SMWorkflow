// -------------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="AGI, Novomatic Group.">
//     Copyright © 2015 Stanley Omoregie. All Rights Reserved.
// </copyright>
// -------------------------------------------------------------------------
using System.Windows;

namespace SMWorkflow.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        #region Overrides of Application

        protected override void OnStartup( StartupEventArgs e )
        {
            base.OnStartup(e);
            var mw = new MainWindow();
            mw.ShowDialog();
        }

        #endregion
    }
}
