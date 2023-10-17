using Serilog;
using System.Collections.Generic;

namespace APPLICATION.Shared.Domain.Filter
{
    public static class AuthorizeExtensions
    {
        private static List<string> _listUsers = new List<string>();

        public static List<string> BlockUsers
        { get { return _listUsers; } }

        private static string _currentUser;

        public static string GetCurrentUser()
        {
            return _currentUser;
        }

        public static void SetCurrentUser(string user)
        {
            _currentUser = user;
        }

        public static void AddBlockUsers(string user)
        {
            _listUsers.Add(user);
        }

        public static void RemoveBlockUsers(string user)
        {
            _listUsers.Remove(user);
        }

        public static bool VerifyBlockedUsers()
        {
            var isBlocked = BlockUsers.Contains(_currentUser);
            if (isBlocked)
            {
                Log.Warning("Usuario Bloqueado pela lista - unauthorized");
                return false;
            }
            return true;
        }
    }
}