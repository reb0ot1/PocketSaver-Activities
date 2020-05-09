using System;

namespace MoneySaver.Activities.Data.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }

        public DateTime CreateOn { get; set; }

        public DateTime ModifyOn { get; set; }

        public int UserId { get; set; }

        public int MarketId { get; set; }

        public int CategoryId { get; set; }

        public double Amount { get; set; }

        public string AdditionalNote { get; set; }
    }
}
