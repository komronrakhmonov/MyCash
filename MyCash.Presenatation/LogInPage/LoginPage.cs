using MyCash.Presenatation.AdminPageUI;
using MyCash.Presenatation.UserPageUI;
using MyCash.Servise.DTOs;
using MyCash.Servise.Services;

namespace MyCash.Presenatation.LogInPage;

public class LoginPage
{
    UserService userServise = new UserService();
    UserCreationDto user = new UserCreationDto();
    public string mail = "";

    public async Task Login()
    {
        start:
        Console.Clear();
        Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
        Console.Write("\t\t\t\t\t\t\t\t\t\t");
        Console.WriteLine("1 - Sign Up");
        Console.Write("\t\t\t\t\t\t\t\t\t\t");
        Console.WriteLine("2 - Sign In");

        var press = byte.Parse(Console.ReadLine());
        switch (press)
        {
            case 1:
                Console.Clear();
                Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
                Console.Write("\t\t\t\t\t\t\t\t\t\t");
                Console.Write("FirstName: ");
                user.FirstName = Console.ReadLine();
                Console.Write("\t\t\t\t\t\t\t\t\t\t");
                Console.Write("LastName: ");
                user.LastName = Console.ReadLine();
                Console.Write("\t\t\t\t\t\t\t\t\t\t");
                Console.Write("Email: ");
                user.Email = Console.ReadLine();
                mail = user.Email;
                Console.Write("\t\t\t\t\t\t\t\t\t\t");
                Console.Write("Password: ");
                user.Password = Console.ReadLine();
                var response1 = await userServise.CreateAsync(user);
                if (response1.Result is null)
                {
                    Console.Clear();
                    Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
                    Console.Write("\t\t\t\t\t\t\t\t\t\t");
                    Console.WriteLine("This user already exists\n\n");
                    Console.Write("\t\t\t\t\t\t\t\t\t\t");
                    Console.Write("Press Enter to return ");
                    var ret = Console.ReadKey().Key;
                    if (ret == ConsoleKey.Enter)
                        goto start;
                }
                else
                {
                    Console.Clear();
                    Console.Clear();
                    var userPage = new UserPage();
                    await userPage.User1(mail);

                }
                   
                break;

            case 2:
                Console.Clear();
                Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
                Console.Write("\t\t\t\t\t\t\t\t\t\t");
                Console.Write("Email: ");               
                user.Email = Console.ReadLine();
                mail = user.Email;
                Console.Write("\t\t\t\t\t\t\t\t\t\t");
                Console.Write("Password: ");
                user.Password= Console.ReadLine();

                var response = await userServise.CheckForExists(user.Email,user.Password);
                if (user.Email == "admin" && user.Password == "admin")
                {
                    var adminPage = new AdminPage();
                    await adminPage.Admin();

                }
                else if (response.Result is null)
                {
                    Console.Clear();
                    Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
                    Console.Write("\t\t\t\t\t\t\t\t\t\t");
                    Console.WriteLine("User is not found\n\n");
                    Console.Write("\t\t\t\t\t\t\t\t\t\t");
                    Console.Write("Press Enter to return: ");
                    var ret = Console.ReadKey().Key;
                    if (ret == ConsoleKey.Enter)
                        goto start;
                }
                else
                {
                    Console.Clear();
                    var userPage1 = new UserPage();
                    await userPage1.User1(mail);

                }
                break;                  
        }

    }
}
