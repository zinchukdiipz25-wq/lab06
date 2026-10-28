using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

record Category(string Name, int Products, List<Category> Children);

class Program
{
    static int callCount = 0;   // лічильник викликів

    static readonly Category catalog =
        new("Каталог", 0, new List<Category>
        {
            new("Одяг", 0, new List<Category>
            {
                new("Верхній одяг", 0, new List<Category>
                {
                    new("Куртки", 12, new List<Category>()),
                    new("Пальта", 7,  new List<Category>()),
                }),
                new("Светри", 15, new List<Category>()),
            }),
            new("Взуття", 4, new List<Category>
            {
                new("Кросівки", 23, new List<Category>()),
                new("Чоботи",   9,  new List<Category>()),
            }),
            new("Аксесуари", 0, new List<Category>
            {
                new("Сумки",  18, new List<Category>()),
                new("Ремені", 5,  new List<Category>()),
            }),
        });

    // TODO 1: вивести ієрархію з відступом (2 пробіли на рівень)
    static void PrintTree(Category node, int level)
    {
        callCount++;
        Console.WriteLine(new string(' ', level * 2) + node.Name);
        foreach (var child in node.Children)
            PrintTree(child, level + 1);
    }

    // TODO 2: загальна кількість товарів у вузлі та всіх підкатегоріях
    static int CountProducts(Category node)
    {
        callCount++;
        if (node.Children.Count == 0)          // базовий випадок
            return node.Products;

        int total = node.Products;
        foreach (var child in node.Children)
            total += CountProducts(child);     // менша задача: піддерево
        return total;
    }

    // TODO 3: глибина найдовшої гілки (листок = 1)
    static int MaxDepth(Category node)
    {
        callCount++;
        if (node.Children.Count == 0)          // базовий випадок
            return 1;

        return 1 + node.Children.Max(child => MaxDepth(child));
    }

    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        callCount = 0;
        PrintTree(catalog, 0);
        Console.WriteLine($"Рядків: {callCount}\n");

        callCount = 0;
        Console.WriteLine($"Товарів усього: {CountProducts(catalog)}");
        Console.WriteLine($"Викликів countProducts: {callCount}\n");

        callCount = 0;
        Console.WriteLine($"Глибина: {MaxDepth(catalog)}");
        Console.WriteLine($"Викликів maxDepth: {callCount}");
    }
}