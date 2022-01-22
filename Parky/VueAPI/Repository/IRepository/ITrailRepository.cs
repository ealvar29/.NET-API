using System.Collections.Generic;
using VueAPI.Models;

namespace VueAPI.Repository.IRepository
{
    public interface ITrailRepository
    {
        ICollection<Trail> GetNationalParks();

        Trail GetTrail(int TrailId);

        bool TrailExists(string name);

        bool TrailExists(int id);

        bool CreateTrail(Trail trail);

        bool UpdateTrail(Trail trail);

        bool DeleteTrail(Trail trail);

        bool Save();

    }
}
