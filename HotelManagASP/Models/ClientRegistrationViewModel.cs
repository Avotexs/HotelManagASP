namespace HotelManagASP.Models
{
    public class ClientRegistrationViewModel
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Adresee { get; set; }
        public string CIN { get; set; }
        public int Tele { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
