using DairyApp;

while (true)
{
    var inputSelection = MenuHelper.AskOption("GÜNLÜK UYGULAMASI",
        ["Yeni Kayıt Ekle", "Kayıtları Listele", "Kayıtları Sil", "Çıkış"]);
    if (inputSelection == 1)
    {
        Console.Clear();
        Console.Write("Sevgili günlük: ");
        Text.SaveToTxt(Console.ReadLine());
    }

    if (inputSelection == 2)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("KAYITLAR");
        Console.ResetColor();
        string readTxt = File.ReadAllText(@"DairyApp.txt");
        Console.WriteLine(readTxt);
    }

    if (inputSelection == 3)
    {
        Console.Clear();
        Console.WriteLine("Bütün kayıtları silmek istiyorsunuz!");
        Console.Write("Devam etmek istiyor musunuz?(E/H): ");
        var inputChoise = Console.ReadKey(true);
        if (inputChoise.Key == ConsoleKey.E)
        {
            Text.DeleteFromTxt();
        }
        else
        {
            continue;
        }
    }

    if (inputSelection == 4)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Program kapanıyor.. Hoşçakal!..");
        Console.ResetColor();
        Thread.Sleep(1000);
        break;
    }
}