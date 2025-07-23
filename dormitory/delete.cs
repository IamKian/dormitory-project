using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

public static class DeleteOperations
{
    public static void DeleteDormitory(DormitoryContext context, string name)
    {
        var dorm = context.Dormitories.Include(d => d.Blocks).FirstOrDefault(d => d.Name == name);
        if (dorm == null)
        {
            Console.WriteLine("Dormitory not found");
            return;
        }
        context.Dormitories.Remove(dorm);
        context.SaveChanges();
        Console.WriteLine("Dormitory deleted");
    }

    public static void DeleteBlock(DormitoryContext context, string name)
    {
        var block = context.Blocks.Include(b => b.Rooms).FirstOrDefault(b => b.Name == name);
        if (block == null)
        {
            Console.WriteLine("Block not found");
            return;
        }
        context.Blocks.Remove(block);
        context.SaveChanges();
        Console.WriteLine("Block deleted");
    }

    public static void DeleteRoom(DormitoryContext context, string number)
    {
        var room = context.Rooms.Include(r => r.Students).FirstOrDefault(r => r.Number == number);
        if (room == null)
        {
            Console.WriteLine("Room not found");
            return;
        }
        if (room.Students.Any())
        {
            Console.WriteLine("Room is not empty");
            return;
        }
        context.Rooms.Remove(room);
        context.SaveChanges();
        Console.WriteLine("Room deleted");
    }

    public static void DeleteStudent(DormitoryContext context, string identifier)
    {
        var student = context.Students.Include(s => s.Tools).FirstOrDefault(s => s.Name == identifier || s.StudentId.ToString() == identifier);
        if (student == null)
        {
            Console.WriteLine("Student not found");
            return;
        }
        if (student.Tools.Any())
        {
            Console.WriteLine("Student has tools " +
                "");
            return;
        }
        context.Students.Remove(student);
        context.SaveChanges();
        Console.WriteLine("Student deleted.");
    }

    public static void DeleteTool(DormitoryContext context, string name)
    {
        var tool = context.Tools.FirstOrDefault(t => t.PartNumber == name);
        if (tool == null)
        {
            Console.WriteLine("Tool not found.");
            return;
        }
        context.Tools.Remove(tool);
        context.SaveChanges();
        Console.WriteLine("Tool deleted.");
    }

    public static void DeleteSupervisor(DormitoryContext context, string Name)
    {
        var supervisor = context.Supervisors.FirstOrDefault(s => s.Name == Name);
        if (supervisor == null)
        {
            Console.WriteLine("Supervisor not found.");
            return;
        }
        context.Supervisors.Remove(supervisor);
        context.SaveChanges();
        Console.WriteLine("Supervisor deleted.");
    }


    //public static void EditStudent(DormitoryContext context, string studentName)
    //{
    //    var student = context.Students.FirstOrDefault(s => s.Name == studentName);
    //    if (student == null)
    //    {
    //        Console.WriteLine("Student not found.");
    //        return;
    //    }

    //    Console.WriteLine("What do you want to edit?");
    //    Console.WriteLine("1. Name");
    //    Console.WriteLine("2. Phone Number");
    //    Console.WriteLine("3. All Information");
    //    Console.Write("--> Your choice: ");
    //    string editChoice = Console.ReadLine();

    //    if (editChoice == "1" || editChoice == "3")
    //    {
    //        Console.Write("New Name: ");
    //        string newName = Console.ReadLine()?.Trim();
    //        if (!string.IsNullOrEmpty(newName))
    //        {
    //            student.Name = newName;
    //        }
    //    }

    //    if (editChoice == "2" || editChoice == "3")
    //    {
    //        Console.Write("New Phone Number: ");
    //        string newPhone = Console.ReadLine()?.Trim();

    //        bool phoneExists =
    //            context.Students.Any(s => s.PhoneNumber == newPhone && s.StudentId != student.StudentId) ||
    //            context.Supervisors.Any(s => s.PhoneNumber == newPhone);

    //        if (phoneExists)
    //        {
    //            Console.WriteLine("This phone number is already registered.");
    //            return;
    //        }

    //        if (!string.IsNullOrEmpty(newPhone))
    //        {
    //            student.PhoneNumber = newPhone;
    //        }
    //    }

    //    context.SaveChanges();
    //    Console.WriteLine("Student edited successfully.");
    //}
    //public static void ShowFullStudentInfo(DormitoryContext context, string studentName)
    //{
    //    var student = context.Students
    //        .Include(s => s.Room)
    //            .ThenInclude(r => r.Block)
    //                .ThenInclude(b => b.Dormitory)
    //        .Include(s => s.Tools)
    //        .FirstOrDefault(s => s.Name == studentName);

    //    if (student == null)
    //    {
    //        Console.WriteLine("Student not found.");
    //        return;
    //    }

    //    Console.WriteLine($"Student Name: {student.Name}");
    //    Console.WriteLine($"Student Number: {student.StudentNumber}");
    //    Console.WriteLine($"National Code: {student.NationalCode}");
    //    Console.WriteLine($"Phone Number: {student.PhoneNumber}");

    //    if (student.Room != null)
    //    {
    //        Console.WriteLine($"Room Number: {student.Room.Number}");
    //        if (student.Room.Block != null)
    //        {
    //            Console.WriteLine($"Block Name: {student.Room.Block.Name}");
    //            if (student.Room.Block.Dormitory != null)
    //            {
    //                Console.WriteLine($"Dormitory Name: {student.Room.Block.Dormitory.Name}");
    //                Console.WriteLine($"Dormitory Address: {student.Room.Block.Dormitory.Address}");
    //            }
    //        }
    //    }

    //    if (student.Tools != null && student.Tools.Any())
    //    {
    //        Console.WriteLine("Tools:");
    //        foreach (var tool in student.Tools)
    //        {
    //            Console.WriteLine($"- Type: {tool.Type}, Part Number: {tool.PartNumber}, Status: {tool.Status}");
    //        }
    //    }
    //    else
    //    {
    //        Console.WriteLine("No tools assigned.");
    //    }
    //}




}
