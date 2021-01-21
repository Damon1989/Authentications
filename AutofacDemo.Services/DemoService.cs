using System;
using AutofacDemo.IRepository;
using AutofacDemo.IServices;
using AutofacDemo.Model;

namespace AutofacDemo.Services
{
    public class DemoService:IDemoService
    {
        private readonly IDemoRepository _demoRepository;

        public DemoService(IDemoRepository demoRepository)
        {
            _demoRepository = demoRepository;
        }
        public DemoModel GetDemo()
        {
            return _demoRepository.GetDemo();
        }
    }
}
