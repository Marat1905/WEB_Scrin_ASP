using System;
using System.Collections.Generic;

namespace WEB_Scrin_ASP
{
    /// <summary>
    /// DTO для получения общего статуса программы «Ноль травм».
    /// </summary>
    public class ZeroInjuryStatusDto
    {
        /// <summary>
        /// Текущая активная программа (если есть).
        /// </summary>
        public ZeroInjuryProgramDto CurrentProgram { get; set; }

        /// <summary>
        /// Последняя завершенная программа (успешно или с прерыванием).
        /// </summary>
        public ZeroInjuryProgramDto LastFinalizedProgram { get; set; }

        /// <summary>
        /// Дата последней значимой травмы (П1/П2).
        /// </summary>
        public DateTime? LastSignificantInjuryDate { get; set; }

        /// <summary>
        /// Минимальная допустимая дата начала новой программы.
        /// </summary>
        public DateTime? NextAllowedStartDate { get; set; }

        /// <summary>
        /// Флаг: можно ли начать новую программу.
        /// </summary>
        public bool CanStartNew { get; set; }
    }

    /// <summary>
    /// DTO программы «Ноль травм».
    /// </summary>
    public class ZeroInjuryProgramDto
    {
        public string Id { get; set; }
        public DateTime StartDate { get; set; }
        public string TriggeringInjuryId { get; set; }
        public string Description { get; set; }
        public int TargetDays { get; set; }
        public int DaysPassed { get; set; }
        public int DaysRemaining { get; set; }

        /// <summary>
        /// Статус: "Active", "Completed", "Interrupted".
        /// </summary>
        public string Status { get; set; }

        public bool IsCompleted { get; set; }
        public bool IsSuccessfullyCompleted { get; set; }
        public string InterruptingInjuryId { get; set; }
        public DateTime? CompletedAt { get; set; }
        public string SetByUserId { get; set; }
        public string SetByUserName { get; set; }
        public List<ZeroInjuryDocumentDto> Documents { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    /// <summary>
    /// DTO документа, прикрепленного к программе.
    /// </summary>
    public class ZeroInjuryDocumentDto
    {
        public string Id { get; set; }
        public string ProgramId { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public long Size { get; set; }
        public string Description { get; set; }
        public string DocumentType { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}