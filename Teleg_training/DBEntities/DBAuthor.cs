using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teleg_training.DBEntities
{
    public class DBAuthor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuthorId { get; set; }
        [Required]
        [MaxLength(30)]
        public required string Name { get; set; }
        public ICollection<DBProgramList> ?ProgramLists { get; set; }
    }
}
