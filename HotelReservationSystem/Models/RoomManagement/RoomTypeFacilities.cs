﻿namespace HotelReservationSystem.Models.RoomManagement
{
    public class RoomTypeFacilities
    {
        public int ID { get; set; }
        public int RoomTypeID { get; set; }
        public RoomType RoomType { get; set; }

        public int FacilityID { get; set; }
        public Facility Facility { get; set; }
    }
}
