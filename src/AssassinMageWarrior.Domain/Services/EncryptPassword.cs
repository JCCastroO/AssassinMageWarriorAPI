using System.Security.Cryptography;
using System.Text;

namespace AssassinMageWarrior.Domain.Services;

public class EncryptPassword
{
    private readonly string _key;

    public EncryptPassword(string key) => _key = key;

    public string Encrypt(string password)
    {
        var newPassword = $"{password}{_key}";

        byte[] bytes = Encoding.UTF8.GetBytes(newPassword);
        byte[] hashBytes = SHA512.HashData(bytes);

        return StringBytes(hashBytes);
    }

    private string StringBytes(byte[] bytes)
    {
        var sb = new StringBuilder();
        foreach (byte bite in bytes)
        {
            var hex = bite.ToString("x2");
            sb.Append(hex);
        }

        return sb.ToString();
    }
}
