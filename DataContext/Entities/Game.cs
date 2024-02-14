﻿namespace UserDataService.DataContext.Entities
{
    public class Game : BaseEntity
    {
        public int WhiteId { get; set; }
        public int BlackId { get; set; }
        public List<Move> Moves { get; set; }
    }
}
