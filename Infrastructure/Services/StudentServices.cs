using System.Diagnostics;
using Dapper;
using Domain.Dtos;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Infrastructure.Services;

public class StudentServices
{
    private DataContext _context;

    public StudentServices(DataContext context)
    {
        _context = context;
    }

    public async Task<List<StudentDto>> GetStudentEntity()
    {
        var sw = new Stopwatch();
        sw.Start();
        var get = _context.Students.Select(x => new StudentDto(x.Id, x.FirstName, x.LaastName, x.BirthDate, x.Address, x.TelePhone, x.Email));
        sw.Stop();
        Console.WriteLine($"Elapsed Times with Entity / {sw.ElapsedMilliseconds}");
        return await get.ToListAsync();
    }

    private string _connectionString = "Server=localhost;Port=5432;Database=StopWhachTest;User Id=postgres;Password=2004;";

    public async Task<List<StudentDto>> GetStudentDapper()
    {
        var sw = new Stopwatch();
        sw.Start();
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM \"Students\" ";
            var result =await connection.QueryAsync<StudentDto>(sql);
            sw.Stop();
            Console.WriteLine($"Elapsed Times with dapper /  {sw.ElapsedMilliseconds}");
            return result.ToList();
        }

    }



    public async Task<List<Student>> GetStudentsNpgsql()
    {
        string sql = "SELECT * FROM \"Students\" ";
        var students = new  List<Student>();
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            var sw = new Stopwatch();
            sw.Start();
            await connection.OpenAsync();
            using (var command = new NpgsqlCommand(sql, connection))
            {
                using (var reader =await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var student = new Student()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LaastName = reader.GetString(reader.GetOrdinal("LaastName")),
                            BirthDate = reader.GetDateTime(reader.GetOrdinal("BirthDate")),
                            Address = reader.GetString(reader.GetOrdinal("Address")),
                            TelePhone = reader.GetString(reader.GetOrdinal("TelePhone")),
                            Email = reader.GetString(reader.GetOrdinal("Email"))

                        };
                        students.Add(student);
                    }
                }
            }

            sw.Stop();
            System.Console.WriteLine($"Elapsed Times Npgsql /  {sw.ElapsedMilliseconds}");
            connection.Close();
        }
        return students;
    }











public async Task AddStudent(StudentDto studentDto)
    { 
        for (int i = 0; i < 500; i++)
        {
            var mapped = new Student(studentDto.Id,studentDto.FirstName,studentDto.LaastName,studentDto.BirthDate,studentDto.Address,studentDto.TelePhone,studentDto.Email);
            await _context.Students.AddAsync(mapped);
            await _context.SaveChangesAsync();
        }  
 
    }
    
}