using System.ComponentModel.DataAnnotations;

namespace WAD.Models
{
    public enum Sort
    {
        [Display(Name = "Choose...")] None, Ascending, Descending
    }
    public enum Time
    {
        [Display(Name = "Choose...")] None, [Display(Name = "Early morining")] Early, Morning, Afternoon, Evening
    }
    public enum Currency
    {
        [Display(Name = "Choose...")]       None,
        [Display(Name = "American Dollar")] USD,
        [Display(Name = "Euro")]            EUR,
        [Display(Name = "British Pound")]   GBP,
        [Display(Name = "Japanese Yen")]    JPY,
        [Display(Name = "Romanian Leu")]    RON
    }
    public class Filter
    {
        public Currency currency { get; set; }
        public Sort sort { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public Time DepartureTime { get; set; }
        public Time ArrivalTime { get; set; }
    }
}
