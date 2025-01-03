using HotelReservationSystem.Models.RoomManagement;

namespace HotelReservationSystem.Models.ReservationManagement;

public class ReservationRoom : BaseModel
{
    public int ReservationID { get; set; }
    public Reservation Reservation { get; set; }
    public int RoomID { get; set; }
    public Room Room { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public decimal Amount { get; set; }
}