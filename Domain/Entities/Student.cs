namespace Domain.Entities;

public class Student
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LaastName { get; set; }
    public DateTime BirthDate { get; set; }
    public string Address { get; set; }
    public string TelePhone { get; set; }
    public string Email { get; set; }

    public Student()
    {
        
    }

    public Student(int id, string firstName, string laastName, DateTime birthDate, string address, string telePhone, string email)
    {
        Id = id;
        FirstName = firstName;
        LaastName = laastName;
        BirthDate = birthDate;
        Address = address;
        TelePhone = telePhone;
        Email = email;
    }
}