﻿namespace Tour_Ready_Capstone.Models
{
    public class GroupMember
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public Boolean IsEditor { get; set; }

    }
}
