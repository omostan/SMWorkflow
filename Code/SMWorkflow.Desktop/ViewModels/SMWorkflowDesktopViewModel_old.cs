// -------------------------------------------------------------------------
// <copyright file="SMWorkflowDesktopViewModel.cs" company="AGI, Novomatic Group.">
//     Copyright © 2015 Stanley Omoregie. All Rights Reserved.
// </copyright>
// -------------------------------------------------------------------------

namespace SMWorkflow.Desktop.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using SMWorkflow.Base;
    using SMWorkflow.Model;

    public class SMWorkflowDesktopViewModel : BindableObject
    {
        /// <summary>
        /// An instance of the ViewModel constructor
        /// </summary>
        public SMWorkflowDesktopViewModel()
        {
            this.SelectedDrink = new Drink();
            this.Drinks = new ObservableCollection<Drink>();
            this.smwMachine = new SMWorkflowMachine();
            //InsertCommand = smwMachine.CreateCommand(SMWorkflowMachine.Trigger.Insert);
            //SelectCommand = smwMachine.SelectCommand(SMWorkflowMachine.Trigger.Select);
            //CheckCommand = smwMachine.CreateCommand(SMWorkflowMachine.Trigger.Check);
            Task.Run( () =>
                    {
                        GetDrinks();
                        
                    });
        }

        #region Automatic Properties

        public SMWorkflowMachine smwMachine { get; set; }

        public ICommand InsertCommand { get; set; }

        public ICommand SelectCommand { get; set; }

        public ICommand CheckCommand { get; set; }

        public ObservableCollection<Drink> Drinks { get; set; }

        #endregion


        #region Bindable SelectedDrink

        private Drink _selectedDrink;

        public Drink SelectedDrink
        {
            get
            {
                return this._selectedDrink;
            }
            set
            {
                if (value == this._selectedDrink)
                {
                    return;
                }
                this._selectedDrink = value;
                this.RaisePropertyChanged("SelectedDrink");
                this.smwMachine.TryFireTrigger(
                    value != null ? SMWorkflowMachine.Trigger.Select : SMWorkflowMachine.Trigger.NotFound);
            }
        }

        #endregion

        #region Load drinks

        private async Task LoadDrinks()
        {
            try
            {
                Drinks.Clear();

                await Task.Delay(2000);

                var drinks = GetDrinks();
                drinks.ForEach(d => Drinks.Add(d));

                this.smwMachine.TryFireTrigger(SMWorkflowMachine.Trigger.SelectedDrink);

                if (Drinks.Count > 0)
                {
                    SelectedDrink = Drinks.First();
                }

            }
            catch (Exception)
            {
                this.smwMachine.TryFireTrigger(SMWorkflowMachine.Trigger.NotFound);
            }
        }

        #endregion

        #region Get List of drinks

        private static List<Drink> GetDrinks()
        {
            return new List<Drink>()
                       {
                           new Drink("Cola", "Beverage", "Coca Cola", "Coca Cola", 2.40),
                           new Drink("Fanta", "Beverage", "Coca Cola", "Coca Cola", 2.40),
                           new Drink("Sprite", "Beverage", "Coca Cola", "Coca Cola", 2.40),
                           new Drink("7up", "Beverage", "Coca Cola", "Coca Cola", 2.40),
                           new Drink("Beer", "Lager", "Heineken", "Heineken International", 3.20),
                           new Drink("Beer", "Lager", "Budweiser Light", "Budweiser", 3.60)
                       };
        }

        #endregion

    }
}
