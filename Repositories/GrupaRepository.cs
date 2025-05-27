using drustvena_mreza.Model;
using System.Text.RegularExpressions;

namespace drustvena_mreza.Repositories
{
    public class GrupaRepository
    {
        private const string putanja = "data/grupe.csv";
        public static Dictionary<int, Grupa> Data;

        public GrupaRepository()
        {
            if(Data == null)
            {
                Load();
            }
        }

        private void Load()
        {
            Data = new Dictionary<int, Grupa>();

            string[] lines = File.ReadAllLines(putanja);

            foreach (string line in lines)
            {
                string[] podatci = line.Split(',');

                int id = int.Parse(podatci[0]);
                string ime = podatci[1];
                DateOnly datumOsnivanja = DateOnly.Parse(podatci[2]);

                Grupa g = new Grupa(id, ime, datumOsnivanja);
                Data.Add(id, g);
            }
            KorisnikRepository korisnikRepo = new KorisnikRepository();

            PoveziClanoveGrupe();

        }

        private void PoveziClanoveGrupe()
        {
            string[] clanstva = File.ReadAllLines("data/clanstva.csv");

            foreach (string line in clanstva)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                string[] info = line.Split(",");
                if (info.Length != 2) continue;

                int clanId = int.Parse(info[0]);
                int grupaId = int.Parse(info[1]);

                if (!Data.ContainsKey(grupaId)) continue;
                if (!KorisnikRepository.Data.ContainsKey(clanId)) continue;

                if (Data[grupaId].ListaKorisnika == null)
                    Data[grupaId].ListaKorisnika = new List<Korisnik>();

                Data[grupaId].ListaKorisnika.Add(KorisnikRepository.Data[clanId]);

                Console.WriteLine($"Dodajem korisnika {clanId} u grupu {grupaId}");
            }
        }
        public void Save()
        {
            List<string> lines = new List<string>();

            foreach (Grupa g in Data.Values)
            {
                lines.Add($"{g.Id},{g.Ime},{g.DatumOsnivanja.ToString("yyyy-MM-dd")}");
            }

            File.WriteAllLines(putanja, lines);
        }
    }
}

