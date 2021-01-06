using BusinessLaag.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLaag.managers
{
    public class ContinentManager
    {
        public IUnitOfWork uow;

        public ContinentManager(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public void Add(Continent continent)
        {
            try
            {
                if (uow.continentRepo.ContinentExists(continent))
                {
                    uow.continentRepo.Add(continent);
                }
                else
                {
                    throw new Exception("Continent exists already!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR Continent Manager in the Add function (" + ex + ")");
            }
        }

        public Continent GetContinentById(int id)
        {
            try
            {
                return uow.continentRepo.GetById(id);
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR Continent Manager in the GetContinentById function (" + ex + ")");
            }
        }

        public List<Continent> GetAllContinents()
        {
            try
            {
                return uow.continentRepo.GetAllContinents();
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR Continent Manager in the GetAllContinents function (" + ex + ")");
            }
        }

        public void UpdateContinent(Continent continent)
        {
            try
            {
                uow.continentRepo.UpdateContinent(continent);
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR Continent Manager in the UpdateContinent function (" + ex + ")");
            }
        }

        public bool ContinentExists(Continent continent)
        {
            return uow.continentRepo.ContinentExists(continent);
        }

        public void RemoveAll()
        {
            try
            {
                uow.continentRepo.RemoveAllContinents();
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR Continent Manager in the RemoveAll function (" + ex + ")");
            }
        }

        public void RemoveContinent(int id)
        {
            try
            {
                uow.continentRepo.DeleteById(id);
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR Continent Manager in the RemoveContinent function (" + ex + ")");
            }
        }
    }
}
