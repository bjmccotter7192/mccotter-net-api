using mccotter_net_api.Models;
using System.Collections.Generic;
using System.Linq;

namespace mccotter_net_api.Services
{
    public static class DiscService
    {
        static List<Disc> Discs { get; }
        static int nextId = 3;
        static DiscService()
        {
            Discs = new List<Disc>
            {
                new Disc {
                    Id = 1, 
                    Name = "Sheriff", 
                    Manufacturer = "Dynamic Discs", 
                    Weight = 171,
                    Speed = 12.0, 
                    Glide = 5.0,
                    Turn = -1.5,
                    Fade = 2.0
                },
                new Disc {
                    Id = 2, 
                    Name = "Destiny", 
                    Manufacturer = "Westside Discs", 
                    Weight = 171,
                    Speed = 14.0, 
                    Glide = 6.0,
                    Turn = -2,
                    Fade = 3.0
                }
            };
        }

        public static List<Disc> GetAll() => Discs;

        public static Disc Get(int id) => Discs.FirstOrDefault(p => p.Id == id);

        public static void Add(Disc Disc)
        {
            Disc.Id = nextId++;
            Discs.Add(Disc);
        }

        public static void Delete(int id)
        {
            var Disc = Get(id);
            if(Disc is null)
                return;

            Discs.Remove(Disc);
        }

        public static void Update(Disc Disc)
        {
            var index = Discs.FindIndex(p => p.Id == Disc.Id);
            if(index == -1)
                return;

            Discs[index] = Disc;
        }
    }
}