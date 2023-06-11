using Dapper;
using NewProj.Models;
using System.Data;
using System.Data.SqlClient;

namespace NewProj.Interface
{
    public class Name : IName
    {

        private readonly IConfiguration _configuration;
        public string ConnString { get; }

        public IDbConnection Connection { get
            { return new SqlConnection(ConnString); }
        }

        public Name(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnString = _configuration.GetConnectionString("ConnString");
        }

        void IName.DeleteProductByID(int Id)
        {
            try
            {
                using(IDbConnection conn = Connection)
                {
                    conn.Open(); 
                    conn.Query<People>("DeleteProductByID", new {id = Id} ,commandType: CommandType.StoredProcedure);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }



        List<People> IName.GetProductList()
        {
            List<People> data = new();
            try
            {
                using (IDbConnection conn = Connection)
                {
                    conn.Open();
                    data = conn.Query<People>("GetProductList", commandType: CommandType.StoredProcedure).ToList();
                    conn.Close();
                    return data;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return data;
            }

        }

        void IName.EditUpdateProduct(People people)
        {
            try
            {
                using(IDbConnection conn = Connection)
                {
                    conn.Open();
                    var sql = conn.Query<People>("EditUpdateProduct", new { id = people.ProductId,name = people.ProductName},commandType: CommandType.StoredProcedure);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }
    }
}