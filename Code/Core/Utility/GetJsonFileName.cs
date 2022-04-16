namespace TraderCadCore;

public static partial class Utility
{
    // Правило именования JSON-файлов вынесено в отдельный метод, чтобы в случае смены концепции всё легко можно было изменить
    public static string GetJsonFileName(Equity equity, Timeframe timeframe) => $"{equity}_{timeframe}.json";
}
