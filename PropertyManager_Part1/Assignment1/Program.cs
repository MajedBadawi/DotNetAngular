using System;
using System.Collections.Generic;

namespace Assignment1 {
    class Program {
        public static List<Property> properties = new List<Property>();
        public static List<Buyer> buyers = new List<Buyer>();
        
        private static void OnPurchaseFinished(object sender, PurchaseFinishedEventArgs args) {
            Console.WriteLine("\t- {0} with ID {1} was purchased by {2} for {3}$", properties[args.propertyID - 1].GetType(),
                properties[args.propertyID - 1].getID(), buyers[args.buyerID - 1].getName(), properties[args.propertyID - 1].getPrice());
        }

        public bool isPurchased(Property property) {
            foreach (Buyer buyer in buyers)
                foreach (Property p in buyer.getOwnedProperties())
                    if (property.getID() == p.getID())
                        return true;
            return false;
        }

        static void Main() {
            Program program = new Program();

            Property apartment1 = new Apartment(properties.Count+1, "Apartment 1", "Beirut", 2);
            properties.Add(apartment1);
            Property apartment2 = new Apartment(properties.Count+1, "Apartment 2", "Dbayeh", 3);
            properties.Add(apartment2);
            Property apartment3 = new Apartment(properties.Count+1, "Apartment 3", "Jounieh", 4);
            properties.Add(apartment3);
            Property apartment4 = new Apartment(properties.Count+1, "Apartment 4", "Byblos", 5);
            properties.Add(apartment4);
            Property apartment5 = new Apartment(properties.Count+1, "Apartment 5", "Kaslik", 6);
            properties.Add(apartment5);
            Property land1 = new Land(properties.Count+1, "Land 1", "Beirut", 60.3, true);
            properties.Add(land1);
            Property land2 = new Land(properties.Count+1, "Land 2", "New York", 129.7, false);
            properties.Add(land2);
            Property shop1 = new Shop(properties.Count+1, "Shop 1", "London", 40.3, Shop.business.Food);
            properties.Add(shop1);
            Property shop2 = new Shop(properties.Count+1, "Shop 2", "Bristol", 70.3, Shop.business.Repair);
            properties.Add(shop2);
            Property shop3 = new Shop(properties.Count+1, "Shop 3", "Manchester", 47.3, Shop.business.Retail);
            properties.Add(shop3);
            Console.WriteLine("1. Created five apartments, two lands, and three shops\n");

            Buyer buyer1 = new Buyer(buyers.Count+1, "John", 60000);
            buyers.Add(buyer1);
            Buyer buyer2 = new Buyer(buyers.Count+1, "Mike", 10000);
            buyers.Add(buyer2);
            Buyer buyer3 = new Buyer(buyers.Count+1, "Stephanie", 400000);
            buyers.Add(buyer3);
            Console.WriteLine("2. Created three buyers:");
            foreach (Buyer b in buyers)
                b.toString();
            Console.WriteLine("\n");

            Console.WriteLine("3. List of properties:");
            foreach (Property p in properties)
                p.toString();
            Console.WriteLine("\n");

            Console.WriteLine("4. List of lands:");
            foreach (Property p in properties)
                if (p is Land)
                    p.toString();
            Console.WriteLine("\n");

            Console.WriteLine("5.Properties whose price is between 45,000$ and 100,000$:");
            foreach (Property p in properties)
                if (45000 <= p.getPrice() && p.getPrice() <= 100000)
                    p.toString();
            Console.WriteLine("\n");

            Console.WriteLine("6. Purchase Simulation:");
            if (program.isPurchased(properties[0]) == false) {
                buyer1.PurchaseFinished += OnPurchaseFinished;
                buyer1.buyProperty(properties[0]);
            }
            if (program.isPurchased(properties[9]) == false) {
                buyer2.PurchaseFinished += OnPurchaseFinished;
                buyer2.buyProperty(properties[9]);
            }
            if (program.isPurchased(properties[2]) == false) {
                buyer3.PurchaseFinished += OnPurchaseFinished;
                buyer3.buyProperty(properties[2]);
            }
            Console.WriteLine("\n");

            Console.WriteLine("7. List of buyers: ");
            foreach (Buyer b in buyers)
                Console.WriteLine("\t- Buyer {" + b.getName() + "}\n" +
                                   "\t  Nb of Owned Properties: {" + b.getOwnedProperties().Count + "}\n" +
                                   "\t  Remaining Credit: {" + b.getCredit() + "$}\n");
            Console.WriteLine("\n");

            Console.WriteLine("8. Property of ID = 2 before and after changing its title");
            properties[1].toString();
            properties[1].setTitle("APARTMENT II");
            properties[1].toString();
            Console.WriteLine("\n");

            var random = new Random();
            int count = 0;
            int[] removedProperties = new int[2];
            while(count != 2) {
                int index = random.Next(properties.Count);
                if(program.isPurchased(properties[index]) == false) {
                    count++;
                    if (count == 1)
                        removedProperties[0] = index;
                    else if(index != removedProperties[0])
                        removedProperties[1] = index;
                }
            }
            properties.RemoveAt(removedProperties[0]);
            properties.RemoveAt(removedProperties[1]);
            Console.WriteLine("9. Removed two unpurchased properties od IDs {" + (removedProperties[0]+1) + ", " + (removedProperties[1]+1) + "} randomly, remaining ones are: ");
            foreach (Property p in properties)
                p.toString();
            Console.WriteLine("\n\n\t\t\t\t\t\t********** GOOD BYE **********\t\t\t\t\t\t");
        }

    }
}
