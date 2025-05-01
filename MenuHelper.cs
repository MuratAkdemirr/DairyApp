namespace DairyApp;

public class MenuHelper
{
    private static void ShowColoredMsg(string msg, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(msg);
        Console.ResetColor();
    }

    public static void ShowErrorMsg(string msg)
    {
        ShowColoredMsg(msg, ConsoleColor.Red);
    }

    public static int AskOption(string question, string[] options)
    {
        if (options.Length == 0)
        {
            throw new ArgumentException($"{nameof(options)} içinde seçenekler olmalı.", nameof(options));
        }

        Console.WriteLine(question);

        for (int i = 0; i < options.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {options[i]}");
        }

        while (true)
        {
            var inputResponse = AskNumber($"Seçimin (1-{options.Length})");
            if (inputResponse >= 1 && inputResponse <= options.Length)
            {
                return inputResponse;
            }

            ShowErrorMsg("Hatalı seçim yaptın.");
        }
    }

    public static string? Ask(string question, bool isRequired = false,
        string validationMsg = "Bu alanı boş bırakamazsın.")
    {
        string? response;
        do
        {
            Console.Write($"{question}: ");
            response = Console.ReadLine();

            if (isRequired && string.IsNullOrWhiteSpace(response))
            {
                ShowErrorMsg(validationMsg);
            }
        } while (isRequired && string.IsNullOrWhiteSpace(response));

        return response?.Trim();
    }

    public static int AskNumber(string question, string validationMsg = "Bir sayı girmelisin.")
    {
        while (true)
        {
            var response = Ask(question, true);
            if (int.TryParse(response, out var result))
            {
                return result;
            }

            ShowErrorMsg(validationMsg);
        }
    }
}