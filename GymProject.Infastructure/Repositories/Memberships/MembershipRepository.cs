using GymProject.Domain.Models.Memberships;
using GymProject.Infastructure.DAL;
using Microsoft.EntityFrameworkCore;

namespace GymProject.Infastructure.Repositories.Memberships;

public class MembershipRepository : IMembershipRepository
{
    private readonly AppDbContext _dbContext;

    public MembershipRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Subscription>> GetAllAsync()
       => await _dbContext.Memberships.ToListAsync();
    
    public async Task<Subscription> GetAsync(Guid Id)
       => await _dbContext.Memberships.FirstOrDefaultAsync(o => o.Id == Id);
}