using Newtonsoft.Json;

namespace Assignment2.Models {
    public class FilterModel {
        public float FromPrice { get; set; }
        public float ToPrice { get; set; }
        public string Address { get; set; }
        public int NumberOfRooms { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
    }
}
