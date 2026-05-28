using AiServices.Domain.Entities.DeliverySchema;
using AiServices.Domain.Entities.OrderSchema;
using AiServices.Domain.Entities.WarehouseSchema;
using Microsoft.EntityFrameworkCore;

namespace AiServices.Application.Abstractions.Persistence.Dispatch;

public interface IDispatchReadDbContext
{
    DbSet<DeliveryOrder> DeliveryOrders { get; }
    DbSet<DeliveryOrderDetail> DeliveryOrderDetails { get; }
    DbSet<DeliveryOrderPO> DeliveryOrderPOs { get; }
    DbSet<Deliverer> Deliverers { get; }
    DbSet<DelivererInfor> DelivererInfors { get; }
    DbSet<MerchandiseOrder> MerchandiseOrders { get; }
    DbSet<MerchandiseOrderDetail> MerchandiseOrderDetails { get; }
    DbSet<WarehouseShelfStock> WarehouseShelfStocks { get; }
}
