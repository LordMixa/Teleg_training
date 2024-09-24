using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teleg_training.Models
{
    public interface IModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
