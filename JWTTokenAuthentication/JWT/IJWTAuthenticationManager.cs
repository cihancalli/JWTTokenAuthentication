namespace JWTTokenAuthentication.JWT
{
    public interface IJWTAuthenticationManager
    {
        string Authenticate(string userName, string password);
    }
}
