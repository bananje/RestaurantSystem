using LuckyFoodSystem.Shared.Domain.Models;
using System.Text.RegularExpressions;

namespace LuckyFoodSystem.Orders.Domain.Models.CustomerAggregate.ValueObjects;

public partial class CustomerPhone : ValueObject
{
    private static readonly Regex PhoneRegex = new Regex(
            @"^\+?[1-9]\d{1,14}$",
            RegexOptions.Compiled);

    public CustomerPhone(string number)
    {
        if (string.IsNullOrWhiteSpace(number))
        {
            throw new ArgumentException("CustomerPhone number cannot be empty.", nameof(number));
        }

        if (!PhoneRegex.IsMatch(number))
        {
            throw new ArgumentException("Invalid phone number format.", nameof(number));
        }

        Value = number;
    }

    public string Value { get; private set; }
}
