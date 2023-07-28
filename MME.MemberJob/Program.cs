// See https://aka.ms/new-console-template for more information
using ImageMagick;
using ImageMagick.ImageOptimizers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MME.Data;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        // services.AddDbContext<MMEAppDBContext>(options => options.UseSqlServer("Server=LT146;Initial Catalog=MME;MultipleActiveResultSets=true;User ID=sa;Password=pass$123;TrustServerCertificate=true"));
        services.AddDbContext<MMEAppDBContext>(options => options.UseSqlServer("Server=sql.bsite.net\\MSSQL2016;Initial Catalog=sachinjawale946_SampleDB;MultipleActiveResultSets=true;User ID=sachinjawale946_SampleDB;Password=Sachin@123;TrustServerCertificate=true"));
    })
    .Build();


int keySize = 64;
int iterations = 350000;
HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
MMEAppDBContext _context = host.Services.GetRequiredService<MMEAppDBContext>();

Byte[] PasswordSalt;
var PasswordHash = HashPasword("123", out PasswordSalt);

var user = _context.Users.FirstOrDefault();

if (user == null)
{
    var start = 1;
    for (int i = 0; i < 25; i++)
    {
        if (i % 2 == 0)
        {
            _context.Users.Add(new MME.Model.Shared.UserModel
            {
                Username = "Sachin_" + start.ToString("0000"),
                PasswordSalt = PasswordSalt,
                Password = PasswordHash,
                FirstName = "Sachin",
                LastName = "Jawale",
                Mobile = start.ToString("0000000000"),
                IsActive = true,
                BirthDate = DateTime.Now.AddYears(-37),
                RoleId = 1,
                Gender = "Male"
            });
        }
        else
        {
            _context.Users.Add(new MME.Model.Shared.UserModel
            {
                Username = "Ashvini_" + start.ToString("0000"),
                PasswordSalt = PasswordSalt,
                Password = PasswordHash,
                FirstName = "Ashvini",
                LastName = "Jawale",
                Mobile = start.ToString("0000000000"),
                IsActive = true,
                BirthDate = DateTime.Now.AddYears(-30),
                RoleId = 1,
                Gender = "Female"
            });
        }
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

var file = new FileInfo("F:\\Sachin_Work\\MyCommunity\\MME\\MME.Web\\wwwroot\\profilepics\\0728DE5B-C474-423C-1D5D-08DB8AC65F8F.jpg");
using (MagickImage image = new MagickImage(file))
{
    {
        image.Thumbnail(new MagickGeometry(300, 300));
        image.Write(@"F:\Sachin_Work\MyCommunity\MME\MME.Web\wwwroot\profilepics_thumbs\0728DE5B-C474-423C-1D5D-08DB8AC65F8F.jpg");
    }
}


Console.ReadLine();
