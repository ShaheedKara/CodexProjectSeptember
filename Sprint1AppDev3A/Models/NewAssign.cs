using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sprint1AppDev3A.Models
{
    public class NewAssign
    {
        [System.ComponentModel.DataAnnotations.Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AssignID { get; set; }

        public int ContID { get; set; }
        [ForeignKey("ContID")]
        public NewContainer ContainerID { get; set; }

        public string ContainerNumber { get; set; }
        public string ContainerSize { get; set; }
        public string Driverid { get; set; }
        public string Driver { get; set; }
        public string Truck { get; set; }
        public string Trailer { get; set; }
        public string Location { get; set; }
        public string Destination { get; set; }
        public string Status { get; set; }
        public string DeadLine { get; set; }
        public string PickUp { get; set; }
       
        public string AssignCode { get; set; }

        public string GenAssCode()
        {
           string s  = ContainerNumber.Substring(0, 4)+AssignID+Driver.Substring(0,3);
           return s;
        }


    }
}