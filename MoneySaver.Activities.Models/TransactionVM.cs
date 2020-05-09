using System;

namespace MoneySaver.Activities.Models
{
    public class TransactionVM
    {
        public int CategoryId { get; set; }

        public int StoreId { get; set; }

        public string AdditionalNote { get; set; }

        public double Amount { get; set; }
    }
}
