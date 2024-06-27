using LuckyFoodSystem.Shared.Domain.Bl.Exceptions;
using System.Text.RegularExpressions;

namespace LuckyFoodSystem.Shared.Domain.Models.ValueObjects;

public partial class Email : ValueObject
{
    private readonly Regex EmailRegex = new Regex(
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public Email(string Email)
    {
        if (string.IsNullOrWhiteSpace(Email))
        {
            throw new BusinessException("Email адрес не может быть пустым");
        }

        if (!EmailRegex.IsMatch(Email))
        {
            throw new ArgumentException($"Введён некорректный адрес электронной почты {Email}");
        }

        Value = Email;
    }

    public string Value { get; private set; }
}
