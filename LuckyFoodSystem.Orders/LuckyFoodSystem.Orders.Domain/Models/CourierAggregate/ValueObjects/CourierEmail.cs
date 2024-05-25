using LuckyFoodSystem.Shared.Domain.Bl.Exceptions;
using LuckyFoodSystem.Shared.Domain.Models;
using System.Text.RegularExpressions;

namespace LuckyFoodSystem.Orders.Domain.Models.CustomerAggregate.ValueObjects;

public partial class CourierEmail : ValueObject
{
    private readonly Regex EmailRegex = new Regex(
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public CourierEmail(string emailAddress)
    {
        if (string.IsNullOrWhiteSpace(emailAddress))
        {
            throw new BusinessException("Email адрес не может быть пустым");
        }

        if (!EmailRegex.IsMatch(emailAddress))
        {
            throw new ArgumentException($"Введён некорректный адрес электронной почты {emailAddress}");
        }

        Value = emailAddress;
    }

    public string Value { get; private set; }
}
