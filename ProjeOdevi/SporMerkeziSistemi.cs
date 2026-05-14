using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Text;
using static System.Net.WebRequestMethods;
using System.Text.Json;




namespace ProjeOdevi
{
    public class SporMerkezi
    {
        public List<Saha> Sahalar { get; set; } = new List<Saha>();
        public List<Kullanici> Kullanicilar { get; set; } = new List<Kullanici>();
        public List<Rezervasyon> Rezervasyonlar { get; set; } = new List<Rezervasyon>();

        private readonly string dosyaYolu = "rezervasyonlar.json";
        public SporMerkezi()
        {
            Kullanicilar = new List<Kullanici>();
            Rezervasyonlar = new List<Rezervasyon>();
            Sahalar = new List<Saha>
            {
            new FutbolSahasi { Id = 1, Ad = "Futbol Sahası 1", SaatlikUcret = 500 },
            new FutbolSahasi { Id = 2, Ad = "Futbol Sahası 2", SaatlikUcret = 500 },
            new FutbolSahasi { Id = 3, Ad = "Futbol Sahası 3", SaatlikUcret = 500 },

            new BasketbolSahasi { Id = 4, Ad = "Basketbol Sahası 1", SaatlikUcret = 300 },
            new BasketbolSahasi { Id = 5, Ad = "Basketbol Sahası 2", SaatlikUcret = 300 },

            new VoleybolSahasi { Id = 6, Ad = "Voleybol Sahası 1", SaatlikUcret = 250 },
            new VoleybolSahasi { Id = 7, Ad = "Voleybol Sahası 2", SaatlikUcret = 250 },

            new TenisKortu { Id = 8, Ad = "Tenis Kortu 1", SaatlikUcret = 600 },
            new TenisKortu { Id = 9, Ad = "Tenis Kortu 1", SaatlikUcret = 600 },
            };
        }

        public void DosyayaKaydet()
        {
            string json = JsonSerializer.Serialize(Rezervasyonlar, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            System.IO.File.WriteAllText("rezervasyonlar.json", json);
        }

        public void DosyadanYukle()
        {
            if (System.IO.File.Exists("rezervasyonlar.json"))
            {
                string json = System.IO.File.ReadAllText("rezervasyonlar.json");
                Rezervasyonlar = JsonSerializer.Deserialize<List<Rezervasyon>>(json)??new List<Rezervasyon>();
            }
        }
        public void RezervasyonEkle()
        {

            string kullaniciAdi = InputHelper.AdSoyadAl();
            string email=InputHelper.EmailAl();
            Rol rol=InputHelper.RolAl();

            Console.WriteLine("Sahalar:");
            foreach (Saha saha in Sahalar)
            {
                Console.WriteLine($"{saha.Id} - {saha.Ad}");
            }
            int sahaId = InputHelper.SahaSec(Sahalar);

            DateTime tarih = InputHelper.TarihAl();

            if (rol==Rol.Halk)
                Console.WriteLine("==HALKA AÇIK KULLANIM SAATLERİ==");
            else
            {
                Console.WriteLine("==ÖĞRENCİ VE PERSONEL İÇİN KULLANIM SAATLERİ==");
            }

            List<TimeSpan> Saatler = InputHelper.SaatListesi(rol);
            TimeSpan saat = InputHelper.SaatSec(Saatler);

            DateTime rezervasyonTarihi = tarih.Date + saat;

            RezervasyonEkle(kullaniciAdi, email, rol, sahaId, rezervasyonTarihi);
        }

        public void RezervasyonEkle(string kullaniciAdi, string email,
            Rol rol,
            int sahaId,
            DateTime rezervasyonTarihi)
        {
            Kullanici kullanici = Kullanicilar.FirstOrDefault(k =>
            k.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

            if (kullanici == null)
            {
                kullanici = new Kullanici
                {
                    Id = Kullanicilar.Any() ? Kullanicilar.Max(k=>k.Id) + 1 : 1,
                    Ad = kullaniciAdi,
                    Email = email,
                    rol = rol
                };

                Kullanicilar.Add(kullanici);
            }

            Saha saha = Sahalar.FirstOrDefault(s => s.Id == sahaId);

            if (saha == null)
            {
                Console.WriteLine("Saha bulunamadı!");
                return;
            }


            bool cakisma = Rezervasyonlar.Any(r =>
            r.Saha.Id == sahaId && r.RezervasyonTarihi == rezervasyonTarihi);

            if (cakisma)
            {
                Console.WriteLine("Bu saat dolu!");
                Console.ReadLine();
                return;
            }

            bool bugunRezervasyonVarMi = Rezervasyonlar.Any(r =>
            r.Kullanici.Email.Equals(email, StringComparison.OrdinalIgnoreCase) &&
            r.RezervasyonTarihi.Date == rezervasyonTarihi.Date);
            if (bugunRezervasyonVarMi)
            {
                Console.WriteLine("Hata: Bir günde sadece 1 rezervasyon yapabilirsiniz!");
                return;
            }

            Rezervasyon rezervasyon = new Rezervasyon
            {
                Id = Rezervasyonlar.Any() ? Rezervasyonlar.Max(r=>r.Id) + 1 : 1,
                Kullanici = kullanici,
                Saha = saha,
                RezervasyonTarihi = rezervasyonTarihi
            };
            Rezervasyonlar.Add(rezervasyon);

            Console.WriteLine("Rezervasyon başarıyla oluşturuldu.");
            DosyayaKaydet();
        }

        public void RezervasyonListele()
        {
            if (Rezervasyonlar.Count == 0)
            {
                Console.WriteLine("Henüz rezervasyon bulunmamaktadır.");
                Console.ReadLine();
                return;
            }
            foreach (var r in Rezervasyonlar)
            {
                Console.WriteLine($"Rezervasyon No: {r.Id} | " +
                    $"Kullanıcı: {r.Kullanici.Ad} | " +
                    $"Saha: {r.Saha.Ad} | " +
                    $"Tarih: {r.RezervasyonTarihi:dd.MM.yyyy HH:mm}");
            }
            Console.ReadLine();
        }

        public void SahalariListele()
        {
            Console.WriteLine("--- SAHALAR ---");

            Console.WriteLine("┌─────────────────────────────────────────────────────┐");
            foreach (var saha in Sahalar)
            {
                Console.WriteLine(saha.SahaBilgisi());
            }
            Console.WriteLine("└─────────────────────────────────────────────────────┘");
            Console.ReadLine();
        }

        public void RezervasyonGuncelle()
        {

            RezervasyonListele();
            try
            {
                Console.Write("E-mail: ");
                string email = Console.ReadLine();
                Console.Write("Güncellenecek Rezervasyon No: ");
                if (!int.TryParse(Console.ReadLine(), out int no))
                {
                    Console.WriteLine("Bir hata oluştu!");
                    return;
                }
               
                Rezervasyon guncellenecekRezervasyon = Rezervasyonlar.FirstOrDefault(r => r.Id == no && r.Kullanici.Email == email);
                if (guncellenecekRezervasyon == null)
                {
                    Console.WriteLine("Rezervasyon bulunamadı veya bilgiler yanlış!");
                    return;
                }
                Console.WriteLine("YENİ TARİH");
                DateTime yeniTarih = InputHelper.TarihAl();
                Console.WriteLine("YENİ SAAT");
                List<TimeSpan> saatler = InputHelper.SaatListesi(guncellenecekRezervasyon.Kullanici.rol);
                TimeSpan yeniSaat = InputHelper.SaatSec(saatler);
                DateTime yeniRezervasyon = yeniTarih.Date + yeniSaat;
                bool cakisma = Rezervasyonlar.Any(r =>
                r.Saha.Id == guncellenecekRezervasyon.Saha.Id &&
                r.RezervasyonTarihi == yeniRezervasyon &&
                r.Id != no);

                if (cakisma)
                {
                    Console.WriteLine("Bu saat dolu!");
                    return;
                }
                guncellenecekRezervasyon.RezervasyonTarihi = yeniRezervasyon;
                DosyayaKaydet();
                Console.WriteLine("Rezervasyon  güncellendi!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Beklenmeyen hata oluştu: " + ex.Message);
            }
        }

        public void RezervasyonSil()
        {
            try
            {
                Console.Write("E-mail: ");
                string email = Console.ReadLine();
                Console.Write("Silinecek rezervasyon ID: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("Geçersiz ID!");
                    return;
                }
                Rezervasyon rez = Rezervasyonlar.FirstOrDefault(r => r.Id == id && r.Kullanici.Email==email);

                if (rez == null)
                {
                    Console.WriteLine("Rezervasyon bulunamadı veya bilgiler yanlış!");
                    return;
                }
               
                Rezervasyonlar.Remove(rez);
                DosyayaKaydet();
                Console.WriteLine("Rezervasyon başarıyla silindi!");
            }
            catch(Exception ex )
            {
                Console.WriteLine("Beklenmeyen hata!" + ex.Message);
            }
        }


    }
}

