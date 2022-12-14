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
        public Function()
        {
            DBEnv.SetUp();
        }

        public async Task<ResAccountJoin> FunctionHandler(ReqAccountJoin req, ILambdaContext context)
        {
            var res = new ResAccountJoin
            {
                ResponseType = ResponseType.Success
            };

            //using (var db = new DBConnector())
            {
                var db = new DBConnector();
                var query = new StringBuilder();
                query.Append("SELECT userid FROM users where userid = '")
                    .Append(req.userId).Append("';");

                using (var cursor = await db.ExecuteReaderAsync(query.ToString()))
                {
                    if (cursor.Read())
                    {
                        res.ResponseType = ResponseType.DuplicateName;
                        res.userId = cursor["userId"].ToString();

                        db.Dispose();
                        return res;
                    }
                }

                query.Clear();
                query.Append("INSERT INTO users (userid, password,mbti, win, loss, score) VALUES ('")
                    .Append(req.userId).Append("','").Append(req.password).Append("','").Append(req.mbti).Append("',0, 0, 1000);");

                await db.ExecuteNonQueryAsync(query.ToString());

                query.Clear();
                query.Append("INSERT INTO gameInfo (gameSessionId, teamName,userid, mbti,roundList) VALUES ('")
                     .Append(req.userId).Append("','").Append(req.userId).Append("','").Append(req.userId).Append("','").Append(req.mbti).Append("','');");

                await db.ExecuteNonQueryAsync(query.ToString());

                res.userId = db.LastInsertedId().ToString();

                db.Dispose();
            }
            return res;
        }
    }
}
