using BusinessLaag.irepositories;
using BusinessLaag.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLaag.repositories
{
    public class ContinentRepo : IContinentRepo
    {
        private DataContext DataContext;

        public ContinentRepo(DataContext dataContext)
        {
            try
            {
                this.DataContext = dataContext;
            }
            catch
            {
                throw new Exception("ERROR Continent Repository");
            }
        }

        public void Add(Continent continent)
        {
            try
            {
                DataContext.DbContinent.Add(continent);
                DataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR Continent Repository in the Add function (" + ex + ")");
            }
        }

        public void DeleteById(int id)
        {
            try
            {
                if (CheckCountryInContinent(id))
                {
                    DataContext.DbContinent.Remove(GetById(id));
                    DataContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR Continent Repository in the DeleteById function (" + ex + ")");
            }
        }

        public bool CheckCountryInContinent(int id)
        {
            var countries = DataContext.DbCountry.ToList().FindAll(x => x.Continent == GetById(id));

            if (countries == null || countries.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Continent GetById(int id)
        {
            try
            {
                Continent continent = DataContext.DbContinent.FirstOrDefault(x => x.ID == id);

                if (continent == null)
                {
                    throw new Exception("Continent not found with the GetById function");
                }
                else
                {
                    return continent;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR Continent Repository in the GetById function (" + ex + ")");
            }
        }

        public List<Continent> GetAllContinents()
        {
            try
            {
                var continents = DataContext.DbContinent.ToList();
                return continents;
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR Continent Repository in the GetAllContinents function (" + ex + ")");
            }
        }

        public void RemoveAllContinents()
        {
            try
            {
                foreach (Continent continent in GetAllContinents())
                {
                    DeleteById(continent.ID);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR Continent Repository in the GetById function (" + ex + ")");
            }
        }

        public void UpdateContinent(Continent continent)
        {
            try
            {
                DataContext.DbContinent.Update(continent);
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR Continent Repository in the UpdateContinent function (" + ex + ")");
            }
        }
        public bool ContinentExists(Continent continent)
        {
            var TempCont = GetAllContinents().FirstOrDefault(x => x.Name == continent.Name);
            if (TempCont == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
