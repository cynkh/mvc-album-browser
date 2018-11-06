using System.Collections.Generic;

 namespace mvc_album_browser.Models
 {
     public class User
     {
         public int Id { get; set; }
         public string Name { get; set; }
         public string UserName { get; set; }
         public string EmailAddress { get; set; }
         public string PhoneNumber { get; set; }
         public string Website { get; set; }
         public Address Address { get; set; }
         public Company Company { get; set; }
         public IEnumerable<Post> Posts { get; set; }
     }
     public class Address
     {
        public string Street { get; set; }
        public string Suite { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public GeoLocation Geo { get; set; }
     }
     public class GeoLocation
     {
         public decimal Latitude { get; set; }
         public decimal Longitude { get; set; }
     }
     public class Company
     {
        public string Name { get; set; }
        public string CatchPhrase { get; set; }
        public string Tags { get; set; }
     }
 }