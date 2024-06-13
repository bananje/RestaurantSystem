using LuckyFoodSystem.Shared.Domain.Features;

namespace LuckyFoodSystem.Shared.Domain.Bl.Exceptions;

public class BusinessRuleException : BusinessExceptionBase
{
    public BusinessRuleException(IBusinessRule businessRule) : base(businessRule.ErrorMessage)
    {
    }

    public BusinessRuleException(IAsyncBusinessRule bussinesRule) : base(bussinesRule.ErrorMessage)
    {
    }
}
