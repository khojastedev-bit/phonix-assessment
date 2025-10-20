using CSharpFunctionalExtensions;
using MediatR;

namespace SubscriptionManagement.Application.Features.SubscriptionPlans.Queries;
public record GetAllActivePlansQuery : IRequest<Result<IEnumerable<SubscriptionPlanDto>>>;
