using System;

namespace Assignment1 {
    class Apartment : Property {
        private int numberOfRooms;
        public Apartment(int id, string title, string address, int numberOfRooms) : base(id, title, address) {
            this.numberOfRooms = numberOfRooms;
            this.price = numberOfRooms * 15000;
        }

        public override void toString() {
            Console.WriteLine("\t- APARTMENT {ID: " + id + ", Title: " + title + ", Price: " + price + "$, Address: " + address + ", Number of rooms: " + numberOfRooms + "}");
        }
    }
}
