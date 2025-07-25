using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        //if (System.IO.File.Exists("Dormitory.db"))
        //{
        //    System.IO.File.Delete("Dormitory.db");
        //    Console.WriteLine(" Old database file deleted.");
        //}
        Console.WriteLine("Welcome To Dormitory Mangment System");
        Console.WriteLine("Please Wait a Seconde...");
        
        using (var ctx = new DormitoryContext())
        {
            ctx.Database.EnsureCreated();
        }

        ShowMenu();

        while (true)
        {
            Console.Write("\n---->> ");  
            var choice = Console.ReadLine()?.Trim();

            if (choice?.ToLower() == "help")
            {
                ShowMenu();
                continue;
            }

            if (choice == "0")
                return;

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
                case "12": ShowStudentTools(); break;
                case "13":
                    Console.Write("Dormitory Name to delete: ");
                    var dormName = Console.ReadLine()?.Trim();
                    DeleteOperations.DeleteDormitory(context, dormName);
                    break;
                case "14":
                    Console.Write("Block Name to delete: ");
                    var blockName = Console.ReadLine()?.Trim();
                    DeleteOperations.DeleteBlock(context, blockName);
                    break;
                case "15":
                    Console.Write("Room Number to delete: ");
                    var roomNumber = Console.ReadLine()?.Trim();
                    DeleteOperations.DeleteRoom(context, roomNumber);
                    break;
                case "16":
                    Console.Write("Student Name to delete: ");
                    var studentIdOrName = Console.ReadLine()?.Trim();
                    DeleteOperations.DeleteStudent(context, studentIdOrName);
                    break;
                case "17":
                    Console.Write("Tool Name to delete: ");
                    var toolName = Console.ReadLine()?.Trim();
                    DeleteOperations.DeleteTool(context, toolName);
                    break;
                case "18":
                    Console.Write("Supervisor Name to delete: ");
                    var supervisorName = Console.ReadLine()?.Trim();
                    DeleteOperations.DeleteSupervisor(context, supervisorName);
                    break;
                case "19":
                    {
                        Console.Write("Student name: ");
                        string studentName = Console.ReadLine()?.Trim();
                        EditStudent(context, studentName);
                        break;
                    }
                case "20":
                    {
                        Console.Write("Student name: ");
                        string studentName2 = Console.ReadLine()?.Trim();
                        ShowFullStudentInfo(context, studentName2);
                        break;
                    }
                case "21": MoveStudent(); break;
                case "22": TransferTool(); break;
                case "23": ShowBrokenTools(); break;
                case "24": ShowAvailableRooms(); break;
                case "25": TransferToolBetweenRooms(); break;
                case "26": EditBlock(); break;
                case "27": EditDormitory(); break;
                case "28": EditSupervisor(); break;
                case "29": ShowBlockSupervisors(); break;
                case "30": ShowDormitorySupervisors(); break;
                default:
                    Console.WriteLine("Invalid choice. Type 'help' to see menu.");
                    break;
            }
        }
                 Console.WriteLine("tashakor");
        Console.ReadKey();

    }

    static void ShowMenu()
    {
        Console.Clear();
        Console.WriteLine("Main Menu:");
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
        Console.WriteLine("12. Show Student Tools");
        Console.WriteLine("13. Delete Dormitory");
        Console.WriteLine("14. Delete Block");
        Console.WriteLine("15. Delete Room");
        Console.WriteLine("16. Delete Student");
        Console.WriteLine("17. Delete Tool");
        Console.WriteLine("18. Delete Supervisor");
        Console.WriteLine("19. Edit Student");
        Console.WriteLine("20. Show Full Student Info");
        Console.WriteLine("21. Move Student");
        Console.WriteLine("22. Transfer Tool");
        Console.WriteLine("23. Show Broken Tools");
        Console.WriteLine("24. Show Available Rooms");
        Console.WriteLine("25. Transfer Tool Between Rooms/Students");
        Console.WriteLine("26. Edit Block");
        Console.WriteLine("27. Edit Dormitory");
        Console.WriteLine("28. Edit Supervisor");
        Console.WriteLine("29. Show Block Supervisors");
        Console.WriteLine("30. Show Dormitory Supervisors");
        Console.WriteLine("0. Exit");
        Console.WriteLine("\nType 'help' at any time to see this menu again.");
        Console.WriteLine(new string('=', 40));
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
        using (var context6 = new DormitoryContext())
        {
            bool dormitoryExists = context6.Dormitories.Any(d =>
              d.Name.ToLower() == name.ToLower() &&
              d.Address.ToLower() == address.ToLower());

            if (dormitoryExists)
            {
                Console.WriteLine($"A dormitory with name '{name}' and address '{address}' already exists.");
                return;
            }
        }
           

        using var context = new DormitoryContext();
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(address))
        {
            Console.WriteLine(" Name and Address are required");
            return;
        }
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



        var dormitory = context.Dormitories
        .Include(d => d.Blocks)
        .FirstOrDefault(d => d.Name.ToLower() == dormitoryName.ToLower());

        if (dormitory == null)
        {
            Console.WriteLine("Dormitory not found");
            return;
        }

        bool blockExists = dormitory.Blocks.Any(b => b.Name.ToLower() == blockName.ToLower());

        if (!blockExists)
        {
            blockExists = context.Blocks.Any(b =>
                b.DormitoryId == dormitory.DormitoryId &&
                b.Name.ToLower() == blockName.ToLower());
        }

        if (blockExists)
        {
            Console.WriteLine($"Block name '{blockName}' already exists in dormitory '{dormitory.Name}'. Each dormitory can only have one block with this name.");
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

        if (!roomExists)
        {
            roomExists = context.Rooms.Any(r =>
                r.BlockId == block.BlockId &&
                r.Number.ToLower() == roomNumber.ToLower());
        }

        if (roomExists)
        {
            Console.WriteLine($"Room number '{roomNumber}' already exists in block '{block.Name}'. Each block can only have one room with this number.");
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

            int? studentId = null;
            string studentName = null;
            string studentNumber = null;

            if (selectedType != ToolType.Ref)
            {
                Console.Write("Owner Student Name: ");
                string ownerStudentName = Console.ReadLine()?.Trim();

                var student = context.Students.FirstOrDefault(s => s.Name.ToLower() == ownerStudentName.ToLower() && s.RoomId == room.RoomId);
                if (student == null)
                {
                    Console.WriteLine(" Student not found in this room");
                    return;
                }

                studentId = student.StudentId;
                studentName = student.Name;
                studentNumber = student.StudentNumber;
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
                StudentId = studentId, 
                Status = selectedStatus
            };

            context.Tools.Add(tool);
            context.SaveChanges();

            if (selectedType == ToolType.Ref)
            {
                Console.WriteLine($" Tool added with Part Number: {partNumber}, Status: {selectedStatus}, Room: {room.Number}");
            }
            else
            {
                Console.WriteLine($" Tool added with Part Number: {partNumber}, Status: {selectedStatus}, for student: {studentName} with student num  {studentNumber}");
            }
        }
    }
    static void ShowDormitories()
    {
        using var context = new DormitoryContext();
        var dorms = context.Dormitories
            .Include(d => d.Supervisors)  
            .ToList();

        if (!dorms.Any())
        {
            Console.WriteLine(" No dormitories found");
            return;
        }

        Console.WriteLine("\n Dormitories:");
        foreach (var d in dorms)
        {
            Console.WriteLine($" ID: {d.DormitoryId}, Name: {d.Name}, Address: {d.Address}, Capacity: {d.Capacity}");

            if (d.Supervisors != null && d.Supervisors.Any())
            {
                Console.WriteLine("   Supervisors:");
                foreach (var supervisor in d.Supervisors)
                {
                    Console.WriteLine($"     {supervisor.Name} (Phone: {supervisor.PhoneNumber})");
                }
            }
            else
            {
                Console.WriteLine("   No supervisors assigned");
            }
        }
    }
    static void ShowBlocksInDormitory()
    {
        Console.Write("Dormitory Name: ");
        var name = Console.ReadLine()?.Trim();

        using var ctx = new DormitoryContext();
        var dorm = ctx.Dormitories
            .Include(d => d.Blocks)
                .ThenInclude(b => b.Supervisors)
            .FirstOrDefault(d => d.Name.ToLower() == name.ToLower());

        if (dorm == null)
        {
            Console.WriteLine(" Dormitory not found.");
            return;
        }

        Console.WriteLine($"\n Blocks in {dorm.Name}:");

        if (!dorm.Blocks.Any())
        {
            Console.WriteLine(" No blocks.");
            return;
        }

        foreach (var b in dorm.Blocks)
        {
            Console.WriteLine($"->  {b.BlockId}.  {b.Name} ");

            if (b.Supervisors != null && b.Supervisors.Any())
            {
                Console.WriteLine("   Supervisors:");
                foreach (var supervisor in b.Supervisors)
                {
                    Console.WriteLine($"   - {supervisor.Name} (Phone: {supervisor.PhoneNumber})");
                }
            }
            else
            {
                Console.WriteLine("   No supervisors assigned");
            }
        }
    }

    
    static void ShowRoomsInBlock()
    {
        Console.Write("Block Name: ");
        var name = Console.ReadLine()?.Trim();

        using var ctx = new DormitoryContext();
        var block = ctx.Blocks
            .Include(b => b.Rooms)
                .ThenInclude(r => r.Students)
            .Include(b => b.Rooms)
                .ThenInclude(r => r.Tools)
            .FirstOrDefault(b => b.Name.ToLower() == name.ToLower());

        if (block == null)
        {
            Console.WriteLine(" Block not found");
            return;
        }

        Console.WriteLine($"\n Rooms in Block {block.Name}:");

        if (!block.Rooms.Any())
        {
            Console.WriteLine(" No rooms");
            return;
        }

        foreach (var r in block.Rooms)
        {
            Console.WriteLine($"->  {r.RoomId}.  {r.Number}  ");

            int studentCount = r.Students?.Count ?? 0;
            int toolCount = r.Tools?.Count ?? 0;
            Console.WriteLine($"   Students: {studentCount}, Tools: {toolCount}");
        }
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
    static void ShowBrokenTools()
    {
        using var context = new DormitoryContext();
        var brokenTools = context.Tools
            .Include(t => t.Room)
                .ThenInclude(r => r.Block)
                    .ThenInclude(b => b.Dormitory)
            .Include(t => t.Student)
            .Where(t => t.Status == Status.Defective || t.Status == Status.UnderRepair)
            .ToList();

        if (!brokenTools.Any())
        {
            Console.WriteLine("No broken or under repair tools found.");
            return;
        }

        Console.WriteLine("\nList of broken and under repair tools:");

        foreach (var tool in brokenTools)
        {
            Console.WriteLine($"Type: {tool.Type} | Part Number: {tool.PartNumber} | Status: {tool.Status}");
            Console.WriteLine($"Location: Dormitory {tool.Room.Block.Dormitory.Name}, Block {tool.Room.Block.Name}, Room {tool.Room.Number}");
            Console.WriteLine($"Owner: {tool.Student?.Name ?? "No owner"}");
        }
    }

    static void ShowAvailableRooms()
    {
        Console.WriteLine("Showing available rooms with space:");

        using var context = new DormitoryContext();

        var availableRooms = context.Rooms
            .Include(r => r.Block)
            .ThenInclude(b => b.Dormitory)
            .ToList()
            .Select(r => new
            {
                Room = r,
                StudentCount = context.Students.Count(s => s.RoomId == r.RoomId),
                AvailableSpace = 6 - context.Students.Count(s => s.RoomId == r.RoomId)
            })
            .Where(r => r.AvailableSpace > 0)
            .OrderBy(r => r.Room.Block.Dormitory.Name)
            .ThenBy(r => r.Room.Block.Name)
            .ThenBy(r => r.Room.Number)
            .ToList();

        if (!availableRooms.Any())
        {
            Console.WriteLine("No rooms with available space found.");
            return;
        }

        string currentDormitory = "";
        string currentBlock = "";

        foreach (var room in availableRooms)
        {
            if (room.Room.Block.Dormitory.Name != currentDormitory)
            {
                currentDormitory = room.Room.Block.Dormitory.Name;
                Console.WriteLine($"\nDormitory: {currentDormitory}");
                currentBlock = "";
            }

            if (room.Room.Block.Name != currentBlock)
            {
                currentBlock = room.Room.Block.Name;
                Console.WriteLine($"  Block: {currentBlock}");
            }

            Console.WriteLine($"    Room: {room.Room.Number}, Available Spaces: {room.AvailableSpace}");
        }
    }

    static void ShowStudentTools()
    {
        Console.Write("Enter Student Name or Number: ");
        string studentIdentifier = Console.ReadLine()?.Trim();

        using var context = new DormitoryContext();

        var student = context.Students
            .Include(s => s.Room)
                .ThenInclude(r => r.Block)
                    .ThenInclude(b => b.Dormitory)
            .FirstOrDefault(s =>
                s.Name.ToLower() == studentIdentifier.ToLower() ||
                s.StudentNumber.ToLower() == studentIdentifier.ToLower());

        if (student == null)
        {
            Console.WriteLine("Student not found.");
            return;
        }

        var tools = context.Tools
            .Where(t => t.StudentId == student.StudentId)
            .ToList();

        Console.WriteLine($"\nTools owned by {student.Name} (Student Number: {student.StudentNumber}):");
        Console.WriteLine($"Location: Dormitory: {student.Room.Block.Dormitory.Name}, Block: {student.Room.Block.Name}, Room: {student.Room.Number}");

        if (!tools.Any())
        {
            Console.WriteLine("This student doesn't own any tools.");
            return;
        }

        Console.WriteLine("\nPersonal Tools:");
        foreach (var tool in tools)
        {
            Console.WriteLine($"- Type: {tool.Type}");
            Console.WriteLine($"  Part Number: {tool.PartNumber}");
            Console.WriteLine($"  Status: {tool.Status}");
            Console.WriteLine();
        }

        var sharedTools = context.Tools
            .Where(t => t.RoomId == student.RoomId && t.StudentId == null)
            .ToList();

        if (sharedTools.Any())
        {
            Console.WriteLine("\nShared Room Tools (not assigned to any student):");
            foreach (var tool in sharedTools)
            {
                Console.WriteLine($"- Type: {tool.Type}");
                Console.WriteLine($"  Part Number: {tool.PartNumber}");
                Console.WriteLine($"  Status: {tool.Status}");
                Console.WriteLine();
            }
        }
    }

    static void TransferToolBetweenRooms()
    {
        using var context = new DormitoryContext();

        Console.Write("Enter Tool Part Number to transfer: ");
        string partNumber = Console.ReadLine()?.Trim();

        var tool = context.Tools
            .Include(t => t.Student)
            .Include(t => t.Room)
                .ThenInclude(r => r.Block)
                    .ThenInclude(b => b.Dormitory)
            .FirstOrDefault(t => t.PartNumber == partNumber);

        if (tool == null)
        {
            Console.WriteLine("Tool not found");
            return;
        }

        Console.WriteLine($"\nTool Information:");
        Console.WriteLine($"Type: {tool.Type}");
        Console.WriteLine($"Part Number: {tool.PartNumber}");
        Console.WriteLine($"Status: {tool.Status}");
        Console.WriteLine($"Current Room: {tool.Room.Number} in Block {tool.Room.Block.Name}");
        Console.WriteLine($"Current Owner: {tool.Student?.Name ?? "No owner (shared tool)"}");

        Console.WriteLine("\nWhere do you want to transfer this tool?");
        Console.WriteLine("1. To another room (and optionally a new student)");
        Console.WriteLine("2. To another student in the same room");
        Console.Write("--> Your choice: ");

        string transferChoice = Console.ReadLine()?.Trim();

        if (transferChoice == "1")
        {
            Console.Write("\nEnter destination Room Number: ");
            string newRoomNumber = Console.ReadLine()?.Trim();

            var newRoom = context.Rooms
                .Include(r => r.Block)
                    .ThenInclude(b => b.Dormitory)
                .FirstOrDefault(r => r.Number.ToLower() == newRoomNumber.ToLower());

            if (newRoom == null)
            {
                Console.WriteLine("Destination room not found");
                return;
            }

            if (newRoom.RoomId == tool.RoomId)
            {
                Console.WriteLine("This is the same room. No transfer needed.");
                return;
            }

            Console.Write("Assign to a specific student? (y/n): ");
            string assignChoice = Console.ReadLine()?.Trim().ToLower();

            if (assignChoice == "y" || assignChoice == "yes")
            {
                var studentsInRoom = context.Students
                    .Where(s => s.RoomId == newRoom.RoomId)
                    .ToList();

                if (!studentsInRoom.Any())
                {
                    Console.WriteLine("No students found in the destination room.");
                    Console.WriteLine("Tool will be transferred as a shared room tool.");
                    tool.StudentId = null;
                }
                else
                {
                    Console.WriteLine("\nStudents in destination room:");
                    foreach (var s in studentsInRoom)
                    {
                        Console.WriteLine($"- {s.Name} (Student Number: {s.StudentNumber})");
                    }

                    Console.Write("\nEnter the name of the student to assign this tool to: ");
                    string newOwnerName = Console.ReadLine()?.Trim();

                    var newOwner = studentsInRoom.FirstOrDefault(s =>
                        s.Name.ToLower() == newOwnerName.ToLower());

                    if (newOwner == null)
                    {
                        Console.WriteLine("Student not found in destination room.");
                        Console.WriteLine("Tool will be transferred as a shared room tool.");
                        tool.StudentId = null;
                    }
                    else
                    {
                        tool.StudentId = newOwner.StudentId;
                        Console.WriteLine($"Tool will be assigned to {newOwner.Name}");
                    }
                }
            }
            else
            {
                tool.StudentId = null;
            }

            tool.RoomId = newRoom.RoomId;
            context.SaveChanges();

            Console.WriteLine($"Tool {tool.PartNumber} ({tool.Type}) successfully transferred to Room {newRoom.Number} in Block {newRoom.Block.Name}");
        }
        else if (transferChoice == "2")
        {
            if (tool.Type == ToolType.Ref)
            {
                Console.WriteLine("Refrigerators are shared tools and cannot be assigned to specific students.");
                return;
            }

            var studentsInRoom = context.Students
                .Where(s => s.RoomId == tool.RoomId)
                .ToList();

            if (studentsInRoom.Count <= 1)
            {
                Console.WriteLine("No other students found in this room to transfer the tool to.");
                return;
            }

            Console.WriteLine("\nStudents in the room:");
            foreach (var s in studentsInRoom)
            {
                if (s.StudentId == tool.StudentId)
                {
                    Console.WriteLine($"- {s.Name} (Student Number: {s.StudentNumber}) [CURRENT OWNER]");
                }
                else
                {
                    Console.WriteLine($"- {s.Name} (Student Number: {s.StudentNumber})");
                }
            }

            Console.Write("\nEnter the name of the new owner: ");
            string newOwnerName = Console.ReadLine()?.Trim();

            var newOwner = studentsInRoom.FirstOrDefault(s =>
                s.Name.ToLower() == newOwnerName.ToLower() && s.StudentId != tool.StudentId);

            if (newOwner == null)
            {
                Console.WriteLine("New owner not found or is the same as the current owner.");
                return;
            }

            tool.StudentId = newOwner.StudentId;
            context.SaveChanges();

            Console.WriteLine($"Tool {tool.PartNumber} ({tool.Type}) successfully transferred to {newOwner.Name}");
        }
        else
        {
            Console.WriteLine("Invalid choice");
        }
    }

    static void EditBlock()
    {
        Console.Write("Enter Block Name to edit: ");
        string blockName = Console.ReadLine()?.Trim();

        using var context = new DormitoryContext();

        var block = context.Blocks
            .Include(b => b.Dormitory)
            .FirstOrDefault(b => b.Name.ToLower() == blockName.ToLower());

        if (block == null)
        {
            Console.WriteLine("Block not found.");
            return;
        }

        Console.WriteLine($"\nCurrent Block Information:");
        Console.WriteLine($"Name: {block.Name}");
        Console.WriteLine($"Dormitory: {block.Dormitory.Name}");

        Console.WriteLine("\nWhat do you want to edit?");
        Console.WriteLine("1. Name");
        Console.WriteLine("2. Dormitory (move block to another dormitory)");
        Console.WriteLine("3. Both");
        Console.Write("--> Your choice: ");

        string editChoice = Console.ReadLine()?.Trim();

        if (editChoice == "1" || editChoice == "3")
        {
            Console.Write("New Block Name: ");
            string newBlockName = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(newBlockName))
            {
                Console.WriteLine("Block name cannot be empty.");
            }
            else
            {
                bool blockNameExists = context.Blocks.Any(b =>
                    b.Name.ToLower() == newBlockName.ToLower() &&
                    b.DormitoryId == block.DormitoryId &&
                    b.BlockId != block.BlockId);

                if (blockNameExists)
                {
                    Console.WriteLine($"A block with name '{newBlockName}' already exists in this dormitory.");
                }
                else
                {
                    block.Name = newBlockName;
                    Console.WriteLine($"Block name updated to '{newBlockName}'.");
                }
            }
        }

        if (editChoice == "2" || editChoice == "3")
        {
            Console.Write("New Dormitory Name: ");
            string newDormitoryName = Console.ReadLine()?.Trim();

            var newDormitory = context.Dormitories.FirstOrDefault(d =>
                d.Name.ToLower() == newDormitoryName.ToLower());

            if (newDormitory == null)
            {
                Console.WriteLine("Dormitory not found.");
            }
            else if (newDormitory.DormitoryId == block.DormitoryId)
            {
                Console.WriteLine("Block is already in this dormitory.");
            }
            else
            {
                bool blockExistsInNewDorm = context.Blocks.Any(b =>
                    b.Name.ToLower() == block.Name.ToLower() &&
                    b.DormitoryId == newDormitory.DormitoryId);

                if (blockExistsInNewDorm)
                {
                    Console.WriteLine($"A block with name '{block.Name}' already exists in dormitory '{newDormitory.Name}'.");
                    Console.WriteLine("Please change the block name first before moving to another dormitory.");
                }
                else
                {
                    block.DormitoryId = newDormitory.DormitoryId;
                    Console.WriteLine($"Block moved to dormitory '{newDormitory.Name}'.");
                }
            }
        }

        if (editChoice == "1" || editChoice == "2" || editChoice == "3")
        {
            context.SaveChanges();
            Console.WriteLine("Block edited successfully.");
        }
        else
        {
            Console.WriteLine("Invalid choice. No changes were made.");
        }
    }
    static void EditDormitory()
    {
        Console.Write("Enter Dormitory Name to edit: ");
        string dormitoryName = Console.ReadLine()?.Trim();

        using var context = new DormitoryContext();

        var dormitory = context.Dormitories
            .FirstOrDefault(d => d.Name.ToLower() == dormitoryName.ToLower());

        if (dormitory == null)
        {
            Console.WriteLine("Dormitory not found.");
            return;
        }

        Console.WriteLine($"\nCurrent Dormitory Information:");
        Console.WriteLine($"Name: {dormitory.Name}");
        Console.WriteLine($"Address: {dormitory.Address}");
        Console.WriteLine($"Capacity: {dormitory.Capacity}");

        Console.WriteLine("\nWhat do you want to edit?");
        Console.WriteLine("1. Name");
        Console.WriteLine("2. Address");
        Console.WriteLine("3. Capacity");
        Console.WriteLine("4. All Information");
        Console.Write("--> Your choice: ");

        string editChoice = Console.ReadLine()?.Trim();

        if (editChoice == "1" || editChoice == "4")
        {
            Console.Write("New Dormitory Name: ");
            string newName = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(newName))
            {
                Console.WriteLine("Dormitory name cannot be empty.");
            }
            else
            {
                bool nameExists = context.Dormitories.Any(d =>
                    d.Name.ToLower() == newName.ToLower() &&
                    d.DormitoryId != dormitory.DormitoryId);

                if (nameExists)
                {
                    Console.WriteLine($"A dormitory with name '{newName}' already exists.");
                }
                else
                {
                    dormitory.Name = newName;
                    Console.WriteLine($"Dormitory name updated to '{newName}'.");
                }
            }
        }

        if (editChoice == "2" || editChoice == "4")
        {
            Console.Write("New Address: ");
            string newAddress = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(newAddress))
            {
                Console.WriteLine("Address cannot be empty.");
            }
            else
            {
                bool addressExists = context.Dormitories.Any(d =>
                    d.Name.ToLower() == dormitory.Name.ToLower() &&
                    d.Address.ToLower() == newAddress.ToLower() &&
                    d.DormitoryId != dormitory.DormitoryId);

                if (addressExists)
                {
                    Console.WriteLine($"A dormitory with name '{dormitory.Name}' and address '{newAddress}' already exists.");
                }
                else
                {
                    dormitory.Address = newAddress;
                    Console.WriteLine($"Address updated to '{newAddress}'.");
                }
            }
        }

        if (editChoice == "3" || editChoice == "4")
        {
            Console.Write("New Capacity: ");
            string capacityInput = Console.ReadLine()?.Trim();

            if (double.TryParse(capacityInput, out double newCapacity))
            {
                int currentOccupancy = context.Students.Count(s => s.DormitoryId == dormitory.DormitoryId);

                if (newCapacity < currentOccupancy)
                {
                    Console.WriteLine($"Cannot set capacity to {newCapacity} as there are currently {currentOccupancy} students in this dormitory.");
                }
                else
                {
                    dormitory.Capacity = newCapacity;
                    Console.WriteLine($"Capacity updated to {newCapacity}.");
                }
            }
            else
            {
                Console.WriteLine("Invalid capacity value. Capacity not updated.");
            }
        }

        if (editChoice == "1" || editChoice == "2" || editChoice == "3" || editChoice == "4")
        {
            context.SaveChanges();
            Console.WriteLine("Dormitory edited successfully.");
        }
        else
        {
            Console.WriteLine("Invalid choice. No changes were made.");
        }
    }

    static void EditSupervisor()
    {
        Console.Write("Enter Supervisor Name to edit: ");
        string supervisorName = Console.ReadLine()?.Trim();

        using var context = new DormitoryContext();

        var supervisor = context.Supervisors
            .Include(s => s.Dormitory)
            .Include(s => s.Block)
            .FirstOrDefault(s => s.Name.ToLower() == supervisorName.ToLower());

        if (supervisor == null)
        {
            Console.WriteLine("Supervisor not found.");
            return;
        }

        Console.WriteLine($"\nCurrent Supervisor Information:");
        Console.WriteLine($"Name: {supervisor.Name}");
        Console.WriteLine($"National Code: {supervisor.NationalCode}");
        Console.WriteLine($"Phone Number: {supervisor.PhoneNumber}");

        string assignmentType = supervisor.DormitoryId != null ? "Dormitory" : "Block";
        string assignedTo = supervisor.DormitoryId != null
            ? supervisor.Dormitory?.Name ?? "Unknown Dormitory"
            : supervisor.Block?.Name ?? "Unknown Block";

        Console.WriteLine($"Assignment Type: {assignmentType}");
        Console.WriteLine($"Assigned To: {assignedTo}");

        Console.WriteLine("\nWhat do you want to edit?");
        Console.WriteLine("1. Name");
        Console.WriteLine("2. Phone Number");
        Console.WriteLine("3. Assignment (Dormitory/Block)");
        Console.WriteLine("4. All Information");
        Console.Write("--> Your choice: ");

        string editChoice = Console.ReadLine()?.Trim();

        if (editChoice == "1" || editChoice == "4")
        {
            Console.Write("New Name: ");
            string newName = Console.ReadLine()?.Trim();

            if (!string.IsNullOrEmpty(newName))
            {
                supervisor.Name = newName;
                Console.WriteLine($"Name updated to '{newName}'.");
            }
            else
            {
                Console.WriteLine("Name cannot be empty. Name not updated.");
            }
        }

        if (editChoice == "2" || editChoice == "4")
        {
            Console.Write("New Phone Number: ");
            string newPhone = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(newPhone))
            {
                Console.WriteLine("Phone number cannot be empty.");
            }
            else
            {
                bool phoneExists =
                    context.Supervisors.Any(s => s.PhoneNumber == newPhone && s.SupervisorId != supervisor.SupervisorId) ||
                    context.Students.Any(s => s.PhoneNumber == newPhone);

                if (phoneExists)
                {
                    Console.WriteLine("This phone number is already registered by another person.");
                }
                else
                {
                    supervisor.PhoneNumber = newPhone;
                    Console.WriteLine($"Phone number updated to '{newPhone}'.");
                }
            }
        }

        if (editChoice == "3" || editChoice == "4")
        {
            Console.WriteLine("\nChange assignment to:");
            Console.WriteLine("1. Dormitory supervisor");
            Console.WriteLine("2. Block supervisor");
            Console.Write("--> Your choice: ");

            string assignmentChoice = Console.ReadLine()?.Trim();

            if (assignmentChoice == "1")
            {
                Console.Write("Enter Dormitory Name: ");
                string dormitoryName = Console.ReadLine()?.Trim();

                var dormitory = context.Dormitories.FirstOrDefault(d =>
                    d.Name.ToLower() == dormitoryName.ToLower());

                if (dormitory == null)
                {
                    Console.WriteLine("Dormitory not found.");
                }
                else
                {
                    bool hasSupervisor = context.Supervisors.Any(s =>
                        s.DormitoryId == dormitory.DormitoryId &&
                        s.SupervisorId != supervisor.SupervisorId);

                    if (hasSupervisor)
                    {
                        Console.WriteLine($"Dormitory '{dormitory.Name}' already has a supervisor assigned.");
                    }
                    else
                    {
                        supervisor.DormitoryId = dormitory.DormitoryId;
                        supervisor.BlockId = null;
                        Console.WriteLine($"Supervisor assigned to dormitory '{dormitory.Name}'.");
                    }
                }
            }
            else if (assignmentChoice == "2")
            {
                Console.Write("Enter Block Name: ");
                string blockName = Console.ReadLine()?.Trim();

                var block = context.Blocks.FirstOrDefault(b =>
                    b.Name.ToLower() == blockName.ToLower());

                if (block == null)
                {
                    Console.WriteLine("Block not found.");
                }
                else
                {
                    bool hasSupervisor = context.Supervisors.Any(s =>
                        s.BlockId == block.BlockId &&
                        s.SupervisorId != supervisor.SupervisorId);

                    if (hasSupervisor)
                    {
                        Console.WriteLine($"Block '{block.Name}' already has a supervisor assigned.");
                    }
                    else
                    {
                        supervisor.BlockId = block.BlockId;
                        supervisor.DormitoryId = null;
                        Console.WriteLine($"Supervisor assigned to block '{block.Name}'.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid choice. Assignment not updated.");
            }
        }

        if (editChoice == "1" || editChoice == "2" || editChoice == "3" || editChoice == "4")
        {
            context.SaveChanges();
            Console.WriteLine("Supervisor edited successfully.");
        }
        else
        {
            Console.WriteLine("Invalid choice. No changes were made.");
        }
    }

    static void ShowBlockSupervisors()
    {
        using var context = new DormitoryContext();

        Console.WriteLine("\nBlock Supervisors:");

        Console.Write("Enter Block Name (or press Enter to show all blocks): ");
        string blockName = Console.ReadLine()?.Trim();

        if (!string.IsNullOrEmpty(blockName))
        {
            var block = context.Blocks
                .Include(b => b.Dormitory)
                .Include(b => b.Supervisors)
                .FirstOrDefault(b => b.Name.ToLower() == blockName.ToLower());

            if (block == null)
            {
                Console.WriteLine("Block not found.");
                return;
            }

            Console.WriteLine($"\nBlock: {block.Name} (in Dormitory: {block.Dormitory.Name})");

            var blockSupervisors = block.Supervisors.ToList();
            if (blockSupervisors.Any())
            {
                Console.WriteLine("Supervisors:");
                foreach (var supervisor in blockSupervisors)
                {
                    Console.WriteLine($"- Name: {supervisor.Name}");
                    Console.WriteLine($"  Phone: {supervisor.PhoneNumber}");
                    Console.WriteLine($"  National Code: {supervisor.NationalCode}");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("No supervisors assigned to this block.");
            }

            var dormitorySupervisors = context.Supervisors
                .Where(s => s.DormitoryId == block.DormitoryId)
                .ToList();

            if (dormitorySupervisors.Any())
            {
                Console.WriteLine("Dormitory Supervisors (who also oversee this block):");
                foreach (var supervisor in dormitorySupervisors)
                {
                    Console.WriteLine($"- Name: {supervisor.Name}");
                    Console.WriteLine($"  Phone: {supervisor.PhoneNumber}");
                    Console.WriteLine($"  National Code: {supervisor.NationalCode}");
                    Console.WriteLine();
                }
            }
        }
        else
        {
            var blocks = context.Blocks
                .Include(b => b.Dormitory)
                .Include(b => b.Supervisors)
                .OrderBy(b => b.Dormitory.Name)
                .ThenBy(b => b.Name)
                .ToList();

            if (!blocks.Any())
            {
                Console.WriteLine("No blocks found.");
                return;
            }

            string currentDormitory = "";

            foreach (var block in blocks)
            {
                if (block.Dormitory.Name != currentDormitory)
                {
                    currentDormitory = block.Dormitory.Name;
                    Console.WriteLine($"\nDormitory: {currentDormitory}");

                    var dormitorySupervisors = context.Supervisors
                        .Where(s => s.DormitoryId == block.DormitoryId)
                        .ToList();

                    if (dormitorySupervisors.Any())
                    {
                        Console.WriteLine("Dormitory Supervisors:");
                        foreach (var supervisor in dormitorySupervisors)
                        {
                            Console.WriteLine($"- {supervisor.Name} (Phone: {supervisor.PhoneNumber})");
                        }
                        Console.WriteLine();
                    }
                }

                Console.WriteLine($"Block: {block.Name}");

                var blockSupervisors = block.Supervisors.ToList();
                if (blockSupervisors.Any())
                {
                    Console.WriteLine("  Block Supervisors:");
                    foreach (var supervisor in blockSupervisors)
                    {
                        Console.WriteLine($"  - {supervisor.Name} (Phone: {supervisor.PhoneNumber})");
                    }
                }
                else
                {
                    Console.WriteLine("  No specific block supervisors assigned.");
                }
                Console.WriteLine();
            }
        }
    }
    static void ShowDormitorySupervisors()
    {
        using var context = new DormitoryContext();

        Console.WriteLine("\nDormitory Supervisors:");

        Console.Write("Enter Dormitory Name (or press Enter to show all dormitories): ");
        string dormitoryName = Console.ReadLine()?.Trim();

        if (!string.IsNullOrEmpty(dormitoryName))
        {
            var dormitory = context.Dormitories
                .Include(d => d.Supervisors)
                .Include(d => d.Blocks)
                    .ThenInclude(b => b.Supervisors)
                .FirstOrDefault(d => d.Name.ToLower() == dormitoryName.ToLower());

            if (dormitory == null)
            {
                Console.WriteLine("Dormitory not found.");
                return;
            }

            Console.WriteLine($"\nDormitory: {dormitory.Name} (Address: {dormitory.Address})");

            var dormitorySupervisors = dormitory.Supervisors.ToList();
            if (dormitorySupervisors.Any())
            {
                Console.WriteLine("\nDormitory-level Supervisors:");
                foreach (var supervisor in dormitorySupervisors)
                {
                    Console.WriteLine($"- Name: {supervisor.Name}");
                    Console.WriteLine($"  Phone: {supervisor.PhoneNumber}");
                    Console.WriteLine($"  National Code: {supervisor.NationalCode}");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("\nNo dormitory-level supervisors assigned.");
            }

            Console.WriteLine("\nBlock-level Supervisors:");
            bool hasBlockSupervisors = false;

            foreach (var block in dormitory.Blocks)
            {
                var blockSupervisors = block.Supervisors.ToList();
                if (blockSupervisors.Any())
                {
                    hasBlockSupervisors = true;
                    Console.WriteLine($"Block: {block.Name}");
                    foreach (var supervisor in blockSupervisors)
                    {
                        Console.WriteLine($"- Name: {supervisor.Name}");
                        Console.WriteLine($"  Phone: {supervisor.PhoneNumber}");
                        Console.WriteLine($"  National Code: {supervisor.NationalCode}");
                        Console.WriteLine();
                    }
                }
            }

            if (!hasBlockSupervisors)
            {
                Console.WriteLine("No block-level supervisors in this dormitory.");
            }
        }
        else
        {
            var dormitories = context.Dormitories
                .Include(d => d.Supervisors)
                .Include(d => d.Blocks)
                    .ThenInclude(b => b.Supervisors)
                .OrderBy(d => d.Name)
                .ToList();

            if (!dormitories.Any())
            {
                Console.WriteLine("No dormitories found.");
                return;
            }

            foreach (var dormitory in dormitories)
            {
                Console.WriteLine($"\nDormitory: {dormitory.Name} ");

                var dormitorySupervisors = dormitory.Supervisors.ToList();
                if (dormitorySupervisors.Any())
                {
                    Console.WriteLine("\nDormitory-level Supervisors:");
                    foreach (var supervisor in dormitorySupervisors)
                    {
                        Console.WriteLine($"- {supervisor.Name} (Phone: {supervisor.PhoneNumber})");
                    }
                }
                else
                {
                    Console.WriteLine("\nNo dormitory-level supervisors assigned.");
                }

                bool hasBlockSupervisors = false;

                foreach (var block in dormitory.Blocks)
                {
                    var blockSupervisors = block.Supervisors.ToList();
                    if (blockSupervisors.Any())
                    {
                        if (!hasBlockSupervisors)
                        {
                            Console.WriteLine("\nBlock-level Supervisors:");
                            hasBlockSupervisors = true;
                        }

                        Console.WriteLine($"Block: {block.Name}");
                        foreach (var supervisor in blockSupervisors)
                        {
                            Console.WriteLine($"- {supervisor.Name} (Phone: {supervisor.PhoneNumber})");
                        }
                    }
                }

                if (!hasBlockSupervisors)
                {
                    Console.WriteLine("\nNo block-level supervisors in this dormitory.");
                }

            }
        }
    }

}
