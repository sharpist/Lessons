using System;
using System.IO;
using System.Threading.Tasks;
using static System.Console;

class Program
{
    static async Task Main(string[] args)
    {
        WriteLine(
            "Укажите расположение файла\n" +
            @"например: C:\Users\Default\[название файла.txt]");

        var values = await getValuesAsync(ReadLine());
        var result = calcResult(values);

        WriteLine($"X = {result}");
    }

    private static Double calcResult(Double[] values)
    {
        double x;
        // C# 8.0
        return (values[0], values[1]) switch {
            var (a, b) when (a > b) => x = a / b + 1,
            var (a, b) when (a == b) => x = a + 25,
            var (a, b) => x = (a * b - 2) / a
        };
    }

    private static async Task<Double[]> getValuesAsync(string path)
    {
        var values = new double[2];
        try {
            // C# 8.0
            using var sr = new StreamReader(path, System.Text.Encoding.Default);

            var i = 0;
            var line = String.Empty;
            while ((line = await sr.ReadLineAsync()) != null) {
                var sublines = line.Split(new char[] { ';', ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var subline in sublines)
                    if (Double.TryParse(subline.Trim('a', 'b', '=', ';', ' '), out values[i]))
                        i++;
            }
        }
        catch (Exception e) {
            WriteLine(e.Message);
        }
        return values;
    }
}
