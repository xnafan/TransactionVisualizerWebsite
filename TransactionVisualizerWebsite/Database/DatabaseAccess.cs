using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using TransactionVisualizerWebsite.Models;

namespace TransactionVisualizerWebsite.Database
{
    public class DatabaseAccess
    {

        public bool RemoveProductIfInStock(StockUpdateParameters parameters)
        {
            SqlConnection? connection = null;
            SqlTransaction? transaction = null;

            try
            {
                connection = CreateConnection();
                connection.Open();
                transaction = connection.BeginTransaction(parameters.IsolationLevel);
                int productStock = GetProductStock(parameters.ProductId, connection, transaction);
                Thread.Sleep(parameters.PauseBeforeUpdateInSeconds * 1000);
                Debug.WriteLine(productStock);
                if (productStock < parameters.StockChangeAmount) { return false; }
                AlterStock(parameters.ProductId, -parameters.StockChangeAmount, connection, transaction);
                transaction.Commit();
                return true;
            }
            catch (Exception)
            {
                transaction?.Rollback();
                return false;
            }
            finally { transaction?.Dispose();
                connection?.Close();
                    }
        }

        public int GetProductStock(int productId, SqlConnection? connection = null, SqlTransaction? transaction = null)
        {
            //if we didn't receive a connection create a new one
            connection = connection ?? CreateConnection();
            //create a command
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT stock FROM Product WHERE Id=@productId";
            //set the transaction (won't give any problems if null)
            command.Transaction = transaction;
            //set the parameter for the SQL
            command.Parameters.AddWithValue("@productId", productId);
            //store whether we're working on an open or closed connection
            var initialConnectionState = connection.State;
            //open the connection if it was closed initially
            if (initialConnectionState == ConnectionState.Closed) { connection.Open(); }
            //get the stock amount
            var stock = (int)command.ExecuteScalar();
            //close the connection if it was closed initially
            if (initialConnectionState == ConnectionState.Closed) { connection.Close(); }
            return stock;
        }
        private bool AlterStock(int productId, int change, SqlConnection? connection = null, SqlTransaction? transaction = null)
        {
            try
            {
                //if we didn't receive a connection create a new one
                connection = connection ?? CreateConnection();
                //create a command
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE Product SET Stock = Stock + @change WHERE Id=@productId";
                //set the transaction (won't give any problems if null)
                if (transaction != null)
                {

                    command.Transaction = transaction;
                }
                //set the parameters for the SQL
                command.Parameters.AddWithValue("@change", change);
                command.Parameters.AddWithValue("@productId", productId);
                //store whether we're working on an open or closed connection
                var initialConnectionState = connection.State;
                //open the connection if it was closed initially
                if (initialConnectionState == ConnectionState.Closed) { connection.Open(); }
                //alter stock
                command.ExecuteNonQuery();
                //close the connection if it was closed initially
                if (initialConnectionState == ConnectionState.Closed) { connection.Close(); }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public void SetStock(int productId, int newStockAmount)
        {
            //if we didn't receive a connection create a new one
            var connection = CreateConnection();
            //create a command
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE Product SET Stock = @newStockAmount WHERE Id=@productId";
            //set the parameters for the SQL
            command.Parameters.AddWithValue("@newStockAmount", newStockAmount);
            command.Parameters.AddWithValue("@productId", productId);

            connection.Open();
            //alter stock
            command.ExecuteNonQuery();
            //close the connection if it was closed initially
            connection.Close();
        }
        private SqlConnection CreateConnection()
        {
            return new SqlConnection("Data Source=.; Initial Catalog=TransactionSample; Integrated Security=true;");
        }
    }
}
