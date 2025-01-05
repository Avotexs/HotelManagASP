namespace HotelManagASP.Models
{
    public class Chambre
    {
        public int id { get; set; }
        public string type_Chambre { get; set; }
        public int numero { get; set; }
        public int Capacite { get; set; }
        public double Prix { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public List<Reservation> Reservations { get; set; }


    }
}
