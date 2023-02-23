
using MyCash.Domain.Entities;
using MyCash.Domain.Enums;
using MyCash.Presenatation.LogInPage;
using MyCash.Servise.DTOs;
using MyCash.Servise.Services;
using System.Xml;

namespace MyCash.Presenatation.UserPageUI;

public class UserPage
{
    UserService userService = new UserService();
    WalletService walletService= new WalletService();
    IncomeService incomeService= new IncomeService();
    ExposeService exposeService= new ExposeService();   
    public async Task User1(string email)
    {
        var userId = (await userService.GetAsync(x => x.Email == email)).Result.Id;
        start1:
        Console.Clear();
        
        Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
        Console.Write("\t\t\t\t\t\t\t\t\t\t");
        Console.WriteLine("1 - My wallets");
        Console.Write("\t\t\t\t\t\t\t\t\t\t");
        Console.WriteLine("2 - New Wallet");
        Console.Write("\t\t\t\t\t\t\t\t\t\t");
        Console.WriteLine("3 - My profile");
        Console.Write("\t\t\t\t\t\t\t\t\t\t");
        Console.WriteLine("4 - Log Out");

        var press = byte.Parse(Console.ReadLine());

        switch (press)
        {
            case 1:
                Console.Clear();
                Console.WriteLine("\n\n");
                Console.Write("\t\t\t\t\t\t\t\t\t\t");
                var response1 = await walletService.GetAllAsync(x => x.Id > 0 && x.UserId==userId);
                if (response1.Result.Count == 0)
                {
                    Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
                    Console.Write("\t\t\t\t\t\t\t\t\t\t");
                    Console.WriteLine("You haven't any wallets yet!\n\n");
                    Console.Write("\t\t\t\t\t\t\t\t\t\t");
                    Console.Write("Press Enter to return: ");
                    var ret = Console.ReadKey().Key;
                    if (ret == ConsoleKey.Enter)
                        goto start1;
                    break;
                }

                Console.WriteLine($"Id\t|\tName\t|\tCurrency\n");
                foreach (var item in response1.Result)
                {
                    Console.Write("\t\t\t\t\t\t\t\t\t\t");
                    Console.WriteLine($"{item.Id}\t|\t{item.Name}\t|\t{item.Currency}");
                }
                Console.WriteLine("\n\n");
                Console.Write("\t\t\t\t\t\t\t\t\t\t");
                Console.WriteLine("1 - Choose wallet");
                Console.Write("\t\t\t\t\t\t\t\t\t\t");
                Console.WriteLine("2 - Back");
                var press4 = byte.Parse(Console.ReadLine());
                if (press4 == 2)
                    goto start1;
                else if (press4 == 1)
                {
                    Console.Write("\n\n\t\t\t\t\t\t\t\t\t\t");
                    Console.Write("Enter Wallet Id: ");
                    var walletId = long.Parse(Console.ReadLine());
                    start:
                    var response2 = await walletService.GetAsync(x => x.Id == walletId);
                    Console.Clear();
                    Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
                    Console.Write("\t\t\t\t\t\t\t\t\t\t");
                    Console.WriteLine($"Name: {response2.Result.Name}\tAmount: {response2.Result.Amount} " +
                        $"{response2.Result.Currency}\n");
                    Console.Write("\t\t\t\t\t\t\t\t\t\t");
                    Console.WriteLine("1 - Income");
                    Console.Write("\t\t\t\t\t\t\t\t\t\t");
                    Console.WriteLine("2 - Expose");
                    Console.Write("\t\t\t\t\t\t\t\t\t\t");
                    Console.WriteLine("3 - All Incomes");
                    Console.Write("\t\t\t\t\t\t\t\t\t\t");
                    Console.WriteLine("4 - All Exposes");
                    Console.Write("\t\t\t\t\t\t\t\t\t\t");
                    Console.WriteLine("5 - Update");
                    Console.Write("\t\t\t\t\t\t\t\t\t\t");
                    Console.WriteLine("6 - Delete");
                    Console.Write("\t\t\t\t\t\t\t\t\t\t");
                    Console.WriteLine("7 - Main Menu");
                    var press1 = byte.Parse(Console.ReadLine());
                    switch (press1)
                    {
                        case 1:
                            Console.Clear();
                            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
                            Console.Write("\t\t\t\t\t\t\t\t\t\t");
                            var newIncome = new IncomeCreationDto();
                            Console.Write("CategoryId: ");
                            newIncome.CategoryId = int.Parse(Console.ReadLine());
                            Console.Write("\t\t\t\t\t\t\t\t\t\t");
                            Console.Write("Description: ");
                            newIncome.Description = Console.ReadLine();
                            Console.Write("\t\t\t\t\t\t\t\t\t\t");
                            Console.Write("Amount: ");
                            newIncome.Amount = decimal.Parse(Console.ReadLine());
                            newIncome.WalletId = walletId;
                            var response3 = await incomeService.CreateAsync(newIncome);
                            Console.Clear();
                            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
                            Console.Write("\t\t\t\t\t\t\t\t\t\t");
                            Console.WriteLine($"{response3.Message}\n\n");
                            Console.Write("\t\t\t\t\t\t\t\t\t\t");
                            Console.Write("Press Enter to return: ");
                            var ret = Console.ReadKey().Key;
                            if (ret == ConsoleKey.Enter)
                                goto start;
                            break;
                        case 2:
                            Console.Clear();
                            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
                            Console.Write("\t\t\t\t\t\t\t\t\t\t");
                            var newExpose = new ExposeCreationDto();
                            Console.Write("CategoryId: ");
                            newExpose.CategoryId = int.Parse(Console.ReadLine());
                            Console.Write("\t\t\t\t\t\t\t\t\t\t");
                            Console.Write("Description: ");
                            newExpose.Description = Console.ReadLine();
                            Console.Write("\t\t\t\t\t\t\t\t\t\t");
                            Console.Write("Amount: ");
                            newExpose.Amount = decimal.Parse(Console.ReadLine());
                            newExpose.WalletId = walletId;
                            var response4 = await exposeService.CreateAsync(newExpose);
                            Console.Clear();
                            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
                            Console.Write("\t\t\t\t\t\t\t\t\t\t");
                            Console.WriteLine($"{response4.Message}\n\n");
                            Console.Write("\t\t\t\t\t\t\t\t\t\t");
                            Console.Write("Press Enter to return: ");
                            var ret1 = Console.ReadKey().Key;
                            if (ret1 == ConsoleKey.Enter)
                                goto start;
                            break;
                        case 3:
                            Console.Clear();
                            Console.WriteLine("\n\n");
                            Console.Write("\t\t\t\t\t\t\t\t\t\t");
                            Console.WriteLine($"ID\t|\tDescription\t|\tAmount\n");
                            var response5 = await incomeService.GetAllAsync(x => x.WalletId == walletId);
                            foreach (var item in response5.Result)
                            {
                                Console.Write("\t\t\t\t\t\t\t\t\t\t");
                                Console.WriteLine($"{item.Id}\t|\t{item.Description}\t|\t{item.Amount}");
                            }
                            Console.Write("\n\n\t\t\t\t\t\t\t\t\t\t");
                            Console.Write("Press Enter to return: ");
                            var ret2 = Console.ReadKey().Key;
                            if (ret2 == ConsoleKey.Enter)
                                goto start;
                            break;
                        case 4:
                            Console.Clear();
                            Console.WriteLine("\n\n");
                            Console.Write("\t\t\t\t\t\t\t\t\t\t");
                            Console.WriteLine($"ID\t|\tDescription\t|\tAmount\n");
                            var response6 = await exposeService.GetAllAsync(x => x.WalletId == walletId);
                            foreach (var item in response6.Result)
                            {
                                Console.Write("\t\t\t\t\t\t\t\t\t\t");
                                Console.WriteLine($"{item.Id}\t|\t{item.Description}\t|\t{item.Amount}");
                            }
                            Console.Write("\n\n\t\t\t\t\t\t\t\t\t\t");
                            Console.Write("Press Enter to return: ");
                            var ret3 = Console.ReadKey().Key;
                            if (ret3 == ConsoleKey.Enter)
                                goto start;
                            break;
                        case 5:
                            Console.Clear();
                            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
                            Console.Write("\t\t\t\t\t\t\t\t\t\t");
                            var newWallet = new WalletCreationDto();
                            Console.Write("Name: ");
                            newWallet.Name = Console.ReadLine();
                            Console.Write("\t\t\t\t\t\t\t\t\t\t");
                            Console.Write("Currency: ");
                            newWallet.Currency = (CurrencyType)int.Parse(Console.ReadLine());
                            Console.Write("\t\t\t\t\t\t\t\t\t\t");
                            Console.Write("Amount: ");
                            newWallet.Amount = decimal.Parse(Console.ReadLine());

                            var respomse6 = await walletService.UpdateAsync(walletId, newWallet);
                            Console.Clear();
                            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
                            Console.Write("\t\t\t\t\t\t\t\t\t\t");
                            Console.WriteLine($"{respomse6.Message}!\n\n");
                            Console.Write("\t\t\t\t\t\t\t\t\t\t");
                            Console.Write("Press Enter to return: ");
                            var ret4 = Console.ReadKey().Key;
                            if (ret4 == ConsoleKey.Enter)
                                goto start;
                            break;
                        case 6:
                            Console.Clear();
                            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
                            Console.Write("\t\t\t\t\t\t\t\t\t\t");
                            Console.WriteLine("1 - Delete");
                            Console.Write("\t\t\t\t\t\t\t\t\t\t");
                            Console.WriteLine("2 - Back");
                            var press3 = byte.Parse(Console.ReadLine());
                            switch (press3)
                            {
                                case 1:
                                    Console.Clear();
                                    Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
                                    Console.Write("\t\t\t\t\t\t\t\t\t\t");
                                    var response7 = await walletService.DeleteAsync(walletId);
                                    Console.WriteLine($"{response7.Message}!\n\n");
                                    Console.Write("\t\t\t\t\t\t\t\t\t\t");
                                    Console.Write("Press Enter to return: ");
                                    var ret5 = Console.ReadKey().Key;
                                    if (ret5 == ConsoleKey.Enter)
                                        goto start1;
                                    break;
                                case 2:
                                    goto start;
                                    
                            }
                            break;
                        case 7:
                            goto start1;

                    }

                    break;
                }
                break;
            case 2:
                Console.Clear();
                Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
                Console.Write("\t\t\t\t\t\t\t\t\t\t");
                var newWallet1 = new WalletCreationDto();
                Console.Write("Name: ");
                newWallet1.Name = Console.ReadLine();
                Console.Write("\t\t\t\t\t\t\t\t\t\t");
                Console.Write("Currency: ");
                newWallet1.Currency = (CurrencyType)int.Parse(Console.ReadLine());
                Console.Write("\t\t\t\t\t\t\t\t\t\t");
                var response8 = await walletService.CreateAsync(userId, newWallet1);
                Console.Clear();
                Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
                Console.Write("\t\t\t\t\t\t\t\t\t\t");
                Console.WriteLine($"{response8.Message}!\n\n");
                Console.Write("\t\t\t\t\t\t\t\t\t\t");
                Console.Write("Press Enter to return: ");
                var ret6 = Console.ReadKey().Key;
                if (ret6 == ConsoleKey.Enter)
                    goto start1;
                break;
            case 3:
                Console.Clear();
                Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
                Console.Write("\t\t\t\t\t\t\t\t\t\t");
                var myUser = await userService.GetAsync(x => x.Id == userId);
                Console.WriteLine($"FirsName: {myUser.Result.FirstName}");
                Console.Write("\t\t\t\t\t\t\t\t\t\t");
                Console.WriteLine($"LastName: {myUser.Result.LastName}");
                Console.Write("\t\t\t\t\t\t\t\t\t\t");
                Console.WriteLine($"Email: {myUser.Result.Email}");
                Console.Write("\t\t\t\t\t\t\t\t\t\t");
                Console.WriteLine($"Password: {myUser.Result.Password}\n\n");
                Console.Write("\t\t\t\t\t\t\t\t\t\t");
                Console.WriteLine("1 - Update");
                Console.Write("\t\t\t\t\t\t\t\t\t\t");
                Console.WriteLine("2 - back");
                var press5 = byte.Parse(Console.ReadLine());
                if (press5==2)
                    goto start1;
                else if (press5 == 1)
                {
                    var user = new UserCreationDto();
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
                    Console.Write("\t\t\t\t\t\t\t\t\t\t");
                    Console.Write("Password: ");
                    user.Password = Console.ReadLine();
                    var response9 = await userService.UpdateAsync(x => x.Id == userId, user);
                    Console.Clear();
                    Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
                    Console.Write("\t\t\t\t\t\t\t\t\t\t");
                    Console.WriteLine($"{response9.Message}!\n\n");
                    Console.Write("\t\t\t\t\t\t\t\t\t\t");
                    Console.Write("Press Enter to return: ");
                    var ret7 = Console.ReadKey().Key;
                    if (ret7 == ConsoleKey.Enter)
                        goto start1;

                }
                break;
            case 4:
                var login = new LoginPage();
                await login.Login();
                break;
        }


    }
}
