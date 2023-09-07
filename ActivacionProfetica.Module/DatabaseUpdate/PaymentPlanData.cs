using ActivacionProfetica.Module.BusinessObjects;
using ActivacionProfetica.Module.SharedKernel;
using DevExpress.Xpo;
using System;
using System.Globalization;
using System.Linq;

namespace ActivacionProfetica.Module.DatabaseUpdate
{
    public class PaymentPlanData : BaseData
    {
        public PaymentPlanData(Updater updater) : base(updater)
        {
        }

        public override void Execute()
        {
            bool isEmpty = !(from ps in Updater.Session.Query<PaymentPlan>()
                             select ps).Any();
            if (isEmpty)
            {
                var pastoresSector = (from s in Updater.Session.Query<Sector>()
                                      where s.InternalId == Sector.PastoresSectorId
                                      select s).Single();

                var maestrosSector = (from s in Updater.Session.Query<Sector>()
                                      where s.InternalId == Sector.MaestrosSectorId
                                      select s).Single();

                var apostolesSector = (from s in Updater.Session.Query<Sector>()
                                       where s.InternalId == Sector.ApostolesSectorId
                                       select s).Single();

                var profetasSector = (from s in Updater.Session.Query<Sector>()
                                      where s.InternalId == Sector.ProfetasSectorId
                                      select s).Single();

                var evangelistasSector = (from s in Updater.Session.Query<Sector>()
                                          where s.InternalId == Sector.EvangelistasSectorId
                                          select s).Single();
                #region Siembra completa

                //Pastores
                var pagoAlContadoPastores = Updater.ObjectSpace.CreateObject<PaymentPlan>();
                pagoAlContadoPastores.Description = "Siembra completa";
                pagoAlContadoPastores.Sector = pastoresSector;
                pagoAlContadoPastores.LimitDate = null;

                var cuota1PagoAlContadoPastores = Updater.ObjectSpace.CreateObject<PaymentPlanDetail>();
                cuota1PagoAlContadoPastores.Number = 1;
                cuota1PagoAlContadoPastores.Description = "Semilla completa";
                cuota1PagoAlContadoPastores.Amount = Constants.AmountTicket;
                cuota1PagoAlContadoPastores.LimitDate = null;

                pagoAlContadoPastores.PaymentPlanDetails.Add(cuota1PagoAlContadoPastores);

                //Maestros
                var pagoAlContadoMaestros = Updater.ObjectSpace.CreateObject<PaymentPlan>();
                pagoAlContadoMaestros.Description = "Siembra completa";
                pagoAlContadoMaestros.Sector = maestrosSector;
                pagoAlContadoMaestros.LimitDate = null;

                var cuota1PagoAlContadoMaestros = Updater.ObjectSpace.CreateObject<PaymentPlanDetail>();
                cuota1PagoAlContadoMaestros.Number = 1;
                cuota1PagoAlContadoMaestros.Description = "Semilla completa";
                cuota1PagoAlContadoMaestros.Amount = Constants.AmountTicket;
                cuota1PagoAlContadoMaestros.LimitDate = null;

                pagoAlContadoMaestros.PaymentPlanDetails.Add(cuota1PagoAlContadoPastores);

                //Apostoles
                var pagoAlContadoApostoles = Updater.ObjectSpace.CreateObject<PaymentPlan>();
                pagoAlContadoApostoles.Description = "Siembra completa";
                pagoAlContadoApostoles.Sector = apostolesSector;
                pagoAlContadoApostoles.LimitDate = null;

                var cuota1PagoAlContadoApostoles = Updater.ObjectSpace.CreateObject<PaymentPlanDetail>();
                cuota1PagoAlContadoApostoles.Number = 1;
                cuota1PagoAlContadoApostoles.Description = "Semilla completa";
                cuota1PagoAlContadoApostoles.Amount = Constants.AmountTicket;
                cuota1PagoAlContadoApostoles.LimitDate = null;

                pagoAlContadoApostoles.PaymentPlanDetails.Add(cuota1PagoAlContadoApostoles);

                //Profetas
                var pagoAlContadoProfetas = Updater.ObjectSpace.CreateObject<PaymentPlan>();
                pagoAlContadoProfetas.Description = "Siembra completa";
                pagoAlContadoProfetas.Sector = profetasSector;
                pagoAlContadoProfetas.LimitDate = null;

                var cuota1PagoAlContadoProfetas = Updater.ObjectSpace.CreateObject<PaymentPlanDetail>();
                cuota1PagoAlContadoProfetas.Number = 1;
                cuota1PagoAlContadoProfetas.Description = "Semilla completa";
                cuota1PagoAlContadoApostoles.Amount = Constants.AmountTicket;
                cuota1PagoAlContadoProfetas.LimitDate = null;

                pagoAlContadoProfetas.PaymentPlanDetails.Add(cuota1PagoAlContadoProfetas);

                //Evangelistas
                var pagoAlContadoEvangelistas = Updater.ObjectSpace.CreateObject<PaymentPlan>();
                pagoAlContadoEvangelistas.Description = "Siembra completa";
                pagoAlContadoEvangelistas.Sector = evangelistasSector;
                pagoAlContadoEvangelistas.LimitDate = null;

                var cuota1PagoAlContadoEvangelistas = Updater.ObjectSpace.CreateObject<PaymentPlanDetail>();
                cuota1PagoAlContadoEvangelistas.Number = 1;
                cuota1PagoAlContadoEvangelistas.Description = "Semilla completa";
                cuota1PagoAlContadoApostoles.Amount = Constants.AmountTicket;
                cuota1PagoAlContadoEvangelistas.LimitDate = null;
                #endregion

                #region No aplica siembra

                //Pastores
                var sinpagoAlContadoPastores = Updater.ObjectSpace.CreateObject<PaymentPlan>();
                sinpagoAlContadoPastores.Description = "No aplica siembra";
                sinpagoAlContadoPastores.Sector = pastoresSector;
                sinpagoAlContadoPastores.LimitDate = null;

                //Maestros
                var sinpagoAlContadoMaestros = Updater.ObjectSpace.CreateObject<PaymentPlan>();
                sinpagoAlContadoMaestros.Description = "No aplica siembra";
                sinpagoAlContadoMaestros.Sector = maestrosSector;
                sinpagoAlContadoMaestros.LimitDate = null;

                //Apostoles
                var sinpagoAlContadoApostoles = Updater.ObjectSpace.CreateObject<PaymentPlan>();
                sinpagoAlContadoApostoles.Description = "No aplica siembra";
                sinpagoAlContadoApostoles.Sector = apostolesSector;
                sinpagoAlContadoApostoles.LimitDate = null;

                //Profetas
                var sinpagoAlContadoProfetas = Updater.ObjectSpace.CreateObject<PaymentPlan>();
                sinpagoAlContadoProfetas.Description = "No aplica siembra";
                sinpagoAlContadoProfetas.Sector = profetasSector;
                sinpagoAlContadoProfetas.LimitDate = null;

                //Evangelistas
                var sinpagoAlContadoEvangelistas = Updater.ObjectSpace.CreateObject<PaymentPlan>();
                sinpagoAlContadoEvangelistas.Description = "No aplica siembra";
                sinpagoAlContadoEvangelistas.Sector = evangelistasSector;
                sinpagoAlContadoEvangelistas.LimitDate = null;
                #endregion

                #region 5 semillas
                var septiembre30 = DateTime.Parse("09/30/2023", new CultureInfo("en-US", true));
                var octubre8 = DateTime.Parse("10/08/2023", new CultureInfo("en-US", true));
                var noviembre12 = DateTime.Parse("11/12/2023", new CultureInfo("en-US", true));
                var diciembre10 = DateTime.Parse("12/10/2023", new CultureInfo("en-US", true));
                var enero14 = DateTime.Parse("01/14/2024", new CultureInfo("en-US", true));

                var cincoSemillasApostoles = Updater.ObjectSpace.CreateObject<PaymentPlan>();
                cincoSemillasApostoles.Description = "5 semillas";
                cincoSemillasApostoles.Sector = apostolesSector;
                cincoSemillasApostoles.LimitDate = septiembre30;

                var cuota1CincoSemillasApostoles = Updater.ObjectSpace.CreateObject<PaymentPlanDetail>();
                cuota1CincoSemillasApostoles.Number = 1;
                cuota1CincoSemillasApostoles.Description = "Semilla 1";
                cuota1CincoSemillasApostoles.Amount = 100;
                cuota1CincoSemillasApostoles.LimitDate = null;

                var cuota2CincoSemillasApostoles = Updater.ObjectSpace.CreateObject<PaymentPlanDetail>();
                cuota2CincoSemillasApostoles.Number = 2;
                cuota2CincoSemillasApostoles.Description = "Semilla 2";
                cuota2CincoSemillasApostoles.Amount = 100;
                cuota2CincoSemillasApostoles.LimitDate = octubre8;

                var cuota3CincoSemillasApostoles = Updater.ObjectSpace.CreateObject<PaymentPlanDetail>();
                cuota3CincoSemillasApostoles.Number = 3;
                cuota3CincoSemillasApostoles.Description = "Semilla 3";
                cuota3CincoSemillasApostoles.Amount = 100;
                cuota3CincoSemillasApostoles.LimitDate = noviembre12;

                var cuota4CincoSemillasApostoles = Updater.ObjectSpace.CreateObject<PaymentPlanDetail>();
                cuota4CincoSemillasApostoles.Number = 4;
                cuota4CincoSemillasApostoles.Description = "Semilla 4";
                cuota4CincoSemillasApostoles.Amount = 100;
                cuota4CincoSemillasApostoles.LimitDate = diciembre10;

                var cuota5CincoSemillasApostoles = Updater.ObjectSpace.CreateObject<PaymentPlanDetail>();
                cuota5CincoSemillasApostoles.Number = 5;
                cuota5CincoSemillasApostoles.Description = "Semilla 5";
                cuota5CincoSemillasApostoles.Amount = 100;
                cuota5CincoSemillasApostoles.LimitDate = enero14;

                cincoSemillasApostoles.PaymentPlanDetails.Add(cuota1CincoSemillasApostoles);
                cincoSemillasApostoles.PaymentPlanDetails.Add(cuota2CincoSemillasApostoles);
                cincoSemillasApostoles.PaymentPlanDetails.Add(cuota3CincoSemillasApostoles);
                cincoSemillasApostoles.PaymentPlanDetails.Add(cuota4CincoSemillasApostoles);
                cincoSemillasApostoles.PaymentPlanDetails.Add(cuota5CincoSemillasApostoles);

                #endregion
            }
        }

        public void Execute2()
        {
            bool isEmpty = !(from ps in Updater.Session.Query<PaymentPlan>()
                             select ps).Any();
            if (isEmpty)
            {
                var lionSector = (from s in Updater.Session.Query<Sector>()
                                  where s.InternalId == Sector.LionSectorId
                                  select s).Single();

                var shofarSector = (from s in Updater.Session.Query<Sector>()
                                    where s.InternalId == Sector.ShofarSectorId
                                    select s).Single();

                var eagleSector = (from s in Updater.Session.Query<Sector>()
                                   where s.InternalId == Sector.EagleSectorId
                                   select s).Single();

                /////Lion contado
                var pagoAlContadoLion = Updater.ObjectSpace.CreateObject<PaymentPlan>();
                pagoAlContadoLion.Description = "Ofrenda al contado";
                pagoAlContadoLion.Sector = lionSector;
                pagoAlContadoLion.LimitDate = null;

                var cuota1PagoAlContadoLion = Updater.ObjectSpace.CreateObject<PaymentPlanDetail>();
                cuota1PagoAlContadoLion.Number = 1;
                cuota1PagoAlContadoLion.Description = "Ofrenda al contado";
                cuota1PagoAlContadoLion.Percentage = 1;
                cuota1PagoAlContadoLion.LimitDate = null;

                pagoAlContadoLion.PaymentPlanDetails.Add(cuota1PagoAlContadoLion);

                /////Lion sin pago
                var sinPagoLion = Updater.ObjectSpace.CreateObject<PaymentPlan>();
                sinPagoLion.Description = "No aplica siembra";
                sinPagoLion.Sector = lionSector;
                sinPagoLion.LimitDate = null;


                //Shofar contado
                var pagoAlContadoShofar = Updater.ObjectSpace.CreateObject<PaymentPlan>();
                pagoAlContadoShofar.Description = "Pago al contado";
                pagoAlContadoShofar.Sector = shofarSector;
                pagoAlContadoShofar.LimitDate = null;

                var cuota1PagoAlContadoShofar = Updater.ObjectSpace.CreateObject<PaymentPlanDetail>();
                cuota1PagoAlContadoShofar.Number = 1;
                cuota1PagoAlContadoShofar.Description = "Pago al contado";
                cuota1PagoAlContadoShofar.Percentage = 1;
                cuota1PagoAlContadoShofar.LimitDate = null;

                pagoAlContadoShofar.PaymentPlanDetails.Add(cuota1PagoAlContadoShofar);
                /////Shofar sin pago
                var sinPagoShofar = Updater.ObjectSpace.CreateObject<PaymentPlan>();
                sinPagoShofar.Description = "Sin pago";
                sinPagoShofar.Sector = shofarSector;
                sinPagoShofar.LimitDate = null;


                //Eagle contado
                var pagoAlContadoEagle = Updater.ObjectSpace.CreateObject<PaymentPlan>();
                pagoAlContadoEagle.Description = "Pago al contado";
                pagoAlContadoEagle.Sector = eagleSector;
                pagoAlContadoEagle.LimitDate = null;

                var cuota1PagoAlContadoEagle = Updater.ObjectSpace.CreateObject<PaymentPlanDetail>();
                cuota1PagoAlContadoEagle.Number = 1;
                cuota1PagoAlContadoEagle.Description = "Pago al contado";
                cuota1PagoAlContadoEagle.Percentage = 1;
                cuota1PagoAlContadoEagle.LimitDate = null;

                pagoAlContadoEagle.PaymentPlanDetails.Add(cuota1PagoAlContadoEagle);
                /////Shofar sin pago
                var sinPagoEagle = Updater.ObjectSpace.CreateObject<PaymentPlan>();
                sinPagoEagle.Description = "Sin pago";
                sinPagoEagle.Sector = eagleSector;
                sinPagoEagle.LimitDate = null;

                var junio30 = DateTime.Parse("06/30/2023", new CultureInfo("en-US", true));
                var agosto1 = DateTime.Parse("08/01/2023", new CultureInfo("en-US", true));
                var agosto13 = DateTime.Parse("08/13/2023", new CultureInfo("en-US", true));
                var julio12 = DateTime.Parse("07/12/2023", new CultureInfo("en-US", true));
                //3 cuotas

                //shofar
                var pago3CuotasShofar = Updater.ObjectSpace.CreateObject<PaymentPlan>();
                pago3CuotasShofar.Description = "3 cuotas";
                pago3CuotasShofar.Sector = shofarSector;
                pago3CuotasShofar.LimitDate = junio30;

                var cuota1Pago3cuotasShofar = Updater.ObjectSpace.CreateObject<PaymentPlanDetail>();
                cuota1Pago3cuotasShofar.Number = 1;
                cuota1Pago3cuotasShofar.Description = "Cuota 1";
                cuota1Pago3cuotasShofar.Percentage = 0.4m;
                cuota1Pago3cuotasShofar.LimitDate = null;

                pago3CuotasShofar.PaymentPlanDetails.Add(cuota1Pago3cuotasShofar);

                var cuota2Pago3cuotasShofar = Updater.ObjectSpace.CreateObject<PaymentPlanDetail>();
                cuota2Pago3cuotasShofar.Number = 2;
                cuota2Pago3cuotasShofar.Description = "Cuota 2";
                cuota2Pago3cuotasShofar.Percentage = 0.3m;
                cuota2Pago3cuotasShofar.LimitDate = julio12;

                pago3CuotasShofar.PaymentPlanDetails.Add(cuota2Pago3cuotasShofar);

                var cuota3Pago3cuotasShofar = Updater.ObjectSpace.CreateObject<PaymentPlanDetail>();
                cuota3Pago3cuotasShofar.Number = 3;
                cuota3Pago3cuotasShofar.Description = "Cuota 3";
                cuota3Pago3cuotasShofar.Percentage = 0.3m;
                cuota3Pago3cuotasShofar.LimitDate = agosto13;

                pago3CuotasShofar.PaymentPlanDetails.Add(cuota3Pago3cuotasShofar);

                //aguila 3 cuotas

                //aguila 
                var pago3CuotasAguila = Updater.ObjectSpace.CreateObject<PaymentPlan>();
                pago3CuotasAguila.Description = "3 cuotas";
                pago3CuotasAguila.Sector = eagleSector;
                pago3CuotasAguila.LimitDate = junio30;

                var cuota1Pago3cuotasAguila = Updater.ObjectSpace.CreateObject<PaymentPlanDetail>();
                cuota1Pago3cuotasAguila.Number = 1;
                cuota1Pago3cuotasAguila.Description = "Cuota 1";
                cuota1Pago3cuotasAguila.Percentage = 0.43m;
                cuota1Pago3cuotasAguila.LimitDate = null;

                pago3CuotasAguila.PaymentPlanDetails.Add(cuota1Pago3cuotasAguila);

                var cuota2Pago3cuotasEagle = Updater.ObjectSpace.CreateObject<PaymentPlanDetail>();
                cuota2Pago3cuotasEagle.Number = 2;
                cuota2Pago3cuotasEagle.Description = "Cuota 2";
                cuota2Pago3cuotasEagle.Percentage = 0.285m;
                cuota2Pago3cuotasEagle.LimitDate = julio12;

                pago3CuotasAguila.PaymentPlanDetails.Add(cuota2Pago3cuotasEagle);

                var cuota3Pago3cuotasEagle = Updater.ObjectSpace.CreateObject<PaymentPlanDetail>();
                cuota3Pago3cuotasEagle.Number = 3;
                cuota3Pago3cuotasEagle.Description = "Cuota 3";
                cuota3Pago3cuotasEagle.Percentage = 0.285m;
                cuota3Pago3cuotasEagle.LimitDate = agosto13;

                pago3CuotasAguila.PaymentPlanDetails.Add(cuota3Pago3cuotasEagle);


                //2 cuotas
                //shofar
                var pago2CuotasShofar = Updater.ObjectSpace.CreateObject<PaymentPlan>();
                pago2CuotasShofar.Description = "2 cuotas";
                pago2CuotasShofar.Sector = shofarSector;
                pago2CuotasShofar.LimitDate = agosto1;

                var cuota1Pago2cuotasShofar = Updater.ObjectSpace.CreateObject<PaymentPlanDetail>();
                cuota1Pago2cuotasShofar.Number = 1;
                cuota1Pago2cuotasShofar.Description = "Cuota 1";
                cuota1Pago2cuotasShofar.Percentage = 0.7m;
                cuota1Pago2cuotasShofar.LimitDate = null;

                pago2CuotasShofar.PaymentPlanDetails.Add(cuota1Pago2cuotasShofar);

                var cuota2Pago2cuotasShofar = Updater.ObjectSpace.CreateObject<PaymentPlanDetail>();
                cuota2Pago2cuotasShofar.Number = 2;
                cuota2Pago2cuotasShofar.Description = "Cuota 2";
                cuota2Pago2cuotasShofar.Percentage = 0.3m;
                cuota2Pago2cuotasShofar.LimitDate = agosto13;

                pago2CuotasShofar.PaymentPlanDetails.Add(cuota2Pago2cuotasShofar);

                //aguila
                var pago2CuotasEagle = Updater.ObjectSpace.CreateObject<PaymentPlan>();
                pago2CuotasEagle.Description = "2 cuotas";
                pago2CuotasEagle.Sector = eagleSector;
                pago2CuotasEagle.LimitDate = agosto1;

                var cuota1Pago2cuotasEagle = Updater.ObjectSpace.CreateObject<PaymentPlanDetail>();
                cuota1Pago2cuotasEagle.Number = 1;
                cuota1Pago2cuotasEagle.Description = "Cuota 1";
                cuota1Pago2cuotasEagle.Percentage = 0.71m;
                cuota1Pago2cuotasEagle.LimitDate = null;

                pago2CuotasEagle.PaymentPlanDetails.Add(cuota1Pago2cuotasEagle);

                var cuota2Pago2cuotasEagle = Updater.ObjectSpace.CreateObject<PaymentPlanDetail>();
                cuota2Pago2cuotasEagle.Number = 2;
                cuota2Pago2cuotasEagle.Description = "Cuota 2";
                cuota2Pago2cuotasEagle.Percentage = 0.29m;
                cuota2Pago2cuotasEagle.LimitDate = agosto13;

                pago2CuotasEagle.PaymentPlanDetails.Add(cuota2Pago2cuotasEagle);


            }
        }

    }
}
