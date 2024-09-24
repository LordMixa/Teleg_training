using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teleg_training.Models
{
    public class ProductModel : IModel
    {
        public required string Name { get; set; }
        public double Calories { get; set; }
        public double Proteins { get; set; }
        public double Fats { get; set; }
        public double Carbohydrates { get; set; }
        public string ?Type { get; set; }
        public required string Description { get; set; }

    }
}
