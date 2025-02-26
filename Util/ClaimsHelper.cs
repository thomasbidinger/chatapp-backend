using System.Security.Claims;

namespace PaceBackend.Util;

public class ClaimsHelper
{
    private static readonly string SUBJECT_CLAIM_TYPE = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
    private static readonly string NAME_CLAIM_TYPE = "name";
    private static readonly string EMAILS_CLAIM_TYPE = "emails";
    
    
    
    
    
    public static string GetSubject(ClaimsPrincipal user)
    {
        return user.Claims.First(c => c.Type == SUBJECT_CLAIM_TYPE)
            .Value;
    }
    
    public static string GetName(ClaimsPrincipal user)
    {
        return user.Claims.First(c => c.Type == NAME_CLAIM_TYPE)
            .Value;
    }
    
    public static string GetEmail(ClaimsPrincipal user)
    {
        return user.Claims.First(c => c.Type == EMAILS_CLAIM_TYPE)
            .Value;
    }
    
    
    
}