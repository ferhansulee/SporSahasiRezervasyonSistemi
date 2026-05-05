using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace ProjeOdevi
{
    public static class InputHelper
    {
        public static string AdSoyadAl()
        {
            while (true)
            {
                Console.Write("Ad Soyad: ");
                string adSoyad = Console.ReadLine() ?? "";

                if (string.IsNullOrWhiteSpace(adSoyad))
                {
                    Console.WriteLine("Ad Soyad boş bırakılamaz!");
                    continue;
                }
                return adSoyad;
            }
        }


        public static string EmailAl()
        {
            while (true)
            {
                Console.Write("Email: ");
                string email = Console.ReadLine() ?? "";

                try
                {
                    new MailAddress(email);
                    return email;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Geçersiz mail, tekrar deneyin!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Beklenmeyen hata oluştu.");
                }
            }
        }

        public static Rol RolAl()
        {
            while (true)
            {
                Console.WriteLine("Rol Seçiniz:");
                Console.WriteLine("1- Öğrenci");
                Console.WriteLine("2- Personel");
                Console.WriteLine("3- Halk");
                Console.Write("Seçiminiz: ");
                if (!int.TryParse(Console.ReadLine(), out int rolSecim))
                {
                    Console.WriteLine("Geçersiz seçim!");
                    continue;
                }

                switch (rolSecim)
                {
                    case 1:
                        return Rol.Ogrenci;
                    case 2:
                        return Rol.Personel;

                    case 3:
                        return Rol.Halk;

                    default:
                        Console.WriteLine("Geçersiz seçim!");
                        break;
                }
            }
        }

        public static int SahaSec(List<Saha> sahalar)
        {

            int sahaId;

            while (true)
            {
                Console.WriteLine("Saha ID: ");
                if (!int.TryParse(Console.ReadLine(), out sahaId) || !sahalar.Any(s => s.Id == sahaId))
                {
                    Console.WriteLine("Gecersiz saha ID!");
                    continue;
                }
                return sahaId;
            }
        }

        public static DateTime TarihAl()
        {
            DateTime tarih;
            while (true)
            {
                Console.WriteLine("Rezervasyon Tarihi (gg.aa.yyyy): ");
                if (!DateTime.TryParse(Console.ReadLine(), out tarih))
                {
                    Console.WriteLine("Geçersiz tarih girdiniz!");
                    continue;
                }
                return tarih;
            }
        }

        public static List<TimeSpan> SaatListesi(Rol rol)
        {
            if (rol==Rol.Halk)
            {
                return new List<TimeSpan>
        {
            new TimeSpan(17,0,0),
            new TimeSpan(19,0,0),
            new TimeSpan(21,0,0)
        };
            }
            return new List<TimeSpan>
    {
        new TimeSpan(7,0,0),
        new TimeSpan(9,0,0),
        new TimeSpan(11,0,0),
        new TimeSpan(13,0,0),
        new TimeSpan(15,0,0)
    };
        }
        public static TimeSpan SaatSec(List<TimeSpan> saatler)
        {
            while (true)
            { 
                for (int i = 0; i < saatler.Count; i++)
                {
                    Console.WriteLine($"{i + 1}- {saatler[i]:hh\\:mm}");
                }

                Console.Write("Seçim: ");

                if (!int.TryParse(Console.ReadLine(), out int secim))
                {
                    Console.WriteLine("Hatalı seçim!");
                    continue;
                }

                if (secim < 1 || secim > saatler.Count)
                {
                    Console.WriteLine("Listeden seç!");
                    continue;
                }

                return saatler[secim - 1];
            }
        }


    }
}
