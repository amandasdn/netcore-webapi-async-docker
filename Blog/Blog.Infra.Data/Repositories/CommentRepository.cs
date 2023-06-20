using Blog.Domain.Entities;
using Blog.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Blog.Infra.Data.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ILogger<CommentRepository> _logger;
        private readonly string _connectionString;

        public CommentRepository(ILogger<CommentRepository>  logger, IConfiguration configuration)
        {
            _logger = logger;
            _connectionString = configuration.GetConnectionString("db_Blog") ?? throw new NullReferenceException("Connection 'db_Blog' not found.");
        }

        public async Task<IEnumerable<CommentEntity>> Get(int page_size, int page_number, string? author)
        {
            _logger.LogInformation("[CommentRepository.Get] Page size: {0}, Page number: {1}, Author: {2}", page_size, page_number, author ?? "-");

            IEnumerable<CommentEntity> comments;

            var prm = new DynamicParameters();
            prm.Add("@author", author);
            prm.Add("@page_size", page_size);
            prm.Add("@page_number", page_number);

            using (var con = new SqlConnection(_connectionString))
            {
                var result = await con.QueryAsync<CommentEntity>("spr_GetComments",
                    prm, commandType: CommandType.StoredProcedure);

                comments = result;
            };

            _logger.LogInformation("[CommentRepository.Get] Comment count: {0}", comments.Count());

            return comments;
        }

        public void Insert(CommentEntity comment)
        {
            _logger.LogInformation("[CommentRepository.Insert] Data: {0}", JsonSerializer.Serialize(comment));

            var currentDateTime = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("America/Sao_Paulo"));

            var prm = new DynamicParameters();
            prm.Add("@id_comment", comment.IdComment);
            prm.Add("@content", comment.Content);
            prm.Add("@author", comment.Author);
            prm.Add("@createdAt", comment.CreatedAt);
            prm.Add("@storedAt", currentDateTime);

            using (var con = new SqlConnection(_connectionString))
            {
                con.Execute("spr_InsertComment", prm, commandType: CommandType.StoredProcedure);
            };
        }
    }
}
