// -------------------------------------------------------------------------
// <copyright file="Drink.cs" company="AGI, Novomatic Group.">
//     Copyright © 2015 Stanley Omoregie. All Rights Reserved.
// </copyright>
// -------------------------------------------------------------------------
namespace SMWorkflow.Model
{
    public class Drink
    {

        public Drink()
        {
        }

        public Drink(string name, string type, string brand, string companyName, double price)
        {
            this.Name = name;
            this.Type = type;
            this.Brand = brand;
            this.CompanyName = companyName;
            this.Price = price;
        }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Brand { get; set; }

        public string CompanyName { get; set; }

        public double Price { get; set; }
    }
}
