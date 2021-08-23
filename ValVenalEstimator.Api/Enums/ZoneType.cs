using System.ComponentModel;

namespace ValVenalEstimator.Api.Enums
{
    public enum ZoneType 
    {
        [Description("Urbain")]   
        Urbaine = 1, 
        [Description("Periurbain")]
        Periurbaine = 2, 
        [Description("Rural")]
        Rurale = 3,
        [Description("Urbain et periurbain")]
        UrbaineEtPeriurbaine = 4
    }
}