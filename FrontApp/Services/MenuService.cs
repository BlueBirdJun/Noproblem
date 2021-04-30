using FrontApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontApp.Services
{
    public class MenuService
    {
        Menu[] allMenu = new[]
        {

            new Menu()
            {
                Name="Index",
                Title="",
                Path="/",
                Icon="&#xe88a",
                Tags=new string[]{"index"}               
            },
            new Menu()
            {
                Name="Login",
                Path="/login",
                Title="로그인",
                Icon="&#xe94c",
                Tags=new string[]{"login"}
            },
            new Menu()
            {
                Name="Boards",
                Path="/boards",
                Title="게시판",
                Icon="&#xe88a",
                Tags=new string[]{"boards"}
            },
            new Menu()
            {
                Name="Gallery",
                Path="/gallery",
                Title="갤러리",
                Icon="&#xe871",
                Tags=new string[]{"gallery"}
            },
            new Menu()
            {
                Name="Resister",
                Title="회원가입",
                Path="/resister",
                Icon="&#xe037",
                Tags=new string[]{"resister"} 
            }
        };

        public IEnumerable<Menu>  Menus
        {
            get
            {
                return allMenu;
            }
        }

        public IEnumerable<Menu> Filter(string term)
        {
            Func<string, bool> contains = value => value.Contains(term, StringComparison.OrdinalIgnoreCase);

            Func<Menu, bool> filter = (example) => contains(example.Name) || (example.Tags != null && example.Tags.Any(contains));

            return Menus.Where(p=>p.Name.ToLower().Contains(term.ToLower()))
                          .Select(p => new Menu()
                          {
                              Name = p.Name,
                              Expanded = true,
                              //Children = category.Children.Where(filter).ToArray()
                          }).ToList();

            //return Menus.Where(category => category.Children != null && category.Children.Any(filter))
            //               .Select(category => new Menu()
            //               {
            //                   Name = category.Name,
            //                   Expanded = true,
            //                   Children = category.Children.Where(filter).ToArray()
            //               }).ToList();
        }

        public Menu FindCurrent(Uri uri)
        {
            return Menus.SelectMany(example => example.Children ?? new[] { example })
                           .FirstOrDefault(example => example.Path == uri.AbsolutePath || $"/{example.Path}" == uri.AbsolutePath);
        }

        public string TitleFor(Menu example)
        {
            if (example != null && example.Name != "First Look")
            {
                return example.Title ?? $"Blazor {example.Name} | a free UI component by Radzen";
            }

            return "Blazor 연습";
        }

        public string DescriptionFor(Menu example)
        {
            return example?.Description ?? "The Radzen Blazor component library provides more than 50 UI controls for building rich ASP.NET Core web applications.";
        }

    }
}

