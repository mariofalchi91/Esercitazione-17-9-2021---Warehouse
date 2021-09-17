using System;

namespace Esercitazione_17_Settembre_Warehouse
{
    public enum PGModCons
    {
        FREEZER,
        FRIGDE,
        SHELF
    }
    class PerishableGood:Good
    {
        public DateTime DataDiScadenza { get; set; }
        public PGModCons ModalitaDiConservazione { get; set; }
        public PerishableGood()
        {

        }
        public PerishableGood(int CodiceMerce, string Descrizione, double Prezzo, DateTime DataDiRicevimento, 
            int QuantitaInGiacenza, DateTime DataDiScadenza, PGModCons ModalitaDiConservazione)
        {
            this.CodiceMerce = CodiceMerce;
            this.Descrizione = Descrizione;
            this.Prezzo = Prezzo;
            this.DataDiRicevimento = DataDiRicevimento;
            this.QuantitaInGiacenza = QuantitaInGiacenza;
            this.DataDiScadenza = DataDiScadenza;
            this.ModalitaDiConservazione = ModalitaDiConservazione;
        }
        public override string ToString()
        {
            return $"Codice Merce: {CodiceMerce}; Descrizione: {Descrizione}; Prezzo: {Prezzo}; " +
                $"Data di ricevimento: {DataDiRicevimento}; Quantità in giacenza:{QuantitaInGiacenza}; " +
                $"Data di scadenza: {DataDiScadenza}; Modalità di conservazione: {ModalitaDiConservazione}";
        }
    }
}
