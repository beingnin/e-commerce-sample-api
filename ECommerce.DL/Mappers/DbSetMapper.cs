using AutoMapper;
using ECommerce.DL.Entity.Dbo;
using ECommerce.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DL.Mappers
{
    internal static class DbSetMapper
    {
        private static Mapper _mapper;
        static DbSetMapper()
        {
            _mapper = ConstructMaps();
        }
        private static Mapper ConstructMaps()
        {
            return new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<OrderDetail, OrderDetailSet>();
                cfg.CreateMap<Order, OrderSet>()
                .ForMember(dest => dest.LineItems, act => act.MapFrom(src => src.LineItems));

                cfg.CreateMap<OrderSet, Order>()
                .ForMember(dest => dest.LineItems, act => act.MapFrom(src => src.LineItems));
                cfg.CreateMap<OrderDetailSet, OrderDetail>();
            }));
        }
        public static OrderSet ToOrderSet(this Order order)
        {
            return _mapper.Map<OrderSet>(order);
        }
        public static Order FromOrderSet(this OrderSet orderSet)
        {
            return _mapper.Map<Order>(orderSet);
        }

    }
}
