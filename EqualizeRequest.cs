using System.ComponentModel.DataAnnotations;

namespace EvaluationAPI
{
    public class EqualizeRequest
    {
        [Required]
        [MinLength(1)]
        public int[] teamSizes { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int changes { get; set; }
    }
}
