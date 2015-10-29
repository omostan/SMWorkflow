// -------------------------------------------------------------------------
// <copyright file="SMWorkflowMachineExtension.cs" company="AGI, Novomatic Group.">
//     Copyright © 2015 Stanley Omoregie. All Rights Reserved.
// </copyright>
// -------------------------------------------------------------------------
namespace SMWorkflow.Model.Extensions
{
    using System.Windows.Input;

    using SMWorkflow.Base;

    using Stateless;
    public static class SMWorkflowMachineExtension
    {
        #region CreateCommand

        public static ICommand CreateCommand<TState, TTrigger>( this StateMachine<TState, TTrigger> stateMachine, TTrigger trigger )
        {
            return new RelayCommand
                (
                    () => stateMachine.Fire(trigger),
                    () => stateMachine.CanFire(trigger)
                );
        }

        #endregion
    }
}
