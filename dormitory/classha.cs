public class Dormitory
{
    public int DormitoryId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public int Capacity { get; set; }
    public List<Block> Blocks { get; set; } = new();
    public List<Student> Students { get; set; } = new();
    public List<Supervisor> Supervisors { get; set; } = new();
}
public class Block
{
    public int BlockId { get; set; }
    public string Name { get; set; }
    public int DormitoryId { get; set; }
    public List<Room> Rooms { get; set; } = new();
    public List<Supervisor> Supervisors { get; set; } = new();
}
public class Room
{
    public int RoomId { get; set; }
    public string Number { get; set; }
    public int BlockId { get; set; }
    public List<Tool> Tools { get; set; } = new();
    public List<Student> Students { get; set; } = new();
}
public enum ToolType
{
    Ref,
    Table,
    Chair,
    Closet,
    Bed
}
public enum Status
{
    Healthy,
    Defective,
    UnderRepair
}


public class Tool
{
    public int ToolId { get; set; }
    public ToolType Type { get; set; }
    public string PartNumber { get; set; }
    public int RoomId { get; set; }
    public Status Status { get; set; } = Status.Healthy;
}
public class Student
{
    public int StudentId { get; set; }
    public string Name { get; set; }
    public string NationalCode { get; set; }
    public string PhoneNumber { get; set; }
    public string StudentNumber { get; set; }

    public int DormitoryId { get; set; }
    public int RoomId { get; set; }

}
public class Supervisor
{
    public int SupervisorId { get; set; }
    public string Name { get; set; }
    public string NationalCode { get; set; }
    public string PhoneNumber { get; set; }
}