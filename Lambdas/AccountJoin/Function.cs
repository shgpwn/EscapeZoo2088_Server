using System;
using System.Text;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using CommonProtocol;
using GameDB;

[assembly: LambdaSerializer(typeof(CustomSerializer.LambdaSerializer))]

namespace AccountJoin
{
    public class Function
    {
        public async Task<ResAccountJoin> FunctionHandler(ReqAccountJoin req, ILambdaContext context)
        {
            DBEnv.SetUp();
            var res = new ResAccountJoin
            {
                ResponseType = ResponseType.Success
            };

            using (var db = new DBConnector())
            {
                var query = new StringBuilder();
                query.Append("SELECT userId FROM users'")
                    .Append(req.userId).Append("';");

                using (var cursor = await db.ExecuteReaderAsync(query.ToString()))
                {
                    if (cursor.Read())
                    {
                        res.ResponseType = ResponseType.DuplicateName;
                        return res;
                    }
                }

                query.Clear();
                query.Append("INSERT INTO users (userid, password,mbti, win, loss, score) VALUES ('")
                    .Append(req.userId).Append("','").Append(req.password).Append("', '', 0, 0, 1000);");
                await db.ExecuteNonQueryAsync(query.ToString());
                res.userId = db.LastInsertedId().ToString();
            }
            return res;
        }
    }
}