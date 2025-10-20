using Microsoft.EntityFrameworkCore;
using School.AdminService.Data;
using School.AdminService.Data.Entities;
using School.AdminService.Repository.Interfaces;

namespace School.AdminService.Repository
{
    public class IdGeneratorService: IIdGeneratorService
    {
        private readonly AppDbContext _context;

        public IdGeneratorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<long> GetNextIdAsync(string entityType)
        {
           
            // Use EF's execution strategy to wrap the entire transaction
            var strategy = _context.Database.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await _context.Database.BeginTransactionAsync();

                try
                {
                    var sequence = await _context.IdSequences
                        .FirstOrDefaultAsync(s => s.EntityType == entityType);

                    long id;
                    if (sequence == null)
                    {
                        sequence = new IdSequence { EntityType = entityType, NextId = 1 };
                        _context.IdSequences.Add(sequence);
                        id = 1;
                    }
                    else
                    {
                        id = sequence.NextId;
                        sequence.NextId += 1;
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return id;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            });
        
        }
    }
}
