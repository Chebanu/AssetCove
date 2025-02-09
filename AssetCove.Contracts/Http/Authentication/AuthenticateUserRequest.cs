namespace AssetCove.Contracts.Http.Authentication;

public class AuthenticateUserRequest
{
    public string Username { get; init; }
    public string Password { get; init; }
}
public class AuthenticateUserResponse
{
    public string Token { get; init; }
}