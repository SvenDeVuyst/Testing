using BusinessLaag.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLaag.irepositories
{
    public interface IContinentRepo
    {
        void Add(Continent continent);
        void DeleteById(int id);
        List<Continent> GetAllContinents();
        Continent GetById(int id);
        void RemoveAllContinents();
        void UpdateContinent(Continent continent);
        bool CheckCountryInContinent(int id);
        bool ContinentExists(Continent continent);
    }
}
