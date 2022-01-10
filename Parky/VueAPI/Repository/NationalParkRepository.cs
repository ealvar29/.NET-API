using System.Collections.Generic;
using System.Linq;
using VueAPI.Data;
using VueAPI.Models;
using VueAPI.Repository.IRepository;

namespace VueAPI.Repository
{
    public class NationalParkRepository : INationalParkReposity
    {
        private readonly ApplicationDbContext _db;

        public NationalParkRepository(ApplicationDbContext db)
        {
             _db = db;
        }

        public bool CreateNationalPark(NationalPark nationalPark)
        {
            _db.NationalParks.Add(nationalPark);
            return Save();
        }

        public bool DeleteNationalPark(NationalPark nationalPark)
        {
            _db.NationalParks.Remove(nationalPark);
            return Save();
        }

        public NationalPark GetNationalPark(int nationalParkId)
        {
            return _db.NationalParks.FirstOrDefault(x => x.Id == nationalParkId);
        }

        public ICollection<NationalPark> GetNationalParks()
        {
            return _db.NationalParks.OrderBy(x => x.Id).ToList();
        }

        public bool NationalParkExists(string name)
        {
            //Might need to update this to .Any(x => x.Name.ToLower().Trim() == name.ToLower().Trim());
            bool value = _db.NationalParks.Any(x => x.Name == name);
            return value;
        }

        public bool NationalParkExists(int id)
        {
            return _db.NationalParks.Any(x => x.Id == id);
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateNationalPark(NationalPark nationalPark)
        {
            _db.NationalParks.Update(nationalPark);
            return Save();
        }
    }
}
