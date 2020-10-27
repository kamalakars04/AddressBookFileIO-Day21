namespace AddressBookSystem
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;
    using NLog;

    [Serializable]
    class AddressBookDetails : IAddressBook
    {
        [NonSerialized]
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        string nameOfAddressBook;

        // Constants
        const string ADD_CONTACT = "add";
        const string UPDATE_CONTACT = "update";
        const string SEARCH_CONTACT = "search";
        const string REMOVE_CONTACT = "remove";
        const string GET_ALL_CONTACTS = "view";
        const string WRITE = "write";
        const string READ = "read";

        // Collection Decleration
        public Dictionary<string, AddressBook> addressBookList = new Dictionary<string, AddressBook>();
        public static Dictionary<string, List<ContactDetails>> cityToContactMap = new Dictionary<string, List<ContactDetails>>();
        public static Dictionary<string, List<ContactDetails>> stateToContactMap = new Dictionary<string, List<ContactDetails>>();
        private Dictionary<string, List<ContactDetails>> cityToCOntactMapInstance;
        private Dictionary<string, List<ContactDetails>> stateToContactMapInstance;

        /// <summary>
        /// Gets the address book.
        /// </summary>
        /// <returns></returns>
        private AddressBook GetAddressBook()
        {
            Console.WriteLine("\nEnter name of Address Book to be accessed or to be added");
            nameOfAddressBook = Console.ReadLine();

            // search for address book in dictionary
            if (addressBookList.ContainsKey(nameOfAddressBook))
            {
                Console.WriteLine("\nAddressBook Identified");
                logger.Info("Address book " + nameOfAddressBook + " is accessed by user");
                return addressBookList[nameOfAddressBook];
            } 

            // Offer to create a address book if not found in dictionary
            logger.Warn("AddressBook " + nameOfAddressBook + " not found");
            Console.WriteLine("\nAddress book not found. Type y to create a new address book or E to abort");

            // If user want to create a new address book
            if ((Console.ReadLine().ToLower()) == "y")
            {
                AddressBook addressBook = new AddressBook(nameOfAddressBook);
                addressBookList.Add(nameOfAddressBook, addressBook);
                Console.WriteLine("\nNew AddressBook Created");
                logger.Info("New address book created with name : " + nameOfAddressBook);
                return addressBookList[nameOfAddressBook];
            }

            // If User want to abort the operation 
            else
            {
                Console.WriteLine("\nAction Aborted");
                logger.Info("User aborted the operation to create new Address book with name : " + nameOfAddressBook);
                return null;
            }
        }

        /// <summary>
        /// Searches the in city.
        /// </summary>
        public void SearchInCity()
        {
            // Returns no record found if address book is empty
            if (addressBookList.Count == 0)
            {
                Console.WriteLine("\nNo record found");
                return;
            }

            // Get the name of city from user
            Console.WriteLine("\nEnter the city name to search for contact");
            string cityName = Console.ReadLine().ToLower();

            // If the city doesnt have any contacts
            if (!cityToContactMap.ContainsKey(cityName) || cityToContactMap[cityName].Count == 0)
            {
                Console.WriteLine("\nNo record found");
                return;
            }

            // Get the person name to be searched
            Console.WriteLine("\nEnter the person firstname to be searched");
            string firstName = Console.ReadLine().ToLower();
            Console.WriteLine("\nEnter the person lastname to be searched");
            string lastName = Console.ReadLine().ToLower();

            // Get the list of contacts whose city and name matches with search
            var searchResult = cityToContactMap[cityName].FindAll(contact => contact.FirstName.ToLower() == firstName
                                                && contact.LastName.ToLower() == lastName);
            if(searchResult.Count == 0)
            {
                Console.WriteLine("\nNo record found");
                return;
            }
            Console.Write("\nThe contacts found in of given search are :");

            // print the list of contacts whose city and name matches with search
            searchResult.ForEach(contact => contact.toString());

        }

        /// <summary>
        /// Counts all by city.
        /// </summary>
        internal void CountAllByCity()
        {
            // Returns no record found if address book is empty
            if (addressBookList.Count == 0)
            {
                Console.WriteLine("\nNo record found");
                return;
            }

            // To get count of contacts in all cities
            foreach(KeyValuePair<string, List<ContactDetails>> keyValuePair in cityToContactMap)
                Console.WriteLine("No of contacts in city {0} is {1}",keyValuePair.Key, keyValuePair.Value.Count());
        }

        /// <summary>
        /// Views all the contacts of a city.
        /// </summary>
        public void ViewAllByCity()
        {
            if (addressBookList.Count == 0)
            {
                Console.WriteLine("\nNo record found");
                return;
            }

            // Get the name of city from user
            Console.WriteLine("\nEnter the city name to search for contact");
            string cityName = Console.ReadLine().ToLower();

            // If the given city doesnt exist
            if (!(cityToContactMap.ContainsKey(cityName)))
            {
                Console.WriteLine("\nNo contacts exist in the city");
                return;
            }

            // Print all contact details in city
            cityToContactMap[cityName].ForEach(contact => contact.toString());
        }

        /// <summary>
        /// Views all the contacts of a state
        /// </summary>
        public void ViewAllByState()
        {
            if (addressBookList.Count == 0)
            {
                Console.WriteLine("\nNo record found");
                return;
            }

            // Get the name of city from user
            Console.WriteLine("\nEnter the state name to search for contact");
            string stateName = Console.ReadLine().ToLower();

            // If the given city doesnt exist
            if (!(stateToContactMap.ContainsKey(stateName)))
            {
                Console.WriteLine("\nNo contacts exist in the state");
                return;
            }

            // To print details of all the contacts
            stateToContactMap[stateName].ForEach(contact => contact.toString());
        }

        /// <summary>
        /// Counts the state of all by.
        /// </summary>
        internal void CountAllByState()
        {
            // Returns no record found if address book is empty
            if (addressBookList.Count == 0)
            {
                Console.WriteLine("\nNo record found");
                return;
            }

            // To get count of contacts in all cities
            foreach (KeyValuePair<string, List<ContactDetails>> keyValuePair in stateToContactMap)
                Console.WriteLine("Nunber of contacts in state {0} is {1}", keyValuePair.Key, keyValuePair.Value.Count());
        }

        /// <summary>
        /// Searches the state of the in.
        /// </summary>
        public void SearchInState()
        {
            // Returns no record found if address book is empty
            if (addressBookList.Count == 0)
            {
                Console.WriteLine("\nNo record found");
                return;
            }

            // Get the name of city from user
            Console.WriteLine("\nEnter the state name to search for contact");
            string stateName = Console.ReadLine().ToLower();

            // If the city doesnt have any contacts
            if (!stateToContactMap.ContainsKey(stateName) || stateToContactMap[stateName].Count == 0)
            {
                Console.WriteLine("\nNo record found");
                return;
            }

            // Get the person name to be searched
            Console.WriteLine("\nEnter the person firstname to be searched");
            string firstName = Console.ReadLine().ToLower();
            Console.WriteLine("\nEnter the person lastname to be searched");
            string lastName = Console.ReadLine().ToLower();
           
            // Get the list of contacts whose city and name matches with search
            var searchResult = stateToContactMap[stateName].FindAll(contact => contact.FirstName.ToLower() == firstName
                                                                    && contact.LastName.ToLower() == lastName);

            // If no contacts exist
            if (searchResult.Count() == 0)
            {
                Console.WriteLine("\nNo contacts found of given search");
                return;
            }
            Console.Write("\nThe contacts found in of given search are :");

            // Display the search results
            searchResult.ForEach(contact => contact.toString());
        }

        /// <summary>
        /// Deletes the address book.
        /// </summary>
        public void DeleteAddressBook()
        {
            // Returns no record found if address book is empty
            if (addressBookList.Count == 0)
            {
                Console.WriteLine("No record found");
                return;
            }
            Console.WriteLine("\nEnter the name of address book to be deleted :");

            //search for address book with given name
            try
            {
                string addressBookName = Console.ReadLine();

                // Remove AddressBook with given name
                addressBookList.Remove(addressBookName);

                // Remove contacts from city dictionary
                foreach (KeyValuePair<string, List<ContactDetails>> keyValuePair in cityToContactMap)
                    cityToContactMap[keyValuePair.Key].RemoveAll(contact => contact.NameOfAddressBook == addressBookName);

                // Remove contacts from state dictionary
                foreach (KeyValuePair<string, List<ContactDetails>> keyValuePair in stateToContactMap)
                    stateToContactMap[keyValuePair.Key].RemoveAll(contact => contact.NameOfAddressBook == addressBookName);
                Console.WriteLine("Address book deleted successfully");
                logger.Info("User deleted the AddressBook " + nameOfAddressBook);
            }
            catch
            {
                Console.WriteLine("Address book not found");
            }
        }

        /// <summary>
        /// Views all address books.
        /// </summary>
        public void ViewAllAddressBooks()
        {
            // Returns no record found if address book is empty
            if (addressBookList.Count() == 0)
            {
                Console.WriteLine("No record found");
                return;
            }

            // Print the address book names available
            Console.WriteLine("\nThe namesof address books available are :");
            foreach (KeyValuePair<string, AddressBook> keyValuePair in addressBookList)
                Console.WriteLine(keyValuePair.Key);
            logger.Info("User viewd all AddressBook names");
        }

        /// <summary>
        /// Adds the or access address book.
        /// </summary>
        public void AddOrAccessAddressBook()
        {
            // To get the name of the addressbook
            AddressBook addressBook = GetAddressBook();

            // Returns no record found if address book is empty
            if (addressBook == null)
            {
                Console.WriteLine("Action aborted");
                return;
            }

            // select the action to be performed in address book   
            while(true)
            {
                Console.WriteLine("\nSelect from below to work on Address book {0}", addressBook.nameOfAddressBook);
                Console.WriteLine("\nType\n\nAdd - To add a contact" +
                                  "\nUpdate- To update a contact" +
                                  "\nView - To view all contacts" +
                                  "\nRemove - To remove a contact and " +
                                  "\nSearch- To search to get contact deatails\nWrite - To write addressbook to file" +
                                  "\nRead - To read from file" +
                                  "\nE - To exit\n ");
                switch (Console.ReadLine().ToLower())
                {
                    case ADD_CONTACT:
                        addressBook.AddContact();
                        break;

                    case UPDATE_CONTACT:
                        addressBook.UpdateContact();
                        break;

                    case SEARCH_CONTACT:
                        addressBook.SearchContactDetails();
                        break;

                    case REMOVE_CONTACT:
                        addressBook.RemoveContact();
                        break;

                    case GET_ALL_CONTACTS:
                        addressBook.GetAllContacts();
                        break;

                    case WRITE:
                        addressBook.WriteAddressBookToFile();
                        break;

                    case READ:
                        addressBook.ReadAddressBookFromFile();
                        break;

                    default:
                        Console.WriteLine("\nInvalid option. Exiting address book");
                        return;
                }

                // Ask the user to continue in same address book or to exit
                Console.WriteLine("\nType y to continue in same address Book or any other key to exit");

                // If not equal to y  then exit
                if (!(Console.ReadLine().ToLower() == "y"))
                {
                    logger.Debug("User exited the address book " + nameOfAddressBook);
                    return;
                }
            }
        }

        /// <summary>
        /// Adds to city dictionary.
        /// </summary>
        /// <param name="cityName">Name of the city.</param>
        /// <param name="contact">The contact.</param>
        public static void AddToCityDictionary(string cityName, ContactDetails contact)
        {
            // Check if the map already has city key
            if (!(cityToContactMap.ContainsKey(cityName)))
                cityToContactMap.Add(cityName, new List<ContactDetails>());

            // Add the contact to list of respective city map
            cityToContactMap[cityName].Add(contact);
        }

        /// <summary>
        /// Adds to state dictionary.
        /// </summary>
        /// <param name="stateName">Name of the state.</param>
        /// <param name="contact">The contact.</param>
        public static void AddToStateDictionary(string stateName, ContactDetails contact)
        {
            // Check if the map already has state key
            if (!stateToContactMap.ContainsKey(stateName))
                stateToContactMap.Add(stateName, new List<ContactDetails>());

            // Add the contact to list of respective city map
            stateToContactMap[stateName].Add(contact);
        }

        /// <summary>
        /// UC 13 Gets from file.
        /// </summary>
        /// <returns></returns>
        public static AddressBookDetails GetFromFile()
        {
            FileStream stream;
            string path = @"C:\Users\kamalakar\Desktop\bridge labs\AddressBookFileIO\PersonContacts.txt";
            try
            {
                // Open the specified path
                // If path is not found then it throws file not found exception
                using (stream = new FileStream(path, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();

                    // Deserialize the data from file
                    // If stream is null then it throws Serialization exception
                    AddressBookDetails addressBookDetails = (AddressBookDetails)formatter.Deserialize(stream);

                    // Copy the details of instance variables to static
                    cityToContactMap = addressBookDetails.cityToCOntactMapInstance;
                    stateToContactMap = addressBookDetails.stateToContactMapInstance;
                    return addressBookDetails;
                };
            }
            catch(FileNotFoundException)
            {
                Console.WriteLine("file not found");
                return new AddressBookDetails();
            }
            catch(SerializationException)
            {
                Console.WriteLine("No previous records");
                return new AddressBookDetails();
            }
        }

        /// <summary>
        /// UC 13 Writes to file.
        /// </summary>
        public void WriteToFile()
        {
            string path = @"C:\Users\kamalakar\Desktop\bridge labs\AddressBookFileIO\PersonContacts.txt";
            FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
            BinaryFormatter formatter = new BinaryFormatter();

            // Copy the details of static variables to instance to serialize them
            cityToCOntactMapInstance = cityToContactMap;
            stateToContactMapInstance = stateToContactMap;
            formatter.Serialize(stream, this.MemberwiseClone());
        }
    }
}
