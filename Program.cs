using Ass2.DataSources;
using System.Globalization;

namespace Ass2
{
	internal class Program
	{
		static void Main(string[] args)
		{
			//1
			var allProducts = Source.ProductList;
			var allCustomers = Source.CustomerList;

			var products1 = allProducts
				.OrderByDescending(p => p.UnitPrice)
				.Take(3)
				.ToList();

			foreach (var p in products1)
			{
				Console.WriteLine($"{p.ProductName} - ${p.UnitPrice}");
			}

			//2
			var products2 = allProducts
				.Skip(5)
				.Take(5)
				.ToList();

			//3
			var products3 = allProducts
				.OrderBy(p => p.UnitPrice)
				.TakeWhile(p => p.UnitPrice < 25)
				.ToList();

			//4
			var products4 = allProducts
				.Where(p => p.Category.Equals("Seafood", StringComparison.OrdinalIgnoreCase) )
				.All(p=> p.UnitsInStock > 0);

			//5
			int[] ids = { 3, 9, 13, 18 };

			var isThere = ids.Contains(9);

			//6
			var products6 = allProducts
				.GroupBy(p => p.Category)
				.ToList();

			foreach (var item in products6)
			{
				Console.WriteLine($"{item.Key} {item.Count()}");
			}

			//7
			var products7 = allProducts
				.GroupBy(p => p.Category)
				.Select(g => new
				{ 
					categoryName = g.Key,
					ProductNames = g.Select(p=>p.ProductName),
					numberOfProducts = g.Count() 
				}).ToList();

			//8
			var products8 = allProducts
				.GroupBy(p => p.Category)
				.Where(g => g.Count() > 3)
				.ToList();

			//9
			var customers9 = from c in allCustomers
							group c by c.Country
							into Countries
							select new
						    {
								CountryName = Countries.Key,
							    CustomerCount = Countries.Count(),
								Orders = Countries.SelectMany(o => o.Orders).Select(o => o.Total).Sum()
							};

			//10
			var products10 = allProducts
				.Select(p => p.UnitsInStock)
				.Sum();

			//11
			var cheapest = allProducts
				.Min(p => p.UnitPrice);

			var mostExpensive = allProducts
				.Max(p => p.UnitPrice);

			//12
			var dis = allProducts
				.Select(p => p.Category)
				.Distinct();

			//13
			int[] setA = { 1, 3, 5, 7, 9, 11, 13 };
			int[] setB = { 3, 6, 9, 12, 15, 13 };

			var there = setA.Except(setB).ToList();

			//14
			string[] list1 = { "Germany", "France", "UK", "Spain" };
			string[] list2 = { "france", "SPAIN", "Italy" };

			var here = list1.Except(list2, StringComparer.OrdinalIgnoreCase).ToList();

			//15
			var dic15 = allProducts.ToDictionary(p => p.ProductID);
			Console.WriteLine(dic15[18]);

			//16
			var products16 = allProducts
				.FirstOrDefault(p => p.UnitPrice > 50);

			//17
			var products17 = allProducts
				.FirstOrDefault(p => p.UnitPrice > 500);

			//18
			var multi = Enumerable
				.Range(1, 10)
				.Select(i => i * 7);

			//19
			var evens = Enumerable
				.Range(1, 30)
				.Where(i => i % 2 == 0);

			//20
			var firstProducts = allProducts
				.Select(p => p.ProductName)
				.Take(3);
			var firstCompanies = allCustomers
				.Select(c => c.CompanyName)
				.Take(3);

			var concat = firstProducts.Concat(firstCompanies)
				.ToList();

			//21
			var zip = allProducts.Zip(allCustomers, (p, c) => new { ProductName = p.ProductName, CompanyName = c.CompanyName });

			foreach (var item in zip)
			{
				Console.WriteLine($"{item.ProductName} sold to {item.CompanyName}");
			}

		}
	}
}
