using ActivacionProfetica.Module.BusinessObjects;
using ActivacionProfetica.Module.SharedKernel;
using System;

namespace ActivacionProfetica.Module.DatabaseUpdate
{
    public class EagleSector : BaseData
    {
        public EagleSector(Updater updater) : base(updater)
        {
        }

        public override void Execute()
        {
            try
            {
                //Create eagle sector
                var eaglePlace = Updater.ObjectSpace.CreateObject<Place>();
                eaglePlace.Id = Place.EagleSector;
                eaglePlace.Name = "Águila";

                for (int i = 1; i <= 1700; i++)
                {
                    var place = Updater.ObjectSpace.CreateObject<Place>();
                    place.Name = i.ToString();
                    place.ParentPlace = eaglePlace;
                    SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
