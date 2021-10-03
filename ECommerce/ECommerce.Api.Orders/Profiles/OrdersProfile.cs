using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.Profiles
{
    public class OrdersProfile: AutoMapper.Profile
    {
        public OrdersProfile()
        {
            CreateMap<DB.Order, Models.Order>();
                //.ForMember(d => d.OrderItems,
                 //  i => i.MapFrom(src => src.OrderItems));
            CreateMap<DB.OrderItem, Models.OrderItem>();
            //CreateMap<IEnumerable<DB.OrderItem>, IEnumerable<Models.OrderItem>>();
        }
    }
}
