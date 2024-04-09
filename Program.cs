using System;
namespace OperatorOverloading_3
{
    class Program
    {
        interface IRealEstate
        {
            public string Address { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public int PostalCode { get; set; }
            public double LotSize { get; set; }
            public double Price { get; set; }

            public double SqftPrice();
            public string FullAddress();
        }
        class Listing : IRealEstate
        {
            public string Address { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public int PostalCode { get; set; }
            public double LotSize { get; set; }
            public double Price { get; set; }
            public Listing()
            {
                Address = string.Empty;
                City = string.Empty;
                State = string.Empty;
                PostalCode = 0;
                LotSize = 0;
                Price = 0;
            }
            public Listing(string address, string city, string state, int postalCode, double lotSize, double price)
            {
                Address = address;
                City = city;
                State = state;
                PostalCode = postalCode;
                LotSize = lotSize;
                Price = price;
            }
            public static Listing operator ++ (Listing listing1)
            {
                listing1.Price += 1000;
                return listing1;
            }
            public static Listing operator --(Listing listing1)
            {
                if (listing1.Price > 1000) 
                { listing1.Price -= 1000;}
                return listing1;
            }
            public static Listing operator + (Listing listing1 , Listing listing2)
            {
                listing1.LotSize += listing2.LotSize;
                listing1.Price += listing2.Price;
                return listing1;
            }
            // compare to see the first property has a lower price/sqft
            public static bool operator < (Listing listing1 , Listing listing2)
            {
                
                if (listing1.SqftPrice() < listing2.SqftPrice())
                {
                    return true;
                }
                else return false;
            }
            // compare to see the first property has a higher price/sqft
            public static bool operator > (Listing listing1, Listing listing2)
            {

                if (listing1.SqftPrice() > listing2.SqftPrice())
                {
                    return true;
                }
                else return false;
            }
            public static bool operator == (Listing listing1, Listing listing2)
            {
                if (listing1.SqftPrice()== listing2.SqftPrice())
                {
                    return true;
                }
                else return false;
            }
            public static bool operator !=(Listing listing1, Listing listing2)
            {
                if (listing1.SqftPrice() != listing2.SqftPrice())
                {
                    return true;
                }
                else return false;
            }

            public double SqftPrice()
            {
                double quotient = Price / LotSize;
                return Math.Ceiling(quotient);
            }
            public string FullAddress()
            {
                return $"{Address}, {City}, {State} {PostalCode}";
            }

        }
        static void Main(string[] args)
        {
            Listing floridaHouse = new Listing("48 Logo Ct", "Fort Myers", "FL", 33912, 1392, 33900);
            Listing traverseHouse = new Listing("711 Hobbs Hwy S", "Traverse City", "MI", 49696, 95832, 374900);
            Listing elkRapidsHouse = new Listing("8095 Cairn Hwy ", "Elk Rapids", "MI", 49629, 37461, 1435000);
            Console.WriteLine("Here are your listings:");
            Console.WriteLine(floridaHouse.FullAddress());
            Console.WriteLine(traverseHouse.FullAddress());
            Console.WriteLine(elkRapidsHouse.FullAddress());

            // use overloaded operator to compare price/sqft.
            Console.WriteLine($"\nWhich has lower price per square foot?");
            Console.WriteLine($"{floridaHouse.FullAddress()} or {traverseHouse.FullAddress()}");
            if (floridaHouse < traverseHouse) 
            { Console.WriteLine($"{floridaHouse.FullAddress()} at {floridaHouse.SqftPrice}/sqft"); }
            else { Console.WriteLine($"{traverseHouse.FullAddress()} at {traverseHouse.SqftPrice()}/sqft"); }

            // increase price of property
            Console.WriteLine("\nIncreasing the price of a property by $1000.");
            Console.WriteLine($"{elkRapidsHouse.FullAddress()} sell price: ${elkRapidsHouse.Price}");
            elkRapidsHouse++;
            Console.WriteLine($"{elkRapidsHouse.FullAddress()} sell price: ${elkRapidsHouse.Price}");

            // combining two properties
            Console.WriteLine($"\nCombining two properties");
            Console.WriteLine($"{elkRapidsHouse.FullAddress()} \n{elkRapidsHouse.LotSize} sqft \n${elkRapidsHouse.Price}" +
                $" \n{traverseHouse.FullAddress()} \n{traverseHouse.LotSize} sqft \n${traverseHouse.Price}");
            elkRapidsHouse += traverseHouse;
            Console.WriteLine($"New Lot size: {elkRapidsHouse.LotSize}sqft \nNew Price: ${elkRapidsHouse.Price}");
            Console.WriteLine($"New price/sqft: ${elkRapidsHouse.SqftPrice()}/sqft");

        }
    }
}