using Microsoft.EntityFrameworkCore; //for Include
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckManagementProject.Models.DB;

namespace TruckManagementProject.Controller
{
    internal class DAO
    {
      
        static public TruckEmployee userLogin(string username, string pwd)
        {
            using (DAD_ChristianContext ctx = new DAD_ChristianContext())
            {
                TruckEmployee tp = ctx.TruckEmployees.Where(tp => tp.Username == username && tp.Password == pwd).FirstOrDefault();
                return tp;
            }
        }
        
        /*------Customer Management-----------------------------------------------------------------------------------------------------------*/
        static public void addCustomer(TruckCustomer cust)
        {
            using (DAD_ChristianContext ctx = new DAD_ChristianContext())
            {
                ctx.TruckCustomers.Add(cust);
                ctx.SaveChanges();

            }
        }

        static public TruckCustomer searchCustomer(int cid)
        {
            using (DAD_ChristianContext ctx = new DAD_ChristianContext())
            {

                return ctx.TruckCustomers.Include(ct => ct.Customer).Where(c => c.CustomerId == cid).FirstOrDefault();
            }
        }

        static public void updateCustomer(TruckCustomer cust, TruckPerson cp)
        {
            using (DAD_ChristianContext ctx = new DAD_ChristianContext())
            {
                ctx.Entry(cust).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                ctx.Entry(cp).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                ctx.SaveChanges();

            }
        }

        static public void addEmployee(TruckEmployee em)
        {
            using (DAD_ChristianContext ctx = new DAD_ChristianContext())
            {
                ctx.TruckEmployees.Add(em);
                ctx.SaveChanges();

            }
        }

        static public TruckEmployee searchEmployee(int eid)
        {
            using (DAD_ChristianContext ctx = new DAD_ChristianContext())
            {
                return ctx.TruckEmployees.Include(et => et.Employee).Where(e => e.EmployeeId == eid).FirstOrDefault();
            }
        }

        static public void updateEmployee(TruckEmployee em, TruckPerson ep)
        {
            using (DAD_ChristianContext ctx = new DAD_ChristianContext())
            {
                ctx.Entry(em).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                ctx.Entry(ep).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        /*------Rental Management-------------------------------------------------------------------------------*/
        public static TruckCustomer getCustomerName(string customerName)
        {
            using (DAD_ChristianContext ctx = new DAD_ChristianContext())
            {
                return ctx.TruckCustomers.Include(c => c.Customer).Where(cn => cn.Customer.Name == customerName).FirstOrDefault();

            }

        }

        public static List<TruckCustomer> getCustomerNameLicense(string customerNameAndLicense)
        {
            using (DAD_ChristianContext ctx = new DAD_ChristianContext())
            {
                return ctx.TruckCustomers.Include(c => c.Customer).Where(cn => cn.Customer.Name == customerNameAndLicense).ToList();

            }

        }
        public static TruckCustomer getCustomerLicense(string customerLicense)
        {
            using (DAD_ChristianContext ctx = new DAD_ChristianContext())
            {
                return ctx.TruckCustomers.Include(c => c.Customer).Where(cn => cn.LicenseNumber == customerLicense).FirstOrDefault();

            }

        }

        public static List<IndividualTruck> getIndividualTrucks()
        {
            using (DAD_ChristianContext ctx = new DAD_ChristianContext())
            {
                return ctx.IndividualTrucks.ToList();

            }
        }

        public static IndividualTruck getTruckRego(string Rego)
        {
            using (DAD_ChristianContext ctx = new DAD_ChristianContext())
            {
                return ctx.IndividualTrucks.Include(tm => tm.TruckModel).Where(r => r.RegistrationNumber == Rego).FirstOrDefault();

            }
        }

        public static void changeAvailabilityStatus(int truckId)
        {
            using (DAD_ChristianContext ctx = new DAD_ChristianContext())
            {
                IndividualTruck id = ctx.IndividualTrucks.Where(it => it.TruckId == truckId).FirstOrDefault();
                if (id != null)
                {
                    if (id.Status == "Available for rent")
                    {
                        id.Status = "Rented";
                    }
                    else
                    {
                        id.Status = "Available for rent";
                    }

                    ctx.SaveChanges();
                }
            }
        }

        public static void rentTruck(TruckRental rental, int id)
        {
            id = rental.TruckId;
            using (DAD_ChristianContext ctx = new DAD_ChristianContext())
            {
                ctx.Add(rental);
                changeAvailabilityStatus(id);
                ctx.SaveChanges();
            }
        }

        public static TruckRental getRentalRecordByRego(string record)
        {
            using (DAD_ChristianContext ctx = new DAD_ChristianContext())
            {
                return ctx.TruckRentals.Include(t => t.Truck).Where(tr => tr.Truck.RegistrationNumber == record).FirstOrDefault();
            }
        }

        public static void ReturnTruck(TruckRental rentalRecord)
        {
            using (DAD_ChristianContext ctx = new DAD_ChristianContext())
            {
                ctx.Entry(rentalRecord).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                int id = rentalRecord.TruckId;
                changeAvailabilityStatus(id);
                ctx.SaveChanges();
            }
        }

        /*------Truck Management-------------------------------------------------------------------------------*/

        //--------Add New Truck
        public static void addTruck(IndividualTruck ittruck, List<TruckFeature> Tassociation)
        {

            List<TruckFeatureAssociation> tfalist = new List<TruckFeatureAssociation>();
            foreach (var feature in Tassociation)
            {
                TruckFeatureAssociation tf = new TruckFeatureAssociation();
                tf.TruckId = ittruck.TruckId;
                tf.FeatureId = feature.FeatureId;
                tfalist.Add(tf);
            }
            ittruck.TruckFeatureAssociations = tfalist;
            using (DAD_ChristianContext ctx = new DAD_ChristianContext())
            {
                ctx.IndividualTrucks.Add(ittruck);
                ctx.SaveChanges();
            }
        }

        public static TruckFeature searchFeatureById(int id)
        {
            using (DAD_ChristianContext ctx = new DAD_ChristianContext())
            {
                //truckF.FeatureId(from TruckFeatures table) == id 
                return ctx.TruckFeatures.Where(truckF => truckF.FeatureId == id).FirstOrDefault();
            }
        }

        //-------SearchTruckByModel
        public static List<CustomSearchTruckByModel> getTruckModelFullDetails()
        {
            using (DAD_ChristianContext ctx = new DAD_ChristianContext())
            {
                return ctx.IndividualTrucks.Include(it => it.TruckModel).Select(
                    tbm => new CustomSearchTruckByModel()
                    {
                        TruckId = tbm.TruckId,
                        Model = tbm.TruckModel.Model,
                        Colour = tbm.Colour,
                        DailyRentalPrice = tbm.DailyRentalPrice,
                        Size = tbm.TruckModel.Size,
                        Status = tbm.Status,
                        features = tbm.TruckFeatureAssociations.Select(f => f.Feature.Description).ToList()
                    }).ToList();
            }
        }
        public static List<CustomSearchTruckByModel> searchTruckModel(string model)
        {
            using (DAD_ChristianContext ctx = new DAD_ChristianContext())
            {

                return ctx.IndividualTrucks.Include(it => it.TruckModel).Where(tm => tm.TruckModel.Model == model).Select(
                    tbm => new CustomSearchTruckByModel()
                    {
                        TruckId = tbm.TruckId,
                        Model = tbm.TruckModel.Model,
                        Colour = tbm.Colour,
                        DailyRentalPrice = tbm.DailyRentalPrice,
                        Size = tbm.TruckModel.Size,
                        Status = tbm.Status,
                        features = tbm.TruckFeatureAssociations.Select(f => f.Feature.Description).ToList()
                    }).ToList();
            }
        }

        public static TruckModel searchModelByName(string name)
        {
            using (DAD_ChristianContext ctx = new DAD_ChristianContext())
            {
                return ctx.TruckModels.Where(truckF => truckF.Model == name).FirstOrDefault();
            }
        }


        //-------------Show All Truck
        public static List<CustomTruckModelClass> displayAllTrucks()
        {
            using (DAD_ChristianContext ctx = new DAD_ChristianContext())
            {
                //linking IndividualTrucks and TruckModel
                return ctx.IndividualTrucks.Include(it => it.TruckModel).Select(
                    im => new CustomTruckModelClass()
                    {
                        TruckId = im.TruckId,
                        Model = im.TruckModel.Model,
                        Status = im.Status,
                        Registration_Num = im.RegistrationNumber,
                        Fuel_Type = im.FuelType,
                        Transmission = im.Transmission,

                        WOFExpiry_Date = im.WofexpiryDate.ToString(),
                        RegistrationExpiry_Date = im.RegistrationExpiryDate.ToString(),
                        Date_Imported = im.DateImported.ToString(),
                        Manufacture_Year = im.ManufactureYear,

                        Rental_Price = im.DailyRentalPrice.ToString(),
                        Advance_Deposit = im.AdvanceDepositRequired.ToString(),
                        Manufacturer = im.TruckModel.Manufacturer,
                        Size = im.TruckModel.Size,
                        features = im.TruckFeatureAssociations.Select(f => f.Feature.Description).ToList()
                    }).ToList();
            }
        }


        //---------Show Available Trucks
        public static List<CustomTruckModelClass> displayAllAvailableTrucks()
        {
            using (DAD_ChristianContext ctx = new DAD_ChristianContext())

            {
                //getting IndividualTruck truck.Status and specifying the value with availaible for rent
                return ctx.IndividualTrucks.Include(it => it.TruckModel).Where(itstat => itstat.Status == "Available for rent").Select(
                    im => new CustomTruckModelClass()
                    {
                        TruckId = im.TruckId,
                        Model = im.TruckModel.Model,
                        Status = im.Status,
                        Registration_Num = im.RegistrationNumber,
                        Fuel_Type = im.FuelType,
                        Transmission = im.Transmission,

                        WOFExpiry_Date = im.WofexpiryDate.ToString(),
                        RegistrationExpiry_Date = im.RegistrationExpiryDate.ToString(),
                        Date_Imported = im.DateImported.ToString(),
                        Manufacture_Year = im.ManufactureYear,

                        Rental_Price = im.DailyRentalPrice.ToString(),
                        Advance_Deposit = im.AdvanceDepositRequired.ToString(),
                        Manufacturer = im.TruckModel.Manufacturer,
                        Size = im.TruckModel.Size,
                        features = im.TruckFeatureAssociations.Select(f => f.Feature.Description).ToList()
                    }).ToList();
            }
        }

        //-----------Update Trucks
        public static List<TruckFeature> getFeatures()
        {
            using (DAD_ChristianContext ctx = new DAD_ChristianContext())
            {
                //truckF.FeatureId(from TruckFeatures table) == id 
                return ctx.TruckFeatures.ToList();
            }
        }

        public static IndividualTruck searchRegisNo(string regisNo)
        {
            using (DAD_ChristianContext ctx = new DAD_ChristianContext())
            {
                return ctx.IndividualTrucks.Include(regis => regis.TruckModel).Where(r => r.RegistrationNumber == regisNo).FirstOrDefault();
            }
        }

        public static List<TruckFeatureAssociation> displayTruckFeaturesOfTruck(int id)
        {
            using (DAD_ChristianContext ctx = new DAD_ChristianContext())
            {
                return ctx.TruckFeatureAssociations.Include(f => f.Feature).Where(truck => truck.TruckId == id).ToList();
            }
        }

        public static void updateTruck(IndividualTruck individualTruck, TruckModel truckModel)
        {
            using (DAD_ChristianContext ctx = new DAD_ChristianContext())
            {
                ctx.Entry(individualTruck).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                ctx.Entry(truckModel).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
                //  ctx.Add(tfa);
                ctx.SaveChanges();
            }
        }

        public static void addTruckFeatures(TruckFeatureAssociation tfa)
        {
            using (DAD_ChristianContext ctx = new DAD_ChristianContext())
            {
                ctx.TruckFeatureAssociations.Add(tfa);
                ctx.SaveChanges();
            }
        }

        public static void deleteExistingFeature(TruckFeatureAssociation tfa)
        {
            using (DAD_ChristianContext ctx = new DAD_ChristianContext())
            {
                ctx.TruckFeatureAssociations.Remove(tfa);
                ctx.SaveChanges();
            }

        }

    }
}
