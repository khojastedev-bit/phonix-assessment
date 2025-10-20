using CSharpFunctionalExtensions;
using MediatR;
using SubscriptionManagement.Core.Repositories;

namespace SubscriptionManagement.Application.Features.SubscriptionPlans.Queries;
public class GetAllActivePlansQueryHandler : IRequestHandler<GetAllActivePlansQuery,
    Result<IEnumerable<SubscriptionPlanDto>>>
{
    private readonly ISubscriptionPlanRepository _planRepository;

    public GetAllActivePlansQueryHandler(ISubscriptionPlanRepository planRepository)
    {
        _planRepository = planRepository;
    }

    public async Task<Result<IEnumerable<SubscriptionPlanDto>>> Handle(
        GetAllActivePlansQuery request, CancellationToken cancellationToken)
    {
        var plans = await _planRepository.GetAllActiveAsync();
        var dtos = plans.Select(plan => new SubscriptionPlanDto(
            plan.Id,
            plan.Name,
            plan.Description,
            plan.Price,
            plan.DurationInDays,
            plan.MaxUsers,
            plan.Features
        ));
        return Result.Success<IEnumerable<SubscriptionPlanDto>>(dtos);
    }
}