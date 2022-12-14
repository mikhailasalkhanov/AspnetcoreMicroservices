using AutoMapper;
using Discount.GRPC.Entites;
using Discount.GRPC.Protos;
using Discount.GRPC.Repositories;
using Grpc.Core;
using static Discount.GRPC.Protos.DiscountProtoService;

namespace Discount.GRPC.Services;

public class DiscountService : DiscountProtoServiceBase
{
    private readonly IDiscountRepository _repository;
    private readonly ILogger<DiscountService> _logger;
    private readonly IMapper _mapper;

    public DiscountService(IDiscountRepository repository, ILogger<DiscountService> logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await _repository.GetDiscount(request.ProductName);
        if (coupon == null)
        {
            throw new RpcException(new Status
                (StatusCode.NotFound, $"Discount with ProductName={request.ProductName} is not found."));
        }
        _logger.LogInformation
            ("Discount is retrieved for ProductName : {ProductName}, Amount : {Amount}",
                coupon.ProductName, coupon.Amount);
        
        var couponModel = _mapper.Map<CouponModel>(coupon);
        return couponModel;
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = _mapper.Map<Coupon>(request.Coupon); 
        await _repository.CreateDiscount(coupon);
        _logger.LogInformation("Discount is created. ProductName : {ProductName}", coupon.ProductName);

        var couponModel = _mapper.Map<CouponModel>(coupon);
        return couponModel;
    }
    
    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = _mapper.Map<Coupon>(request.Coupon);
        await _repository.UpdateDiscount(coupon);
        _logger.LogInformation("Discount is updated. ProductName : {ProductName}", coupon.ProductName);

        var couponModel = _mapper.Map<CouponModel>(coupon);
        return couponModel;
    }
    
    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupon = new Coupon() { ProductName = request.ProductName };
        var deleted = await _repository.DeleteDiscount(coupon);
        _logger.LogInformation("Discount is deleted. ProductName : {ProductName}", coupon.ProductName);

        return new DeleteDiscountResponse { Success = deleted };
    }
}