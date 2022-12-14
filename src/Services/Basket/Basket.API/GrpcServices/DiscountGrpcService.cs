using System.Threading.Tasks;
using Discount.GRPC.Protos;
using static Discount.GRPC.Protos.DiscountProtoService;

namespace Basket.API.GrpcServices;

public class DiscountGrpcService
{
    private readonly DiscountProtoServiceClient _discountProtoService;

    public DiscountGrpcService(DiscountProtoServiceClient discountProtoService)
    {
        _discountProtoService = discountProtoService;
    }

    public async Task<CouponModel> GetDiscount(string productName)
    {
        var discountRequest = new GetDiscountRequest { ProductName = productName };
        return await _discountProtoService.GetDiscountAsync(discountRequest);
    }
}