namespace SMWorkflow.Model.StateMachine
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    using Stateless;
    using System.Threading;

    public sealed class SMWorkflowModel : StateMachine<SMWMachineState, SMWMachineTrigger>, INotifyPropertyChanged
    {
        //public SMWorkflowModel(Func<SMWMachineState> stateAccessor, Action<SMWMachineState> stateMutator)
        //    : base(stateAccessor, stateMutator)
        //{
        //}

        private double _SelectedDrink;

        public double SelectedDrink
        {
            get { return this._SelectedDrink; }
            private set
            {
                if( Equals( this._SelectedDrink, value ) )
                {
                    return;
                }
                this._SelectedDrink = value;
                this.OnPropertyChanged( "SelectedDrink" );
            }
        }

        private double _InsertedMoney;

        public double InsertedMoney
        {
            get { return this._InsertedMoney; }
            private set
            {
                if (Equals(this._InsertedMoney, value))
                {
                    return;
                }
                this._InsertedMoney = value;
                this.OnPropertyChanged( "InsertedMoney" );
            }
        }

        private double _ServingProcess;
        public double ServingProcess
        {
            get { return this._ServingProcess; }
            private set
            {
                if( Equals(this._ServingProcess, value ) )
                {
                    return;
                }
                this._ServingProcess = value;
                this.OnPropertyChanged( "ServingProcess" );
            }
        }

        public SMWorkflowModel()
            : base(SMWMachineState.Idle)
        {
            this.ConfigureMachine();
        }

        private void ConfigureMachine()
        {
            this.Configure( SMWMachineState.Idle )
                .Permit( SMWMachineTrigger.InsertMoney, SMWMachineState.CoinBox );
            this.Configure( SMWMachineState.RefundMoney )
                .OnEntry( this.RefundMoney )
                .Permit( SMWMachineTrigger.MoneyRefunded, SMWMachineState.Idle );
            //this.Configure( SMWMachineState.Idle )
            //    .Permit( SMWMachineTrigger.Select, SMWMachineState.SelectDrink );
            this.Configure( SMWMachineState.CoinBox )
                .PermitReentry( SMWMachineTrigger.InsertMoney )
                .Permit( SMWMachineTrigger.RefundMoney, SMWMachineState.RefundMoney )
                //.Permit( SMWMachineTrigger.CheckMoney, SMWMachineState.ControlMoney )
                .Permit(SMWMachineTrigger.EnoughMoney, SMWMachineState.SelectDrink);
            this.Configure( SMWMachineState.SelectDrink )
                .PermitReentry( SMWMachineTrigger.InsertMoney )
                .Permit( SMWMachineTrigger.RefundMoney, SMWMachineState.RefundMoney )
                .Permit(SMWMachineTrigger.ServeDrink, SMWMachineState.ServingDrink );
            this.Configure(SMWMachineState.ServingDrink)
                .OnEntry(this.ServeDrink)
                .Permit(SMWMachineTrigger.DrinkServed, SMWMachineState.DrinkReady);
            this.Configure( SMWMachineState.DrinkReady )
                .Permit( SMWMachineTrigger.TakeDrink, SMWMachineState.RefundMoney );
            this.OnTransitioned( this.NotisfyStateChanged );
        }

        private void EnoughMoney()
        {
            (new Task(() =>
                {
                    const double amount = 0;
                    var insertedMoney = this;
                    insertedMoney.InsertedMoney = insertedMoney.InsertedMoney + amount;
                    if( ( this.State == SMWMachineState.CoinBox && this.InsertedMoney >= 2 ) )
                    {
                        this.Fire( SMWMachineTrigger.EnoughMoney );
                    }
                } )).Start();
        }

        public void InsertCoin(double amount)
        {
            var insertedMoney = this;
            insertedMoney.InsertedMoney = insertedMoney.InsertedMoney + amount;
            if( (this.State == SMWMachineState.CoinBox && this.InsertedMoney >= 2) )
            {
                this.Fire( SMWMachineTrigger.EnoughMoney );
            }
        }

        private void NotisfyStateChanged(Transition transition)
        {
            this.OnPropertyChanged( "State" );
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = this.PropertyChanged;
            if( handler != null )
            {
                handler( this, new PropertyChangedEventArgs( propertyName ) );
            }
        }

        private void ServeDrink()
        {
            (new Task(() => {
                this.InsertedMoney = this.InsertedMoney - 2;
                while( this.ServingProcess < 100 )
                {
                    Thread.Sleep( 50 );
                    SMWorkflowModel servingProcess = this;
                    servingProcess.ServingProcess = servingProcess.ServingProcess + 1;
                }
                this.ServingProcess = 0;
                this.Fire(SMWMachineTrigger.DrinkServed);
            })).Start();
        }

        private void RefundMoney()
        {
            (new Task(() => {
                while( this.InsertedMoney > 1 )
            {
                    Thread.Sleep( 200 );
                this.InsertedMoney = this.InsertedMoney - 1;
            }
                this.InsertedMoney = 0;
                this.Fire( SMWMachineTrigger.MoneyRefunded );
            } )).Start();
        }

        //private void DeselectDrink()
        //{
        //    ( new Task( () => {
        //        while( this.SelectedDrink > 1 )
        //        {
        //            Thread.Sleep( 2000 );
        //            this.SelectedDrink = 0;
        //            this.Fire( SMWMachineTrigger.DeselectDrink );
        //        }
        //    } ) ).Start();
        //}

        public event PropertyChangedEventHandler PropertyChanged;
                
    }
}
