// See https://aka.ms/new-console-template for more information
using ImageMagick;
using ImageMagick.ImageOptimizers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MME.Data;
using MME.MemberJob;
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



MMEAppDBContext _context = host.Services.GetRequiredService<MMEAppDBContext>();
var membersScript = new MembersScript(_context);
membersScript.InsertMembers();


//Console.WriteLine("Hello, World!");

//var file = new FileInfo("F:\\Sachin_Work\\MyCommunity\\MME\\MME.Web\\wwwroot\\profilepics\\0728DE5B-C474-423C-1D5D-08DB8AC65F8F.jpg");
//using (MagickImage image = new MagickImage(file))
//{
//    {
//        image.Thumbnail(new MagickGeometry(300, 300));
//        image.Write(@"F:\Sachin_Work\MyCommunity\MME\MME.Web\wwwroot\profilepics_thumbs\0728DE5B-C474-423C-1D5D-08DB8AC65F8F.jpg");
//    }
//}


Console.ReadLine();
