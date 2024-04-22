namespace BMajozi.Models.ViewModel
{
    public class Summary
    {
        public int Count { get; set; }
        public double AverageAge { get; set; }
        public int MaxAge { get; set; }
        public int MinAge { get; set; }
        public double PizzaLikersPercent { get; set; }
        public double PapNWLikersPercent { get;  set; }
        public double PastaLikersPercent { get;  set; }
        public int PPLWatchingTV { get; internal set; }
        public int PPLListiningRadio { get; internal set; }
        public int PPLEating { get; internal set; }
        public int PPLWatchingMovie { get; internal set; }
    }
}
