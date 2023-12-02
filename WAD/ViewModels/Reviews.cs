using WAD.Models;

namespace WAD.ViewModels
{
    public class Reviews
    {
        public Hotel? Hotel { get; set; }
        public List<Review>? Posts { get; set; }
        public Review? Review { get; set; }
    }
}
