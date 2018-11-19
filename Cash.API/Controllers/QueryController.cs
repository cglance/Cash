using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Cash.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueryController : ControllerBase
    {
        private const string ConnectionString = @"data source=.\sql2014; initial catalog = Cash; persist security info = True; user id=web; password=web;";

        [HttpPost("")]
        public async Task<IActionResult> RunQuery([FromBody] QueryModel query, CancellationToken cancellationToken)
        {
            var result = new List<List<string>>();

            using (var conn = new SqlConnection(ConnectionString))
            {
                await conn.OpenAsync(cancellationToken);
                using (SqlCommand command = conn.CreateCommand())
                {
                    command.CommandText = query.Query;
                    command.CommandType = CommandType.Text;
                    using (SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken))
                    {
                        int fieldCount = reader.FieldCount;

                        while (await reader.ReadAsync(cancellationToken))
                        {
                            var row = new List<string>();
                            result.Add(row);
                            for (int i = 0; i < fieldCount; i++)
                            {
                                row.Add(reader[i] == DBNull.Value ? null : reader[i].ToString());
                            }
                        }
                    }
                }
            }

            return Ok(new ResponseModel {Data = result});
        }
    }

    public class QueryModel
    {
        public string Query { get; set; }

        public string[] Parameters { get; set; }
    }

    public class ResponseModel
    {
        public List<List<string>> Data { get; set; }
    }
}