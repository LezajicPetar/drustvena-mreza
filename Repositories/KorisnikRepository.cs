using drustvena_mreza.Model;
using Microsoft.Extensions.ObjectPool;

namespace drustvena_mreza.Repositories
{
    public class KorisnikRepository
    {
        private static string putanja = "data/korisnici.csv";
        public static Dictionary<int, Korisnik> Data;

        public KorisnikRepository()
        {
            if (Data == null)
            {
                Load();
            }
        }

        private void Load()
        {
            Data = new Dictionary<int, Korisnik>();

            string[] lines = File.ReadAllLines(putanja);

            foreach (string line in lines)
            {
                string[] podatci = line.Split(',');

                int id = int.Parse(podatci[0]);
                string userName = podatci[1];
                string ime = podatci[2];
                string prezime = podatci[3];
                DateOnly datumRodjenja = DateOnly.Parse(podatci[4]);

                Korisnik k = new Korisnik(id, userName, ime, prezime, datumRodjenja);
                Data.Add(id, k);
            }
        }
        public void Save()
        {
            List<string> lines = new List<string>();

            foreach(Korisnik k in Data.Values)
            {
                lines.Add($"{k.Id},{k.UserName},{k.Ime},{k.Prezime},{k.DatumRodjenja.ToString("yyyy-MM-dd")}");
            }

            File.WriteAllLines(putanja, lines);
        }
    }
}
