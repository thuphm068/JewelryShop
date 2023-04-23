using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryShop.Domain.Shared.Const
{
    public enum OrderStatus
    {
        Paid,//đã thanh toán
        Pending,//chờ thanh toán
        Shipped,//đã gửi cho đơn vị vận chuyển
        Returned, //giao hàng thất bại -> trả về
        Completed, // đơn hàng hoàn tất
        Failed //đơn hàng thất bại (shop không đủ hàng, lí do khác)


    }
}
