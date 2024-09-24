using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teleg_training.Models
{
    internal class ModelList : IModel
    {
        public string ?Author { get; set; }
        public required string Name { get; set; }
        public double Difficult { get; set; }
        public required string Description { get; set; }
        public required string Program { get; set; }
        public string ?CodeName { get; set; }
        public string ?Gender { get; set; }
        public string ?Mode { get; set; }
        public int Likes { get; set; }
    }
}
