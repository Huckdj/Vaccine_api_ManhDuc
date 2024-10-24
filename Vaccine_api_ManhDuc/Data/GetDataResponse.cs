using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Vaccine_api_ManhDuc.Data
{
    public class DataRepository
    {
        private readonly string _connectionString;

        public DataRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Dictionary<string, object>>> GetDataResponse(string procedureName, Dictionary<string, object> parameters)
        {
            var resultList = new List<Dictionary<string, object>>();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(procedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    foreach (var parameter in parameters)
                    {
                        command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                    }

                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var row = new Dictionary<string, object>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                row[reader.GetName(i)] = reader.GetValue(i);
                            }
                            resultList.Add(row);
                        }
                    }
                }
            }

            return resultList;
        }
    }
}
