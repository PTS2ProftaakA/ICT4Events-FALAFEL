using System;
using System.DirectoryServices;
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

        public static string AddUser(string username, string email, string password)
        {
            /*
            using (var pc = new PrincipalContext(ContextType.Domain, Address, "CN=Users,DC=PTS06-DOMAIN,DC=local", "Administrator", "Wat3rmeloen"))
            {
                using (var user = new UserPrincipal(pc, username, password, true))
                {
                    user.EmailAddress = email;
                    user.Save();
                }
            }
            */
            /*
            using (var entry = new DirectoryEntry("LDAP://" + Address + "/CN=Users,DC=PTS06-DOMAIN,DC=local","Administrator", "Wat3rmeloen"))
            {
                entry.RefreshCache();

                var user = entry.Children.Add("CN=" + username, "user");
                user.Properties["samAccountName"].Add(username);
                user.Properties["mail"].Add(email);
                user.CommitChanges();
                user.RefreshCache();

                user.Invoke("SetOption", 6, 389);
                user.Invoke("SetOption", 7, 1);
                user.Invoke("SetPassword", password);
                user.CommitChanges();
                user.RefreshCache();
            }
            */

            DirectoryEntry entry, user;
            AuthenticationTypes AuthTypes = AuthenticationTypes.Signing | AuthenticationTypes.Sealing | AuthenticationTypes.Secure;
            string shortName;

            try
            {
                entry = new DirectoryEntry("LDAP://" + Address, "Administrator", "Wat3rmeloen", AuthTypes);
                entry.RefreshCache();
            }
            catch (Exception e)
            {
                return e.Message;
            }

            try
            {
                shortName = username.Length > 19 ? username.Substring(0, 19) : username;

                user = entry.Children.Add("CN=" + username + ",CN=Users", "user");
                user.Properties["sAMAccountName"].Add(shortName);
                user.Properties["mail"].Add(email);
                user.CommitChanges();
                user.RefreshCache();

                user.Invoke("SetOption", new object[] { 6, 389 });
                user.Invoke("SetOption", new object[] { 7, 1 });
                user.Invoke("SetPassword", new object[] { password });
                user.CommitChanges();
                user.RefreshCache();

                user.Properties["userAccountControl"].Value = 0x200;
                //user.Properties["pwdLastSet"].Value = 0;
                user.CommitChanges();
                user.RefreshCache();

            }
            catch (Exception e)
            {
                return e.Message;
            }

            return "DONE";
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