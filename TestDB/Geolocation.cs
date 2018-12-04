using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDB
{
    [Table("Geolocations")]
    public class Geolocation
    {
        [Key, Column(Order = 1), ForeignKey("device")]
        public int DeviceID { get; set; }
        [Key, Column(Order = 2), ForeignKey("user")]
        public int UserID { get; set; }
        [Key, Column(TypeName = "date", Order =3)]
        public DateTime DateTimeRecorded { get; set; }

        public Decimal Longitude { get; set; }
        public Decimal Latitude { get; set; }

        public virtual User user { get; set; }
        public virtual Device device { get; set; }


    }
}
