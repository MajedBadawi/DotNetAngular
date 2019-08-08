using System;

namespace Assignment1 {
    abstract class Property {
        protected int id;
        protected string title;
        protected double price;
        protected string address;

        public Property(int id, string title, string address) {
            this.id = id;
            this.title = title;
            this.address = address;
        }

        public abstract void toString();
        public int getID() {
            return id;
        }

        public double getPrice() {
            return price;
        }

        public void setTitle(string title) {
            this.title = title;
        }

    }
}
