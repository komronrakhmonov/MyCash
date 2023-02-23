using MyCash.Presenatation.LogInPage;
Console.BackgroundColor = ConsoleColor.Gray;
Console.Clear();
Console.ForegroundColor = ConsoleColor.Black;
var login = new LoginPage();
await login.Login();