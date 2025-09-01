using School.AdminService.Data;
using System.Data.Common;
using System.Data;
using Microsoft.EntityFrameworkCore;
using School.AdminService.Repository.Interfaces;
using School.AdminService.Data.Entities;
using System.Linq.Expressions;

namespace School.AdminService.Repository
{
    public class RawSqlRepository: IRawSqlRepository
    {
        private readonly AppDbContext _db;
        public RawSqlRepository(AppDbContext db)
        {
            this._db = db;
        }

        private List<T> RawSqlQuery<T>(string query, Func<DbDataReader, T> map)
        {
            //https://entityframeworkcore.com/knowledge-base/35631903/raw-sql-query-without-dbset---entity-framework-core
            using (var command = _db.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = CommandType.Text;

                _db.Database.OpenConnection();

                using (var result = command.ExecuteReader())
                {
                    var entities = new List<T>();

                    while (result.Read())
                    {
                        entities.Add(map(result));
                    }

                    return entities;
                }
            }
        }

        private async Task<List<T>> RawSqlQueryAsync<T>(string query, Func<DbDataReader, T> map)
        {
            //https://entityframeworkcore.com/knowledge-base/35631903/raw-sql-query-without-dbset---entity-framework-core
            using (var command = _db.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = CommandType.Text;

                _db.Database.OpenConnection();

                using (var result = await command.ExecuteReaderAsync())
                {
                    var entities = new List<T>();

                    while (result.Read())
                    {
                        entities.Add(map(result));
                    }

                    return entities;
                }
            }
        }

        public void ExecuteSqlRaw(string sql, params object[] param)
        {
            try
            {
                _db.Database.ExecuteSqlRaw(sql, param);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async void ExecuteSqlRawAsync(string sql, params object[] param)
        {
            try
            {
                await _db.Database.ExecuteSqlRawAsync(sql, param);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IEnumerable<RawSqlColumns>> Vw_StudentInfo()
        {
            var rawSql = string.Format(@"select AcademicYear,ClassID,ISNULL(GuardianMobile,'')GuardianMobile,ISNULL(GuardianName,'')GuardianName,ISNULL(GuardianRelation,'')GuardianRelation,ISNULL(Status,'')Status,StudentId, DateOfBirth
              ,ISNULL(Email,'') Email,ISNULL(FatherMobile,'')as FatherMobile,ISNULL(FatherName,'') as FatherName,ISNULL(FatherOccupation,'') as FatherOccupation,ISNULL(Gender,'') as Gender,ISNULL(ImageUrl,'') as ImageUrl,ISNULL(Mobile,'') as Mobile,ISNULL(MotherMobile,'') as MotherMobile,ISNULL(MotherName,'') as MotherName,ISNULL(MotherOccupation,'') as MotherOccupation
                ,ISNULL(Name,'') as Name,PersonnelCode,
                ISNULL(Religion,'') as Religion
                ,PermanentAddressId,ISNULL(PermanentAddressType,'') as PermanentAddressType,ISNULL(PermanentStreetDetails,'')as PermanentStreetDetails,PermanentThanasId
                ,PresentAddressId,ISNULL(PresentAddressType,'') as PresentAddressType,ISNULL(PresentStreetDetails,'') as PresentStreetDetails,PresentThanasId,ClassName
                ,PersonalnfoId
                ,ISNULL(PresentDistrictId,0)PresentDistrictId
				,ISNULL(PermanentDistrictId,0)PermanentDistrictId
               from Vw_StudentInfo ");
            var sql = await RawSqlQueryAsync(rawSql, x => new RawSqlColumns
            {
                AcademicYear = (int?)x[0],
                ClassID = (int?)x[1],
                GuardianMobile = (string)x[2],
                GuardianName = (string)x[3],
                GuardianRelation = (string)x[4],
                Status = (string)x[5],
                StudentId = (Int64?)x[6],

                DateOfBirth = (DateTime?)x[7],
                Email = (string)x[8],
                FatherMobile = (string)x[9],
                FatherName = (string)x[10],
                FatherOccupation = (string)x[11],
                Gender = (string)x[12],
                ImageUrl = (string)x[13],
                Mobile = (string)x[14],
                MotherMobile = (string)x[15],
                MotherName = (string)x[16],
                MotherOccupation = (string)x[17],
                Name = (string)x[18],
                PersonnelCode = (string)x[19],
                Religion = (string)x[20],
                PermanentAddressId = (Int64?)x[21],
                PermanentAddressType = (string)x[22],
                PermanentStreetDetails = (string)x[23],
                PermanentThanasId = (int?)x[24],
                PresentAddressId = (Int64?)x[25],
                PresentAddressType = (string)x[26],
                PresentStreetDetails = (string)x[27],
                PresentThanasId = (int?)x[28],
                ClassName = (string)x[29],
                PersonalnfoId = (Int64)x[30],
                PresentDistrictId = (int)x[31],
                PermanentDistrictId = (int)x[32],
            });

            return sql;
        }


        public async Task<IEnumerable<RawSqlColumns>> Get_RecentFeesByClassId(int classId = 0)
        {
            var sql =await RawSqlQueryAsync(string.Format("exec Get_RecentFeesByClassId {0}", classId), x => new RawSqlColumns
            {
                ClassesId = (int)x[0],
                FeesNameID = (int)x[1],
                Amount = (decimal)x[2],
                AppliedDate = (DateTime)x[3],
                ClassName = (string)x[4],
                FeesName = (string)x[5],
                Frequency = (string)x[6],
            }
            );

            return sql;
        }

        public RawSqlColumns GetFeesInvoice()
        {
            string pref = "Fee" + DateTime.Now.ToString("yyMM");
            string query = @"select '" + pref + "'+CONVERT(nvarchar,ISNULL(MAX(CONVERT(int,RIGHT(InvoiceNo,LEN(InvoiceNo)-7))),0)+1)  from FeesCollectionMasters where InvoiceNo like '%" + pref + "%'";
            var sql = RawSqlQuery(query, x => new RawSqlColumns
            {
                InvoiceNo = (string)x[0],
            }
            ).FirstOrDefault();

            return sql;
        }
    }
}
