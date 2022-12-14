using AutoMapper;
using Discount.GRPC.Entites;
using Discount.GRPC.Protos;

namespace Discount.GRPC.Mapper;

public class DiscountProfile : Profile
{
    public DiscountProfile()
    {
        CreateMap<CouponModel, Coupon>().ReverseMap();
    }
}