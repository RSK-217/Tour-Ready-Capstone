namespace Tour_Ready_Capstone.Models
{
    public class ShowsByIdViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string Venue { get; set; }
        public DateTime ShowDate { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string SetList { get; set; }
        public string ShowNotes { get; set; }
        public int MerchSales { get; set; }
        public int Payout { get; set; }
        public Boolean IsFavorite { get; set; }
    }
}
