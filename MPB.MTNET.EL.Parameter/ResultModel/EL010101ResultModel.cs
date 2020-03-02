using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPB.MTNET.EL.Parameter.ResultModel
{
    public class EL010101ResultModel
    {

        [Required]
        [StringLength(50)]
        [Display(Name = "locationName")]
        public string locationName { get; set; }

        [Required]
        [StringLength(5)]
        [Display(Name = "elementName")]
        public string elementName { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "parameterName")]
        public string parameterName { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "parameterValue")]
        public string parameterValue { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "parameterUnit")]
        public string parameterUnit { get; set; }

        [Display(Name = "startTime")]
        public DateTime? startTime { get; set; }

        [Display(Name = "endTime")]
        public DateTime? endTime { get; set; }

    }
}
