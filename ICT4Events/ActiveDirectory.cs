using System;
using System.DirectoryServices.AccountManagement;

namespace ICT4Events
{
    public static class ActiveDirectory
    {
        private const string Address = "192.168.20.28:389";

        public static bool AuthenticateUser(string username, string password)
        {
            try
            {
                using (var pc = new PrincipalContext(ContextType.Domain, Address))
                {
                    return pc.ValidateCredentials(username, password);
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void AddUser(string username, string email, string password)
        {
            try
            {
                using (var pc = new PrincipalContext(ContextType.Domain, Address))
                {
                    using (var user = new UserPrincipal(pc))
                    {
                        user.SamAccountName = username;
                        user.EmailAddress = email;
                        user.SetPassword(password);
                        user.Enabled = true;
                        user.ExpirePasswordNow();
                        user.Save();
                    }
                }
            }
            catch (Exception) { }
        }

        public static bool UserIsAdministrator(string username)
        {
            try
            {
                using (var pc = new PrincipalContext(ContextType.Domain, Address))
                {
                    using (var user = UserPrincipal.FindByIdentity(pc, username))
                    {
                        using (var group = GroupPrincipal.FindByIdentity(pc, "Domain Admins"))
                        {
                            if (user != null && group != null)
                            {
                                return user.IsMemberOf(group);
                            }
                        }
                    }
                }
                return false;

            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}