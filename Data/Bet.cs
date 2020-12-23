//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Bet
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Nullable<int> TournamentId { get; set; }
        public Nullable<int> TourId { get; set; }
        public int MatchId { get; set; }
        public Nullable<int> ScoreH { get; set; }
        public Nullable<int> ScoreA { get; set; }
        public bool IsSet { get; set; }
    
        public virtual Match Match { get; set; }
        public virtual Tour Tour { get; set; }
        public virtual Tournament Tournament { get; set; }
        public virtual User User { get; set; }
    }
}