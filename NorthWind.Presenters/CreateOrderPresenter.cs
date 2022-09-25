using NorthWind.UseCasesPorts.CreateOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.Presenters
{
    public class CreateOrderPresenter : ICreateOrderOutputPort , IPresenter<string>
    {
        public string Content { get; private set; }
                
        public Task Handle(int orderId)
        {
            Content = $"Order ID: {orderId}";
            return Task.CompletedTask;
        }

    }
}
