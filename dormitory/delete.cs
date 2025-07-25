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

}
