using System;
using System.Collections.Generic;
using System.Text;

namespace ProjeOdevi
{
    public class Rezervasyon
    {
        public int Id { get; set; }
        public Saha Saha{ get; set; }
        public Kullanici Kullanici { get; set; }
        public DateTime RezervasyonTarihi { get; set; }
    }
}
