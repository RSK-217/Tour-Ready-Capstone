namespace Tour_Ready_Capstone.Models
{
    public class MemebersOfGroupViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public Boolean IsEditor { get; set; }
        public string GroupName { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }

    }
}
