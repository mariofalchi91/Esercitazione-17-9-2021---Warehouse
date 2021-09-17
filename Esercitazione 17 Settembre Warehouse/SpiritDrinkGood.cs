using System;

namespace Esercitazione_17_Settembre_Warehouse
{
    public enum SDGType { WHISKY,WODKA,GRAPPA,GIN,OTHER}
    class SpiritDrinkGood:Good
    {
        public SDGType Tipo { get; set; }
        public double GradazioneAlcoolica { get; set; }
        public SpiritDrinkGood()
        {

        }
        public SpiritDrinkGood(int CodiceMerce, string Descrizione, double Prezzo, DateTime DataDiRicevimento, 
            int QuantitaInGiacenza, SDGType Tipo, double GradazioneAlcoolica)
        {
            this.CodiceMerce = CodiceMerce;
            this.Descrizione = Descrizione;
            this.Prezzo = Prezzo;
            this.DataDiRicevimento = DataDiRicevimento;
            this.QuantitaInGiacenza = QuantitaInGiacenza;
            this.Tipo = Tipo;
            this.GradazioneAlcoolica = GradazioneAlcoolica;
        }
        public override string ToString()
        {
            return $"Codice Merce: {CodiceMerce}; Descrizione: {Descrizione}; Prezzo: {Prezzo}; " +
                $"Data di ricevimento: {DataDiRicevimento}; Quantità in giacenza:{QuantitaInGiacenza};" +
                $"Tipo: {Tipo}; Gradazione alcolica: {GradazioneAlcoolica}";
        }
    }

}
