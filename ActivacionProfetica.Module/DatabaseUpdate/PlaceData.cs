using ActivacionProfetica.Module.BusinessObjects;
using ActivacionProfetica.Module.SharedKernel;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using System;
using System.Data;
using System.Linq;

namespace ActivacionProfetica.Module.DatabaseUpdate
{
    public class PlaceData : BaseData
    {
        private const string FileName = "seats.csv";
        private readonly string[] eagleSector = { "A", "B", "C", "D", "E", "F" };

        public PlaceData(Updater updater) : base(updater)
        {
        }

        public override void Execute()
        {
            bool existPlaces = (from cc in Updater.Session.Query<Place>()
                                select cc).Any();
            if (existPlaces)
            {
                return;
            }

            //Create lion sector
            var lionPlace = Updater.ObjectSpace.CreateObject<Place>();
            lionPlace.InternalId = Place.LionSector;
            lionPlace.Name = "León";

            //Create shofar sector
            var shofarPlace = CreateObject<Place>();
            shofarPlace.InternalId = Place.ShofarSector;
            shofarPlace.Name = "Shofar";

            SaveChanges();

            DataTable csvData = CSVRead(FileName);
            foreach (DataRow row in csvData.Rows)
            {
                //Sector
                var sectorName = Convert.ToString(row["SECTOR"]);
                var sector = FindObject<Place>(CriteriaOperator.Parse($"Name = '{sectorName}'"));
                if (sector is null)
                {
                    var parentPlace = (eagleSector.Contains(sectorName)) ? lionPlace : shofarPlace;
                    sector = CreateObject<Place>();
                    sector.ParentPlace = parentPlace;
                    sector.Name = sectorName;
                }
                SaveChanges();

                //Row
                var rowName = "F" + Convert.ToString(row["FILA"]);
                var rowObj = FindObject<Place>(CriteriaOperator.Parse($"Name = '{rowName}' And ParentPlace.Name = '{sectorName}'"));
                if (rowObj is null)
                {
                    rowObj = CreateObject<Place>();
                    rowObj.ParentPlace = sector;
                    rowObj.Name = rowName;
                }
                SaveChanges();

                //Seet
                var seetName = Convert.ToString(row["ASIENTO"]);
                var seet = FindObject<Place>(CriteriaOperator.Parse($"Name = '{seetName}' And ParentPlace.Name = '{sectorName}' And ParentPlace.ParentPlace.Name = '{rowName}'"));
                if (seet is null)
                {
                    seet = CreateObject<Place>();
                    seet.ParentPlace = rowObj;
                    seet.Name = seetName;
                    seet.IsLeaf = true;
                    seet.Sector = (seet.ParentPlace.ParentPlace.InternalId == Place.LionSector) ? BusinessObjects.Enums.Sector.Lion : BusinessObjects.Enums.Sector.Shofar;
                }
                SaveChanges();
            }

            //Create eagle sector
            var eaglePlace = Updater.ObjectSpace.CreateObject<Place>();
            eaglePlace.InternalId = Place.EagleSector;
            eaglePlace.Name = "Águila";

            for (int i = 1; i <= 1700; i++)
            {
                var place = Updater.ObjectSpace.CreateObject<Place>();
                place.Name = i.ToString();
                place.Sector = BusinessObjects.Enums.Sector.Eagle;
                place.ParentPlace = eaglePlace;
                place.IsLeaf = true;
            }
            SaveChanges();
        }
    }
}
