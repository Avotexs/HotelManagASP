namespace HotelManagASP.Models
{
    public class Client
    {
        public int id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Adresee { get; set; }
        public string CIN { get; set; }
        public int Tele { get; set; }
        public DateTime DateRejoin { get; set; }

        public string MotDePasse { get; set; }
        public string Email { get; set; }
        public bool IsEmailVerified { get; set; }
        public List<Reservation> Reservations { get; set; }




    }
}
