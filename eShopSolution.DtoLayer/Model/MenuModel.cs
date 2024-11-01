using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.DtoLayer.Model
{
    public class MenuModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string? icon { get; set; }
        public string? title { get; set; }
        public string? URL { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is MenuModel menu)
            {
                return ID == menu.ID; 
            }
            return false;
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode(); 
        }
    }
}
