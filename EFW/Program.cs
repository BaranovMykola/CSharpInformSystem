using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFW
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new Model2())
            {
                //db.Teas.Add(new Tea() { Name = "abc", Components = new List<Component>() { new Component() { Comp = "comp1", Lic = new License() { Lic = "license1" } }, new Component() { Comp = "comp2" , Lic = new License() { Lic = "license2" } } },  });
                foreach (var tea in db.Teas)
                {
                    tea.pr();
                }
                //db.SaveChanges();
            }
            //string conn =
            //    @"data source=(LocalDb)\MSSQLLocalDB;initial catalog=EFW.Model1;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
            //SqlConnection c = new SqlConnection(conn);
            //c.Open();
            //var r = new SqlCommand("SELECT * FROM Teas",c).ExecuteReader();
            //r.Read();
            //Console.WriteLine(r[1]);
            Console.WriteLine("Ended");
            Console.ReadKey();

        }

    }

    public class Tea
    {
        public Tea()
        {
            //Components = new List<Component>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Component> Components { get; set; }

        public void pr()
        {
            string s = $"id [{Id}] Name [{Name}]";
            Console.WriteLine(s);
            foreach (var component in Components)
            {
                Console.WriteLine(component.Comp);
                Console.WriteLine(component.Lic.Lic);
            }
            Console.WriteLine();
            Console.WriteLine();
        }

    }

    public class Component
    {
        [Key]
        public int Id { get; set; }

        public int TeaId { get; set; }
        public string Comp { get; set; }

        public License Lic { get; set; }

    }

    public class License
    {
        public string Lic { get; set; }
    }
}
