namespace BookStore.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {

        public int Id { get; set; }

        public int UserId { get; set; }

        public int BookId { get; set; }

        public string BookName { get; set; }

    [Column(TypeName = "smalldatetime")]
        public DateTime Date { get; set; }

        public virtual Book Book { get; set; }

        public virtual User User { get; set; }
    }
}
