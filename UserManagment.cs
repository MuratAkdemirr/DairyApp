namespace DairyApp;

public class UserManagment
{
    public static string UserName { get; set; }
    public static string UserPasword { get; set; }

    public static List<string> UserData = new List<string>
    {
        UserName,
        UserPasword
    };

    public static bool LogScreen()
    {   
        string inputUserName = "";
        string inputUserPasword = "";
        var inputSelection = MenuHelper.AskOption("Giriş Ekranı: ", ["GİRİŞ YAP", "KAYIT OL"]);
        if (inputSelection == 1)
        {
            Console.WriteLine("===============================================");
            Console.Write("Kullanıcı Adı: ");
            inputUserName = Console.ReadLine();
            Console.Write("Parola: ");
            inputUserPasword = Console.ReadLine();
            string userName = UserIdControl();
            string userPassword = UserPassControl();
            Console.Clear();
            
            
            if (inputUserName == userName && inputUserPasword == userPassword)
            {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Giriş Başarılı!");
                    Console.ResetColor();
                    Thread.Sleep(1000);
                    Console.Clear();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Hatalı Giriş");
                Console.ResetColor();
                Thread.Sleep(1000);
                Console.Clear();
                LogScreen();
            }
                
        }

        if (inputSelection == 2)
        {
            Console.WriteLine("============================================");
            Console.Write("Kullanıcı Adı: ");
            inputUserName = Console.ReadLine();
            Console.Write("Parola: ");
            inputUserPasword = Console.ReadLine();
            UserData.Add(UserName = inputUserName);
            UserData.Add(UserPasword = inputUserPasword);
            using (StreamWriter writer = new StreamWriter("UserData.txt", true))
            {
                writer.WriteLine($"{UserName}|{UserPasword}|");
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Başarıyla Kayıt Olundu. Lütfen Giriş Yapınız!");
            Console.ResetColor();
            Thread.Sleep(1000);
            Console.Clear();
            LogScreen();
        }
        return true;
    }
    
    private static string ReadSecretLine()
    {
        var line = "";
        ConsoleKeyInfo key;
        do
        {
            key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Backspace && line.Length > 0)
            {
                line = line.Substring(0, line.Length - 1);
                Console.Write("\b \b");
                continue;
            }

            if (!IsSecurePassChar(key.KeyChar))
            {
                continue;
            }

            line += key.KeyChar;

            Console.Write(key.KeyChar);
            Thread.Sleep(61);
            Console.Write("\b*");
        } while (key.Key != ConsoleKey.Enter);

        Console.WriteLine("\n");
        return line;
    }

    private static bool IsSecurePassChar(char c)
    {
        return char.IsLetterOrDigit(c) || char.IsPunctuation(c) || char.IsSymbol(c) || char.IsWhiteSpace(c);
    }

    private static string UserIdControl()
    {
        List<string> userDataText = new List<string>();
        using (StreamReader reader = new StreamReader(@"UserData.txt"))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                userDataText.Add(line);
            }
        }
        
        if (userDataText.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Kullanıcı Kayıtı Bulunamadı!");
            Console.ResetColor();
        }
        
        string userName = "";
        int index = userDataText.Count - 1;
        while (index >= 0)
        {
            string[] parts = userDataText[index].Split('|');
            if (parts.Length == 3)
            {
                userName = parts[0];
                break;
            }
        }
        return userName;
    }
    
    private static string UserPassControl()
    {
        List<string> userDataText = new List<string>();
        using (StreamReader reader = new StreamReader(@"UserData.txt"))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                userDataText.Add(line);
            }
        }
        
        if (userDataText.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Kullanıcı Kayıtı Bulunamadı!");
            Console.ResetColor();
        }
        
        string userPassword = "";
        int index = userDataText.Count - 1;
        while (index >= 0)
        {
            string[] parts = userDataText[index].Split('|');
            if (parts.Length == 3)
            {
                userPassword = parts[1];
                break;
            }
        }
        return userPassword;
    }
}
