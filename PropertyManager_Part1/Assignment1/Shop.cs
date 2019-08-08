using System;

namespace Assignment1 {
    class Shop : Property {
        public enum business {
            Food, Repair, Retail
        };
        private double area;
        private business businessType;

        public Shop(int id, string title, string address, double area, business businessType) : base(id, title, address) {
            this.area = area;
            this.businessType = businessType;
            if (area > 50)
                this.price = 120000;
            else
                this.price = 80000;
        }

        public override void toString() {
            Console.WriteLine("\t- SHOP {ID: " + id + ", Title: " + title + ", Price: " + price + "$, Address: " + address + ", Area: " + area + ", Business Type: " + businessType + "}");
        }
    }
}
