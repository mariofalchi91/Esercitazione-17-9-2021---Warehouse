using System;

namespace Esercitazione_17_Settembre_Warehouse
{
    class ElectronicGood:Good
    {
        public string Produttore { get; set; }
        public ElectronicGood() { }
        public ElectronicGood(int CodiceMerce, string Descrizione, double Prezzo, 
            DateTime DataDiRicevimento, int QuantitaInGiacenza, string Produttore)
        {
            this.CodiceMerce = CodiceMerce;
            this.Descrizione = Descrizione;
            this.Prezzo = Prezzo;
            this.DataDiRicevimento = DataDiRicevimento;
            this.QuantitaInGiacenza = QuantitaInGiacenza;
            this.Produttore = Produttore;
        }
        public override string ToString()
        {
            return $"Codice Merce: {CodiceMerce}; Descrizione: {Descrizione}; Prezzo: {Prezzo}; " +
                $"Data di ricevimento: {DataDiRicevimento}; Quantità in giacenza:{QuantitaInGiacenza}; Produttore: {Produttore}";
        }
    }
}
