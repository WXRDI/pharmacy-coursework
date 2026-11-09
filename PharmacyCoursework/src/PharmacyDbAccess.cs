using System.Security.Cryptography;
using System.Text;

namespace PharmacyCoursework
{
    internal class PharmacyDbAccess
    {
        static List<string> roles = ["manager", "pharmacist"]; //TODO: remove later if not used

        public static (int id, string name, string login, string role)? ReadEmployees(string searchKey, string value) //reads from emplyees table and returns all
        {                                                                                                      //info except password hash
            var allowedKeys = new List<string> { "id", "full_name", "login", "role" };
            if (!allowedKeys.Contains(searchKey))
            {
                throw new ArgumentException("Invalid key name", nameof(searchKey));
            }

            using var conn = Database.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = $"SELECT id, full_name, login, role FROM employees WHERE {searchKey} = $v";
            cmd.Parameters.AddWithValue("$v", value);
            using var r = cmd.ExecuteReader();
            if (!r.Read()) return null;
            return (r.GetInt32(0), r.GetString(1), r.GetString(2), r.GetString(3) );
        }

        public static void LogOut()
        {
            Properties.Settings.Default.AutoLogIn = false;
            Properties.Settings.Default.RememberMe = false;
            Properties.Settings.Default.UserToken = null;
            Properties.Settings.Default.Save();
        }

        //TODO: read write methods
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

        public static (int id, string role) FindRoleFromToken() // used in auto log in
        {
            string userId;

            string encryptedString = Properties.Settings.Default.UserToken;

            if (!string.IsNullOrEmpty(encryptedString))
            {
                byte[] encryptedData = Convert.FromBase64String(encryptedString);
                byte[] decryptedData = ProtectedData.Unprotect(encryptedData, null, DataProtectionScope.CurrentUser);
                userId = Encoding.UTF8.GetString(decryptedData);
            }
            else throw new Exception("No user id token saved");

            string? role = ReadEmployees("id", userId).Value.role;
            if (role == null ) throw new ArgumentNullException(nameof(role), "Cant be null");
            if (!int.TryParse(userId, out int intUserId)) throw new ArgumentException("Unable to convert user id from string to int.");
            return (intUserId, role);
        }

        public static void OnLoginSuccess(string token, bool isRememberMeActive) // encrypts id to store and use to auto login
        {
            Properties.Settings.Default.RememberMe = isRememberMeActive;

            if (isRememberMeActive)
            {
                byte[] toEncrypt = Encoding.UTF8.GetBytes(token);
                byte[] encryptedData = ProtectedData.Protect(toEncrypt, null, DataProtectionScope.CurrentUser);

                Properties.Settings.Default.UserToken = Convert.ToBase64String(encryptedData);
                Properties.Settings.Default.AutoLogIn = true;
            }
            else
            {
                Properties.Settings.Default.UserToken = string.Empty;
                Properties.Settings.Default.AutoLogIn = false;
            }

            Properties.Settings.Default.Save();
        }

        public static bool AutoLogIn() //returns true if user has valid token
        {
            if (Properties.Settings.Default.RememberMe && Properties.Settings.Default.AutoLogIn)
            {
                try
                {
                    string encryptedString = Properties.Settings.Default.UserToken;
                    if (!string.IsNullOrEmpty(encryptedString))
                    {
                        //open form 2
                        return true;
                    }
                }

                catch (CryptographicException)
                {
                    Properties.Settings.Default.RememberMe = false;
                    Properties.Settings.Default.AutoLogIn = false;
                    Properties.Settings.Default.UserToken = string.Empty;
                    Properties.Settings.Default.Save();
                    return false;
                }
            }

            return false;
        }


    }
}
