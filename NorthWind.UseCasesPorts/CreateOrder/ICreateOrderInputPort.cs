using NorthWind.UseCasesDTOs.CreateOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.UseCasesPorts.CreateOrder
{
    public interface ICreateOrderInputPort
    {
        Task Handle(CreateOrderParams order);

    }
}
