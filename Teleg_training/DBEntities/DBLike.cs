using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Teleg_training.DBEntities
{
    public class DBLike
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LikeId { get; set; }
        public long TGId { get; set; }
        public int ProgramListId { get; set; }
        public DBUser User { get; set; } = null!;
        public DBProgramList ProgramList { get; set; } = null!;
    }
}
