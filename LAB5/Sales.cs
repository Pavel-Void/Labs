using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooshopApp
{
    class Sale
    {
        int id;
        int id_animal;
        int id_customer;
        DateTime date;
        decimal price;

        public Sale(int id, int id_animal, int id_customer, DateTime date, decimal price)
        {
            this.id = id;
            this.id_animal = id_animal;
            this.id_customer = id_animal;
            this.date = date;
            this.price = price;
        }

        public int GetID()
        {
            return id;
        }

        public int GetIdAnimals()
        {
            return id_animal;
        }

        public int GetIdCustomer()
        {
            return id_customer;
        }

        public DateTime GetDate()
        {
            return date;
        }

        public decimal GetPrice()
        {
            return price;
        }

        public override string ToString()
        {
            return $"{id} | {id_animal} | {id_customer} | {date} | {price}";
        }
    }
}
