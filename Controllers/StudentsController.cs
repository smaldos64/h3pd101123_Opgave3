using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Opgave3.Models;
using Opgave3.DTO;
using Mapster;

namespace Opgave3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public StudentsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
          if (_context.Students == null)
          {
              return NotFound();
          }

          List<Student> StudentList = new List<Student>();

          StudentList = await _context.Students.
                        Include(t => t.Team).
                        Include(s => s.StudentCourses).
                        ThenInclude(c => c.Course).
                        Include(s => s.StudentCourses).
                        ThenInclude(ch => ch.Character).ToListAsync();

          List<StudentDTO> StudentDTOList = new List<StudentDTO>();
          StudentDTOList = StudentList.Adapt<StudentDTO[]>().ToList();

          return Ok(StudentDTOList);
        }

        // GET: api/Students/5
        [HttpGet("{StudentId}")]
        public async Task<ActionResult<Student>> GetStudent(int StudentId)
        {
          if (_context.Students == null)
          {
              return NotFound();
          }
            var student = await _context.Students.FindAsync(StudentId);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

      [HttpGet]
      [Route("[action]")]
      public async Task<ActionResult<StudentDTO>> GetStudentsWithTeamIdAndCourseIdAllCoursesForStudents(int TeamId, 
                                                                                                        int CourseId)
      {
        if (_context.Students == null)
        {
          return NotFound();
        }

        List<Student> StudentList = new List<Student>();

        StudentList = await _context.Students.
                      Include(t => t.Team).
                      Include(s => s.StudentCourses).
                      ThenInclude(c => c.Course).
                      Include(s => s.StudentCourses).
                      ThenInclude(ch => ch.Character).ToListAsync();

      //StudentList = await _context.Students.
      //            Include(t => t.Team).
      //            Include(s => s.StudentCourses).
      //            ThenInclude(c => c.Course).
      //            Include(s => s.StudentCourses).
      //            ThenInclude(ch => ch.Character). 
      //            Where(t => t.TeamId == TeamId).
      //            SelectMany(s => s.StudentCourses).
      //            Where(c => c.CourseId == CourseId).ToListAsync();

        List<StudentCourse> StudentCourseList = new List<StudentCourse>();
        StudentCourseList = StudentList.
                            Where(t => t.TeamId == TeamId).
                            SelectMany(s => s.StudentCourses).
                            Where(c => c.CourseId == CourseId).ToList();
       
        StudentList = StudentList.Where(s => s.TeamId == TeamId &&
                                        s.StudentCourses.Any(c => c.CourseId == CourseId)).ToList();

        //StudentList = StudentList.Where(s => s.TeamId == TeamId &&
        //                                s.StudentCourses.SelectMany(c => c.CourseId == CourseId)).ToList();

        List<StudentDTO> StudentDTOList = new List<StudentDTO>();

        StudentDTOList = StudentList.Adapt<StudentDTO[]>().ToList();

        return Ok(StudentDTOList);
      }

      [HttpGet]
      [Route("[action]")]
      public async Task<ActionResult<StudentCourseDTO>> GetStudentsWithTeamIdAndCourseIdSelectedCourseForStudents(int TeamId,
                                                                                                                  int CourseId)
      {
        if (_context.Students == null)
        {
          return NotFound();
        }

        List<Student> StudentList = new List<Student>();

        StudentList = await _context.Students.
                      Include(t => t.Team).
                      Include(s => s.StudentCourses).
                      ThenInclude(c => c.Course).
                      Include(s => s.StudentCourses).
                      ThenInclude(ch => ch.Character).ToListAsync();

        List<StudentCourse> StudentCourseList = new List<StudentCourse>();
        StudentCourseList = StudentList.
                            Where(t => t.TeamId == TeamId).
                            SelectMany(s => s.StudentCourses).
                            Where(c => c.CourseId == CourseId).ToList();

        List<StudentCourseDTO> StudentCourseDTOList = new List<StudentCourseDTO>();

        StudentCourseDTOList = StudentCourseList.Adapt<StudentCourseDTO[]>().ToList();
        
        return Ok(StudentCourseDTOList);
      }

      [HttpGet]
      [Route("[action]")]
      public async Task<ActionResult<StudentCourseDTO>> GetStudentsWithTeamIdAndCourseIdAndCharacterIdSelectedCourseForStudents(int TeamId,
                                                                                                                          int CourseId,
                                                                                                                          int CharacterId)
      {
        if (_context.Students == null)
        {
          return NotFound();
        }

        List<Student> StudentList = new List<Student>();

        StudentList = await _context.Students.
                      Include(t => t.Team).
                      Include(s => s.StudentCourses).
                      ThenInclude(c => c.Course).
                      Include(s => s.StudentCourses).
                      ThenInclude(ch => ch.Character).ToListAsync();

        List<StudentCourse> StudentCourseList = new List<StudentCourse>();
        StudentCourseList = StudentList.
                            Where(t => t.TeamId == TeamId).
                            SelectMany(s => s.StudentCourses).
                            Where(c => c.CourseId == CourseId && c.CharacterId == CharacterId).ToList();

        //var GroupedCourses = StudentCourseList.GroupBy(s => s.Student);

        //var StudentCourses = GroupedCourses.Select(g => new
        //{
        //  Student = g.Key,
        //  StudentCourses = g.Select(s => s.Course).ToList()
        //});

        //var StudentCourseList1 = StudentCourses.ToList();

        //List<StudentCourseDTO> StudentCourseDTOList1 = new List<StudentCourseDTO>();
        //StudentCourseDTOList1 = StudentCourseList1.Adapt<StudentCourseDTO[]>().ToList();

        StudentList = StudentCourseList.Select(s => s.Student).ToList();

        List<StudentCourseDTO> StudentCourseDTOList = new List<StudentCourseDTO>();
        StudentCourseDTOList = StudentCourseList.Adapt<StudentCourseDTO[]>().ToList();

        List<StudentDTO> StudentDTOList = new List<StudentDTO>();
        StudentDTOList = StudentList.Adapt<StudentDTO[]>().ToList();
        
        return Ok(StudentCourseDTOList);
        //return Ok(StudentDTOList);
    }

      // PUT: api/Students/5
      // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
      [HttpPut("{id}")]
      public async Task<IActionResult> PutStudent(int id, Student student)
      {
          if (id != student.StudentId)
          {
              return BadRequest();
          }

          _context.Entry(student).State = EntityState.Modified;

          try
          {
              await _context.SaveChangesAsync();
          }
          catch (DbUpdateConcurrencyException)
          {
              if (!StudentExists(id))
              {
                  return NotFound();
              }
              else
              {
                  throw;
              }
          }

          return NoContent();
      }

      // POST: api/Students
      // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
      [HttpPost]
      public async Task<ActionResult<Student>> PostStudent(Student student)
      {
        if (_context.Students == null)
        {
            return Problem("Entity set 'DatabaseContext.Students'  is null.");
        }
          _context.Students.Add(student);
          await _context.SaveChangesAsync();

          return CreatedAtAction("GetStudent", new { id = student.StudentId }, student);
      }

      // DELETE: api/Students/5
      [HttpDelete("{id}")]
      public async Task<IActionResult> DeleteStudent(int id)
      {
          if (_context.Students == null)
          {
              return NotFound();
          }
          var student = await _context.Students.FindAsync(id);
          if (student == null)
          {
              return NotFound();
          }

          _context.Students.Remove(student);
          await _context.SaveChangesAsync();

          return NoContent();
      }

      private bool StudentExists(int id)
      {
          return (_context.Students?.Any(e => e.StudentId == id)).GetValueOrDefault();
      }
    }
}
