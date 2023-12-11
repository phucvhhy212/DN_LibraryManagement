namespace LibraryCore.ViewModels
{
    public class AddOrderViewModel
    {
        public int UserId { get; set; }

        public string Address { get; set; }

        public double Total { get; set; }

        public DateTime OrderDate { get; set; }

        public string Status { get; set; }
    }
}
