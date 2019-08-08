using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1 {
    public class PurchaseFinishedEventArgs : EventArgs {
        public int buyerID { get; set; }
        public int propertyID { get; set; }
    }
}
