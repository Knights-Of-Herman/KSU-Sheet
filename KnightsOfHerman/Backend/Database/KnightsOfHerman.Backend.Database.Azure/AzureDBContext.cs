using Microsoft.EntityFrameworkCore;

namespace KnightsOfHerman.Backend.Database.Azure
{
    /// <summary>
    /// Context for the Azure Database
    /// </summary>
    public class AzureDBContext : DbContext
    {

        public AzureDBContext(DbContextOptions options)
        : base(options)
        {
        }

        /// <summary>
        /// A testing querriy that is outdated
        /// </summary>
        /// <returns></returns>
        public List<int> GetTestInts()
        {
            var testInts = new List<int>();

            // Open a connection to the database
            var conn = Database.GetDbConnection();
            try
            {
                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    // Set your command text to the raw SQL query
                    command.CommandText = "SELECT TestInt FROM TestTable";

                    using (var reader = command.ExecuteReader())
                    {
                        // Read the data and add it to the list
                        while (reader.Read())
                        {
                            testInts.Add(reader.GetInt32(0)); // Assumes TestInt is the first column
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                // Ensure the connection is closed
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return testInts;
        }
    }
}
