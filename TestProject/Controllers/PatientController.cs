using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestProject.Models;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing;

namespace TestProject.Controllers
{
    public class PatientController : Controller
    {
        // GET: Patient
        /// <summary>
        /// 根据姓名查询病人
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetPatients(string name){
            List<Patient> patients = new List<Patient>();
            SqlConnection con = null;
            try
            {
                con = new SqlConnection("Data Source=192.168.3.172 ;Initial Catalog=NQPeis_Fs;User ID=sa;Password=zheng");
                if (con.State != ConnectionState.Open) {
                    con.Open();
                }
                SqlCommand com = new SqlCommand($"select * from vExamineInfo where PersonName = '{name}' ", con);
                SqlDataReader res = com.ExecuteReader();
                while (res.Read())
                {
                    Patient patient = new Patient();
                    int PersonNO = int.Parse(res["PersonNO"].ToString());
                    string FeeSerialNo = res["FeeSerialNo"].ToString();
                    string PersonName = res["PersonName"].ToString();
                    int Age = int.Parse(res["Age"].ToString());
                    int SexID = int.Parse(res["SexID"].ToString());
                    string FeeItemName =res["FeeItemName"].ToString();
                    int FeeItemID = int.Parse(res["FeeItemID"].ToString());
                    int DepartID = int.Parse(res["DepartID"].ToString());
                    patient.PersonNO = PersonNO;
                    patient.FeeSerialNo = FeeSerialNo;
                    patient.PersonName = PersonName;
                    patient.Age = Age;
                    patient.SexID = SexID;
                    patient.FeeItemName = FeeItemName;
                    patient.FeeItemID = FeeItemID;
                    patient.DepartID = DepartID;
                    patients.Add(patient);
                }
                
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            finally {
                if (con != null && con.State == System.Data.ConnectionState.Open) {
                    con.Close();
                }
            }
            return new JsonResult { Data  = patients,JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public string UploadImg(string fileBas64,string name) {
            string base64 = fileBas64;

           var  base64Str = base64.Replace("data:image/png;base64,", "").Replace("data:image/jgp;base64,", "")
                .Replace("data:image/jpg;base64,", "").Replace("data:image/jpeg;base64,", ""); //将base64头部信息替换
            byte[] bytes = Convert.FromBase64String(base64Str);
            string filePath = Server.MapPath("~/Uploads/");
            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);
            var path = filePath + name;

            using (MemoryStream stream = new MemoryStream(bytes))
                {
                using (Bitmap bit = new Bitmap(stream)) {
                    bit.Save(path);
                    return "/Uploads/" + name;
                }
                }
        }
        //保存
        public void SavePatient(int PersonNO, string FeeSerialNo, string PersonName, int Age, int SexID, string FeeItemName, int FeeItemID, int DepartID, string Office_img, string Solution, string Doctor, string Check_time, string Check_img) {
            SqlConnection con = null;
           


            try
            {
                con = new SqlConnection("Data Source= localhost;Initial Catalog=HeQiTest;User ID=sa;Password=123");
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                } 
                string sql = "insert into diagnose values (" + PersonNO + ",'" + FeeSerialNo + "','" + PersonName + "'," + Age + "," + SexID + ",'" + FeeItemName + "'," + FeeItemID + "," + DepartID + ",'" + Office_img + "','" + Solution + "','" + Doctor + "','" + Check_time + "','" + Check_img + "')";
                SqlCommand com = new SqlCommand(sql,con);

                com.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                if (con != null && con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }



        [HttpGet]
        public JsonResult GetPatientsAllInfo(string name2)
        {
            List<Patient> patientsInfo = new List<Patient>();
            SqlConnection con = null;
            try
            {
                con = new SqlConnection("Data Source= localhost;Initial Catalog=HeQiTest;User ID=sa;Password=123");
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlCommand com = new SqlCommand($"select * from diagnose where PersonName = '{name2}' ", con);
                SqlDataReader res = com.ExecuteReader();
                while (res.Read())
                {
                    Patient patient = new Patient();
                    int PersonNO = int.Parse(res["PersonNO"].ToString());
                    string FeeSerialNo = res["FeeSerialNo"].ToString();
                    string PersonName = res["PersonName"].ToString();
                    int Age = int.Parse(res["Age"].ToString());
                    int SexID = int.Parse(res["SexID"].ToString());
                    string FeeItemName = res["FeeItemName"].ToString();
                    int FeeItemID = int.Parse(res["FeeItemID"].ToString());
                    int DepartID = int.Parse(res["DepartID"].ToString());
                    string Office_img = res["Office_img"].ToString();
                    string Solution = res["Solution"].ToString();
                    string Doctor = res["Doctor"].ToString();
                    DateTime Check_time = (DateTime)res["Check_time"] ;
                    string Check_img = res["Check_img"].ToString();
                    patient.Id = int.Parse(res["Id"].ToString());
                    patient.PersonNO = PersonNO;
                    patient.FeeSerialNo = FeeSerialNo;
                    patient.PersonName = PersonName;
                    patient.Age = Age;
                    patient.SexID = SexID;
                    patient.FeeItemName = FeeItemName;
                    patient.FeeItemID = FeeItemID;
                    patient.DepartID = DepartID;
                    patient.Office_img = Office_img;
                    patient.Solution = Solution;
                    patient.Doctor = Doctor;
                    patient.Check_time = Check_time;
                    patient.Check_img = Check_img;
                    patientsInfo.Add(patient);
                }

            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                if (con != null && con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return new JsonResult { Data = patientsInfo, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}