using System.Web.Security;
using SimpleShop.WebUI.Infrastructure.Abstract;

namespace SimpleShop.WebUI.Infrastructure.Concrete
{
    public class FormAuthProvider : IAuthProvider
    {
        public bool Authenticate(string username, string password)
        {
            bool result = FormsAuthentication.Authenticate(username, password);
            if (result)
            {
                FormsAuthentication.SetAuthCookie(username, false);
            }
            return result;
        } // end Authenticate()


    } // end class

} // end namespace