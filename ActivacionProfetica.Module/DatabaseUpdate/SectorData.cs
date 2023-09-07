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
                var pastoresSector = Updater.ObjectSpace.CreateObject<Sector>();
                pastoresSector.InternalId = Sector.PastoresSectorId;
                pastoresSector.Name = Sector.PastoresSectorName;
                pastoresSector.Amount = 500;

                var maestrosSector = Updater.ObjectSpace.CreateObject<Sector>();
                maestrosSector.InternalId = Sector.MaestrosSectorId;
                maestrosSector.Name = Sector.MaestrosSectorName;
                maestrosSector.Amount = 500;

                var apostolesSector = Updater.ObjectSpace.CreateObject<Sector>();
                apostolesSector.InternalId = Sector.ApostolesSectorId;
                apostolesSector.Name = Sector.ApostolesSectorName;
                apostolesSector.Amount = 500;

                var profetasSector = Updater.ObjectSpace.CreateObject<Sector>();
                profetasSector.InternalId = Sector.ProfetasSectorId;
                profetasSector.Name = Sector.ProfetasSectorName;
                profetasSector.Amount = 500;

                var evangelistasSector = Updater.ObjectSpace.CreateObject<Sector>();
                evangelistasSector.InternalId = Sector.EvangelistasSectorId;
                evangelistasSector.Name = Sector.EvangelistasSectorName;
                evangelistasSector.Amount = 500;
            }
        }

        public void Execute2()
        {
            bool isEmpty = !(from ps in Updater.Session.Query<Sector>()
                             select ps).Any();
            if (isEmpty)
            {
                var lionSector = Updater.ObjectSpace.CreateObject<Sector>();
                lionSector.InternalId = Sector.LionSectorId;
                lionSector.Name = Sector.LionSectorName;
                lionSector.Amount = 250;

                var shofarSector = Updater.ObjectSpace.CreateObject<Sector>();
                shofarSector.InternalId = Sector.ShofarSectorId;
                shofarSector.Name = Sector.ShofarSectorName;
                shofarSector.Amount = 200;

                var eagleSector = Updater.ObjectSpace.CreateObject<Sector>();
                eagleSector.InternalId = Sector.EagleSectorId;
                eagleSector.Name = Sector.EagleSectorName;
                eagleSector.Amount = 140;
            }
        }
    }
}