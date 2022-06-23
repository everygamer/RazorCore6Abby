using Abby.DataAccess.Data;
using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abby.DataAccess.Repository
{
    internal class FoodTypeRepository : Repository<FoodType>, IFoodTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public FoodTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(FoodType foodType)
        {
            // using the below method you could enforce only
            // updating specific attributes or applying logic to the update process
            var objFromDb = _db.FoodType.FirstOrDefault(u => u.Id == foodType.Id);
            objFromDb.Name = foodType.Name;

            // data will be saved when SaveChanges is called, the entity framework will
            // track the above changes as they are associated to the object
        }
    }
}
