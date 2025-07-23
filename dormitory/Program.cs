using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void AddDormitoryFromUser()
    {
        Console.Write("Dormitory name: ");
        string name = Console.ReadLine()?.Trim();
        Console.Write("Address: ");
        string address = Console.ReadLine()?.Trim();
        Console.Write("Capacity: ");
        if (!int.TryParse(Console.ReadLine(), out int capacity))
        {
            Console.WriteLine("❌ Invalid capacity.");
            return;
        }

        using var context = new DormitoryContext();
        var dormitory = new Dormitory { Name = name, Address = address, Capacity = capacity };
        context.Dormitories.Add(dormitory);
        context.SaveChanges();
        Console.WriteLine("Dormitory added.");
    }
