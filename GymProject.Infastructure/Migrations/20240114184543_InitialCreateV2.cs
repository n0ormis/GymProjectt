using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GymProject.Infastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Memberships",
                keyColumn: "Id",
                keyValue: new Guid("7bfa74da-071d-4248-a6ab-aa36d0ae1065"));

            migrationBuilder.InsertData(
                table: "Memberships",
                columns: new[] { "Id", "Description", "Duration", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("5f7dcf95-54a7-4b94-a24d-03d06d988aa3"), "Choose from workouts. You'll also find world-renowned Les Mills workouts and our proprietary fitness classes, such as Antistress and the hugely popular stationary bike workouts.", 30, "Subscription to group classes", 30m },
                    { new Guid("76a4f67f-3269-4002-9872-c788d1877cb0"), "Boxing classes are a great workout for anyone who wants to work on strengthening the body, but also the psyche. It is definitely a very intense and challenging workout. With these exercises, you will quickly get rid of unwanted body fat and get strong and sculpted muscles. At the workout, you will learn the technique, as well as learn specific sequences of movements. Conscious work with your body and mind will bring you a lot of satisfaction and increase your self-confidence.", 30, "Boxing class subscription", 70m },
                    { new Guid("7eb60613-3c67-44a4-9c7e-9fb1ff9ecf82"), "The second installment of the Antistress Harmony program focused on balanced work on the whole body and releasing daily stress. As in the first edition, classes will include positions and techniques known from Pilates, yoga, stretching and Healthy Spine, among others. There will also be accessory exercises and breath work. This time, however, more attention will be paid to mobility training and body stabilization.", 30, "Subscription \"Children's\"", 50m },
                    { new Guid("983ad7a6-2fac-42ef-bba5-51454397e35a"), "These classes are breaking records in popularity and are gaining recognition from more and more people. They are based mainly on relaxation and breathing techniques, so they have a positive effect not only on the body, but also on the overall mood and well-being. Calming exercises to the rhythm of calm music will allow you to soothe your thoughts and gain energy for new challenges of everyday life. Often, workout accessories such as straps or blocks are used during training.", 30, "Subscription with a yoga trainer", 50m },
                    { new Guid("a1d0d92d-79d2-473c-9a9f-c0e98eca68c7"), "The pass provides the opportunity to participate in systematic individual training in a professionally equipped gym, as well as in group organized classes aimed at intensive work of various body parts.", 30, "Monthly subscription", 20m },
                    { new Guid("ea9111cc-8df3-4bf3-ab29-596c2fffff21"), "A special set of exercises that use training accessories, primarily smaller and larger balls. Such training, first of all, will help improve stability and coordination. You will also strengthen the deep muscles, which are responsible, among other things, for correct posture. Performing exercises on the ball requires concentration and focus, so it's a good opportunity to forget about everyday problems and responsibilities, to focus only on the activity being performed.", 30, "Subscription Lesson with a trainer", 70m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Memberships",
                keyColumn: "Id",
                keyValue: new Guid("5f7dcf95-54a7-4b94-a24d-03d06d988aa3"));

            migrationBuilder.DeleteData(
                table: "Memberships",
                keyColumn: "Id",
                keyValue: new Guid("76a4f67f-3269-4002-9872-c788d1877cb0"));

            migrationBuilder.DeleteData(
                table: "Memberships",
                keyColumn: "Id",
                keyValue: new Guid("7eb60613-3c67-44a4-9c7e-9fb1ff9ecf82"));

            migrationBuilder.DeleteData(
                table: "Memberships",
                keyColumn: "Id",
                keyValue: new Guid("983ad7a6-2fac-42ef-bba5-51454397e35a"));

            migrationBuilder.DeleteData(
                table: "Memberships",
                keyColumn: "Id",
                keyValue: new Guid("a1d0d92d-79d2-473c-9a9f-c0e98eca68c7"));

            migrationBuilder.DeleteData(
                table: "Memberships",
                keyColumn: "Id",
                keyValue: new Guid("ea9111cc-8df3-4bf3-ab29-596c2fffff21"));

            migrationBuilder.InsertData(
                table: "Memberships",
                columns: new[] { "Id", "Description", "Duration", "Name", "Price" },
                values: new object[] { new Guid("7bfa74da-071d-4248-a6ab-aa36d0ae1065"), "1", 1, "1", 1m });
        }
    }
}
