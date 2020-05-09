using Microsoft.Extensions.ObjectPool;
using MoneySaver.Activities.Services.Contracts;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneySaver.Activities.Services.Implementation
{
    public class RabbitMqManager : IRabbitMqManager
    {
        private readonly DefaultObjectPool<IModel> _objectPool;

        public RabbitMqManager(IPooledObjectPolicy<IModel> objectPolicy)
        {
            this._objectPool = new DefaultObjectPool<IModel>(objectPolicy, Environment.ProcessorCount * 2);
        }

        public void Publish<T>(T message, string exchangeName, string exchangeType, string routeKey)
            where T : class
        {
            if (message == null)
            {
                return;
            }

            var channel = _objectPool.Get();

            try
            {
                //channel.ExchangeDeclare(exchangeName, exchangeType, true, false, null);

                var sendBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.BasicPublish(exchangeName, routeKey, properties, sendBytes);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                _objectPool.Return(channel);
            }
        }
    }
}
