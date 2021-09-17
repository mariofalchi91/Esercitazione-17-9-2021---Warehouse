using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Esercitazione_17_Settembre_Warehouse
{
    public class Warehouse<T>:IEnumerable<T> where T:Good
    {
        public Warehouse() 
        {
            IdMagazzino = Guid.NewGuid();
            DataUltimaOperazione = DateTime.Now;
            MerciInGiacenza = new();
        }
        
        public Warehouse(string nome, string indirizzo, List<T> merci_in_giacenza)
        {
            IdMagazzino = Guid.NewGuid();
            DataUltimaOperazione = DateTime.Now;
            Nome = nome;
            Indirizzo = indirizzo;
            MerciInGiacenza = new(merci_in_giacenza);
        }
        
        public Guid IdMagazzino { get; }
        
        public string Nome { get; set; }
        
        public string Indirizzo { get; set; }
        
        public double ImportoTotaleMerci { get { return CalcolaImportoTotale(); } }
        
        public DateTime DataUltimaOperazione { get; set; }

        private List<T> MerciInGiacenza;
        
        public IEnumerator<T> GetEnumerator()
        {
            return GetEnumerator();
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var item in MerciInGiacenza)
            {
                yield return item;
            }
        }
        
        public void Aggiungi (T item)
        {
            var trovato = MerciInGiacenza.FirstOrDefault(i => i.CodiceMerce == item.CodiceMerce);

            if (trovato == null)
            {
                item.QuantitaInGiacenza = 1;
                MerciInGiacenza.Add(item);
            }
            else item.QuantitaInGiacenza++;

            DataUltimaOperazione = DateTime.Now;
            CalcolaImportoTotale();
        }
        
        public void Rimuovi (T item)
        {
            var trovato = MerciInGiacenza.FirstOrDefault(i => i.CodiceMerce == item.CodiceMerce);

            if (trovato!=null)
            {
                if (item.QuantitaInGiacenza >= 1)
                {
                    item.QuantitaInGiacenza--;
                    DataUltimaOperazione = DateTime.Now;
                    CalcolaImportoTotale();
                }
                else //se è già a zero, sperando che non sia negativo...
                {
                    item.QuantitaInGiacenza = 0;
                    Console.WriteLine($"Attenzione: prodotto {item.CodiceMerce} già esaurito");  //magari qua un'eccezione
                }
            }
        }
       
        public double CalcolaImportoTotale()
        {
            double tot = 0;

            foreach (var item in MerciInGiacenza)
                tot += item.Prezzo * item.QuantitaInGiacenza;

            return tot;
        }
        
        public static Warehouse<T> operator + (Warehouse<T> WH, T item)
        {
            WH.Aggiungi(item);
            return WH;
            // come dovrebbe essere usato:
            // WH += Prodotto;
        }
        
        public static Warehouse<T> operator - (Warehouse<T> WH, T item)
        {
            WH.Rimuovi(item);
            return WH;
        }
        
        public void StockList()
        {
            Console.WriteLine($"id:{IdMagazzino}; nome:{Nome}; indirizzo:{Indirizzo}; " +
                $"importo totale merci:{ImportoTotaleMerci}; data ultima operazione:{DataUltimaOperazione}");

            foreach (var item in MerciInGiacenza)
                item.ToString();
        }

        /*public enum GoodType
        {
            GENERIC,
            PERISHABLE,
            SPIRITDRINK,
            ELECTRONIC
        }*/

        public void CaricaDaFile(string path)
        {
            // mi devo inventare il formato del file da leggere
            // la prima riga sarà:
            // CodiceMerce; Descrizione; Prezzo; DataDiRicevimento; QuantitaInGiacenza; GOODTYPE;
            // che sarà la formattazione generica dei beni
            // quindi avrò il tipo di bene al campo 5
            // per i beni generici il campo GOODTYPE avrà il valore GENERIC
            // per gli altri funziona così:
            // per i PerishableGood il campo GOODTYPE vale PERISHABLE
            // e la formattazione sarà
            // ... GOODTYPE; DataDiScadenza; ModalitaDiConservazione;
            // analogo per gli altri 2.

            string riga, goodtype;
            string [] rs; // riga splittata
            List<T> NuoveMerci = new();

            using StreamReader reader = new(path);

            reader.ReadLine(); //salto la prima

            do
            {
                riga = reader.ReadLine();

                if (riga!=null)
                {
                    rs = riga.Split(';');
                    goodtype = rs[5];
                    
                    if (goodtype.Equals("GENERIC"))
                    {
                        int.TryParse(rs[0], out int codice);
                        double.TryParse(rs[2], out double prezzo);
                        DateTime.TryParse(rs[3], out DateTime data);
                        int.TryParse(rs[4], out int quantita);

                        Good g =  new (codice, rs[1], prezzo, data, quantita);
                        NuoveMerci.Add(g);    //errore
                    }

                    if (goodtype.Equals("PERISHABLE"))
                    {
                        int.TryParse(rs[0], out int codice);
                        double.TryParse(rs[2], out double prezzo);
                        DateTime.TryParse(rs[3], out DateTime data);
                        int.TryParse(rs[4], out int quantita);
                        DateTime.TryParse(rs[6], out DateTime datascadenza);
                        PGModCons.TryParse(rs[7], out PGModCons tipo);

                        PerishableGood g = new (codice, rs[1], prezzo, data, quantita, datascadenza, tipo);
                        NuoveMerci.Add(g);  //errore
                    }

                    if (goodtype.Equals("SPIRITDRINK"))
                    {
                        int.TryParse(rs[0], out int codice);
                        double.TryParse(rs[2], out double prezzo);
                        DateTime.TryParse(rs[3], out DateTime data);
                        int.TryParse(rs[4], out int quantita);
                        SDGType.TryParse(rs[6], out SDGType tipo);
                        double.TryParse(rs[7], out double gradi);

                        SpiritDrinkGood g = new(codice, rs[1], prezzo, data, quantita, tipo, gradi);
                        NuoveMerci.Add(g); //errore
                    }

                    if (goodtype.Equals("ELECTRONIC"))
                    {
                        int.TryParse(rs[0], out int codice);
                        double.TryParse(rs[2], out double prezzo);
                        DateTime.TryParse(rs[3], out DateTime data);
                        int.TryParse(rs[4], out int quantita);

                        ElectronicGood g = new(codice, rs[1], prezzo, data, quantita, rs[6]);
                        NuoveMerci.Add(g); //errore
                    }
                }

            } while (riga != null);

            MerciInGiacenza = NuoveMerci;
        }
    }
}
