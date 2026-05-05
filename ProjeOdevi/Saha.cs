using System;
using System.Collections.Generic;
using System.Text;

namespace ProjeOdevi
{
    public class Saha
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public int SaatlikUcret { get; set; }

        public virtual string SahaBilgisi()
        {
            return $"{Id} - {Ad} ({SaatlikUcret} TL/saat)";
        }
    }

    public class FutbolSahasi : Saha
    {
        public bool CimMİ { get; set; } = true;

        public override string SahaBilgisi()
        {
            return base.SahaBilgisi()+ "| Tür: Futbol";
        }
    }

    public class BasketbolSahasi : Saha
    {
        public bool KapaliAlanMi { get; set; } = true;

        public override string SahaBilgisi()
        {
            return base.SahaBilgisi() + " | Tür: Basketbol";
        }
    }
    public class VoleybolSahasi : Saha
    {
        public bool KumZeminMi { get; set; } = true;

        public override string SahaBilgisi()
        {
            return base.SahaBilgisi() + " | Tür: Voleybol";
        }
    }

    public class TenisKortu : Saha
    {
        public bool KapaliAlanMi { get; set; } = false;
        public override string SahaBilgisi()
        {
            return base.SahaBilgisi() + " | Tür: Tenis";
        }
    }
}
