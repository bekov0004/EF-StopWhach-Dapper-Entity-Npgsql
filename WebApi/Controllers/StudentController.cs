using Domain.Dtos;
using Domain.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]
public class StudentController
{
    private StudentServices _studentServices;

    public StudentController(StudentServices studentServices)
    {
        _studentServices = studentServices;
    }

    [HttpGet("GetStudentEntity")]
    public async Task<List<StudentDto>> GetStudentEntity()
    { 
        return await _studentServices.GetStudentEntity();
    }
    
    [HttpGet("GetStudentDapper")]
    public async Task<List<StudentDto>> GetStudentDapper()
    { 
        return await _studentServices.GetStudentDapper();
    }
    
    [HttpGet("GetStudentNpgsql")]
    public async Task<List<Student>> GetStudentNpgsql()
    { 
        return await _studentServices.GetStudentsNpgsql();
    }

    [HttpPost("AddStudent")]
    public async Task GetStudent(StudentDto studentDto)
    {
        await _studentServices.AddStudent(studentDto);
    }
}