using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;
using System.Data;
using System.Collections;
using System.Reflection; 

namespace SkyWebApplication.DAL
{
    public class SkyWebDataService
    {
        public static string ConnectionString = ConfigurationManager.AppSettings["SkyWebConnection"];
  
        #region 更新一个字段
        public static string SetFiledOneByOne(string table, string strwhere, string columnname, string columnvalue)
        {

            string msg = "更新字段成功";

            SqlParameter[] arParames = new SqlParameter[4];
            arParames[0] = new SqlParameter("@table ", SqlDbType.VarChar, 200);
            arParames[0].Value = table;

            arParames[1] = new SqlParameter("@Where ", SqlDbType.VarChar, 8000);
            arParames[1].Value = strwhere;

            arParames[2] = new SqlParameter("@columnname ", SqlDbType.VarChar, 200);
            arParames[2].Value = columnname;

            arParames[3] = new SqlParameter("@columnvalue ", SqlDbType.VarChar, 200);
            arParames[3].Value = columnvalue;
            SqlConnection myconn = null;
            try
            {
                myconn = new SqlConnection(SkyWebDataService.ConnectionString);
                SqlDataReader dr = SqlHelper.ExecuteReader(myconn, CommandType.StoredProcedure, "setFiledOneByOne", arParames);
            }
            catch (SqlException ex)
            {
                msg = "Connection error Or  Update One Field error:：" + ex.Message;
                throw ex;
            }
            finally
            {

                myconn.Close();
                myconn.Dispose();
            }
            return msg;

        }
        #endregion

        #region 获取一个对象

        public static DataTable GetOneByWhere(string table, string strwhere)
        {

            SqlParameter[] arParames = new SqlParameter[2];
            arParames[0] = new SqlParameter("@table ", SqlDbType.VarChar, 200);
            arParames[0].Value = table;
            arParames[1] = new SqlParameter("@Where ", SqlDbType.VarChar, 8000);
            arParames[1].Value = strwhere;
            SqlConnection myconn = null;
            try
            {
                myconn = new SqlConnection(SkyWebDataService.ConnectionString);
                DataTable dt = null;

                DataSet ds = SqlHelper.ExecuteDataset(myconn, CommandType.StoredProcedure, "getOneByWhere", arParames);
                dt = ds.Tables[0];


                return dt;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {

                myconn.Close();
                myconn.Dispose();
            }


        }
        #endregion

        #region 获取多个对象集合

        public static DataTable GetSomeByWhere(string table, string strwhere)
        {


            SqlParameter[] arParames = new SqlParameter[2];
            arParames[0] = new SqlParameter("@table ", SqlDbType.VarChar, 200);
            arParames[0].Value = table;

            arParames[1] = new SqlParameter("@Where ", SqlDbType.VarChar, 8000);
            arParames[1].Value = strwhere;
            SqlConnection myconn = null;
            try
            {
                myconn = new SqlConnection(SkyWebDataService.ConnectionString);
                DataTable dt = null;

                DataSet ds = SqlHelper.ExecuteDataset(myconn, CommandType.StoredProcedure, "getSomeByWhere", arParames);
                dt = ds.Tables[0];


                return dt;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {

                myconn.Close();
                myconn.Dispose();
            }


        }
        #endregion

        #region 获取一些特殊值
        public static int GetSomeValueByWhere(string table, string strwhere, string thevalue)
        {

            int count = 0;
            SqlParameter[] arParames = new SqlParameter[3];
            arParames[0] = new SqlParameter("@table ", SqlDbType.VarChar, 200);
            arParames[0].Value = table;

            arParames[1] = new SqlParameter("@Where ", SqlDbType.VarChar, 8000);
            arParames[1].Value = strwhere;

            arParames[2] = new SqlParameter("@thevalue ", SqlDbType.VarChar, 200);
            arParames[2].Value = thevalue;

            SqlConnection myconn = null;
            try
            {
                myconn = new SqlConnection(SkyWebDataService.ConnectionString);
                IDataReader dr = SqlHelper.ExecuteReader(myconn, CommandType.StoredProcedure, "getSomeValueByWhere", arParames);


                if (dr.Read())
                {
                    string countvalue = dr.GetValue(0).ToString();
                    if (!string.IsNullOrEmpty(countvalue))
                    {
                        count = int.Parse(countvalue);
                    }

                }
                return count;
            }
            catch (SqlException ex)
            {

                throw ex;
            }
            finally
            {

                myconn.Close();
                myconn.Dispose();
            }


        }
        #endregion
    }
}