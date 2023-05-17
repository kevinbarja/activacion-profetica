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
        private readonly string[] eagleSectorLetters = { "A", "B", "C", "D", "E", "F" };

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

            var lionSector = (from s in Updater.Session.Query<Sector>()
                              where s.InternalId == Sector.LionSectorId
                              select s).Single();

            var shofarSector = (from s in Updater.Session.Query<Sector>()
                                where s.InternalId == Sector.ShofarSectorId
                                select s).Single();

            var eagleSector = (from s in Updater.Session.Query<Sector>()
                               where s.InternalId == Sector.EagleSectorId
                               select s).Single();


            var availablePlaceStatus = (from s in Updater.Session.Query<PlaceStatus>()
                                        where s.InternalId == PlaceStatus.AvailablePlaceStatus
                                        select s).Single();

            //Create lion sector
            var lionPlace = Updater.ObjectSpace.CreateObject<Place>();
            lionPlace.InternalId = Sector.LionSectorId;
            lionPlace.Name = Sector.LionSectorName;
            lionPlace.LetterName = string.Empty;
            lionPlace.RowName = string.Empty;

            //Create shofar sector
            var shofarPlace = CreateObject<Place>();
            shofarPlace.InternalId = Sector.ShofarSectorId;
            shofarPlace.Name = Sector.ShofarSectorName;
            shofarPlace.LetterName = string.Empty;
            shofarPlace.RowName = string.Empty;

            SaveChanges();

            DataTable csvData = CSVRead(FileName);
            foreach (DataRow row in csvData.Rows)
            {
                //Sector
                var sectorName = Convert.ToString(row["SECTOR"]);
                var sector = FindObject<Place>(CriteriaOperator.Parse($"Name = '{sectorName}'"));
                if (sector is null)
                {
                    var parentPlace = (eagleSectorLetters.Contains(sectorName)) ? lionPlace : shofarPlace;
                    sector = CreateObject<Place>();
                    sector.ParentPlace = parentPlace;
                    sector.Name = sectorName;
                    sector.LetterName = sectorName;
                    sector.RowName = string.Empty;
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
                    rowObj.LetterName = sectorName;
                    rowObj.RowName = rowName;
                }
                SaveChanges();

                //Seet
                var seetName = Convert.ToString(row["ASIENTO"]);
                var seet = FindObject<Place>(CriteriaOperator.Parse($"Name = '{seetName}' And ParentPlace.Name = '{rowName}' And ParentPlace.ParentPlace.Name = '{sectorName}'"));
                if (seet is null)
                {
                    seet = CreateObject<Place>();
                    seet.ParentPlace = rowObj;
                    seet.Name = seetName;
                    seet.IsLeaf = true;
                    seet.Sector = (sector.ParentPlace.InternalId == Sector.LionSectorId) ? lionSector : shofarSector;
                    seet.LetterName = sectorName;
                    seet.RowName = rowName;
                    seet.Status = availablePlaceStatus;
                }
                else
                {
                    //TODO: Uncomment this validation
                    //throw new Exception($"Seet repeated: seetName = '{seetName}' And rowName = '{rowName}' And sectorName = '{sectorName}'");
                }
                SaveChanges();
            }

            //Create eagle sector
            var eaglePlace = Updater.ObjectSpace.CreateObject<Place>();
            eaglePlace.InternalId = Sector.EagleSectorId;
            eaglePlace.Name = Sector.EagleSectorName;
            eaglePlace.LetterName = string.Empty;
            eaglePlace.RowName = string.Empty;
            for (int i = 1; i <= 1700; i++)
            {
                var place = Updater.ObjectSpace.CreateObject<Place>();
                place.Name = i.ToString();
                place.Sector = eagleSector;
                place.ParentPlace = eaglePlace;
                place.IsLeaf = true;
                place.LetterName = string.Empty;
                place.RowName = string.Empty;
                place.Status = availablePlaceStatus;
            }
            SaveChanges();
        }
    }
}
