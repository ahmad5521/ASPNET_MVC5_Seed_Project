using Inspinia_MVC5_SeedProject.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Inspinia_MVC5_SeedProject.Controllers
{
    [Authorize]
    public class VacationRequestController : Controller
    {
        string cs = ConfigurationManager.ConnectionStrings["kamcConnectionString"].ConnectionString;

        // GET: VacationRequest
        public ActionResult Index()
        {
            return View();
        }

        
        /// <summary>
        /// VacationRequest/GetBalance
        /// </summary>
        /// <returns></returns>
        /// 

        [Route("VacationRequest/GetBalance/{GeneralNumber}")]
        [HttpGet]
        public async Task<JsonResult> GetBalance(string GeneralNumber)
        {
            VacationBalance vb = new VacationBalance();
            SqlConnection con = new SqlConnection(cs);

            await Task.Delay(7000);
            
            using (con)
            {
                try
                {
                    con.Open();

                    //read annual balance
                    SqlCommand com = new SqlCommand("Workflow_GetAnnualBalanceNewF", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@GENERALNO", GeneralNumber);
                    com.Parameters.AddWithValue("@dat","03/14/2018");
                    SqlDataReader rdr = com.ExecuteReader();
                    rdr.Read();
                    vb.Annual = Convert.ToInt32(rdr["TotalBalance"]);
                    rdr.Close();

                    //read other balance
                    SqlCommand com2 = new SqlCommand("Workflow_GetUserOvertimeHolidaysUrgentMng", con);
                    com2.CommandType = CommandType.StoredProcedure;
                    com2.Parameters.AddWithValue("@GENERALNO", "63012460");
                    com2.Parameters.AddWithValue("@YearUrgent", "1439");
                    SqlDataReader rdr2 = com2.ExecuteReader();
                    rdr2.Read();
                    vb.Overtime = Convert.ToInt32(rdr2["Overtime"]);
                    vb.Holyday = Convert.ToInt32(rdr2["Holyday"]);
                    vb.Urgent = Convert.ToInt32(rdr2["Urgent"]);
                    vb.Mng = Convert.ToInt32(rdr2["Mng"]);
                    vb.Priv = Convert.ToInt32(rdr2["Priv"]);
                    rdr2.Close();
                }
                catch (Exception err)
                {
                    con.Close();
                    return Json(err.Message, JsonRequestBehavior.AllowGet);
                }
                finally
                {
                    con.Close();
                }
            }
           
            

            con.Close();
            return Json(vb, JsonRequestBehavior.AllowGet);
        }
    }
}