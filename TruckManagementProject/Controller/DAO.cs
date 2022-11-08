using Microsoft.EntityFrameworkCore; //for Include
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        //-------------Adds Truck Record to TruckRental object in the database rental table and changes the availablity status when user rents a truck
        public static void rentTruck(TruckRental rental, int id)
        {
            id = rental.TruckId;
            using (DAD_ChristianContext ctx = new DAD_ChristianContext())
            {
                ctx.TruckRentals.Add(rental);
                changeAvailabilityStatus(id);
                ctx.SaveChanges();
            }
        }

        //-------------gets or searches Customer By name
        public static TruckCustomer getCustomerName(string customerName)
        {
            using (DAD_ChristianContext ctx = new DAD_ChristianContext())
            {
                return ctx.TruckCustomers.Include(c => c.Customer).Where(cn => cn.Customer.Name == customerName).FirstOrDefault();

            }

        }

        //-------------Creates a List of All customers
        public static List<TruckCustomer> getAllCustomers()
        {
            using (DAD_ChristianContext ctx = new DAD_ChristianContext())
            {
                return ctx.TruckCustomers.Include(c => c.Customer).ToList();
            }
        }

        //-------------Creates a list of Customers by Name
        public static List<TruckCustomer> getCustomerNameLicense(string customerNameAndLicense)
        {
            using (DAD_ChristianContext ctx = new DAD_ChristianContext())
            {
                return ctx.TruckCustomers.Include(c => c.Customer).Where(cn => cn.Customer.Name == customerNameAndLicense).ToList();

            }

        }

        //-------------Get Truck By registration
        public static IndividualTruck getTruckRego(string Rego)
        {

            using (DAD_ChristianContext ctx = new DAD_ChristianContext())
            {

                return ctx.IndividualTrucks.Include(tm => tm.TruckModel).Where(r => r.RegistrationNumber == Rego).FirstOrDefault();


            }
        }

        //-------------Creates a List of All Customers
        public static TruckCustomer getCustomerLicense(string customerLicense)
        {
            using (DAD_ChristianContext ctx = new DAD_ChristianContext())
            {
                return ctx.TruckCustomers.Include(c => c.Customer).Where(cn => cn.LicenseNumber == customerLicense).FirstOrDefault();

            }

        }
        //-------------Gets individual Trucks
        public static List<IndividualTruck> getIndividualTrucks()
        {
            using (DAD_ChristianContext ctx = new DAD_ChristianContext())
            {
                return ctx.IndividualTrucks.ToList();

            }
        }


        //-------------Get all available trucks with "Available for Rent" Status
        public static List<IndividualTruck> getAvailableTrucks()
        {
            using (DAD_ChristianContext ctx = new DAD_ChristianContext())
            {
                return ctx.IndividualTrucks.Include(tm => tm.TruckModel).Where(ts => ts.Status == "Available for Rent").ToList();
            }
        }

        //-------------Get all available trucks with "Rented" Status
        public static List<IndividualTruck> getRentedOutTrucks()
        {
            using (DAD_ChristianContext ctx = new DAD_ChristianContext())
            {
                return ctx.IndividualTrucks.Include(rm => rm.TruckModel).Where(ro => ro.Status == "Rented").ToList();
            }

        }
        //---------Gets attributes and methods from the CustomDisplayRentalsByCustomer class and turns it into a List
        public static List<CustomDisplayRentalsByCustomer> displayRentalsByCustomer(int cid)
        {
            using (DAD_ChristianContext ctx = new DAD_ChristianContext())
            {
                return ctx.TruckRentals.Where(t => t.CustomerId == cid).Select(rentalRecords => new CustomDisplayRentalsByCustomer
                {
                    RentalId = rentalRecords.RentalId,
                    RegistrationNumber = rentalRecords.Truck.RegistrationNumber,
                    Name = rentalRecords.Customer.Customer.Name,
                    RentDate = rentalRecords.RentDate.ToString("dd/MM/yyyy"),
                    ReturnDueDate = rentalRecords.ReturnDueDate.ToString("dd/MM/yyyy"),
                    ReturnDate = rentalRecords.ReturnDate.ToString("dd/MM/yyyy"),
                    TotalPrice = string.Format("{0:F2}", rentalRecords.TotalPrice),

                }).ToList();

            }

        }

        //---------Gets Rental Records by Rentdate and matches it between Two Input Parameters with the DateTime DataType firstDate and secondDate to see how many Trucks have been rented between those two input parameters or Selected Dates
        public static List<CustomDisplayRentalsByCustomer> getRentalBetweenDates(DateTime firsDate, DateTime secondDate)
        {
            using (DAD_ChristianContext ctx = new DAD_ChristianContext())
            {
                return ctx.TruckRentals.Where(rd => rd.RentDate >= firsDate && rd.RentDate <= secondDate).Select(rentalRecords => new CustomDisplayRentalsByCustomer
                {

                    RentalId = rentalRecords.RentalId,
                    RegistrationNumber = rentalRecords.Truck.RegistrationNumber,
                    Name = rentalRecords.Customer.Customer.Name,
                    RentDate = rentalRecords.RentDate.ToString("dd/MM/yyyy"),
                    ReturnDueDate = rentalRecords.ReturnDueDate.ToString("dd/MM/yyyy"),
                    ReturnDate = rentalRecords.ReturnDate.ToString("dd/MM/yyyy"),
                    TotalPrice = string.Format("{0:F2}", rentalRecords.TotalPrice),
                }).ToList();

            }
        }


        //---------Changes The status of each individual truck when called
        private static void changeAvailabilityStatus(int truckId)
        {
            using (DAD_ChristianContext ctx = new DAD_ChristianContext())
            {
                IndividualTruck id = ctx.IndividualTrucks.Where(it => it.TruckId == truckId).FirstOrDefault();
                if (id != null)
                {
                    if (id.Status == "Available for rent")
                    {
                        id.Status = "Rented";
                        //ctx.Entry(id.Status).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                    else
                    {
                        id.Status = "Available for rent";

                    }


                    ctx.SaveChanges();
                }
            }
        }


        //---------Gets Truck Rental Record of Individual Truck By registration Number
        public static TruckRental getRentalRecordByRego(string record)
        {
            using (DAD_ChristianContext ctx = new DAD_ChristianContext())
            {
                return ctx.TruckRentals.Include(t => t.Truck).Where(tr => tr.Truck.RegistrationNumber == record).FirstOrDefault();
            }
        }


        //---------Changes the rental record status of an individual truck when User returns a truck
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
                //assigns the truck id and feature id for the TruckFeatureAssociation table
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

        //Created a Custom class to
        //display combine individual truck, model, and features table in datagrid
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
                        Features = tbm.TruckFeatureAssociations.Select(f => f.Feature.Description).ToList()
                    }).ToList();
            }
        }

        //Displays the selected model details after user search
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
                        Features = tbm.TruckFeatureAssociations.Select(f => f.Feature.Description).ToList()
                    }).ToList();
            }
        }

        //Used to determine if the model input from user matches models in database
        public static TruckModel searchModelByName(string name)
        {
            using (DAD_ChristianContext ctx = new DAD_ChristianContext())
            {
                return ctx.TruckModels.Where(truckF => truckF.Model == name).FirstOrDefault();
            }
        }


        //-------------Show All Truck

        //Display all trucks with details from the Database
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
                        Features = im.TruckFeatureAssociations.Select(f => f.Feature.Description).ToList()
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
                        Features = im.TruckFeatureAssociations.Select(f => f.Feature.Description).ToList()
                    }).ToList();
            }
        }

        //-----------Update Trucks

        //Get all Truck Features
        public static List<TruckFeature> getFeatures()
        {
            using (DAD_ChristianContext ctx = new DAD_ChristianContext())
            {
                return ctx.TruckFeatures.ToList();
            }
        }

        //Searching Truck based on Registration Number
        public static IndividualTruck searchRegisNo(string regisNo)
        {
            using (DAD_ChristianContext ctx = new DAD_ChristianContext())
            {
                return ctx.IndividualTrucks.Include(regis => regis.TruckModel).Where(r => r.RegistrationNumber == regisNo).FirstOrDefault();
            }
        }

        //Display features of a specific truck using Truck.ID
        public static List<TruckFeatureAssociation> displayTruckFeaturesOfTruck(int id)
        {
            using (DAD_ChristianContext ctx = new DAD_ChristianContext())
            {
                return ctx.TruckFeatureAssociations.Include(f => f.Feature).Where(truck => truck.TruckId == id).ToList();
            }
        }


        //Update Truck Details
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

        //Adding new truck features 
        public static void addTruckFeatures(TruckFeatureAssociation tfa)
        {
            using (DAD_ChristianContext ctx = new DAD_ChristianContext())
            {
                ctx.TruckFeatureAssociations.Add(tfa);
                ctx.SaveChanges();
            }
        }

        //Deleting existing features 
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