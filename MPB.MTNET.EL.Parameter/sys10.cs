using System;
using System.ComponentModel.DataAnnotations;
namespace MPB.MTNET.EL.Parameter
{
    /*(sys10) 詳細敘述如下*/
    /// <summary>
    /// 
    /// </summary>
    /// <info>Author: Wei; Date: 2019/07/24 </info>
    /// <history>
    /// xx.  YYYY/MM/DD   Ver   Author      Comments
    /// ===  ==========  ====  ==========  ==========
    /// 01.  2019/07/24   1.00    Admin        Create
    /// </history>
    public class sys10
    {
        /// <summary></summary>
        [Required]
        [Display(Name = "")]
        public int? s10_seq { get; set; }
        /// <summary></summary>
        [StringLength(200)]
        [Display(Name = "")]
        public string s10_post_arg { get; set; }

    }
}
