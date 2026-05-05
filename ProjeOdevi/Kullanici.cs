using System;
using System.Collections.Generic;
using System.Text;

namespace ProjeOdevi
{
    public enum Rol
    {
        Ogrenci,
        Personel,
        Halk
    }
    public class Kullanici
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Email { get; set; }
        public Rol rol { get; set; }
    }
}
