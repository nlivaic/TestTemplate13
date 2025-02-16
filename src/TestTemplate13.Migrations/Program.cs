using System;
using System.IO;
using DbUp;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace TestTemplate13.Migrations
{
    public class Program
    {
        public static int Main(string[] args)
        {
            var connectionString = string.Empty;
            var dbUser = string.Empty;
            var dbPassword = string.Empty;
            var scriptsPath = string.Empty;
            var sqlUsersGroupName = string.Empty;

            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
                ?? "Production";
            Console.WriteLine($"Environment: {env}.");
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env}.json", true, true)
                .AddEnvironmentVariables();

            var config = builder.Build();
            InitializeParameters();
            var connectionStringBuilderTestTemplate13 = new SqlConnectionStringBuilder(connectionString);
            if (env.Equals("Development"))
            {
                connectionStringBuilderTestTemplate13.UserID = dbUser;
                connectionStringBuilderTestTemplate13.Password = dbPassword;
            }
            else
            {
                connectionStringBuilderTestTemplate13.UserID = dbUser;
                connectionStringBuilderTestTemplate13.Password = dbPassword;
                connectionStringBuilderTestTemplate13.Authentication = SqlAuthenticationMethod.ActiveDirectoryPassword;
            }
            var upgraderTestTemplate13 =
                DeployChanges.To
                    .SqlDatabase(connectionStringBuilderTestTemplate13.ConnectionString)
                    .WithVariable("SqlUsersGroupNameVariable", sqlUsersGroupName)    // This is necessary to perform template variable replacement in the scripts.
                    .WithScriptsFromFileSystem(
                        !string.IsNullOrWhiteSpace(scriptsPath)
                                ? Path.Combine(scriptsPath, "TestTemplate13Scripts")
                            : Path.Combine(Environment.CurrentDirectory, "TestTemplate13Scripts"))
                    .LogToConsole()
                    .Build();
            Console.WriteLine($"Now upgrading TestTemplate13.");
            if (env == "Development")
            {
                upgraderTestTemplate13.MarkAsExecuted("0000_AzureSqlContainedUser.sql");
            }
            var resultTestTemplate13 = upgraderTestTemplate13.PerformUpgrade();

            if (!resultTestTemplate13.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"TestTemplate13 upgrade error: {resultTestTemplate13.Error}");
                Console.ResetColor();
                return -1;
            }

            // Uncomment the below sections if you also have an Identity Server project in the solution.
            /*
            var connectionStringTestTemplate13Identity = string.IsNullOrWhiteSpace(args.FirstOrDefault())
                ? config["ConnectionStrings:TestTemplate13IdentityDb"]
                : args.FirstOrDefault();

            var upgraderTestTemplate13Identity =
                DeployChanges.To
                    .SqlDatabase(connectionStringTestTemplate13Identity)
                    .WithScriptsFromFileSystem(
                        scriptsPath != null
                            ? Path.Combine(scriptsPath, "TestTemplate13IdentityScripts")
                            : Path.Combine(Environment.CurrentDirectory, "TestTemplate13IdentityScripts"))
                    .LogToConsole()
                    .Build();
            Console.WriteLine($"Now upgrading TestTemplate13 Identity.");
            if (env != "Development")
            {
                upgraderTestTemplate13Identity.MarkAsExecuted("0004_InitialData.sql");
                Console.WriteLine($"Skipping 0004_InitialData.sql since we are not in Development environment.");
                upgraderTestTemplate13Identity.MarkAsExecuted("0005_Initial_Configuration_Data.sql");
                Console.WriteLine($"Skipping 0005_Initial_Configuration_Data.sql since we are not in Development environment.");
            }
            var resultTestTemplate13Identity = upgraderTestTemplate13Identity.PerformUpgrade();

            if (!resultTestTemplate13Identity.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"TestTemplate13 Identity upgrade error: {resultTestTemplate13Identity.Error}");
                Console.ResetColor();
                return -1;
            }
            */

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();
            return 0;

            void InitializeParameters()
            {
                // Local database, populated from .env file.
                if (args.Length == 0)
                {
                    connectionString = config["TestTemplate13Db_Migrations_Connection"];
                    dbUser = config["DbUser"];
                    dbPassword = config["DbPassword"];
                }

                // Deployed database
                else if (args.Length == 5)
                {
                    connectionString = args[0];
                    dbUser = args[1];
                    dbPassword = args[2];
                    scriptsPath = args[3];
                    sqlUsersGroupName = args[4];
                }
            }
        }
    }
}
