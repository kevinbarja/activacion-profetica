using ActivacionProfetica.Module.BusinessObjects;
using ActivacionProfetica.Module.SharedKernel;
using DevExpress.Xpo;
using System.Linq;

namespace ActivacionProfetica.Module.DatabaseUpdate
{
    public class SectorData : BaseData
    {
        public SectorData(Updater updater) : base(updater)
        {
        }

        public override void Execute()
        {
            bool isEmpty = !(from ps in Updater.Session.Query<Sector>()
                             select ps).Any();
            if (isEmpty)
            {
                var lionSector = Updater.ObjectSpace.CreateObject<Sector>();
                lionSector.InternalId = Sector.LionSectorId;
                lionSector.Name = Sector.LionSectorName;

                var shofarSector = Updater.ObjectSpace.CreateObject<Sector>();
                shofarSector.InternalId = Sector.ShofarSectorId;
                shofarSector.Name = Sector.ShofarSectorName;

                var eagleSector = Updater.ObjectSpace.CreateObject<Sector>();
                eagleSector.InternalId = Sector.EagleSectorId;
                eagleSector.Name = Sector.EagleSectorName;
            }
        }
    }
}