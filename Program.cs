namespace AddressBookSystem
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Interface created for abstraction and easy access for user
    /// </summary>
    public interface IAddressBook
    {
        void AddOrAccessAddressBook();
        void ViewAllAddressBooks();
        void DeleteAddressBook();
    }

    [Serializable]
    public class Program
    {
        // Constants
        public const string TO_ADD_OR_ACCESS = "a";
        public const string TO_VIEW_ALL_ADDRESSBOOKS = "view";
        public const string TO_DELETE_ADDRESS_BOOK = "delete";
        public const string SEARCH_PERSON_IN_CITY = "city";
        public const string SEARCH_PERSON_IN_STATE = "state";
        public const string VIEW_ALL_IN_CITY = "vcity";
        public const string VIEW_ALL_IN_STATE = "vstate";
        public const string COUNT_ALL_IN_CITY = "ccity";
        public const string COUNT_ALL_IN_STATE = "cstate";
        public const string EXIT = "e";

        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome To Address Book Program");

            // UC 13 Getting the previously stored records
            AddressBookDetails addressBookDetails = AddressBookDetails.GetFromFile();
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("\nType to select address book" +
                                  "\nA      - To add or access address book" +
                                  "\nview   - To view all names of address books" +
                                  "\nDelete - To delete Address book" +
                                  "\nCity   - To search contact in a city" +
                                  "\nState  - To search contact in a state" +
                                  "\nVCity  - To view all contacts in a city" +
                                  "\nVState - To view all contacts in a state" +
                                  "\nCCity  - To get count of contacts city wise" +
                                  "\nCState - To get count of contacts state wise" +
                                  "\nE      - To exit\n\n");
                switch (Console.ReadLine().ToLower())
                {
                    // To add or access new Address book
                    case TO_ADD_OR_ACCESS:
                        addressBookDetails.AddOrAccessAddressBook();
                        break;

                    // To view all address book names
                    case TO_VIEW_ALL_ADDRESSBOOKS:
                        addressBookDetails.ViewAllAddressBooks();
                        break;

                    // To delete an address book
                    case TO_DELETE_ADDRESS_BOOK:
                        addressBookDetails.DeleteAddressBook();
                        break;

                    // To search for a person in a city
                    case SEARCH_PERSON_IN_CITY:
                        addressBookDetails.SearchInCity();
                        break;

                    // To search for a person in a state
                    case SEARCH_PERSON_IN_STATE:
                        addressBookDetails.SearchInState();
                        break;

                    // To view all contacts in a city
                    case VIEW_ALL_IN_CITY:
                        addressBookDetails.ViewAllByCity();
                        break;

                    // To view all contacts in a city
                    case VIEW_ALL_IN_STATE:
                        addressBookDetails.ViewAllByState();
                        break;

                    // To get count of contacts in a city
                    case COUNT_ALL_IN_CITY:
                        addressBookDetails.CountAllByCity();
                        break;

                    // To get count of contacts in a state
                    case COUNT_ALL_IN_STATE:
                        addressBookDetails.CountAllByState();
                        break;
                    case EXIT:
                        Console.WriteLine("User exited application");
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Invalid entry");
                        break;
                }
            }

            // UC 13 Writing to the previously stored records.
            addressBookDetails.WriteToFile();
        }
    }
}
