using Data.Common.Enums;
using Data.Common.Services;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Persistence.Repositories
{
    public class LogRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public LogRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

        // Retrieve
        public IEnumerable<Log> GetAll() => _dbContext.Logs.AsNoTracking().Include("Tags").ToList();
        public IEnumerable<Log> GetByType(LogType logType) => _dbContext.Logs.AsNoTracking().Where(log => log.Type == logType).Include("Tags").ToList();
        public IEnumerable<Log> GetByTag(Tag tag) => _dbContext.Logs.AsNoTracking().Where(log => log.Tags.Contains(tag)).Include("Tags").ToList();
        public Log GetById(int id) => _dbContext.Logs.AsNoTracking().Include("Tags").FirstOrDefault(log => log.Id == id);
        public Log GetByTitle(string title) => _dbContext.Logs.AsNoTracking().Include("Tags").FirstOrDefault(log => log.EncodedTitle == title);

        // Save
        public async Task<bool> SaveChangesAsync() => await _dbContext.SaveChangesAsync() > 0;

        // Modify
        public void Remove(int id) => _dbContext.Logs.Remove(_dbContext.Logs.FirstOrDefault(log => log.Id == id));

        public void Upsert(Log log)
        {
            Log temp;
            if (log.Id == 0)
            {
                temp = new Log()
                {
                    Title = log.Title,
                    Body = log.Body,
                    Type = log.Type,
                    EncodedTitle = ValidateTitle(log.Title.Encode()),
                    Tags = log.Tags,
                    CreatedBy = log.CreatedBy,
                    CreatedDate = DateTime.UtcNow
                };
                _dbContext.Logs.Add(temp);
            }
            else
            {
                log.EncodedTitle = ValidateTitle(log.Title.Encode());
                log.ModifiedDate = DateTime.UtcNow;
                _dbContext.Logs.Update(log);
            }
        }

        private string ValidateTitle(string encodedTitle)
        {
            int matches = _dbContext.Logs.Where(log => log.EncodedTitle == encodedTitle).Count();
            if (matches > 0)
                return encodedTitle += "-" + matches++;
            else
                return encodedTitle;
        }
    }
}