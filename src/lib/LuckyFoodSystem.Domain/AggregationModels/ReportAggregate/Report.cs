using LuckyFoodSystem.AggregationModels.ImageAggregate;
using LuckyFoodSystem.AggregationModels.ProductAggregate.ValueObjects;
using LuckyFoodSystem.Domain.AggregationModels.ReportAggregate.ValueObjects;
using LuckyFoodSystem.Domain.Models;

namespace LuckyFoodSystem.Domain.AggregationModels.ReportAggregate
{
    /// <summary>
    /// Модель пользовательского отзыва
    /// </summary>
    public class Report : AggregateRoot<ReportId>
    {       
        private readonly List<Image> _images = new();

        /// <summary>
        /// ID пользователя оставившего отзыв
        /// </summary>
        public UserId UserId { get; private set; }

        /// <summary>
        /// Сообщение к отзыву
        /// </summary>
        public Message Message { get; private set; }

        /// <summary>
        /// Дата написания отзыва
        /// </summary>
        public DateTime WriteDate { get; private set; }

        /// <summary>
        /// Оценка к отзыву
        /// </summary>
        public Grade Grade { get; private set; }

        /// <summary>
        /// Приложенные к отзыву изображения
        /// </summary>
        public IReadOnlyCollection<Image> Images  => _images;

        public Report() { }
        private Report(ReportId reportId,
                       UserId userId,
                       Message message,
                       Grade grade,
                       DateTime writeDate)
        {
            UserId = userId;
            Message = message;
            Grade = grade;
            WriteDate = writeDate;
        }

    }
}
