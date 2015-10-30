namespace SMWorkflow.Desktop.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;

    using Base.Commands;

    using Model.StateMachine;

    using Microsoft.Practices.Prism.Commands;
    using Microsoft.Practices.Prism.Mvvm;

    using Stateless;

    public class SMWorkflowDesktopViewModel : BindableBase
    {
        private SMWorkflowModel _drinkMachine;
        public SMWorkflowModel DrinkMachine
        {
            get { return this._drinkMachine; }
            set
            {
                if (this._drinkMachine == value)
                {
                    return;
                }
                this._drinkMachine = value;
                this.OnPropertyChanged( "DrinkMachine" );
            }
        }

        private DelegateCommand<double?> _insertCoinCommand;
        public DelegateCommand<double?> InsertCoinCommand
        {
            get
            {
                return this._insertCoinCommand;
            }
            set
            {
                if( this._insertCoinCommand == value )
                {
                    return;
                }
                this._insertCoinCommand = value;
                this.OnPropertyChanged( "InsertCoinCommand" );
            }
        }

        private DelegateCommand _serveDrinkCommand;
        public DelegateCommand ServeDrinkCommand
        {
            get
            {
                return this._serveDrinkCommand;
            }
            set
            {
                if( this._serveDrinkCommand == value )
                {
                    return;
                }
                this._serveDrinkCommand = value;
                this.OnPropertyChanged( "ServeDrinkCommand" );
            }
        }

        private DelegateCommand _selectDrinkCommand;
        public DelegateCommand SelectDrinkCommand
        {
            get
            {
                return this._selectDrinkCommand;
            }
            set
            {
                if( this._selectDrinkCommand == value )
                {
                    return;
                }
                this._selectDrinkCommand = value;
                this.OnPropertyChanged( "SelectDrinkCommand" );
            }
        }

        private DelegateCommand _refundMoneyCommand;
        public DelegateCommand RefundMoneyCommand
        {
            get
            {
                return this._refundMoneyCommand;
            }
            set
            {
                if( this._refundMoneyCommand == value )
                {
                    return;
                }
                this._refundMoneyCommand = value;
                this.OnPropertyChanged( "RefundMoneyCommand" );
            }
        }

        private DelegateCommand _takeDrinkCommand;
        public DelegateCommand TakeDrinkCommand
        {
            get
            {
                return this._takeDrinkCommand;
            }
            set
            {
                if( this._takeDrinkCommand == value )
                {
                    return;
                }
                this._takeDrinkCommand = value;
                this.OnPropertyChanged( "TakeDrinkCommand" );
            }
        }

        private string _userMessage;
        public string UserMessage
        {
            get
            {
                return this._userMessage;
            }
            set
            {
                if( string.Equals( this._userMessage, value, StringComparison.Ordinal ) )
                {
                    return;
                }
                this._userMessage = value;
                this.OnPropertyChanged( "UserMessage" );
            }
        }

        public SMWorkflowDesktopViewModel()
        {
            this.UserMessage = "Ready";
            this.DrinkMachine = new SMWorkflowModel();
            this.DrinkMachine.OnTransitioned(this.OnTransitionAction);
            this.InsertCoinCommand = this.DrinkMachine.CreateCommand( SMWMachineTrigger.InsertMoney,
                ( double? param ) =>
                {
                    var coffeeMachine = this.DrinkMachine;
                    var nullable = param;
                    coffeeMachine.InsertCoin( ( nullable.HasValue ? nullable.GetValueOrDefault() : 0 ) );
                }, null );

            this.RefundMoneyCommand = this.DrinkMachine.CreateCommand( SMWMachineTrigger.RefundMoney, null, null );
            this.ServeDrinkCommand = this.DrinkMachine.CreateCommand( SMWMachineTrigger.ServeDrink, null, null );
            this.TakeDrinkCommand = this.DrinkMachine.CreateCommand( SMWMachineTrigger.TakeDrink, null, null );
            this.DrinkMachine.PropertyChanged += this.DrinkMachineOnPropertyChanged;
        }

        private void DrinkMachineOnPropertyChanged( object sender, PropertyChangedEventArgs propertyChangedEventArgs )
        {
            if( ( propertyChangedEventArgs.PropertyName == "InsertedMoney" || propertyChangedEventArgs.PropertyName == "ServingProcess" ) )
            {
                this.UpdateScreenMessage();
            }
        }

        private void OnTransitionAction( StateMachine<SMWMachineState, SMWMachineTrigger>.Transition transition )
        {
            var source = new object[] { transition.Source, transition.Destination, transition.Trigger };
            Debug.WriteLine( @"Transition from {0} to {1}, trigger = {2}.", source );
            this.UpdateScreenMessage();
            this.RefreshCommands();
        }

        private void RefreshCommands()
        {
            this.InsertCoinCommand.RaiseCanExecuteChanged();
            this.RefundMoneyCommand.RaiseCanExecuteChanged();
            this.ServeDrinkCommand.RaiseCanExecuteChanged();
            this.TakeDrinkCommand.RaiseCanExecuteChanged();
        }

        private void UpdateScreenMessage()
        {
            switch (this.DrinkMachine.State)
            {
                case SMWMachineState.Idle:
                    {
                        this.UserMessage = "Ready";
                        break;
                    }
                case SMWMachineState.CoinBox:
                    {
                        this.UserMessage = string.Empty;
                        break;
                    }
                case SMWMachineState.SelectDrink:
                    {
                        this.UserMessage = string.Empty;
                        break;
                    }
                case SMWMachineState.ServingDrink:
                    {
                        this.UserMessage = string.Format( "Serving drink {0} %...", this.DrinkMachine.ServingProcess );
                        break;
                    }
                case SMWMachineState.DrinkReady:
                    {
                        this.UserMessage = "Your drink is ready!";
                        break;
                    }
                case SMWMachineState.RefundMoney:
                    {
                        this.UserMessage = "Refunding money";
                        break;
                    }
                default:
                    {
                        this.UserMessage = "Out of order";
                        break;
                    }
            }
        }
    }
}
