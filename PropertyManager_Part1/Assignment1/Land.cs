using System;

namespace Assignment1 {
    class Land : Property {
        private double area;
        private bool canBeFarmed;
        public Land(int id, string title, string address, double area, bool canBeFarmed) : base(id, title, address) {
            this.area = area;
            this.canBeFarmed = canBeFarmed;
            this.price = area * 3000;
        }
        
        public override void toString() {
            Console.WriteLine("\t- LAND {ID: " + id + ", Title: " + title + ", Price: " + price + "$, Address: " + address + ", Area: " + area + ", Can be farmed: " + canBeFarmed + "}");
        }
    }
}
