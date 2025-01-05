using Microsoft.EntityFrameworkCore;
using HotelManagASP.Models;

namespace HotelManagASP.Data
{
    public class ContexteHM : DbContext
    {
        public ContexteHM( DbContextOptions<ContexteHM> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation>()
                .HasKey(cl => new { cl.ClientId, cl.ChambreId });

            modelBuilder.Entity<Reservation>()
                .HasOne<Client>(c => c.Client)
                .WithMany(cl => cl.Reservations)
                .HasForeignKey(c => c.ClientId);

            modelBuilder.Entity<Reservation>()
                .HasOne<Chambre>(h => h.Chambre)
                .WithMany(cl => cl.Reservations)
                .HasForeignKey(h => h.ChambreId);

            modelBuilder.Entity<Chambre>().HasData(
                new Chambre
                {
                    id = 1,
                    type_Chambre = "Simple",
                    numero = 1,
                    Capacite = 1,
                    Prix = 100,
                    ImageUrl = "https://www.hotelarmoniparis.com/_novaimg/galleria/1535876.jpg",
                    Description = "Chambre Simple"
                   
                }
                );

            modelBuilder.Entity<Client>().HasData(
                new Client
                {
                    id = 1,
                    Nom = "Hassan",
                    Prenom = "Hassani",
                    Adresee = "Rue 1",
                    CIN = "A",
                    Tele = 0612345678,
                    DateRejoin = new DateTime(2021, 1, 1),
                    MotDePasse = "123456",
                    Email = "admin@gmail.com"
                }
                );

            modelBuilder.Entity<Reservation>().HasData(
                    new Reservation
                    {
                        id = 1,
                        ClientId = 1,
                        ChambreId = 1,
                        dateArrive = new DateTime(2021, 1, 1),
                        dateSortie = new DateTime(2021, 1, 4),
                        
                        prixtotal = 100,
                        statut = "En cours"
                    }
                    );


            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Chambre> Chambres { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}
