using NZWalks.API.Models.Domain;
using NZWalks.API.Data;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.API.Repositories
{
	public class SQLRegionRepository: IRegionRepository
    {
		private readonly NZWalksDbContext dbContext;
		public SQLRegionRepository(NZWalksDbContext dbContext)
		{
			this.dbContext = dbContext;
		}
		public async Task<List<Region>> GetAllAsync()
		{
			return await dbContext.Regions.ToListAsync();
		}

		public async Task<Region> GetByIDAsync(Guid id)
		{
			return await dbContext.Regions.FindAsync(id);
        }
		public async Task<Region> CreateAsync(Region region)
		{
			await dbContext.Regions.AddAsync(region);
			await dbContext.SaveChangesAsync();
			return region;

        }
		public async Task<Region> UpdateAsync(Guid id, Region region)
		{
			var existingRegion = await dbContext.Regions.FindAsync(id);
			if(existingRegion == null)
			{
				return null;
			}
			existingRegion.Code = region.Code;
			existingRegion.Name = region.Name;
			await dbContext.SaveChangesAsync();
			return existingRegion;
		}

		public async Task<Region> DeleteAsync(Guid id)
		{
			var existingRegion = await dbContext.Regions.FindAsync(id);
            //If not found return not found
            if (existingRegion == null)
            {
                return null;
            }
            //Delete Region, we don't have a removeAsync method , so this is synchronous
            dbContext.Regions.Remove(existingRegion);
            //Save changes
            await dbContext.SaveChangesAsync();
			return existingRegion;
        }
    }
}

