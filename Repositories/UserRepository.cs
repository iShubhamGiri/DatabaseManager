using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DatabaseManager.Helpers;
using DatabaseManager.Interfaces;
using DatabaseManager.Models;

namespace DatabaseManager.Repositories
{
    public class UserRepository : IUserRepository
    {
        public void InsertUser(User user)
        {
            using var conn = DbHelper.GetConnection();
            conn.Open();
            var cmd = new SqlCommand("INSERT INTO Users (FirstName, LastName, Email) VALUES (@FirstName, @LastName, @Email)", conn);
            cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
            cmd.Parameters.AddWithValue("@LastName", user.LastName);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.ExecuteNonQuery();
        }

        public void UpdateUser(User user)
        {
            using var conn = DbHelper.GetConnection();
            conn.Open();
            var cmd = new SqlCommand("UPDATE Users SET FirstName=@FirstName, LastName=@LastName, Email=@Email WHERE Id=@Id", conn);
            cmd.Parameters.AddWithValue("@Id", user.Id);
            cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
            cmd.Parameters.AddWithValue("@LastName", user.LastName);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.ExecuteNonQuery();
        }

        public void DeleteUser(int id)
        {
            using var conn = DbHelper.GetConnection();
            conn.Open();
            var cmd = new SqlCommand("DELETE FROM Users WHERE Id=@Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }

        public User GetUserById(int id)
        {
            using var conn = DbHelper.GetConnection();
            conn.Open();
            var cmd = new SqlCommand("SELECT * FROM Users WHERE Id=@Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new User
                {
                    Id = (int)reader["Id"],
                    FirstName = reader["FirstName"].ToString(),
                    LastName = reader["LastName"].ToString(),
                    Email = reader["Email"].ToString()
                };
            }

            return null;
        }

        public List<User> GetAllUsers()
        {
            var users = new List<User>();
            using var conn = DbHelper.GetConnection();
            conn.Open();
            var cmd = new SqlCommand("SELECT * FROM Users", conn);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                users.Add(new User
                {
                    Id = (int)reader["Id"],
                    FirstName = reader["FirstName"].ToString(),
                    LastName = reader["LastName"].ToString(),
                    Email = reader["Email"].ToString()
                });
            }

            return users;
        }
    }
}
