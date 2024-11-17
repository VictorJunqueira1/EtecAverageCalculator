using System;
using System.Collections.Generic;
using System.Linq;

namespace EtecAverageCalculator
{
    public enum Grade
    {
        I, // Irregular
        R, // Regular
        B, // Bom
        MB // Muito Bom
    }

    internal static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Calculadora de Média - ETEC");
            Console.WriteLine("Digite até 4 notas (I, R, B, MB). A média será calculada automaticamente.");
            Console.WriteLine("Digite 'S' para sair.\n");

            var grades = new List<Grade>();

            while (grades.Count < 4)
            {
                Console.Write($"Nota {grades.Count + 1}: ");
                string input = Console.ReadLine()?.ToUpper();

                if (input == "S")
                {
                    Console.WriteLine("\nPrograma encerrado. Até logo!");
                    return;
                }

                if (Enum.TryParse(input, out Grade grade) && !char.IsDigit(input[0]))
                {
                    grades.Add(grade);

                    Grade finalGrade = CalculateFinalGrade(grades);
                    Console.WriteLine($"Notas: {string.Join(", ", grades)}");
                    Console.WriteLine($"Média atual: {finalGrade}\n");
                }
                else
                {
                    Console.WriteLine("Nota inválida. Digite apenas I, R, B ou MB.\n");
                }
            }

            Console.WriteLine("Número máximo de 4 notas atingido.");
            Console.WriteLine($"Notas finais: {string.Join(", ", grades)}");
            Console.WriteLine($"Média final: {CalculateFinalGrade(grades)}");
        }

        static Grade CalculateFinalGrade(List<Grade> grades)
        {
            Grade firstGrade = grades[0];
            Grade lastGrade = grades[^1];

            // Ajuste baseado na evolução
            if (lastGrade > firstGrade)
                return AdjustGrade(firstGrade, 1);
            if (lastGrade < firstGrade)
                return AdjustGrade(firstGrade, -1);

            return CalculateCentralGrade(grades);
        }

        static Grade CalculateCentralGrade(List<Grade> grades)
        {
            grades.Sort();
            int midIndex = grades.Count / 2;
            return grades[midIndex];
        }

        static Grade AdjustGrade(Grade currentGrade, int adjustment)
        {
            int adjustedValue = Math.Clamp((int)currentGrade + adjustment, 0, Enum.GetValues<Grade>().Length - 1);
            return (Grade)adjustedValue;
        }
    }
}