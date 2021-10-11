using LinqToDB.Mapping;

namespace Linq2DbPerformanceRegression
{
    [Table(Name = "TESTTABLE")]
    public class TESTTABLE
    {
        [PrimaryKey]
        public string COLUMN1 { get; set; }

        [Column]
        public string COLUMN2 { get; set; }
        [Column]
        public string COLUMN3 { get; set; }
        [Column]
        public string COLUMN4 { get; set; }
        [Column]
        public string COLUMN5 { get; set; }
        [Column]
        public string COLUMN6 { get; set; }
        [Column]
        public string COLUMN7 { get; set; }
        [Column]
        public string COLUMN8 { get; set; }
        [Column]
        public string COLUMN9 { get; set; }
        [Column]
        public string COLUMN10 { get; set; }

        [Column]
        public string COLUMN11 { get; set; }

        [Column]
        public string COLUMN12 { get; set; }
        [Column]
        public decimal COLUMN15 { get; set; }

        [Column]
        public decimal COLUMN16 { get; set; }
    }
}