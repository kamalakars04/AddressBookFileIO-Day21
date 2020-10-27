namespace AddressBookSystem
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    [Serializable]
    public class ContactDetails
    {
        // Variables
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string NameOfAddressBook { get; set; }

        public ContactDetails()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ContactDetails"/> class.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="address">The address.</param>
        /// <param name="city">The city.</param>
        /// <param name="state">The state.</param>
        /// <param name="zip">The zip.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="email">The email.</param>
        public ContactDetails(string firstName, string lastName, string address, string city, string state, string zip, 
                               string phoneNumber, string email, string nameOfAddressBook)
        {
            this.FirstName = firstName.ToLower();
            this.LastName = lastName.ToLower();
            this.Address = address;
            this.City = city;
            this.State = state;
            this.Zip = zip;
            this.PhoneNumber = phoneNumber;
            this.Email = email;
            this.NameOfAddressBook = nameOfAddressBook;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>
        ///   <see langword="true" /> if the specified object  is equal to the current object; otherwise, <see langword="false" />.
        /// </returns>
        public override bool Equals(Object obj)
        {
            // if the list is null
            if (obj == null)
                return false;
            try
            {
                // Get the contacts from list with same name
                var duplicates = ((List<ContactDetails>)obj).Find(contact => ((contact.FirstName).ToLower() == (FirstName).ToLower()
                                                                        && (contact.LastName).ToLower() == (LastName).ToLower()
                                                                        && contact.NameOfAddressBook == NameOfAddressBook));

                // Return true if duplicate is found else false
                if (duplicates != null)
                    return true;
                else
                    return false;
            }
            catch
            {
                // Get the contacts from list with same name
                var contact = ((ContactDetails)obj);
                return ((contact.FirstName).ToLower() == (FirstName).ToLower()
                        && (contact.LastName).ToLower() == (LastName).ToLower()
                        && contact.NameOfAddressBook == NameOfAddressBook);
            }
        }
        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <param name="contact">The contact.</param>
        public void toString()
        {
            // For null contact
            if(NameOfAddressBook == null)
            {
                Console.WriteLine("No record found");
                return;
            }

            // Display all the atributes of contact
            int rowNum = 1;
            Console.WriteLine("\nname of contact is {0}", FirstName + " " + LastName);
            Console.WriteLine("{0}-firstname is {1}", rowNum++, FirstName);
            Console.WriteLine("{0}-lastname is {1}", rowNum++, LastName);
            Console.WriteLine("{0}-address is {1}", rowNum++, Address);
            Console.WriteLine("{0}-city is {1}", rowNum++, City);
            Console.WriteLine("{0}-state is {1}", rowNum++, State);
            Console.WriteLine("{0}-zip is {1}", rowNum++, Zip);
            Console.WriteLine("{0}-phoneNumber is {1}", rowNum++, PhoneNumber);
            Console.WriteLine("{0}-email is {1}", rowNum++, Email);
        }
    }
}
