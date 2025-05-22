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
            PoveziClanoveGrupe();
        }

        private void PoveziClanoveGrupe()
        {
            string[] clanstva = File.ReadAllLines("data/clanstva.csv");

            foreach (string line in clanstva)
            {
                string[] info = line.Split(",");
                int clanId = int.Parse(info[0]);
                int grupaId = int.Parse(info[1]);

                Data[grupaId].ListaKorisnika.Add(KorisnikRepository.Data[clanId]);
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

