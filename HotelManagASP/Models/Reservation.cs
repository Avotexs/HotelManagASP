namespace HotelManagASP.Models
{
    public class Reservation
    {
        public int id { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }

        public int ChambreId { get; set; }
        public Chambre Chambre { get; set; }

        public DateTime dateArrive { get; set; }
        public DateTime dateSortie { get; set; }
        public double prixtotal { get; set; }
        public string statut { get; set; }

        public  List<Reservation> Reservations { get; set; }
    }
}
