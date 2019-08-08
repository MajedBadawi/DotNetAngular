using System;
using System.Collections.Generic;

namespace Assignment1 {
    class Buyer {
        private int id;
        private string name;
        private double credit;
        private List<Property> ownedProperties;

        public event PurchaseFinishedDelegate PurchaseFinished;

        public Buyer(int id, string name, double credit) {
            this.id = id;
            this.name = name;
            this.credit = credit;
            this.ownedProperties = new List<Property>();
        }

        public void toString() {
            Console.WriteLine("\t- BUYER {ID: " + id + ", Name: " + name + ", Credit: " + credit + "$}");
        }

        public double getCredit() {
            return credit;
        }

        public void setCredit(double credit) {
            this.credit = credit;
        }

        public int getID() {
            return id;
        }

        public string getName() {
            return name;
        }

        public void addProperty(Property p) {
            ownedProperties.Add(p);
        }

        public List<Property> getOwnedProperties() {
            return ownedProperties;
        }

        public void buyProperty(Property property) {
            if (credit >= property.getPrice()) {
                setCredit(credit - property.getPrice());
                addProperty(property);
                //fire event
                if (PurchaseFinished != null) {
                    PurchaseFinishedEventArgs args = new PurchaseFinishedEventArgs();
                    args.buyerID = id;
                    args.propertyID = property.getID();
                    PurchaseFinished(this, args);
                }
            } else
                Console.WriteLine("\t- Operation failed, " + name + " does not have enough credit to buy property of ID = " + property.getID());
        }

    }
}
