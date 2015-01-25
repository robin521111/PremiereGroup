using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;

namespace LeduInfo.Models
{
    public class BulkManager 
    {
        public static void BulkFromAdventure()
        {
            SqlConnectionStringBuilder Builder=new SqlConnectionStringBuilder();
            Builder.InitialCatalog = "AdventureWorks2008";
            Builder.DataSource=@".\sqlexpress";
            Builder.IntegratedSecurity = true;
            using(SqlConnection conn=new SqlConnection (Builder.ConnectionString))
	        {
                SqlCommand Comm = new SqlCommand();
                Comm.Connection = conn;
                conn.Open();
                Comm.CommandText = "SELECT TOP 1000 [FirstName],[rowguid]FROM [AdventureWorks2008].[Person].[Person]";
                SqlDataReader Reader= Comm.ExecuteReader();
                string Constr = string.Empty;

                SqlBulkCopy bulk = new SqlBulkCopy(ConfigurationManager.ConnectionStrings["leduInfo"].ConnectionString);
                bulk.DestinationTableName = "dbo.LoginModels";
                bulk.ColumnMappings.Add("FirstName", "UserName");
                bulk.ColumnMappings.Add("rowguid", "rowguid");
                bulk.WriteToServer(Reader);
	        }

        }
    
    }
}
