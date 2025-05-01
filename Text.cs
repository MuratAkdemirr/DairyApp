namespace DairyApp;

public class Text
{
    public static void SaveToTxt(string dairyLine)
    {
        using (var writer = new StreamWriter(@"DairyApp.txt", true))
        {
            writer.WriteLine(DateTime.Now.ToString("dd/MM/yyyy"));
            writer.WriteLine(dairyLine);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Kayıt Başarıyla Gerçekleştirildi.");
            Console.ResetColor();
            writer.Close();
            Thread.Sleep(1000);
            Console.Clear();
        }
    }

    public static void DeleteFromTxt()
    {
        using (var writer = new StreamWriter(@"DairyApp.txt")) ;
        {
            Console.WriteLine(" ");
        }
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Kayıtlar Başarıyla Silindi.");
        Console.ResetColor();
        Thread.Sleep(1000);
        Console.Clear();
    }
}