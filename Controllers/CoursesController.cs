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
    public class CoursesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public CoursesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDTO>>> GetCourses()
        {
            if (_context.Courses == null)
            {
                return NotFound();
            }   

            List <Course> CourseList = new List<Course>();

            CourseList = await _context.Courses.
                Include(s => s.StudentCourses).
                ThenInclude(s => s.Student).
                ThenInclude(t => t.Team).
                Include(s => s.StudentCourses).
                ThenInclude(ch => ch.Character).ToListAsync();

            List<CourseDTO> CourseDTOList = new List<CourseDTO>();

            CourseDTOList = CourseList.Adapt<CourseDTO[]>().ToList();           

            return Ok(CourseDTOList);
        }

        // GET: api/Courses/5
        [HttpGet("{CourseId}")]
        public async Task<ActionResult<CourseDTO>> GetCourse(int CourseId)
        {
            if (_context.Courses == null)
            {
                    return NotFound();
            }       

            Course Course_Object = new Course();

            Course_Object = await _context.Courses.
                      Include(s => s.StudentCourses).
                      ThenInclude(s => s.Student).
                      ThenInclude(t => t.Team).
                      Include(s => s.StudentCourses).
                      ThenInclude(ch => ch.Character).
                      FirstOrDefaultAsync(c => c.CourseId == CourseId);

            if (null == Course_Object)
            {
                return NotFound();
            }

            CourseDTO CourseDTO_Object = new CourseDTO();

            CourseDTO_Object = Course_Object.Adapt<CourseDTO>();

            return Ok(CourseDTO_Object);
        }

        //[HttpGet("{TeamId}")]
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<CourseDTO>> GetCoursesWithTeamId(int TeamId)
        {
            if (_context.Courses == null)
            {
                return NotFound();
            }

            List<Course> CourseList = new List<Course>();

            CourseList = await _context.Courses.
                        Include(s => s.StudentCourses).
                        ThenInclude(s => s.Student).
                        ThenInclude(t => t.Team).
                        Include(s => s.StudentCourses).
                        ThenInclude(ch => ch.Character).ToListAsync();

            CourseList = CourseList.Where(c => c.StudentCourses.Any(s => s.Student.TeamId == TeamId)).ToList();

            if (null == CourseList)
            {
                return NotFound();
            }

            List<CourseDTO> CourseDTOList = new List<CourseDTO>();

            CourseDTOList = CourseList.Adapt<CourseDTO[]>().ToList();

            return Ok(CourseDTOList);
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            if (id != course.CourseId)
            {
                return BadRequest();
            }

            _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
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

        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
          if (_context.Courses == null)
          {
              return Problem("Entity set 'DatabaseContext.Courses'  is null.");
          }
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourse", new { id = course.CourseId }, course);
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            if (_context.Courses == null)
            {
                return NotFound();
            }
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseExists(int id)
        {
            return (_context.Courses?.Any(e => e.CourseId == id)).GetValueOrDefault();
        }
    }
}
