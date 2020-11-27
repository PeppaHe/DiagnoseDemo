using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestProject.Models
{
    /// <summary>
    /// 完整的病人类
    /// </summary>
    public class Patient
    {
        public int Id { get; set; }
        public int PersonNO { get; set; }
        public string FeeSerialNo { get; set; }
        public string PersonName { get; set; }
        public int Age { get; set; }
        public int SexID { get; set; }
        public string FeeItemName { get; set; }
        public int FeeItemID { get; set; }
        public int DepartID { get; set; }
        public string Office_img { get; set; }
        public string Solution { get; set; }
        public string Doctor { get; set; }
        public DateTime Check_time { get; set; }
        public string Check_img { get; set; }
    }
}