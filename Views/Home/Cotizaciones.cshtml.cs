using Microsoft.AspNetCore.Mvc.RazorPages;
using ClosedXML.Excel;
using System;
using System.Data;
using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;

namespace quotes_project.Views.Cotizaciones
{

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
    public IActionResult Index()
    {
        List<Person> people = new List<Person>
    {
        new Person { Name = "John", Age = 30 },
        new Person { Name = "Alice", Age = 25 },
        new Person { Name = "Bob", Age = 40 }
    };

        return View(people);
    }
}