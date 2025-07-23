using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        if (System.IO.File.Exists("Dormitory.db"))
        {
            System.IO.File.Delete("Dormitory.db");
            //Console.WriteLine(" Old database file deleted.");
        }

        using (var ctx = new DormitoryContext())
            ctx.Database.Migrate();

        while (true)
        {
            Console.WriteLine("\nMain Menu:");
            Console.WriteLine("1. Add Dormitory");
            Console.WriteLine("2. Add Block");
            Console.WriteLine("3. Add Room");
            Console.WriteLine("4. Add Tool");
            Console.WriteLine("5. Add Student");
            Console.WriteLine("6. Add Supervisor");
            Console.WriteLine("7. Show All Dormitories");
            Console.WriteLine("8. Show Blocks in Dormitory");
            Console.WriteLine("9. Show Rooms in Block");
            Console.WriteLine("10. Show Students in Room");
            Console.WriteLine("11. Show Tools in Room");
            Console.WriteLine("12. Delete Dormitory");
            Console.WriteLine("13. Delete Block");
            Console.WriteLine("14. Delete Room");
            Console.WriteLine("15. Delete Student");
            Console.WriteLine("16. Delete Tool");
            Console.WriteLine("17. Delete Supervisor");
            Console.WriteLine("0. Exit");
            Console.Write("--> Your choice: ");
            var choice = Console.ReadLine();

            using var context = new DormitoryContext();

            switch (choice)
            {
                case "1": AddDormitory(); break;
                case "2": AddBlock(); break;
                case "3": AddRoom(); break;
                case "4": AddTool(); break;
                case "5": AddStudent(); break;
                case "6": AddSupervisor(); break;
                case "7": ShowDormitories(); break;
                case "8": ShowBlocksInDormitory(); break;
                case "9": ShowRoomsInBlock(); break;
                case "10": ShowStudentsInRoom(); break;
                case "11": ShowToolsInRoom(); break;

                case "12":
                    Console.Write("Dormitory Name to delete: ");
                    var dormName = Console.ReadLine()?.Trim();
                    DeleteOperations.DeleteDormitory(context, dormName);
                    break;

                case "13":
                    Console.Write("Block Name to delete: ");
                    var blockName = Console.ReadLine()?.Trim();
                    DeleteOperations.DeleteBlock(context, blockName);
                    break;

                case "14":
                    Console.Write("Room Number to delete: ");
                    var roomNumber = Console.ReadLine()?.Trim();
                    DeleteOperations.DeleteRoom(context, roomNumber);
                    break;

                case "15":
                    Console.Write("Student Name to delete: ");
                    var studentIdOrName = Console.ReadLine()?.Trim();
                    DeleteOperations.DeleteStudent(context, studentIdOrName);
                    break;

                case "16":
                    Console.Write("Tool Name to delete: ");
                    var toolName = Console.ReadLine()?.Trim();
                    DeleteOperations.DeleteTool(context, toolName);
                    break;

                case "17":
                    Console.Write("Supervisor Phone Number to delete: ");
                    var supervisorPhone = Console.ReadLine()?.Trim();
                    DeleteOperations.DeleteSupervisor(context, supervisorPhone);
                    break;

                case "0": return;

                default:
                    Console.WriteLine(" Invalid choice");
                    break;
            }
        }
    }
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

        Console.WriteLine("Room added ");
    }
    static void AddStudent()
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
                Console.WriteLine(" National Code and Phone Number are required ");
                return;
            }

            bool nationalCodeExists = context.Students.Any(s => s.NationalCode == nationalCode)
                                    || context.Supervisors.Any(s => s.NationalCode == nationalCode);

            if (nationalCodeExists)
            {
                Console.WriteLine(" This National Code is already registered ");
                return;
            }

            bool phoneNumberExists = context.Students.Any(s => s.PhoneNumber == phoneNumber)
                                   || context.Supervisors.Any(s => s.PhoneNumber == phoneNumber);

            if (phoneNumberExists)
            {
                Console.WriteLine(" This Phone Number is already registered ");
                return;
            }

            var dormitory = context.Dormitories
                .FirstOrDefault(d => d.Name.ToLower() == dormitoryName.ToLower());
            if (dormitory == null)
            {
                Console.WriteLine(" Dormitory not found ");
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
            Console.WriteLine("Student added");
        }
    }
    static void AddSupervisor()
    {
        Console.Write("Name: ");
        string name = Console.ReadLine()?.Trim();
        Console.Write("National Code: ");
        string nationalCode = Console.ReadLine()?.Trim();
        Console.Write("Phone Number: ");
        string phoneNumber = Console.ReadLine()?.Trim();

        Console.Write("Supervisor Type (dormitory // block): ");
        string type = Console.ReadLine()?.Trim().ToLower();

        using (var context = new DormitoryContext())
        {
            bool nationalCodeExists = context.Supervisors.Any(s => s.NationalCode == nationalCode)
                                    || context.Students.Any(s => s.NationalCode == nationalCode);
            bool phoneNumberExists = context.Supervisors.Any(s => s.PhoneNumber == phoneNumber)
                                   || context.Students.Any(s => s.PhoneNumber == phoneNumber);

            if (nationalCodeExists)
            {
                Console.WriteLine(" This National Code is already registered.");
                return;
            }

            if (phoneNumberExists)
            {
                Console.WriteLine(" This Phone Number is already registered");
                return;
            }

            var supervisor = new Supervisor
            {
                Name = name,
                NationalCode = nationalCode,
                PhoneNumber = phoneNumber
            };

            if (type == "dormitory")
            {
                Console.Write("Dormitory Name: ");
                var dormitoryName = Console.ReadLine()?.Trim();
                var dormitory = context.Dormitories.FirstOrDefault(d => d.Name.ToLower() == dormitoryName.ToLower());
                if (dormitory == null)
                {
                    Console.WriteLine(" Dormitory not found");
                    return;
                }
                supervisor.DormitoryId = dormitory.DormitoryId;
                supervisor.BlockId = null;
            }
            else if (type == "block")
            {
                Console.Write("Block Name: ");
                var blockName = Console.ReadLine()?.Trim();
                var block = context.Blocks.FirstOrDefault(b => b.Name.ToLower() == blockName.ToLower());
                if (block == null)
                {
                    Console.WriteLine(" Block not found");
                    return;
                }
                supervisor.BlockId = block.BlockId;
                supervisor.DormitoryId = null;
            }
            else
            {
                Console.WriteLine(" Invalid supervisor type");
                return;
            }

            context.Supervisors.Add(supervisor);
            context.SaveChanges();

            Console.WriteLine("Supervisor added");
        }
    }
    static void AddTool()
    {
        var toolTypeCodes = new Dictionary<ToolType, string>
    {
        { ToolType.Ref, "001" },
        { ToolType.Table, "002" },
        { ToolType.Chair, "003" },
        { ToolType.Closet, "004" },
        { ToolType.Bed, "005" }
    };

        Console.WriteLine("Available Tool Types:");
        foreach (var type in Enum.GetValues(typeof(ToolType)))
        {
            Console.WriteLine($"- {type}");
        }

        Console.Write("Enter Tool Type: ");
        string inputType = Console.ReadLine()?.Trim();

        if (!Enum.TryParse<ToolType>(inputType, true, out var selectedType))
        {
            Console.WriteLine(" Invalid Tool Type");
            return;
        }

        Console.Write("Room Number: ");
        string roomNumber = Console.ReadLine()?.Trim();

        Console.Write("Owner Student Name: ");
        string ownerStudentName = Console.ReadLine()?.Trim();

        Console.WriteLine("Available Statuses:");
        foreach (var status in Enum.GetValues(typeof(Status)))
        {
            Console.WriteLine($"- {status}");
        }

        Console.Write("Enter Tool Status: ");
        string inputStatus = Console.ReadLine()?.Trim();

        if (!Enum.TryParse<Status>(inputStatus, true, out var selectedStatus))
        {
            Console.WriteLine(" Invalid Status");
            return;
        }

        using (var context = new DormitoryContext())
        {
            var room = context.Rooms.FirstOrDefault(r => r.Number.ToLower() == roomNumber.ToLower());
            if (room == null)
            {
                Console.WriteLine(" Room not found");
                return;
            }

            var student = context.Students.FirstOrDefault(s => s.Name.ToLower() == ownerStudentName.ToLower() && s.RoomId == room.RoomId);
            if (student == null)
            {
                Console.WriteLine(" Student not found in this room");
                return;
            }

            string prefix = toolTypeCodes[selectedType];
            int count = context.Tools.Count(t => t.Type == selectedType);
            string sequentialNumber = (count + 1).ToString("D4");
            string partNumber = prefix + sequentialNumber;

            var tool = new Tool
            {
                Type = selectedType,
                RoomId = room.RoomId,
                PartNumber = partNumber,
                StudentId = student.StudentId,
                Status = selectedStatus
            };

            context.Tools.Add(tool);
            context.SaveChanges();

            Console.WriteLine($" Tool added with Part Number: {partNumber}, Status: {selectedStatus}, for student: {student.Name} with {student.StudentNumber}");
        }
    }
    static void ShowDormitories()
    {
        using var context = new DormitoryContext();
        var dorms = context.Dormitories.ToList();

        if (!dorms.Any())
        {
            Console.WriteLine(" No dormitories found");
            return;
        }

        Console.WriteLine("\n Dormitories:");
        foreach (var d in dorms)
        {
            Console.WriteLine($" ID: {d.DormitoryId}, Name: {d.Name}, Address: {d.Address}, Capacity: {d.Capacity}");
        }
    }
    static void ShowBlocksInDormitory()
    {
        Console.Write("Dormitory Name: ");
        var name = Console.ReadLine()?.Trim();
        using var ctx = new DormitoryContext();
        var dorm = ctx.Dormitories.Include(d => d.Blocks).FirstOrDefault(d => d.Name.ToLower() == name.ToLower());
        if (dorm == null) { Console.WriteLine(" Dormitory not found."); return; }
        Console.WriteLine($"\n Blocks in {dorm.Name}:");
        if (!dorm.Blocks.Any()) { Console.WriteLine(" No blocks."); return; }
        foreach (var b in dorm.Blocks)
            Console.WriteLine($"-> {b.Name} (ID {b.BlockId})");
    }
    static void ShowRoomsInBlock()
    {
        Console.Write("Block Name: ");
        var name = Console.ReadLine()?.Trim();
        using var ctx = new DormitoryContext();
        var block = ctx.Blocks.Include(b => b.Rooms).FirstOrDefault(b => b.Name.ToLower() == name.ToLower());
        if (block == null) { Console.WriteLine(" Block not found"); return; }
        Console.WriteLine($"\n Rooms in Block {block.Name}:");
        if (!block.Rooms.Any()) { Console.WriteLine(" No rooms"); return; }
        foreach (var r in block.Rooms)
            Console.WriteLine($"-> {r.Number} (ID {r.RoomId})");
    }
    static void ShowStudentsInRoom()
    {
        Console.Write("Room Number: ");
        var num = Console.ReadLine()?.Trim();
        using var ctx = new DormitoryContext();
        var room = ctx.Rooms.Include(r => r.Students).FirstOrDefault(r => r.Number.ToLower() == num.ToLower());
        if (room == null) { Console.WriteLine(" Room not found"); return; }
        Console.WriteLine($"\n Students in Room {room.Number}:");
        if (!room.Students.Any()) { Console.WriteLine(" No students found"); return; }
        foreach (var s in room.Students)
            Console.WriteLine($"-> {s.Name}, Student Num: {s.StudentNumber}, NationalCode: {s.NationalCode}, Phone Num :{s.PhoneNumber}");
    }
    static void ShowToolsInRoom()
    {
        Console.Write("Room Number: ");
        var num = Console.ReadLine()?.Trim();
        using var ctx = new DormitoryContext();
        var room = ctx.Rooms.Include(r => r.Tools).FirstOrDefault(r => r.Number.ToLower() == num.ToLower());
        if (room == null) { Console.WriteLine(" Room not found"); return; }
        Console.WriteLine($"\n Tools in Room {room.Number}:");
        if (!room.Tools.Any()) { Console.WriteLine(" No tools found"); return; }
        foreach (var t in room.Tools)
            Console.WriteLine($"-> {t.Type} | PartNumber: {t.PartNumber} | Owner StudentId: {t.StudentId}");
    }
}
