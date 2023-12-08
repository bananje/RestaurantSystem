using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace RestaurantMenu.Models.Models;

public class MenuDTO
{
    [Key]
    [HiddenInput(DisplayValue = false)]
    public int MenuId { get; set; }

    [HiddenInput(DisplayValue = false)]
    public int CategoryId { get; set; }

    [HiddenInput(DisplayValue = false)]
    public Guid ImageId { get; set; }

    [StringLength(50, MinimumLength = 3, ErrorMessage = "Название меню должно быть от 3 - 50 символов")]
    [Required(ErrorMessage = "Не заполнено обязательное для ввода поле")]
    [Display(Name = "Наименование")]
    public string Name { get; set; } = null!;
   
}
