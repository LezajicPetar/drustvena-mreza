namespace drustvena_mreza.Model
{
    public class Grupa
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public DateOnly DatumOsnivanja { get; set; }
        public List<Korisnik> ListaKorisnika { get; set; }

        public Grupa(int id, string ime, DateOnly datumOsnivanja)
        {
            this.Id = id;
            this.Ime = ime;
            this.DatumOsnivanja = datumOsnivanja;
            ListaKorisnika = new List<Korisnik>();
        }

    }
}
