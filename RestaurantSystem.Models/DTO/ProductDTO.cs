using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace RestaurantMenu.Models.Models;

public class ProductDTO
{
    [Key]
    [HiddenInput(DisplayValue = false)]
    public int ProductId { get; set; }

    [Required(ErrorMessage = "Не заполнено обязательное для ввода поле")]
    [Display(Name = "Наименование")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Название продукта должно быть от 3 - 50 символов")]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage = "Не заполнено обязательное для ввода поле")]
    [StringLength(300, MinimumLength = 15, ErrorMessage = "Описание продукта должно быть от 15 - 300 символов")]
    [Display(Name = "Описание")]
    public string Description { get; set; } = null!;

    [Required(ErrorMessage = "Не заполнено обязательное для ввода поле")]
    [Range(1, float.MaxValue, ErrorMessage = "Минимальное значение - '1'")]
    [Display(Name = "Цена")]
    public float Price { get; set; }

    [Required(ErrorMessage = "Не заполнено обязательное для ввода поле")]
    [Range(1, float.MaxValue, ErrorMessage = "Минимальное значение - '1'")]
    [Display(Name = "Вес")]
    public float Weight { get; set; }

    [Required(ErrorMessage = "Не заполнено обязательное для ввода поле")]
    [Display(Name = "Мера")]
    public string WeightValue { get; set; } = null!;

    [HiddenInput(DisplayValue = false)]
    [Display(Name = "Меню")]
    public int MenuId { get; set; }

    [HiddenInput(DisplayValue = false)]
    [Display(Name = "Категория")]
    [Required(ErrorMessage = "Не заполнено обязательное для ввода поле")]
    public int CategoryId { get; set; }

    [HiddenInput(DisplayValue = false)]
    [Display(Name = "Изображение")]
    public Guid? ImageId { get; set; }
}
