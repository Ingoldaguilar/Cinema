namespace Backend.Helpers
{
    public class UsersHelper
    {
        public static bool CheckUsersRole(string role)
        {
            role = role.ToLower();
            if (role == "admin" || role == "customer" || role == "cashier")
            {
                return true;
            }
            return false;
        }
    }
}
