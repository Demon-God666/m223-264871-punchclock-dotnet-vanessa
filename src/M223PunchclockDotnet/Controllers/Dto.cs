using System.ComponentModel.DataAnnotations;

namespace M223PunchclockDotnet.Dto
{
    public class EntryDto : IValidatableObject
    {
        [Required]
        public DateTime? CheckIn { get; set; }

        [Required]
        public DateTime? CheckOut { get; set; }

        public IEnumerable<ValidationResult> Validate(
            ValidationContext validationContext)
        {
            if (CheckIn.HasValue && CheckOut.HasValue &&
                CheckIn.Value >= CheckOut.Value)
            {
                yield return new ValidationResult(
                    "Check-In muss vor Check-Out liegen.",
                    new[] { nameof(CheckIn), nameof(CheckOut) }
                );
            }
        }
    }
}