using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MelbourneData.Areas.MoulaCodeChalleng.Models;

namespace MelbourneData.Areas.MoulaCodeChalleng.Controllers
{
    public class CustomerController : Controller
    {
        public IDbContext dbContext;

        //Dependency Injections
        public CustomerController()
        {
            dbContext = new DatabaseContext();
        }
        public CustomerController(IDbContext repo)
        {
            dbContext = repo;
        }

        /// <summary>
        /// Default page load method
        /// </summary>
        /// <returns>Create customer page</returns>
        [HttpGet]
        public ViewResult CreateCustomer()
        {
            return View();
        }

        /// <summary>
        /// Retrieves all customers and displays them in a table
        /// </summary>
        /// <returns>Page with a model containing all customers</returns>
        [HttpGet]
        public ViewResult ListCustomer()
        {
            List<Customer> customers = new List<Customer>();
            using (var databaseContext = dbContext)
            {
                try
                {
                    var customerRepository = new Repository.CustomerRepository(databaseContext);
                    customers = customerRepository.GetAllCustomers();
                }
                ///////???????????????????????????????????????????????? empty Catch
                catch { }
                return View(customers);
            }
        }

        /// <summary>
        /// takes the input fields and validates user doesnt
        /// </summary>
        /// <param name="newCustomer">data from web form</param>
        /// <returns>result message and redirection to index if pass and new customerPage if the insert fails </returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult CreateCustomer(Customer newCustomer)
        {
            if (ModelState.IsValid)
            {
                //Creation of the customer code from first, last and Dob.
                string custCode = CreateCustomerCode(newCustomer.FirstName, newCustomer.LastName, newCustomer.DateOfBirth);
                try
                {
                    // Check to see if customer exists otherwise insert net customer and return message.
                    using (var dataContext = dbContext)
                    {
                        var customerRepository = new Repository.CustomerRepository(dataContext);

                        if (customerRepository.DoesCustomerExist(custCode))
                        {
                            TempData["Message"] = "Customer Already Exists";
                            return View("CreateCustomer");
                        }

                        //creation of customer code
                        newCustomer.CustCode = custCode;
                        customerRepository.Insert(newCustomer);
                        dataContext.SaveChanges();
                    }
                    TempData["Message"] = "Registered Successfully";
                    return RedirectToAction("Index", "Home");
                }
                // catch all exceptions for things like unable to connect to database
                catch (Exception e)
                {
                    //temporary logging till a logger can be implemented
                    Console.Out.WriteLine(e.Message);
                    TempData["Message"] = "Failed to create customer";
                    return View("CreateCustomer");
                }
            }
            return View();
        }

        /// <summary>
        /// Creation from variables the customer code. 
        /// Note. no validation on incoming data performed
        /// </summary>
        /// <param name="firstName">first name of customer</param>
        /// <param name="lastName">last name of customer</param>
        /// <param name="dateOfBirth">Date of birth in DateTime variable</param>
        /// <returns>concatenated lowercase string of input values</returns>
        [NonAction]
        public string CreateCustomerCode(string firstName, string lastName, DateTime dateOfBirth)
        {
            return string.Join(firstName.ToLower(), lastName.ToLower(), dateOfBirth.ToString("yyyyMMdd"));
        }
    }
}
