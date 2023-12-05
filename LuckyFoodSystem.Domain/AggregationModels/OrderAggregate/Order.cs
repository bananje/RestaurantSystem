using LuckyFoodSystem.Domain.Models;
using LuckyFoodSystem.AggregationModels.ProductAggregate.ValueObjects;
using LuckyFoodSystem.Domain.AggregationModels.OrderAggregate.ValueObjects;
using LuckyFoodSystem.Domain.AggregationModels.OrderAggregate.Enumerations;
using LuckyFoodSystem.AggregationModels.ProductAggregate;

namespace LuckyFoodSystem.Domain.AggregationModels.OrderAggregate
{
    public class Order : AggregateRoot<OrderId>
    {
        private readonly List<Product> _products = new();

        /// <summary>
        /// Порядковый номер заказа
        /// </summary>
        public Number Number { get; private set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public UserName UserName { get; private set; }

        /// <summary>
        /// Контактный телефон
        /// </summary>
        public PhoneNumber PhoneNumber { get; private set; }

        /// <summary>
        /// Адрес доставки (город, улица, дом, кв)
        /// </summary>
        public AdressId AdressId { get; private set; }

        /// <summary>
        /// ID пользователя заказчика
        /// null, если пользователь не авторизован
        /// </summary>
        public UserId? CustomerId { get; private set; }

        /// <summary>
        /// ID пользователя курьера
        /// </summary>
        public UserId CourierId { get; private set; } = null!;

        /// <summary>
        /// Общая сумма заказа
        /// </summary>
        public TotalPrice TotalPrice { get; private set; }

        /// <summary>
        /// Сумма скидки исходя из количества заказов и обшей стоимости
        /// Значение - 0, если юзер не авторизован
        /// </summary>
        public Discount Discount { get; private set; }

        /// <summary>
        /// enum со статусами заказа
        /// </summary>
        public OrderStatus OrderStatus { get; private set; }

        /// <summary>
        /// Комментарий заказчика к заказу
        /// </summary>
        public Comment? Comment { get; private set; }
        public DateTime OrderDate { get; private set; }

        /// <summary>
        /// Флаг, проверяющий авторизацию заказчика
        /// </summary>
        public bool IsAutorize { get; private set; }

        /// <summary>
        /// Флаг, проверяющий заверешенность заказа
        /// </summary>
        public bool IsCompleted { get; private set; }

        /// <summary>
        /// Товары находящиеся в заказе
        /// </summary>
        public IReadOnlyCollection<Product> Products => _products;

        public Order() { }

        // Первичная инициализация заказа
        private Order(OrderId orderId,
                     Number number,
                     UserName userName,
                     PhoneNumber phoneNumber,
                     Discount discount,
                     AdressId adressId,
                     UserId? customerId,
                     Comment? comment,
                     bool isAuthorize)
        {
            Discount = discount;
            Number = number;
            UserName = userName;
            PhoneNumber = phoneNumber;
            AdressId = adressId;
            CustomerId = customerId;
            TotalPrice = new TotalPrice(Products.Sum(u => u.Price.Value))
                ?? throw new Exception("Total price must be not null");
            Comment = comment;
            IsAutorize = isAuthorize;
            OrderStatus = OrderStatus.Issued;
        }

        // Передача заказа в доставку
        private Order(OrderId orderId,
                     Number number,
                     UserName userName,
                     PhoneNumber phoneNumber,
                     Discount discount,
                     AdressId adressId,
                     UserId? customerId,
                     Comment? comment,
                     bool isAuthorize,
                     UserId courierId) 
             : this(orderId, number, 
                    userName, phoneNumber,
                    discount,
                    adressId, customerId, 
                    comment, isAuthorize)
        {            
            OrderStatus = OrderStatus.Delivered;
            CourierId = courierId;
        }

        // Заверешение заказа (для сохранения в БД)
        private Order(OrderId orderId,
                     Number number,
                     UserName userName,
                     PhoneNumber phoneNumber,
                     Discount discount,
                     AdressId adressId,
                     UserId? customerId,
                     Comment? comment,
                     bool isAuthorize,
                     UserId courierId,
                     bool isCompleted)
             : this(orderId, number,
                  userName, phoneNumber,
                  discount,
                  adressId, customerId,
                  comment, isAuthorize, courierId)
        {           
            OrderStatus = OrderStatus.Delivered;
            IsCompleted = isCompleted;
        }

        // Создание заказа
        public static Order Create(Number number,
                                   UserName userName,
                                   PhoneNumber phoneNumber,
                                   Discount discount,
                                   AdressId adressId,
                                   UserId? customerId,
                                   Comment? comment,
                                   bool isAuthorize)

            => new(OrderId.CreateUnique(),
                   number, userName, phoneNumber, discount,
                   adressId, customerId, comment, isAuthorize);

        // Отправка в доставку
        public static Order Delivere(Number number,
                                     UserName userName,
                                     PhoneNumber phoneNumber,
                                     Discount discount,
                                     AdressId adressId,
                                     UserId? customerId,
                                     Comment? comment,
                                     bool isAuthorize,
                                     UserId courierId)

            => new(OrderId.CreateUnique(),
                   number, userName, phoneNumber, discount,
                   adressId, customerId, comment, 
                   isAuthorize, courierId);

        // Завершение заказа
        public static Order Complete(Number number,
                                     UserName userName,
                                     PhoneNumber phoneNumber,
                                     Discount discount,
                                     AdressId adressId,
                                     UserId? customerId,
                                     Comment? comment,
                                     bool isAuthorize,
                                     UserId courierId,
                                     bool isComplete)

            => new(OrderId.CreateUnique(),
                   number, userName, phoneNumber, discount,
                   adressId, customerId, comment, isAuthorize,
                   courierId, isComplete);
    }
}
