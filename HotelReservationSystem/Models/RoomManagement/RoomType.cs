﻿using HotelReservationSystem.Data.Enums;

namespace HotelReservationSystem.Models.RoomManagement
{
    public class RoomType : BaseModel
    {
        public RoomTypeName RoomTypeName { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }

        public ICollection<Room> Rooms { get; set; }
        public ICollection<RoomTypeFacility> RoomTypeFacilities { get; set; }  = new List<RoomTypeFacility>();
        
    }
}
