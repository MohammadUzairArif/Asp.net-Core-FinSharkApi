/* IdentityUser already contain karta hai:

UserName

Email

PasswordHash

PhoneNumber

Id (user ID)

Ab aap is AppUser class mein apni custom properties bhi add kar sakte ho jaise:
 */


using Microsoft.AspNetCore.Identity;

namespace Asp.netCore_FinSharkProjAPI.Models
{
    public class AppUser : IdentityUser
    {

    }
}
