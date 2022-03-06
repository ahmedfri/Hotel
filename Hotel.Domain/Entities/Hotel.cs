﻿



using System.Collections.Generic;

namespace Hotel.Domain.Entities
{
    public class Hotel : BaseEntity
    {
        public Hotel()
        {
            Rooms = new List<Room>();
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
     
        public virtual List<HotelImage> Images { get; set; }
        public virtual List<Room> Rooms { get; set; }
        public virtual List<Review> Reviews { get; set; }
    }
}