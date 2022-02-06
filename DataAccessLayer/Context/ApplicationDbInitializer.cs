using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace DataAccessLayer.Context
{
    public class ApplicationDbInitializer
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Instruments.Any())
                {
                    return;
                }

                var brands = new Brand[]
                {
                    new Brand
                    {
                        Id = 1,
                        BrandName = "Yamaha",
                        BrandDetails = "Yamaha Corporation is a Japanese multinational corporation and conglomerate with a very wide range of products and services",
                        LogoURL = "https://miro.medium.com/max/2400/0*ivJrUEfyy2-WWk6e.jpg",
                        Comment = "Here you can buy some instruments"
                    },
                    new Brand
                    {
                        Id = 2,
                        BrandName = "Gibson",
                        BrandDetails = "Gibson Brands, is an American manufacturer of guitars, other musical instruments, and consumer and professional electronics.",
                        LogoURL = "https://miro.medium.com/max/2400/0*T1iZ_ttKcLud12A0.png",
                        Comment = "They say it is number one"
                    },
                    new Brand
                    {
                        Id = 3,
                        BrandName = "Roland",
                        BrandDetails = "Roland Corporation is a Japanese manufacturer of electronic musical instruments, electronic equipment and software.",
                        LogoURL = "https://miro.medium.com/max/2400/0*_Afhq1ZT8ZhhCp4F.png",
                        Comment = "You will find here your instrument"
                    }
                };

                var instruments = new Instrument[]
                {
                    new Instrument
                    {
                        Name = "Fender CD-60 Dread V3 WN NAT",
                        Price = 899,
                        ImageURL = "https://sklepmuzyczny.pl/10322-large_default/yamaha-f310.jpg",
                        Description = "Yamaha F310 NT to gitara akustyczna wykonana z wyselekcjonowanych materiałów. Instrument oferuje niesamowitą jakość w stosunku do ceny, a świerkowa płyta wierzchnia sprawia, że z jego wnętrz wydobywa się ciepłe, słodkie brzmienie. Głębokość korpusu została delikatnie pomniejszona, co w połączeniu ze średnią menzurą składa się na niezwykły komfort gry - to fenomenalny wybór dla początkujących, jak i średniozaawansowanych muzyków.",
                        InstrumentTypeValue = Enum.InstrumentTypeEnum.String,
                        BrandId = brands[0].Id
                    },
                    new Instrument
                    {
                        Name = "Gibson Les Paul Special Tribute Humbucker Worn White Satin",
                        Price = 4298,
                        ImageURL = "https://sklepmuzyczny.pl/29720-large_default/gibson-les-paul-special-tribute-humbucker-worn-white-satin.jpg",
                        Description = "Gibson przedstawia nową odsłonę Les Paul Special Tribute. Gitara oparta na dwóch przetwornikach typu humbucker zagwarantuje fanom rocka brzmienie dokładnie takie jakiego im trzeba. Jest to gitara zarówno dla osób początkujących jak i dla zawaansowanych gitarzystów.",
                        InstrumentTypeValue = Enum.InstrumentTypeEnum.String,
                        BrandId = brands[1].Id
                    },
                    new Instrument
                    {
                        Name = "Roland FP 10 BK",
                        Price = 2699,
                        ImageURL = "https://sklepmuzyczny.pl/16586-large_default/roland-fp-10-bk.jpg",
                        Description = "Ten cyfrowy fortepian jest wystarczająco kompaktowy, aby wygodnie zmieścić się w salonie lub pokoju rodzinnym. Oferuje naturalnie brzmiące dźwięki i wspaniałe wrażenia z gry jak na Rolandzie PHA-4 Standard 88-note hammer action keyboard, wraz z nowoczesnymi funkcjami.",
                        InstrumentTypeValue = Enum.InstrumentTypeEnum.Keyboard,
                        BrandId = brands[2].Id
                    }
                };

                context.Brands.AddRange(brands);
                context.SaveChanges();

                context.Instruments.AddRange(instruments);
                context.SaveChanges();
            }
        }
    }
}
