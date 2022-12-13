using AutoMapper;
using Discount.GRPC.Protos;

namespace Discount.GRPC.Mapper;

public class DiscountProfile : Profile
{
    public DiscountProfile()
    {
        CreateMap<CouponModel, CouponModel>().ReverseMap();
    }
}