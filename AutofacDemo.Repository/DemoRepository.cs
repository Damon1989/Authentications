using System;
using AutofacDemo.IRepository;
using AutofacDemo.Model;

namespace AutofacDemo.Repository
{
    public class DemoRepository:IDemoRepository
    {
        public DemoModel GetDemo()
        {
            return new DemoModel
            {

                Id = "1",
                Name = "Damon"
            };
        }
    }
}
