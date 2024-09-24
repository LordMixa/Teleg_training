using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teleg_training.DBEntities;
using Microsoft.EntityFrameworkCore;

namespace Teleg_training.DBEntities
{
    public class DBProgramList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProgramId { get; set; }
        [ForeignKey("DBAuthor")]
        public int AuthorId { get; set; }
        public DBAuthor Author { get; set; } = null!;
        [Required]
        public required string Name { get; set; }
        [Range(0, 5)]
        public double Difficult { get; set; }
        public string ?Description { get; set; }
        public string ?Program { get; set; }
        public string ?Gender { get; set; }
        public string ?Mode { get; set; }
        public ICollection<DBLike> ?Likes { get; set; }

    }
}
