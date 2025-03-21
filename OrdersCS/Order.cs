using System;
using System.Collections.Generic;
using System.Text;
using TradingEngineServer.Orders;

namespace TradingEngineServer.Orders
{
    public class Order : IOrderCore
    {
        public Order(IOrderCore orderCore, bool isBuySide, long price, uint quantity)
        {
            //PROPERTIES//
            Price = price;
            IsBuySide = isBuySide;
            InitialQuantity = quantity;
            CurrentQuantity = quantity;


            //fields//
            _orderCore = orderCore;
                  
        }

        public Order(ModifyOrder modifyOrder) : 
            this(modifyOrder, modifyOrder.IsBuySide, modifyOrder.Price, modifyOrder.Quantity)
        {

            
        }
        //PROPERTIES//
        public long Price { get; private set; }
        public uint InitialQuantity { get; private set; }
        public uint CurrentQuantity { get; private set; }

        public bool IsBuySide { get; private set; }

        public long OrderId => _orderCore.OrderId; //same as {get => _oderCore.OrderId; private set; }//

        public string UserName => _orderCore.UserName;

        public int SecurityId => _orderCore.SecurityId;

        //METHODS//
        public void IncreaseQuantity (uint quantityDelta)
        {
            CurrentQuantity += quantityDelta;
        }

        public void DecreaseQuantity (uint quantityDelta)
        {
            if (quantityDelta > CurrentQuantity)
                throw new InvalidOperationException(($"QuantityDelta > Current Quantity for OrderId = {OrderId}"));
            CurrentQuantity -= quantityDelta;
        }


        //FIELDS//
        private readonly IOrderCore _orderCore;
    }
}
