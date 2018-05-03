using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace customerIDConnection
{
    class DataClass
    {
        const string connstring = @"server=PL3\MTCDB;Database=AdventureWorks2012;Trusted_Connection=True;User ID=AdvWorks2012;Password=1234";
        SqlConnection sqlConnection;


        private bool dataconnect(string connectionstring = "")

        {

            bool returnvalue;


            if (connectionstring.Length == 0)
            {
                connectionstring = connstring;
            }



            try
            {
                // opening a connection to sql 
                sqlConnection = new SqlConnection(connectionstring);

                sqlConnection.Open();


                returnvalue = true;

            }
            catch (Exception ex)
            {
                returnvalue = false;
                throw ex;
            }

            finally
            {

                if (sqlConnection != null)
                {
                    sqlConnection.Close();
                }

            }

            return returnvalue;


        }
    }
}











