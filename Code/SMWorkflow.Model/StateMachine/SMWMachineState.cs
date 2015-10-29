namespace SMWorkflow.Model.StateMachine
{
    public enum SMWMachineState
    {
        Idle,
            SelectDrink,
            CoinBox,
            ControlMoney,
            SelectedDrink,
            ServingDrink,
            DrinkReady,
            MoneyRefunded
    }
}
