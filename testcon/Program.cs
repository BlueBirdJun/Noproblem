using Nop.Repository;
using System;

namespace testcon
{
    class Program
    {
        static void Main(string[] args)
        {
            using (NopContext context = new NopContext())
            {
                context.Database.EnsureCreated();

                //context.Member.Add(new Nop.Repository.NopModels.Member() { Name = "김광일", UserId = "aaaa11", Address1 = "안녕#$%^&*()_#%%^" });
                //context.SaveChanges();
            }

            Console.WriteLine("Hello World!");
        }
    }
}
