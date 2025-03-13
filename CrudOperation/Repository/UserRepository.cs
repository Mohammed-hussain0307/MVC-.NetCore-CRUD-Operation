using CrudOperation.Models;
using Microsoft.Data.SqlClient;

namespace CrudOperation.Repository
{
    public class UserRepository
    {
        private SqlConnection sqlConnection = null;
        SqlCommand sqlCommand = null;
        List<UserModel> userList = new List<UserModel>();

        //configuration to read the connection string
        public static IConfiguration Configuration { get; set; }

        //method to read the connection string
        public UserRepository()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            sqlConnection = new SqlConnection(Configuration.GetConnectionString("Connection"));
        }

        //view all employee
        public List<UserModel> GetAllUsers()
        {            
            using (sqlConnection)
            {
                sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.CommandText = "SPR_User";
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                while(sqlDataReader.Read())
                {
                    userList.Add(new UserModel
                    {
                        Id = Convert.ToInt32(sqlDataReader["id"]),
                        FullName = sqlDataReader["full_name"].ToString(),
                        DateOfBirth = DateOnly.FromDateTime(Convert.ToDateTime(sqlDataReader["date_of_birth"])),
                        Email = sqlDataReader["email"].ToString(),
                        MobileNumber = sqlDataReader["mobile_number"].ToString()
                    });

                }
                sqlConnection.Close();

                return userList;
            }
        }

        //create user
        public bool CreateUser(UserModel userModel)
        {
            int check = 0;
            using (sqlConnection)
            {
                sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.CommandText = "SPC_User";
                sqlCommand.Parameters.AddWithValue("@FullName", userModel.FullName);
                sqlCommand.Parameters.AddWithValue("@DateOfBirth", userModel.DateOfBirth);
                sqlCommand.Parameters.AddWithValue("@Email", userModel.Email);
                sqlCommand.Parameters.AddWithValue("@MobileNumber", userModel.MobileNumber);

                sqlConnection.Open();
                check = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
            return check > 0;
        }

        //get user by ID
        public List<UserModel> GetUserById(int id)
        {
            using (sqlConnection)
            {
                sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.CommandText = "SPG_User";
                sqlCommand.Parameters.AddWithValue("@Id", id);

                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    userList.Add(new UserModel
                    {
                        FullName = sqlDataReader["full_name"].ToString(),
                        DateOfBirth = DateOnly.FromDateTime(Convert.ToDateTime(sqlDataReader["date_of_birth"])),
                        Email = sqlDataReader["email"].ToString(),
                        MobileNumber = sqlDataReader["mobile_number"].ToString()
                    });
                }
            }
            return userList;
        }
    }
}
