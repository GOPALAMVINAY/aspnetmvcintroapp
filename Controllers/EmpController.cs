using aspnetmvcintroapp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Data;
using System.Data.Common;
namespace aspnetmvcintroapp.Controllers
{
    public class EmpController : Controller
    {
        
        [HttpGet]
        public ActionResult viewempdata(string search,int filter=0)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DbConnectionstring"].ConnectionString;
             SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd;
            if (filter == 0)
            {
                 cmd = new SqlCommand("sp_getempdata", con);
                 cmd.CommandType = CommandType.StoredProcedure;
            }
            else 
            {
                 cmd = new SqlCommand("sp_searchresults", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@search", search);
            }
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            DataTable dt = ds.Tables[0];

            List<employee> emp = new List<employee>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                employee e = new employee();
                e.empid = Convert.ToInt32(dt.Rows[i]["empid"]);
                e.ename = dt.Rows[i]["ename"].ToString();
                e.etype = dt.Rows[i]["etype"].ToString();
                e.dj = dt.Rows[i]["dj"].ToString();
                e.mbnumber = dt.Rows[i]["mbnumber"].ToString();
                emp.Add(e);
            }

            ViewBag.empList = emp;
            return View();
            //if (filter == 0)
            //{
            //    string connectionString = ConfigurationManager.ConnectionStrings["DbConnectionstring"].ConnectionString;
            //    SqlConnection con = new SqlConnection(connectionString);
            //    SqlCommand cmd = new SqlCommand("sp_getempdata", con);
            //    cmd.CommandType = CommandType.StoredProcedure;

            //    DataSet ds = new DataSet();
            //    SqlDataAdapter da = new SqlDataAdapter(cmd);
            //    da.Fill(ds);
            //    DataTable dt = ds.Tables[0];

            //    List<employee> emp = new List<employee>();
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        employee e = new employee();
            //        e.empid = Convert.ToInt32(dt.Rows[i]["empid"]);
            //        e.ename = dt.Rows[i]["ename"].ToString();
            //        e.etype = dt.Rows[i]["etype"].ToString();
            //        e.dj = dt.Rows[i]["dj"].ToString();
            //        e.mbnumber = dt.Rows[i]["mbnumber"].ToString();
            //        emp.Add(e);
            //    }

            //    ViewBag.empList = emp;
               
            //}
            //else
            //{
            //    string connectionString = ConfigurationManager.ConnectionStrings["DbConnectionstring"].ConnectionString;
            //    SqlConnection con = new SqlConnection(connectionString);
            //    SqlCommand cmd = new SqlCommand("sp_searchresults", con);
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.AddWithValue("@search", search);
            //    DataSet ds = new DataSet();
            //    SqlDataAdapter da = new SqlDataAdapter(cmd);
            //    da.Fill(ds);
            //    DataTable dt = ds.Tables[0];

            //    List<employee> emp = new List<employee>();
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        employee e = new employee();
            //        e.empid = Convert.ToInt32(dt.Rows[i]["empid"]);
            //        e.ename = dt.Rows[i]["ename"].ToString();
            //        e.etype = dt.Rows[i]["etype"].ToString();
            //        e.dj = dt.Rows[i]["dj"].ToString();
            //        e.mbnumber = dt.Rows[i]["mbnumber"].ToString();
            //        emp.Add(e);
            //    }

            //    ViewBag.empList = emp;
            //}
            //return View();
        }
        // GET: Emp
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Insert(employee emp)
        {
            // Retrieve the connection string by name
            string connectionString = ConfigurationManager.ConnectionStrings["DbConnectionstring"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            //string query = "INSERT INTO emptb (ename, etype, dj, mbnumber) VALUES (@ename, @etype, @dj, @mbnumber)";
            SqlCommand cmd = new SqlCommand("sp_insertemp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ename", emp.ename);
            cmd.Parameters.AddWithValue("@etype", emp.etype);
            cmd.Parameters.AddWithValue("@dj", emp.dj);
            cmd.Parameters.AddWithValue("@mbnumber", emp.mbnumber);
            cmd.ExecuteNonQuery();

            return Json("save");
        }

        [HttpPost]
        public JsonResult Update(employee emp)
        {
            // Retrieve the connection string by name
            string connectionString = ConfigurationManager.ConnectionStrings["DbConnectionstring"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            //string query = "INSERT INTO emptb (ename, etype, dj, mbnumber) VALUES (@ename, @etype, @dj, @mbnumber)";
            SqlCommand cmd = new SqlCommand("sp_updateempdetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@empid", emp.empid);
            cmd.Parameters.AddWithValue("@ename", emp.ename);
            cmd.Parameters.AddWithValue("@etype", emp.etype);
            cmd.Parameters.AddWithValue("@dj", emp.dj);
            cmd.Parameters.AddWithValue("@mbnumber", emp.mbnumber);
            cmd.ExecuteNonQuery();

            return Json("abc");
        }
        [HttpPost]
        public JsonResult delete(employee emp)
        {
            // Retrieve the connection string by name
            string connectionString = ConfigurationManager.ConnectionStrings["DbConnectionstring"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            //string query = "INSERT INTO emptb (ename, etype, dj, mbnumber) VALUES (@ename, @etype, @dj, @mbnumber)";
            SqlCommand cmd = new SqlCommand("sp_deleteemp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@empid", emp.empid);
            cmd.ExecuteNonQuery();

            return Json("dc");
        }
    }
}