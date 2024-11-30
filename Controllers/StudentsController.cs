using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortal.Data;
using StudentPortal.Models;
using StudentPortal.Models.Entity;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StudentPortal.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public StudentsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel viewModel) {
            var student = new Student
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
                Subscribed = viewModel.Subscribed,
            };
            await dbContext.students.AddAsync(student);
            await dbContext.SaveChangesAsync();
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> List() {

            var student = await dbContext.students.ToListAsync();
            return View(student);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var student = await dbContext.students.FindAsync(id);
            if (student is null) { return NotFound(); }
            return View(student);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Student viewModel)
        {
            var student = await dbContext.students.FindAsync(viewModel.Id);
            if (student is not null)
            {
                student.Name = viewModel.Name;
                student.Email = viewModel.Email;
                student.Phone = viewModel.Phone;
                student.Subscribed = viewModel.Subscribed;

                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Students");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Student viewModel)
        {
            var student = await dbContext.students.AsNoTracking().FirstOrDefaultAsync(x=>x.Id==viewModel.Id);
            //Improved Performance: When entities are not tracked, Entity Framework doesn’t need to monitor changes, which reduces memory usage and processing time.
            //Read - Only Data: .AsNoTracking() is useful when retrieving data only for display, as it doesn’t require any modifications.
                        if (student is not null)
            {
                dbContext.students.Remove(student);
               await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Students");
        }
     }
}


/*
Additional Useful Entity Framework Core Functionalities
ASP.NET Core with Entity Framework Core provides many functionalities to manage how data is queried, updated, and tracked. Here are some other commonly used methods:

.FindAsync():

This method is useful for retrieving an entity by its primary key. It’s efficient because it checks the tracking cache first before querying the database, reducing unnecessary database queries.
Example:
csharp
Copy code
var student = await dbContext.students.FindAsync(viewModel.Id);
.Add() and .AddRange():

These methods are used to add new entities to the database. .AddRange() allows you to add multiple entities in one go.
Example:
csharp
Copy code
dbContext.students.Add(newStudent);
await dbContext.SaveChangesAsync();
.Update() and .UpdateRange():

.Update() marks an entity as modified, so its changes will be saved in the next SaveChanges call. .UpdateRange() does the same for multiple entities.
Example:
csharp
Copy code
dbContext.students.Update(existingStudent);
await dbContext.SaveChangesAsync();
.Include() and .ThenInclude():

These are used to load related data (e.g., related entities in a one-to-many or many-to-many relationship) along with the main entity, which is known as eager loading.
Example:
csharp
Copy code
var studentsWithCourses = await dbContext.students.Include(s => s.Courses).ToListAsync();
.FirstOrDefaultAsync() and .SingleOrDefaultAsync():

.FirstOrDefaultAsync() returns the first matching entity or null if none are found. It’s commonly used when you expect multiple results but only need one.
.SingleOrDefaultAsync() throws an error if more than one entity is found, making it suitable when you expect exactly one match.
Example:
csharp
Copy code
var student = await dbContext.students.FirstOrDefaultAsync(s => s.Email == email);
.Where():

The .Where() method is used to filter records based on conditions. It’s flexible and allows you to apply LINQ expressions for advanced filtering.
Example:
csharp
Copy code
var subscribedStudents = await dbContext.students.Where(s => s.Subscribed).ToListAsync();
.AnyAsync():

This checks if any records match a given condition, returning true if matches are found, and false otherwise. It’s a quick way to check for existence without loading full records.
Example:
csharp
Copy code
bool exists = await dbContext.students.AnyAsync(s => s.Id == viewModel.Id);
.CountAsync():

This counts the number of records matching a condition, commonly used for reporting or pagination.
Example:
csharp
Copy code
int count = await dbContext.students.CountAsync(s => s.Subscribed);
.OrderBy() and .OrderByDescending():

These methods allow you to sort query results by a specific column in ascending or descending order.
Example:
csharp
Copy code
var sortedStudents = await dbContext.students.OrderBy(s => s.Name).ToListAsync();
.FromSqlRaw():

This allows you to run raw SQL queries directly if you need complex queries that aren’t easily handled by LINQ.

var students = await dbContext.students.FromSqlRaw("SELECT * FROM students WHERE Subscribed = 1").ToListAsync();

 */