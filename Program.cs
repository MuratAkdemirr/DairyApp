using System.Net.Mime;
using DairyApp;

var textList = new List<TextLog>();
bool access = UserManagment.LogScreen();
while (access)
{
    var inputSelection = MenuHelper.AskOption("GÜNLÜK UYGULAMASI",
        ["Yeni Kayıt Ekle", "Kayıtları Listele", "Kayıt Bul", "Kayıtları Sil", "Çıkış"]);
    if (inputSelection == 1)
    {
        Console.Clear();
        HelperForTxt.SaveToTxt();
    }

    if (inputSelection == 2)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("KAYITLAR");
        Console.ResetColor();
        HelperForTxt.ListTxtRecords();
    }

    if (inputSelection == 3)
    {
        HelperForTxt.DateLogList();
    }

    if (inputSelection == 4)
    {
        HelperForTxt.DeleteAllLog();
    }
    
    if (inputSelection == 5)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Program kapanıyor.. Hoşçakal!..");
        Console.ResetColor();
        Thread.Sleep(1000);
        break;
    }
}
