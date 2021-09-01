using System;
using System.Globalization;

namespace ValVenalEstimator.Web.ViewModels
{
    public class ResponseDTO
    {
        public double ValVenal { get; set; }
        public double ValEnregistrement { get; set; }
        public double DroitDeTimbre { get; set; }
        public double PriceOfBornageContradictoire { get; set; }
        public double PriceToPay { get; set; }
        public string PrefectureName { get; set; }
        public string ZoneName { get; set; }
        public string ZoneType { get; set; }
        public string PlaceName { get; set; }
        public int Area { get; set; }

        //Pour l'affichage des montants avec le séparateur de milliers
        NumberFormatInfo nfi = new NumberFormatInfo {NumberGroupSeparator = " ", NumberDecimalDigits = 0};     
        public string valV{ get {return ValVenal.ToString("n", nfi);} }
        public string valE { get {return ValEnregistrement.ToString("n", nfi);} }
        public string droitD { get {return DroitDeTimbre.ToString("n", nfi);} }
        public string priceO { get {return PriceOfBornageContradictoire.ToString("n", nfi);} }
        public string priceT { get {return PriceToPay.ToString("n", nfi);} }
        public string aire { get {return Area.ToString("n", nfi);} }


    }
}