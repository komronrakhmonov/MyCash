
using MyCash.Domain.Enums;
using MyCash.Presenatation.LogInPage;
using MyCash.Servise.DTOs;
using MyCash.Servise.Services;

namespace MyCash.Presenatation.AdminPageUI;

public  class AdminPage
{
    CategoryService categoryService = new CategoryService();
    UserService userService= new UserService();
    public async Task Admin()
    {
        start:
        Console.Clear();
        Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
        Console.Write("\t\t\t\t\t\t\t\t\t\t");
        Console.WriteLine("1 - Categories");
        Console.Write("\t\t\t\t\t\t\t\t\t\t");
        Console.WriteLine("2 - Users");
        Console.Write("\t\t\t\t\t\t\t\t\t\t");
        Console.WriteLine("3 - Log Out");


        var press = byte.Parse(Console.ReadLine());
        switch (press)
        {
            
            case 1:
                start1:
                Console.Clear();
                Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
                Console.Write("\t\t\t\t\t\t\t\t\t\t");
                Console.WriteLine("1 - Create new Category");
                Console.Write("\t\t\t\t\t\t\t\t\t\t");
                Console.WriteLine("2 - Delete category");
                Console.Write("\t\t\t\t\t\t\t\t\t\t");
                Console.WriteLine("3 - Show all Categories");
                Console.Write("\t\t\t\t\t\t\t\t\t\t");
                Console.WriteLine("4 - Back");

                var press1 = byte.Parse(Console.ReadLine());
                switch (press1)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
                        Console.Write("\t\t\t\t\t\t\t\t\t\t");
                        var newCategory = new CategoryCreationDto();
                        Console.Write("Name: ");
                        newCategory.Name = Console.ReadLine();
                        Console.Write("\t\t\t\t\t\t\t\t\t\t");
                        Console.Write("Description: ");
                        newCategory.Description = Console.ReadLine();
                        Console.Write("\t\t\t\t\t\t\t\t\t\t");
                        Console.Write("Type: ");
                        newCategory.Type = (CategoryType)int.Parse(Console.ReadLine());

                        var response1 = await categoryService.CreateAsync(newCategory);
                        Console.Clear();
                        Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
                        Console.Write("\t\t\t\t\t\t\t\t\t\t");
                        Console.WriteLine($"{response1.Message}!\n\n");
                        Console.Write("\t\t\t\t\t\t\t\t\t\t");
                        Console.Write("Press Enter to return: ");
                        var ret = Console.ReadKey().Key;
                        if (ret == ConsoleKey.Enter)
                            goto start1;

                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
                        Console.Write("\t\t\t\t\t\t\t\t\t\t");
                        Console.Write("Enter name category: ");
                        var nameCategory = Console.ReadLine();
                        var response2 = await categoryService.DeleteAsync(x => x.Name.ToLower() == nameCategory.ToLower());
                        Console.Clear();
                        Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
                        Console.Write("\t\t\t\t\t\t\t\t\t\t");
                        Console.WriteLine($"{response2.Message}!\n\n");
                        Console.Write("\t\t\t\t\t\t\t\t\t\t");
                        Console.Write("Press Enter to return: ");
                        var ret1 = Console.ReadKey().Key;
                        if (ret1 == ConsoleKey.Enter)
                            goto start1;
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("\n\n");
                        Console.Write("\t\t\t\t\t\t\t\t");
                        Console.WriteLine("ID\t|\tName\t|\tDescription\t|\tType\n\n");
                        var response3 = await categoryService.GetAllAsync(x => x.Id > 0);
                        if (response3.Result.Count== 0)
                        {
                            Console.Clear();
                            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
                            Console.Write("\t\t\t\t\t\t\t\t\t\t");
                            Console.WriteLine("Category is empty!!");
                            Console.Write("\t\t\t\t\t\t\t\t\t\t");
                            Console.Write("Press Enter to return: ");
                            var ret2 = Console.ReadKey().Key;
                            if (ret2 == ConsoleKey.Enter)
                                goto start1;
                        }
                        foreach (var item in response3.Result)
                        {
                            Console.Write("\t\t\t\t\t\t\t\t");
                            Console.WriteLine($"{item.Id}\t|\t{item.Name}\t|\t{item.Description}\t|\t{item.Type}");
                        }
                        Console.WriteLine("\n\n");
                        Console.Write("\t\t\t\t\t\t\t\t\t\t");
                        Console.WriteLine("1 - Update");
                        Console.Write("\t\t\t\t\t\t\t\t\t\t");
                        Console.WriteLine("2 - Back");
                        var press2 = byte.Parse(Console.ReadLine());
                        if (press2 == 2)
                            goto start1;
                        else if (press2 == 1)
                        {
                            Console.Write("\n\n\t\t\t\t\t\t\t\t\t\t");
                            Console.Write("Enter Category Id: ");
                            var categoryId = int.Parse(Console.ReadLine());
                            Console.Clear();
                            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
                            Console.Write("\t\t\t\t\t\t\t\t\t\t");
                            var newCategory1 = new CategoryCreationDto();
                            Console.Write("Name: ");
                            newCategory1.Name = Console.ReadLine();
                            Console.Write("\t\t\t\t\t\t\t\t\t\t");
                            Console.Write("Description: ");
                            newCategory1.Description = Console.ReadLine();
                            Console.Write("\t\t\t\t\t\t\t\t\t\t");
                            Console.Write("Type: ");
                            newCategory1.Type = (CategoryType)int.Parse(Console.ReadLine());

                            var response4 = await categoryService.UpdateAsync(x=>x.Id==categoryId, newCategory1);
                            Console.Clear();
                            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
                            Console.Write("\t\t\t\t\t\t\t\t\t\t");
                            Console.WriteLine($"{response4.Message}!\n\n");
                            Console.Write("\t\t\t\t\t\t\t\t\t\t");
                            Console.Write("Press Enter to return: ");
                            var ret2 = Console.ReadKey().Key;
                            if (ret2 == ConsoleKey.Enter)
                                goto start1;

                        }
                        break;
                    case 4:
                        goto start;
                }
                break;
            case 2:
            start2:
                Console.Clear();
                Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
                Console.Write("\t\t\t\t\t\t\t\t\t\t");
                Console.WriteLine("1 - Show All Users");
                Console.Write("\t\t\t\t\t\t\t\t\t\t");
                Console.WriteLine("2 - Back");
                var press3 = byte.Parse(Console.ReadLine());
                if (press3==2)
                    goto start;
                else if (press3 == 1)
                {
                    Console.Clear();
                    Console.WriteLine("\n\n");
                    Console.Write("\t\t\t\t\t\t");
                    Console.WriteLine("ID\t|\tFirstName\t|\tLastName\t|\tEmail\n\n");
                    var response4 = await userService.GetAllAsync(x => x.Id > 0);
                    if (response4.Result.Count == 0)
                    {
                        Console.Clear();
                        Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
                        Console.Write("\t\t\t\t\t\t\t\t\t\t");
                        Console.WriteLine("User is empty!!");
                        Console.Write("\t\t\t\t\t\t\t\t\t\t");
                        Console.Write("Press Enter to return: ");
                        var ret2 = Console.ReadKey().Key;
                        if (ret2 == ConsoleKey.Enter)
                            goto start2;
                    }
                    foreach (var item in response4.Result)
                    {
                        Console.Write("\t\t\t\t\t\t");
                        Console.WriteLine($"{item.Id}\t|\t{item.FirstName}\t|\t{item.LastName}\t|\t{item.Email}");
                    }

                    Console.Write("\n\n\t\t\t\t\t\t\t\t\t\t");
                    Console.Write("Press Enter to return: ");
                    var ret3 = Console.ReadKey().Key;
                    if (ret3 == ConsoleKey.Enter)
                        goto start2;
                }
                break;
            case 3:
                var login = new LoginPage();
                await login.Login();
                break;

        }
    }
}
