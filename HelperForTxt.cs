using System.ComponentModel.Design;
using System.Diagnostics;
using System.Net.Mime;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Channels;

namespace DairyApp;

public class HelperForTxt
{
    public static void SaveToTxt()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Bugün içerisinde zaten bir kayıt girilmiş.");
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Bugün günlük kaydı girdin, aynı tarihte yeni bir kayıt eklemek ister misin?(E/H)");
        Console.ResetColor();
        ConsoleKeyInfo keyInfo = Console.ReadKey();
        if (keyInfo.Key == ConsoleKey.E)
        {
            Console.Clear();
        }
        else
        {
            Console.Clear();
            return;
        }

        try
        {
            Console.Write("Sevgili Günlük: ");
            var inputText = Console.ReadLine();
            Console.Write("Şirfeleme parolası giriniz: ");
            string inputCryptoPass = Console.ReadLine(); 
            var cryptedText = CryptoHelper.Encrypt(inputText, inputCryptoPass);
            var systemDate = DateTime.Now.ToString("dd/MM/yyyy");
            using (var writer = new StreamWriter("DairyApp.txt", true))
            {
                writer.WriteLine($"{systemDate}|{cryptedText}");
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Kayıt Başarıyla Gerçekleştirildi.");
            Console.ResetColor();
            Thread.Sleep(1000);
            Console.Clear();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.ToString());
            Console.ResetColor();
            Thread.Sleep(1000);
            Console.Clear();
        }
    }

    public static bool CheckDate(string date)
    {
        if (!File.Exists(@"DairyApp.txt"))
        {
            return false;
        }

        try
        {
            using (var reader = new StreamReader(@"DairyApp.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith(date))
                    {
                        return true;
                    }
                }
            }
        }

        catch (IOException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("DOSYA OKUMA HATASI");
            Console.ResetColor();
            return true;
        }

        return false;
    }


    public static void ListTxtRecords()
    {
        if (!File.Exists(@"DairyApp.txt"))
        {
            return;
        }

        try
        {
            List<string> textLog = new List<string>();
            using (StreamReader reader = new StreamReader(@"DairyApp.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    textLog.Add(line);
                }
            }

            if (textLog.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Kayıt bulunamadı.");
                Console.ResetColor();
                return;
            }

            int index = textLog.Count - 1;
            while (index >= 0)
            {
                string[] parts = textLog[index].Split('|');
                if (parts.Length == 2)
                {
                    string date = parts[0];
                    string text = parts[1];
                    Console.WriteLine(date);
                    Console.Write("Şirfeleme parolası giriniz: ");
                    string inputCryptoPass = Console.ReadLine();
                    string cipherText = CryptoHelper.Decrypt(text, inputCryptoPass);
                    Console.WriteLine(cipherText);
                    Console.WriteLine("=========================================================");
                    Console.WriteLine("Önceki (K)ayıt | (D)üzenleme | Kayıt (S)ilme | (A)na Menü");
                    ConsoleKeyInfo button = Console.ReadKey();

                    switch (button.Key)
                    {
                        case ConsoleKey.K:
                            Console.Clear();
                            index--;
                            break;
                        case ConsoleKey.D:
                            Console.Clear();
                            Console.WriteLine($"Kayıt: {parts[0]}|{parts[1]}|");
                            text = UpdateTextIndex(textLog[index], date);
                            parts[1] = text;
                            parts[0] = DateTime.Now.ToString("dd/MM/yyyy");
                            textLog[index] = string.Join("|", parts);
                            GeneralTexTUpdate(textLog);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Kayıt Başarıyla Güncellendi.");
                            Console.ResetColor();
                            break;
                        case ConsoleKey.S:
                            Console.Clear();
                            DeleteLog(index, textLog);
                            return;
                        case ConsoleKey.A:
                            Console.Clear();
                            return;
                    }
                }
                else
                {
                    Console.WriteLine("Geçersiz Satır.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Dosya Okuma Hatası.");
            Console.ResetColor();
        }
    }

    private static void GeneralTexTUpdate(List<string> textLog)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(@"DairyApp.txt"))
            {
                foreach (string line in textLog)
                {
                    writer.WriteLine(line);
                }
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    private static string UpdateTextIndex(string oldText, string date)
    {
        Console.WriteLine(date);
        Console.WriteLine(oldText.Split('|')[1]);
        Console.WriteLine($"Yeni kaydı girin: ");
        string newText = Console.ReadLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("KAYIT BAŞARI İLE GÜNCELLENDİ!");
        Console.ResetColor();
        return newText;
    }

    private static void DeleteLog(int index, List<string> textLog)
    {
        textLog.RemoveAt(index);
        GeneralTexTUpdate(textLog);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Kayıt Silindi.");
        Console.ResetColor();
        Thread.Sleep(2000);
        Console.Clear();
    }

    public static void DeleteAllLog()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("Tüm kayıtları silmek istediğinizden emin misiniz? (E/H): ");
        Console.ResetColor();
        var inputAnswer = Console.ReadKey();
        if (inputAnswer.Key == ConsoleKey.E)
        {
            File.Delete(@"DairyApp.txt");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nTüm Kayıtlar Silindi!");
            Thread.Sleep(2000);
            Console.ResetColor();
            Console.Clear();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nKayıtlar silinemedi!");
            Console.ResetColor();
            Thread.Sleep(2000);
            Console.Clear();
            
        }
    }


    public static void DateLogList()
    {
        Console.Write("Aranan Tarihi Giriniz:(gg.AA.yyyy)");
        string inputDate = Console.ReadLine();
        List<string> textLog1 = new List<string>();
        using (StreamReader reader = new StreamReader(@"DairyApp.txt"))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                textLog1.Add(line);
            }
        }

        if (textLog1.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Kayıt bulunamadı.");
            Console.ResetColor();
            Thread.Sleep(2000);
            Console.Clear();
            
        }

        int index = textLog1.Count - 1;
        while (index >= 0)
        {
            string[] parts = textLog1[index].Split('|');
            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i] == inputDate)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("KAYITLAR");
                    Console.ResetColor();
                    Console.WriteLine($"{parts[i]}");
                    Console.Write("Şirfeleme parolası giriniz: ");
                    string inputCryptoPass = Console.ReadLine();
                    string text= CryptoHelper.Decrypt(parts[i+1], inputCryptoPass);
                    Console.WriteLine("====================================");
                    Console.WriteLine(text);
                    Console.WriteLine("====================================");
                    
                }
                index--;
                Console.WriteLine("(A)na menü");
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.A)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Ana menüye yönlendiriliyorsunuz.");
                    Console.ResetColor();
                    Thread.Sleep(2000);
                    Console.Clear();
                    break;
                }
            }
        }
    }
}
