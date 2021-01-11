using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TestDemo.Controllers
{
    [ApiController,Route("[controller]")]
    public class SelectController:ControllerBase
    {
        [HttpGet("index")]
        public IActionResult Index()
        {
            string[] text = { "Albert was here", "Burke slept late", "Connor is happy" };
            var tokens = text.Select(s => s.Split(' '));
            foreach (var line in tokens)
            {
                foreach (var token in line)
                {
                    Console.WriteLine(token);
                }
            }

            Console.WriteLine("*********************");
            var tokenInfo = text.SelectMany(c => c.Split(' '));
            foreach (var token in tokenInfo)
            {
                Console.WriteLine(token);   
            }
            return Ok("Index");
        }
        [HttpGet("index1")]
        public IActionResult Index1()
        {
            List<Person> PersonLists = new List<Person>()
            {
                new Person { Name = "张三",Age = 20,Gender = "男",
                    Phones = new List<Phone> {
                        new Phone { Country = "中国", City = "北京", Name = "小米" },
                        new Phone { Country = "中国",City = "北京",Name = "华为"},
                        new Phone { Country = "中国",City = "北京",Name = "联想"},
                        new Phone { Country = "中国",City = "台北",Name = "魅族"},
                    }
                },
                new Person { Name = "松下",Age = 30,Gender = "男",
                    Phones = new List<Phone> {
                        new Phone { Country = "日本",City = "东京",Name = "索尼"},
                        new Phone { Country = "日本",City = "大阪",Name = "夏普"},
                        new Phone { Country = "日本",City = "东京",Name = "松下"},
                    }
                },
                new Person { Name = "克里斯",Age = 40,Gender = "男",
                    Phones = new List<Phone> {
                        new Phone { Country = "美国",City = "加州",Name = "苹果"},
                        new Phone { Country = "美国",City = "华盛顿",Name = "三星"},
                        new Phone { Country = "美国",City = "华盛顿",Name = "HTC"}
                    }
                }
            };

            Console.WriteLine("这是该方法的第一种重载：");
            var firstLists = PersonLists.Select(p => p.Name);
            foreach (var list in firstLists)
            {
                Console.WriteLine($"{list}");
            }

            Console.WriteLine("这是该方法的第二种重载,就是加了一个索引项参数:");
            var secondLists = PersonLists.Select((p, q) => (q.ToString() + p.Name));
            foreach (var list in secondLists)
            {
                Console.WriteLine($"{list}");
            }

            var selectManyList = PersonLists.SelectMany(p => p.Phones);
            foreach (var list in selectManyList)
            {
                Console.WriteLine($"{list.Country}---{list.City}---{list.Name}");
            }


            var secondSelectManyList = PersonLists.SelectMany((p, i) => {
                p.Phones.ForEach(q => { q.Country += i.ToString(); });
                return p.Phones;
            });

            foreach (var list in secondSelectManyList)
            {
                Console.WriteLine($"{list.Country}---{list.City}---{list.Name}");
            }

            var thirdSelectManyList =
                PersonLists.SelectMany(p => p.Phones, (p, q) => new {PersonName = p.Name, PhoneName = q.Name});
            foreach (var list in thirdSelectManyList)
            {
                Console.WriteLine($"{list.PersonName}---{list.PhoneName}");
            }

            var fourthSelectManyList = PersonLists.SelectMany((p, i) =>
            {
                p.Phones.ForEach(q => { q.Name += i.ToString(); });
                return p.Phones;
            }, (p, q) => new { PersonName = p.Name, PhoneName = q.Name });

            foreach (var list in fourthSelectManyList)
            {
                Console.WriteLine($"{list.PersonName}---{list.PhoneName}");
            }

            return Ok("index1");
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public List<Phone> Phones { get; set; }
    }

    public class Phone
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Name { get; set; }
    }
}
