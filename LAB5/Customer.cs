using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooshopApp
{
    class Customer
    {
        int id;
        string name;
        int age;
        string address;

        public Customer(int id, string name, int age, string address)
        {
            this.id = id;
            this.name = name;
            this.age = age;
            this.address = address;
        }

        public int GetID()
        {
            return id;
        }

        public string GetName()
        {
            return name;
        }

        public int GetAge()
        {
            return age;
        }

        public string GetAddress()
        {
            return address;
        }

        public override string ToString()
        {
            return $"{id} | {name} | {age} | {address}";
        }
    }
}
