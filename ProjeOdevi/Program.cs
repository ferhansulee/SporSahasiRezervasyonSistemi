using ProjeOdevi;
using System;


class Program
{
    static void Main(string[] args)
    {
        SporMerkezi sistem = new SporMerkezi();
        sistem.DosyadanYukle();
        while (true)
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("=== SPOR SAHASI REZERVASYON SİSTEMİ ===");
            Console.WriteLine("========================================");
            Console.WriteLine("[1] Rezervasyon Oluştur");
            Console.WriteLine("[2] Rezervasyonları Listele");
            Console.WriteLine("[3] Rezervasyon Güncelle");
            Console.WriteLine("[4] Rezervasyon Sil");
            Console.WriteLine("[5] Sahaları Listele");
            Console.WriteLine("[6] Çıkış");
            Console.Write("Seçiminiz: ");
            if(!int.TryParse(Console.ReadLine(),out int secim))
            {
                Console.WriteLine("Lütfen sayı giriniz!");
                Console.ReadKey();
                continue;
            }
            switch (secim)
            {
                case 1:
                    sistem.RezervasyonEkle();
                    break;
                case 2:
                    sistem.RezervasyonListele();
                    break;
                case 3:
                    sistem.RezervasyonGuncelle();
                    break;
                case 4:
                    sistem.RezervasyonSil();
                    break;
                case 5:
                    sistem.SahalariListele();
                    break;
                case 6:
                   Console.WriteLine("Program sonlandırılıyor.");
                   return;
                default:
                   Console.WriteLine("Geçersiz seçim!");
                   Console.ReadLine();
                   break;
            }

        }
    }
}