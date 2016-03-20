using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WCFRESTfulService.Model;

namespace WCFRESTfulService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CustomerService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CustomerService.svc or CustomerService.svc.cs at the Solution Explorer and start debugging.
    public class CustomerService : ICustomerService
    {
        NorthwindEntities dc;
        public CustomerService()
        {
            dc = new NorthwindEntities();
        }
        public List<CustomerDataContract> GetAllCustomer()
        {
            var query = (from a in dc.Customers
                         select a).Distinct();

            List<CustomerDataContract> CustomersList = new List<CustomerDataContract>();

            query.ToList().ForEach(x =>
            {
                CustomersList.Add(new CustomerDataContract
                {
                    CustomerID = Convert.ToString(x.CustomerID),
                    CompanyName = x.CompanyName,
                    ContactName = x.ContactName,
                    ContactTitle = x.ContactTitle,
                    Address = x.Address,
                    City = x.City,
                    Region = x.Region,
                    PostalCode = x.PostalCode,
                    Country = x.Country,
                    Phone = x.Phone,
                    Fax = x.Fax,
                });
            });
            return CustomersList;
        }

        public CustomerDataContract CustomerDetails(string customerID)
        {
            CustomerDataContract Cust = new CustomerDataContract();
            try
            {
                var query = (from a in dc.Customers
                             where a.CustomerID.Equals(customerID)
                             select a).Distinct().FirstOrDefault();
                Cust.CustomerID = query.CustomerID;
                Cust.CompanyName = query.CompanyName;
                Cust.ContactName = query.ContactName;
                Cust.ContactTitle = query.ContactTitle;
                Cust.Address = query.Address;
                Cust.City = query.City;
                Cust.Region = query.Region;
                Cust.PostalCode = query.PostalCode;
                Cust.Country = query.Country;
                Cust.Phone = query.Phone;
                Cust.Fax = query.Fax;
            }
            catch (Exception ex)
            {
                throw new FaultException<string>(ex.Message);
            }
            return Cust;
        }
    }
}
