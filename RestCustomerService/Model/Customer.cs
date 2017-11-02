using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RestCustomerService.Model
{

    //Datacontract not necessary as we are using JSON
    //Why use a Datacontract ?
    //https://docs.microsoft.com/en-us/dotnet/framework/wcf/feature-details/data-member-order
    //How to order data member?
    //https://stackoverflow.com/questions/4836683/when-to-use-datacontract-and-datamember-attributes

    [DataContract]
    public class Customer
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public String FirstName { get; set; }

        [DataMember]
        public String LastName { get; set; }

        [DataMember]
        public int Year { get; set; }


        public Customer()
        { //Start data generation
        }

        public Customer(String firstName)
        {
            this.ID = Service1.nextId++;
            this.FirstName = firstName;
        }

        public Customer(String firstName, String lastName, int year)
        {
            this.ID = Service1.nextId++; //I also have this in AddCustomer, dont have it two times
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Year = year;
        }

    }
}
