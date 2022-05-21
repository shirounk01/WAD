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
    public class Filter
    {
        public Sort sort { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public Time DepartureTime { get; set; }
        public Time ArrivalTime { get; set; }
    }
}
