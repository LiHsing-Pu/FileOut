using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MPB.MTNET.EL.Web.Helpers
{
    public class TaskListJSON
    {
        public TaskListJSON() { }
        public TaskJSON oTS;


        public class TaskJSON
        {
            public TaskJSON() { }

            [Display(Name = "sTaskID")]
            public string sTaskID { get; set; }
            [Display(Name = "sTaskSetID")]
            public string sTaskSetID { get; set; }
            [Display(Name = "Activity")]
            public string Activity { get; set; }

            [Display(Name = "sPosition")]
            public string sPosition { get; set; }

            [Display(Name = "FullUnitCode")]
            public string FullUnitCode { get; set; }
            [Display(Name = "EmployeeNumber")]
            public string EmployeeNumber { get; set; }

            [Display(Name = "TaskField_FormPagePath")]
            public string TaskField_FormPagePath { get; set; }
            [Display(Name = "FormPagePath")]
            public string FormPagePath { get; set; }
        }
    }    
}