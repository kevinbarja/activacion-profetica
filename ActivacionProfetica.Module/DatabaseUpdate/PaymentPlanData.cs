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
                pagoAlContadoLion.Description = "Pago al contado";
                pagoAlContadoLion.Sector = lionSector;
                pagoAlContadoLion.LimitDate = null;

                var cuota1PagoAlContadoLion = Updater.ObjectSpace.CreateObject<PaymentPlanDetail>();
                cuota1PagoAlContadoLion.Number = 1;
                cuota1PagoAlContadoLion.Description = "Pago al contado";
                cuota1PagoAlContadoLion.Percentage = 1;
                cuota1PagoAlContadoLion.LimitDate = null;

                pagoAlContadoLion.PaymentPlanDetails.Add(cuota1PagoAlContadoLion);

                /////Lion sin pago
                var sinPagoLion = Updater.ObjectSpace.CreateObject<PaymentPlan>();
                sinPagoLion.Description = "Sin pago";
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
