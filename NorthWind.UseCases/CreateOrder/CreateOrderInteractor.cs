using FluentValidation;
using MediatR;
using NorthWind.Entities.Exceptions;
using NorthWind.Entities.Interfaces;
using NorthWind.Entities.POCOEntities;
using NorthWind.UseCases.Common.Validators;
using NorthWind.UseCasesDTOs.CreateOrder;
using NorthWind.UseCasesPorts.CreateOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NorthWind.UseCases.CreateOrder
{
    public class CreateOrderInteractor : ICreateOrderInputPort
    {
        readonly IOrderRepository OrderRepository;
        readonly IOrderDetailRepository OrderDetailRepository;
        readonly IUnitOfWork UnitOfWork;
        readonly ICreateOrderOutputPort OutputPort;
        readonly IEnumerable<IValidator<CreateOrderParams>> Validators;
        public CreateOrderInteractor(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository,
            IUnitOfWork unitOfWork, ICreateOrderOutputPort outputPort,
            IEnumerable<IValidator<CreateOrderParams>> validators) =>
            (OrderRepository, OrderDetailRepository, UnitOfWork, OutputPort, Validators) =
            (orderRepository, orderDetailRepository, unitOfWork, outputPort, validators);

        public async Task Handle(CreateOrderParams order)
        {
            await Validator<CreateOrderParams>.Validate(order, Validators);

            Order Order = new Order()
            {
                CustomerId = order.CustomerId,
                OrderDate = DateTime.Now,
                ShipAddress = order.ShipAddress,
                ShipCity = order.ShipCity,
                ShipCountry = order.ShipCountry,
                ShipPostalCode = order.ShipPostalCode,
                ShippingType = Entities.Enums.ShippingType.Road,
                DiscountType = Entities.Enums.DiscountType.Percentage,
                Discount = 10
            };
            OrderRepository.Create(Order);

            foreach (var item in order.OrderDetails)
            {
                OrderDetailRepository.Create(
                    new OrderDetail
                    {
                        Order = Order,
                        ProductId = item.ProductId,
                        UnitPrice = item.UnitPrice,
                        Quantity = item.Quantity,

                    });
            }
            try
            {
                await UnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new GeneralException("Error al crear la orden.", ex.Message);
            }

            await OutputPort.Handle(Order.Id);
        }

    }
}
