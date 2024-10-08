﻿using Microsoft.EntityFrameworkCore;
using StudentsProblem.Interfaces;
using StudentsProblem.Models;

namespace StudentsProblem.Repositories
{
    public class SchoolRepository : ISchoolRepository
    {
        private readonly ApplicationDbContext context;

        public SchoolRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<School>> GetAllSchoolsAsync()
        {
            return await context.School
                .Include(a => a.Address)
                .Include(s => s.Students)
                .OrderBy(x => x.Id).ToListAsync();
        }

        public async Task<School> GetSchoolByIdAsync(int id)
        {
            var school = await context.School
                .Include(a => a.Address)
                .FirstOrDefaultAsync(sch => sch.Id == id);

            if (school == null)
            {
                throw new ArgumentException("School with that id can not be found");
            }
            else
            {
                return school;
            }
        }

        public async Task<int> AddSchoolAsync(School school)
        {
            context.School.Add(school);
            return await context.SaveChangesAsync();
        }

        public async Task<int> UpdateSchoolAsync(School school)
        {
            context.School.Update(school);
            return await context.SaveChangesAsync();
        }

        public async Task<int> DeleteSchoolAsync(int id)
        {
            var schoolToDelete = await context.School
                .Include(a => a.Address)
                .FirstOrDefaultAsync(sch => sch.Id == id);

            if (schoolToDelete == null)
            {
                throw new ArgumentException("Can not find school with that id.");
            }
            else
            {
                context.Remove(schoolToDelete);
                return await context.SaveChangesAsync();
            }
        }
    }
}
