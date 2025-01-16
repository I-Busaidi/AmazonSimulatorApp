﻿using AmazonSimulatorApp.Data;

namespace AmazonSimulatorApp.Services
{
    public interface IOrderDetailsService
    {
        void AddOrderProducts(List<OrderDetail> orderDetails);
        List<OrderDetail> GetOrderProducts(int orderId);
    }
}