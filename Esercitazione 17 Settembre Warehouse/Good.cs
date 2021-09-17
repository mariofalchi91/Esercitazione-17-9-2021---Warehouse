using System;

namespace Esercitazione_17_Settembre_Warehouse
{
    public  class Good
    {
        public int CodiceMerce { get; set; }
        public string Descrizione { get; set; }
        public double Prezzo { get; set; }
        public DateTime  DataDiRicevimento { get; set; }
        public int QuantitaInGiacenza { get; set; }
        public Good() { }
        public Good(int CodiceMerce, string Descrizione, double Prezzo, DateTime DataDiRicevimento, int QuantitaInGiacenza)
        {
            this.CodiceMerce = CodiceMerce;
            this.Descrizione = Descrizione;
            this.Prezzo = Prezzo;
            this.DataDiRicevimento = DataDiRicevimento;
            this.QuantitaInGiacenza = QuantitaInGiacenza;
        }
        public override string ToString()
        {
            return $"Codice Merce: {CodiceMerce}; Descrizione: {Descrizione}; Prezzo: {Prezzo}; " +
                $"Data di ricevimento: {DataDiRicevimento}; Quantità in giacenza:{QuantitaInGiacenza}";
        }

        
    }
}