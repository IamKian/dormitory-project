using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void AddDormitory()
    {
        Console.Write("Dormitory name: ");
        string name = Console.ReadLine()?.Trim();
        Console.Write("Address: ");
        string address = Console.ReadLine()?.Trim();
        Console.Write("Capacity: ");
        if (!double.TryParse(Console.ReadLine(), out double capacity))
        {
            Console.WriteLine(" Invalid capacity");
            return;
        }

        using var context = new DormitoryContext();
        var dormitory = new Dormitory { Name = name, Address = address, Capacity = capacity };
        context.Dormitories.Add(dormitory);
        context.SaveChanges();
        Console.WriteLine("Dormitory added.");
    }
    static void AddBlock()
    {
        Console.Write("Block Name: ");
        string blockName = Console.ReadLine()?.Trim();

        Console.Write("Dormitory Name: ");
        string dormitoryName = Console.ReadLine()?.Trim();

        using var context = new DormitoryContext();

        var dormitory = context.Dormitories.FirstOrDefault(d => d.Name.ToLower() == dormitoryName.ToLower());
        if (dormitory == null)
        {
            Console.WriteLine(" Dormitory not found");
            return;
        }

        var block = new Block { Name = blockName, DormitoryId = dormitory.DormitoryId };
        context.Blocks.Add(block);
        context.SaveChanges();

        Console.WriteLine("Block added");
    }
    static void AddRoom()
    {
        Console.Write("Room Number: ");
        string roomNumber = Console.ReadLine()?.Trim();

        Console.Write("Block Name: ");
        string blockName = Console.ReadLine()?.Trim();

        using var context = new DormitoryContext();

        var block = context.Blocks.FirstOrDefault(b => b.Name.ToLower() == blockName.ToLower());
        if (block == null)
        {
            Console.WriteLine(" Block not found ");
            return;
        }

        var room = new Room { Number = roomNumber, BlockId = block.BlockId };
        context.Rooms.Add(room);
        context.SaveChanges();

        Console.WriteLine("Room added.");
    }
    static void AddStudentFromUser()
    {
        Console.Write("Name: ");
        string name = Console.ReadLine()?.Trim();
        Console.Write("National Code: ");
        string nationalCode = Console.ReadLine()?.Trim();
        Console.Write("Phone Number: ");
        string phoneNumber = Console.ReadLine()?.Trim();
        Console.Write("Student Number: ");
        string studentNumber = Console.ReadLine()?.Trim();
        Console.Write("Dormitory Name: ");
        string dormitoryName = Console.ReadLine()?.Trim();
        Console.Write("Room Number: ");
        string roomNumber = Console.ReadLine()?.Trim();

        using (var context = new DormitoryContext())
        {
            if (string.IsNullOrEmpty(nationalCode) || string.IsNullOrEmpty(phoneNumber))
            {
                Console.WriteLine(" National Code and Phone Number are required.");
                return;
            }

            bool nationalCodeExists = context.Students.Any(s => s.NationalCode == nationalCode)
                                    || context.Supervisors.Any(s => s.NationalCode == nationalCode);

            if (nationalCodeExists)
            {
                Console.WriteLine(" This National Code is already registered.");
                return;
            }

            bool phoneNumberExists = context.Students.Any(s => s.PhoneNumber == phoneNumber)
                                   || context.Supervisors.Any(s => s.PhoneNumber == phoneNumber);

            if (phoneNumberExists)
            {
                Console.WriteLine(" This Phone Number is already registered.");
                return;
            }

            var dormitory = context.Dormitories
                .FirstOrDefault(d => d.Name.ToLower() == dormitoryName.ToLower());
            if (dormitory == null)
            {
                Console.WriteLine(" Dormitory not found.");
                return;
            }

            var room = context.Rooms
                .Include(r => r.Block)
                .FirstOrDefault(r => r.Number.ToLower() == roomNumber.ToLower() && r.Block.DormitoryId == dormitory.DormitoryId);
            if (room == null)
            {
                Console.WriteLine(" Room not found ");
                return;
            }

            var student = new Student
            {
                Name = name,
                NationalCode = nationalCode,
                PhoneNumber = phoneNumber,
                StudentNumber = studentNumber,
                DormitoryId = dormitory.DormitoryId,
                RoomId = room.RoomId
            };
            context.Students.Add(student);
            context.SaveChanges();
            Console.WriteLine("Student added.");
        }
    }
}
