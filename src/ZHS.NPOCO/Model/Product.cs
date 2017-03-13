using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZHS.NPOCO.Model
{
    [TableName("product")]
    [PrimaryKey("product_id",AutoIncrement =true)]
    public class Product
    {
        [Column("product_id")]
        public Int32 Id { get; set; }

        [Column("name")]
        public String Name { get; set; }

        [Column("addedtime")]
        public DateTime AddedTime { get; set; }

        [Column("modifiedtime")]
        public DateTime ModifiedTime { get; set; }
    }
}
