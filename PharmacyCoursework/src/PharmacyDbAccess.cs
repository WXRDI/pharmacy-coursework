using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Text;

namespace PharmacyCoursework
{
    internal class PharmacyDbAccess
    {

        static string? userRole;
        static int? userId;
        static List<string> roles = ["manager", "pharmacist"];

        public static void AuthoriseUser ((int id, string role) authData)
        {
            userId = authData.id;
            userRole = authData.role;
        }

        public static void LogOut()
        {
            userId = null;
            userRole = null;
        }

        public static bool AddEmployeeInDatabase(string role)
        {
            for (int i = 0; i < roles.Count; i++)
            {
                if (role.ToLower().Trim() == roles[i])
                {
                    userRole = roles[i];
                    return true;
                }
            }

            return false;
        }

        //TODO: Passwrd and login check, read write methods, comments
        public static bool LoginCheck()
        {
            return false;
        }

        public static void AddEmployee(string fullName, string login, string password, string role)
        {
            using var conn = Database.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO employees (full_name, login, password_hash, role)
                        VALUES ($n, $l, $h, $r)";
            cmd.Parameters.AddWithValue("$n", fullName);
            cmd.Parameters.AddWithValue("$l", login);
            cmd.Parameters.AddWithValue("$h", PasswordHasher.Hash(password));
            cmd.Parameters.AddWithValue("$r", role);
            cmd.ExecuteNonQuery();
        }

        public static (int id, string role)? Login(string login, string password)
        {
            using var conn = Database.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT id, password_hash, role FROM employees WHERE login = $l";
            cmd.Parameters.AddWithValue("$l", login);
            using var r = cmd.ExecuteReader();
            if (!r.Read()) return null;
            if (!PasswordHasher.Verify(password, r.GetString(1))) return null;
            return (r.GetInt32(0), r.GetString(2));
        }
    }
}
