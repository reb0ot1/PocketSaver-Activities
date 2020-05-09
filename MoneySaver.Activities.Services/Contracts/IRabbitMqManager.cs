using System;
using System.Collections.Generic;
using System.Text;

namespace MoneySaver.Activities.Services.Contracts
{
    public interface IRabbitMqManager
    {
        void Publish<T>(T message, string exchangeName, string exchangeType, string routeKey)
            where T : class;
    }
}
