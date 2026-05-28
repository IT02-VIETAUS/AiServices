using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiServices.Domain.Entities.OrderSchema;
using AiServices.Domain.Entities.WarehouseSchema;

namespace AiServices.Domain.Entities.DeliverySchema
{
    public class DeliveryOrderPO
    {
        public Guid DeliveryOrderId { get; set; }
        public Guid MerchandiseOrderId { get; set; }
        public bool IsActive { get; set; } = true;
        public DeliveryOrder DeliveryOrder { get; set; } = default!;
        public MerchandiseOrder MerchandiseOrder { get; set; } = default!;
    }
}
