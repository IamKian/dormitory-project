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
            Console.WriteLine("18. Edit Student");
            Console.WriteLine("19. Show Full Student Info");
            Console.WriteLine("20. Move Student");
            Console.WriteLine("21. Transfer Tool");
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
                    Console.Write("Supervisor Name to delete: ");
                    var supervisorName = Console.ReadLine()?.Trim();
                    DeleteOperations.DeleteSupervisor(context, supervisorName);
                    break;
                case "18":
                    {
                        Console.Write("Student name: ");
                        string studentName = Console.ReadLine()?.Trim();
                        EditStudent(context, studentName);
                        break;
                    }
                case "19":
                    {
                        Console.Write("Student name: ");
                        string studentName2 = Console.ReadLine()?.Trim();
                        ShowFullStudentInfo(context, studentName2);
                        break;
                    }
                case "20":
                    MoveStudent();
                    break;

                case "21":
                    TransferTool();
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
        bool blockExists = dormitory.Blocks.Any(b => b.Name.ToLower() == blockName.ToLower());
        if (blockExists)
        {
            Console.WriteLine($" Error: Block name '{blockName}' already exists in dormitory '{dormitory.Name}'. Each dormitory can only have one block with this name.");
            return;
        }

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
        bool roomExists = block.Rooms.Any(r => r.Number.ToLower() == roomNumber.ToLower());
        if (roomExists)
        {
            Console.WriteLine($" Room number '{roomNumber}' already exists in block '{block.Name}'. Each block can only have one room with a  number.");
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
            int RoomCpacity = context.Students.Count(s => s.RoomId == room.RoomId);
            if (RoomCpacity >= 6)
            {
                Console.WriteLine("  Room is at maximum capacity (6 students). Cannot add more students to this room.");
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
    public static void EditStudent(DormitoryContext context, string studentName)
    {
        var student = context.Students.FirstOrDefault(s => s.Name == studentName);
        if (student == null)
        {
            Console.WriteLine("Student not found.");
            return;
        }

        Console.WriteLine("What do you want to edit?");
        Console.WriteLine("1. Name");
        Console.WriteLine("2. Phone Number");
        Console.WriteLine("3. All Information");
        Console.Write("--> Your choice: ");
        string editChoice = Console.ReadLine();

        if (editChoice == "1" || editChoice == "3")
        {
            Console.Write("New Name: ");
            string newName = Console.ReadLine()?.Trim();
            if (!string.IsNullOrEmpty(newName))
            {
                student.Name = newName;
            }
        }

        if (editChoice == "2" || editChoice == "3")
        {
            Console.Write("New Phone Number: ");
            string newPhone = Console.ReadLine()?.Trim();

            bool phoneExists =
                context.Students.Any(s => s.PhoneNumber == newPhone && s.StudentId != student.StudentId) ||
                context.Supervisors.Any(s => s.PhoneNumber == newPhone);

            if (phoneExists)
            {
                Console.WriteLine("This phone number is already registered.");
                return;
            }

            if (!string.IsNullOrEmpty(newPhone))
            {
                student.PhoneNumber = newPhone;
            }
        }

        context.SaveChanges();
        Console.WriteLine("Student edited successfully.");
    }
    public static void ShowFullStudentInfo(DormitoryContext context, string studentName)
    {
        var student = context.Students
            .Include(s => s.Room)
                .ThenInclude(r => r.Block)
                    .ThenInclude(b => b.Dormitory)
            .Include(s => s.Tools)
            .FirstOrDefault(s => s.Name == studentName);

        if (student == null)
        {
            Console.WriteLine("Student not found.");
            return;
        }

        Console.WriteLine($"Student Name: {student.Name}");
        Console.WriteLine($"Student Number: {student.StudentNumber}");
        Console.WriteLine($"National Code: {student.NationalCode}");
        Console.WriteLine($"Phone Number: {student.PhoneNumber}");

        if (student.Room != null)
        {
            Console.WriteLine($"Room Number: {student.Room.Number}");
            if (student.Room.Block != null)
            {
                Console.WriteLine($"Block Name: {student.Room.Block.Name}");
                if (student.Room.Block.Dormitory != null)
                {
                    Console.WriteLine($"Dormitory Name: {student.Room.Block.Dormitory.Name}");
                    Console.WriteLine($"Dormitory Address: {student.Room.Block.Dormitory.Address}");
                }
            }
        }

        if (student.Tools != null && student.Tools.Any())
        {
            Console.WriteLine("Tools:");
            foreach (var tool in student.Tools)
            {
                Console.WriteLine($"- Type: {tool.Type}, Part Number: {tool.PartNumber}, Status: {tool.Status}");
            }
        }
        else
        {
            Console.WriteLine("No tools assigned.");
        }
    }
    static void MoveStudent()
    {
        using var context = new DormitoryContext();

        Console.Write("Enter Student Num or Name to move: ");
        string studentIdOrName = Console.ReadLine()?.Trim();

        var student = context.Students
            .Include(s => s.Room)
            .ThenInclude(r => r.Block)
            .ThenInclude(b => b.Dormitory)
            .FirstOrDefault(s => s.StudentNumber.ToString() == studentIdOrName || s.Name == studentIdOrName);

        if (student == null)
        {
            Console.WriteLine("Student not found");
            return;
        }

        Console.WriteLine($" location fely: Dormitory: {student.Room.Block.Dormitory.Name}, Block: {student.Room.Block.Name}, Room: {student.Room.Number}");

        Console.Write("Enter new Room Number: ");
        string newRoomNumber = Console.ReadLine()?.Trim();

        var newRoom = context.Rooms
            .Include(r => r.Block)
            .ThenInclude(b => b.Dormitory)
            .FirstOrDefault(r => r.Number == newRoomNumber);

        if (newRoom == null)
        {
            Console.WriteLine("Room not found");
            return;
        }

        student.RoomId = newRoom.RoomId;
        context.SaveChanges();

        Console.WriteLine($"Student {student.Name} moved successfully to Dormitory: {newRoom.Block.Dormitory.Name}, Block: {newRoom.Block.Name}, Room: {newRoom.Number}");
    }
    static void TransferTool()
    {
        using var context = new DormitoryContext();

        Console.Write("Enter Tool Part Number: ");
        string partNumber = Console.ReadLine()?.Trim();

        var tool = context.Tools
            .Include(t => t.Student)
            .Include(t => t.Room)
            .FirstOrDefault(t => t.PartNumber == partNumber);

        if (tool == null)
        {
            Console.WriteLine("Tool not found");
            return;
        }

        Console.WriteLine($"Tool Information: Type: {tool.Type}, Status: {tool.Status}");
        Console.WriteLine($"Current Owner: {tool.Student?.Name ?? "No owner"}");

        Console.Write("Enter New Owner's Name: ");
        string newOwnerName = Console.ReadLine()?.Trim();

        var newOwner = context.Students
            .FirstOrDefault(s => s.Name.ToLower() == newOwnerName.ToLower() && s.RoomId == tool.RoomId);

        if (newOwner == null)
        {
            Console.WriteLine("Student not found in this room");
            return;
        }

        if (newOwner.StudentId == tool.StudentId)
        {
            Console.WriteLine("This student is already the owner of this tool");
            return;
        }

        tool.StudentId = newOwner.StudentId;
        context.SaveChanges();

        Console.WriteLine($"Tool {tool.PartNumber} ({tool.Type}) transferred successfully to {newOwner.Name}");
    }

}
