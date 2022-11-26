namespace Tour_Ready_Capstone.Models
{
    public class Show
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public string Venue { get; set; }
        public DateTime ShowDate { get; set; }
        public int CityId { get; set; }
        public string SetList { get; set; }
        public string ShowNotes { get; set; }
        public int MerchSales { get; set; }
        public int Payout { get; set; }
        public Boolean IsFavorite { get; set; }



    }
}
