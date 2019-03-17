using System;
using System.Collections.Generic;
using System.Xml;

namespace CustomerMaintenance
{
    /// <summary>
    /// CustomerDB class
    /// </summary>
    public static class CustomerDB
	{
        const string path = @"C:\Computer Science\Projects\Customer\Customers.xml";

        public static void SaveCustomers(List<Customer> customers)
		{
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "    "
            };
            XmlWriter writer = XmlWriter.Create(path, settings);
            writer.WriteStartDocument();
            writer.WriteStartElement("Customers");

            foreach (Customer c in customers)
            {
                writer.WriteStartElement("Customer");
                writer.WriteElementString("FirstName", c.FirstName);
                writer.WriteElementString("LastName", c.LastName);
                writer.WriteElementString("Email", c.Email);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.Close();
        }

        public static List<Customer> LoadCustomers()
        {
            List<Customer> customers = new List<Customer>();
            XmlReaderSettings settings = new XmlReaderSettings
            {
                IgnoreWhitespace = true,
                IgnoreComments = true
            };
            XmlReader reader = XmlReader.Create(path, settings);

            if (reader.ReadToDescendant("Customer"))
            {
                do
                {
                    reader.ReadStartElement("Customer");
                    Customer c = new Customer
                    {
                        FirstName = reader.ReadElementContentAsString(),
                        LastName = reader.ReadElementContentAsString(),
                        Email = reader.ReadElementContentAsString()
                    };
                    customers.Add(c);
                }
                while (reader.ReadToNextSibling("Customer"));                
            }
            reader.Close();
            return customers;
        }

        public static List<Customer> GetCustomers()
        {            
            List<Customer> customers = new List<Customer>();
            XmlReaderSettings settings = new XmlReaderSettings
            {
                IgnoreWhitespace = true,
                IgnoreComments = true
            };
            XmlReader reader = XmlReader.Create(path, settings);            
            reader.ReadToDescendant("Customer");
            
            do
            {
                reader.ReadStartElement("Customer");
                Customer c = new Customer();                
                c.FirstName = reader.ReadElementContentAsString();
                c.LastName = reader.ReadElementContentAsString();
                c.Email = reader.ReadElementContentAsString();
                customers.Add(c);
            }
            while (reader.ReadToNextSibling("Customer"));
           
            reader.Close();

            return customers;
        }
    }
}
