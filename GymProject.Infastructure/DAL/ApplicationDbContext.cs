using GymProject.Domain.Models.Auth;
using GymProject.Domain.Models.Memberships;
using GymProject.Domain.Models.Payments;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GymProject.Infastructure.DAL;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgresv8;Username=postgres;Password=Admin123@"); 
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Subscription>().HasData(
            new Subscription()
            {
                Id = Guid.NewGuid(),
                Name = "Monthly subscription",
                Description = "The pass provides the opportunity to participate in systematic individual training in a professionally equipped gym, as well as in group organized classes aimed at intensive work of various body parts.",
                Duration = 30,
                Price = 20
            },
            new Subscription()
            {
                Id = Guid.NewGuid(),
                Name = "Subscription Lesson with a trainer",
                Description = "A special set of exercises that use training accessories, primarily smaller and larger balls. Such training, first of all, will help improve stability and coordination. You will also strengthen the deep muscles, which are responsible, among other things, for correct posture. Performing exercises on the ball requires concentration and focus, so it's a good opportunity to forget about everyday problems and responsibilities, to focus only on the activity being performed.",
                Duration = 30,
                Price = 70
            },
            new Subscription()
            {
                Id = Guid.NewGuid(),
                Name = "Subscription with a yoga trainer",
                Description = "These classes are breaking records in popularity and are gaining recognition from more and more people. They are based mainly on relaxation and breathing techniques, so they have a positive effect not only on the body, but also on the overall mood and well-being. Calming exercises to the rhythm of calm music will allow you to soothe your thoughts and gain energy for new challenges of everyday life. Often, workout accessories such as straps or blocks are used during training.",
                Duration = 30,
                Price = 50
            },
            new Subscription()
            {
                Id = Guid.NewGuid(),
                Name = "Subscription to group classes",
                Description = "Choose from workouts. You'll also find world-renowned Les Mills workouts and our proprietary fitness classes, such as Antistress and the hugely popular stationary bike workouts.",
                Duration = 30,
                Price = 30
            },
            new Subscription()
            {
                Id = Guid.NewGuid(),
                Name = "Boxing class subscription",
                Description = "Boxing classes are a great workout for anyone who wants to work on strengthening the body, but also the psyche. It is definitely a very intense and challenging workout. With these exercises, you will quickly get rid of unwanted body fat and get strong and sculpted muscles. At the workout, you will learn the technique, as well as learn specific sequences of movements. Conscious work with your body and mind will bring you a lot of satisfaction and increase your self-confidence.",
                Duration = 30,
                Price = 70
            },
            new Subscription()
            {
                Id = Guid.NewGuid(),
                Name = "Subscription \"Children's\"",
                Description = "The second installment of the Antistress Harmony program focused on balanced work on the whole body and releasing daily stress. As in the first edition, classes will include positions and techniques known from Pilates, yoga, stretching and Healthy Spine, among others. There will also be accessory exercises and breath work. This time, however, more attention will be paid to mobility training and body stabilization.",
                Duration = 30,
                Price = 50
            });
        base.OnModelCreating(builder);
    }

    public DbSet<Subscription> Memberships { get; set; }
    public DbSet<PaymentSession> PaymentSessions { get; set; }
}