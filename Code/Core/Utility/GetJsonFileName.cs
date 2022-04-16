namespace Core;

public static partial class Utility
{
    // Правило именования JSON-файлов вынесено в отдельный метод, чтобы в случае смены концепции всё легко можно было изменить
    public static string GetJsonFileName(Equities equity, Timeframe timeframe) => $"{equity}_{timeframe}.json";
}
