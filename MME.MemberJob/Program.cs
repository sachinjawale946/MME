// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MME.Data;
using System.Security.Cryptography;
using System.Text;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddDbContext<MMEAppDBContext>(options => options.UseSqlServer("Server=LT146;Initial Catalog=MME;MultipleActiveResultSets=true;User ID=sa;Password=pass$123;TrustServerCertificate=true"));
    })
    .Build();

// code

//await host.RunAsync();


int keySize = 64;
int iterations = 350000;
HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
MMEAppDBContext _context = host.Services.GetRequiredService<MMEAppDBContext>();

Byte[] PasswordSalt;
var PasswordHash = HashPasword("123", out PasswordSalt);

var user = _context.Users.FirstOrDefault();

if(user == null)
{
    var start = 1;
    for (int i = 0; i < 1000; i++)
    {
        _context.Users.Add(new MME.Model.Shared.UserModel
        {
            Username = "Sachin_" + start.ToString("0000"),
            PasswordSalt = PasswordSalt,
            Password = PasswordHash,
            FirstName = "Sachin",
            LastName = "Jawale",
            Mobile = "8369498118",
            IsActive = true,
            BirthDate = DateTime.Now,
            RoleId = 1,
        });
        start++;
    }
    _context.SaveChanges();
}


Console.WriteLine("Hello, World!");


string HashPasword(string password, out byte[] salt)
{
    salt = RandomNumberGenerator.GetBytes(keySize);
    var hash = Rfc2898DeriveBytes.Pbkdf2(
        Encoding.UTF8.GetBytes(password),
        salt,
        iterations,
        hashAlgorithm,
        keySize);
    return Convert.ToHexString(hash);
}
