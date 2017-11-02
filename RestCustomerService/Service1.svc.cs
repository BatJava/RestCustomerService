/*
 * RestCustomerService
 *
 * Author Michael Claudius, ZIBAT Computer Science
 * Version 1.0. 2017.11.10
 * Copyright 2017 by Michael Claudius
 * Revised 2017.11.11, 2017.10.31
 * All rights reserved
 */
using RestCustomerService.Model;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Web;

namespace RestCustomerService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        //http://miclrestcustomerservice.azurewebsites.net/Service1.svc/customers
        //http://localhost:19844/Service1.svc/customers

        public string GetData()
        {
            return "Hej";
        }

        //Statuscode coming with HttpResponse
        //https://stackoverflow.com/questions/22164780/how-do-i-return-http-status-code-420-from-my-wcf-service

        public static int nextId = 0;
        
        WebOperationContext webContext = WebOperationContext.Current;

        private static List<Customer> cList = new List<Customer>()
        {    new Customer("Michael", "Claudius", 1971),
             new Customer("Yulia", "Claudius", 1978)
        };

        public IList<Customer> GetCustomers()
        {
            return cList;
        }

        public Customer GetCustomer(string id)
        {
            webContext.OutgoingResponse.StatusCode = HttpStatusCode.OK;
            int idNumber = int.Parse(id);
            Customer c = cList.FirstOrDefault(customer => customer.ID == idNumber);
            //Set statuscode of response
            if (c == null) webContext.OutgoingResponse.StatusCode = HttpStatusCode.NotFound;

            //Alternatively 
            //if (c==null) webContext.OutgoingResponse.StatusCode = (System.Net.HttpStatusCode)404;
            //Any number can be used for tyupe casting, even customized numbers like 420

            return c;
        }

        public Customer DeleteCustomer(string id)
        {
            Customer c = GetCustomer(id);
            if (c == null) return null;
            cList.Remove(c);
            return c;

        }
 
        public Customer InsertCustomer(Customer customer)
        {
            customer.ID = Service1.nextId++;
            cList.Add(customer);
            return customer;
        }

        public Customer UpdateCustomer(Customer customer, string id)
        {
            // string id = customer.ID.ToString();
           
            Customer oldCustomer = DeleteCustomer(id);
            if (oldCustomer != null)
            {   customer.ID = oldCustomer.ID;
                cList.Add(customer);
            }
            return GetCustomer(id);
        }

    }
}

