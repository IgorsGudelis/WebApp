using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DTO;

namespace DAL.Users
{
    public class UsersRepository : IUser
    {
        private readonly SqlConnection _connect = new SqlConnection();
        private const string StringConnection = @"Data Source=DESKTOP-2ABAPV0\LOCAL;
            Initial Catalog=MyBlog;Integrated Security=True;Pooling=False";


        public void OpenConnection()
        {
            _connect.ConnectionString = StringConnection;

            try
            {
                _connect.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void CloseConnection()
        {
            _connect.Close();
        }

        public SqlParameter CmdParam(string parameterName, string value, SqlDbType type)
        {
            var param = new SqlParameter
            {
                ParameterName = parameterName,
                Value = value,
                SqlDbType = type
            };

            return param;
        }

        public SqlDataReader CmdDataReader(string strSql, SqlConnection connection)
        {
            var myCommand = new SqlCommand(strSql, connection);
            var dr = myCommand.ExecuteReader();

            return dr;
        }

        public SqlDataReader CmdDataReader(string strSql, SqlConnection connection, SqlParameter paramSql)
        {
            var myCommand = new SqlCommand(strSql, connection);
            myCommand.Parameters.Add(paramSql);
            var dr = myCommand.ExecuteReader();

            return dr;
        }
    
        public void DataReaderReadAdd(List<UserDTO> allUsers, SqlDataReader dr)
        {
            var columns = Enumerable.Range(0, dr.FieldCount).Select(dr.GetName).ToList();

            var idIndx = columns.FindIndex(x => x == "UserID");
            var firstNameIndx = columns.FindIndex(x => x == "FirstName");
            var lastNameIndx = columns.FindIndex(x => x == "LastName");
            var emailIndx = columns.FindIndex(x => x == "Email");

            while (dr.Read())
            {                
                var tmpUser = new UserDTO
                {                   
                    ID = (int) dr[idIndx],
                    FirstName = dr[firstNameIndx].ToString(),
                    LastName = dr[lastNameIndx].ToString(),
                    Email = dr[emailIndx].ToString()
                };


                allUsers.Add(tmpUser);
            }
        }

        public List<UserDTO> GetAllUsers(string user = null)
        {
            var allUsers = new List<UserDTO>();

            OpenConnection();
 
            const string strSql = "Select * From Users Order BY LastName, FirstName";

            //throw new Exception();

            var dr = CmdDataReader(strSql, _connect);

            DataReaderReadAdd(allUsers, dr);

            CloseConnection();

            return allUsers;
        }

        public List<UserDTO> GetUsers(string find)
        {
            var allUsers = new List<UserDTO>();

            OpenConnection();

            const string strSql = "Select * From Users Where (FirstName Like @find) OR (LastName Like @find) Order BY LastName, FirstName";

            var dr = CmdDataReader(strSql, _connect, CmdParam("@find", "%" + find + "%", SqlDbType.NVarChar));

            DataReaderReadAdd(allUsers, dr);

            CloseConnection();

            return allUsers;
        }

        public void AddUserDto(UserDTO addUserDto)
        {
            OpenConnection();

            const string strSql = "Insert Into Users(FirstName, LastName, Email) " +
                               "Values(@FName, @LName, @EMail)";

            using (var cmd = new SqlCommand(strSql, _connect))
            {
                cmd.Parameters.Add(CmdParam("@FName",addUserDto.FirstName, SqlDbType.NVarChar));

                cmd.Parameters.Add(CmdParam("@LName", addUserDto.LastName, SqlDbType.NVarChar));

                cmd.Parameters.Add(CmdParam("@EMail", addUserDto.Email, SqlDbType.NVarChar));

                cmd.ExecuteNonQuery();
            }

            CloseConnection();
        }

        public void DeleteUser(int idParam)
        {
            OpenConnection();

            var strSql = string.Format("Delete from Users where UserID = @UserId", idParam);

            var cmd = new SqlCommand(strSql, _connect);

            var param = new SqlParameter
            {
                ParameterName = "@UserID",
                Value = idParam,
                SqlDbType = SqlDbType.Int
            };

            cmd.Parameters.Add(param);
            cmd.ExecuteNonQuery();

            CloseConnection();
        }

        public UserDTO EditUserQuery(int idParam)
        {
            var user = new UserDTO();

            OpenConnection();

            var strSql = String.Format("Select * From Users Where UserID = {0}", idParam);

            var cmd = new SqlCommand(strSql, _connect);

            var param = new SqlParameter
            {
                ParameterName = "@UserID",
                Value = idParam,
                SqlDbType = SqlDbType.Int
            };

            cmd.Parameters.Add(param);

            var dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                user.FirstName = dr[1].ToString();
                user.LastName = dr[2].ToString();
                user.Email = dr[3].ToString();
            }

            CloseConnection();

            return user;
        }

        public void EditUser(int idParam, string firstName, string lastName, string eMail)
        {
            OpenConnection();

            const string strSql = "Update Users Set FirstName = @FName, LastName = @LName, Email = @Email " +
                                  "Where UserID = @Id";

            using (var cmd = new SqlCommand(strSql, _connect))
            {
                cmd.Parameters.Add(CmdParam("@FName", firstName, SqlDbType.NVarChar));

                cmd.Parameters.Add(CmdParam("@LName", lastName, SqlDbType.NVarChar));

                cmd.Parameters.Add(CmdParam("@Email", eMail, SqlDbType.NVarChar));

                var param = new SqlParameter
                {
                    ParameterName = "@Id",
                    Value = idParam,
                    SqlDbType = SqlDbType.Int
                };
                cmd.Parameters.Add(param);

                cmd.ExecuteNonQuery();
            }

            CloseConnection();
        }
    }
}
