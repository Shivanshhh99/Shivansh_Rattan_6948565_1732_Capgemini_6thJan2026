using System.Text.Json;

namespace SmartHospital.MVC.Helpers;

public static class SessionHelper
{
    public static void Set<T>(ISession session, string key, T value) =>
        session.SetString(key, JsonSerializer.Serialize(value));

    public static T? Get<T>(ISession session, string key)
    {
        var value = session.GetString(key);
        return value == null ? default : JsonSerializer.Deserialize<T>(value);
    }

    public const string JwtToken = "JwtToken";
    public const string UserRole = "UserRole";
    public const string UserId = "UserId";
    public const string UserName = "UserName";
    public const string UserEmail = "UserEmail";
}