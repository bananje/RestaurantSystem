using LuckyFoodSystem.Shared.Domain.Bl.Exceptions;
using LuckyFoodSystem.Shared.Domain.Models;
using System.Text.RegularExpressions;

namespace LuckyFoodSystem.Orders.Domain.Models.CustomerAggregate.ValueObjects;

public partial class CustomerEmail : ValueObject
{
    private readonly Regex EmailRegex = new Regex(
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public CustomerEmail(string Email)
    {
        if (string.IsNullOrWhiteSpace(Email))
        {
            throw new BusinessException("CustomerEmail адрес не может быть пустым");
        }

        if (!EmailRegex.IsMatch(Email))
        {
            throw new ArgumentException($"Введён некорректный адрес электронной почты {Email}");
        }

        Value = Email;
    }

    public string Value { get; private set; }
}
