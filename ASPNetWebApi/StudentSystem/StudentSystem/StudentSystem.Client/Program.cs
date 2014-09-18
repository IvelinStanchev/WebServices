using StudentSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            StudentSystemDbContext context = new StudentSystemDbContext();

            context.Courses.Any();
        }
    }
}
