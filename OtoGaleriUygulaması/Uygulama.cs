using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace OtoGaleriUygulaması
{
    class Uygulama
    {
        Galeri OtoGaleri = new Galeri();


        public void Calistir()
        {

            //Class'lara ait hiç bir özelliği dışarıdan direkt olarak değiştirmeyelim.
            //Bütün işlemleri metodlarla yapalım.

            Menu();
            while (true)
            {
                Console.WriteLine();
                string secim = SecimAl();

                switch (secim)
                {

                    
                    case "1":
                        ArabaKirala();
                        break;
                    case "2":
                        ArabaTeslimi();
                        break;
                    case "3":
                        ArabalariListele(DURUM.Kirada);
                        break;                   
                    case "4":
                        ArabalariListele(DURUM.Galeride);
                        break;                    
                    case "5":
                        ArabalariListele();
                        break;                   
                    case "6":
                        KiralamaIptal();
                        break;                   
                    case "7":
                        YeniAraba();
                        break;                   
                    case "8":
                        ArabaSil();
                        break;                  
                    case "9":
                        BilgileriGoster();
                        break;
                    case "x":
                        Environment.Exit(0);
                        break;

                }
            }

        }

        public void ArabaKirala()
        {
            Console.WriteLine("-Araç Kirala-            ");
            if (OtoGaleri.Arabalar.Count == 0)
            {
                Console.WriteLine("Galeride araç yok.");
                return;
            }
            string plaka;

            while (true)
            {

                while (true)
                {
                    Console.Write("Kiralanacak aracın plakası: ");
                    plaka = Console.ReadLine();
                    DURUM arabaDurum = OtoGaleri.DurumGetir(plaka);
                    //veri girildikten sonra; bu plaka mı? galeride böyle bir araç var mı? araç kiralamaya müsait mi?     
                    if (!AracGerecler.PlakaMi(plaka))
                    {
                        Console.WriteLine("Giriş tanımlanamadı. Tekrar deneyin.");
                    }
                    else if (arabaDurum == DURUM.Kirada)
                    {
                        Console.WriteLine("Araç zaten kirada.");
                    }
                    else if (arabaDurum == DURUM.Empty)
                    {
                        Console.WriteLine("Böyle bir araç yok.");
                    }
                    else
                    {
                        break;
                    }
                }

                int sure = AracGerecler.SayiAl("Kiralanma süresi: ");


                try
                {
                    OtoGaleri.ArabaKirala(plaka, sure);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }


                Console.WriteLine();
                Console.WriteLine(plaka + " plakalı araç " + sure + " saatliğine kiralandı.");
                return;

            }



        }


        public void ArabaTeslimi()
        {
            Console.WriteLine("-Araç Teslim-            ");
            if (OtoGaleri.KiradakiAracSayisi == 0)
            {
                Console.WriteLine("Kirada araç yok.");
                return;
            }
            string plaka;

            while (true)
            {
                Console.Write("Teslim edilecek aracın plakası: ");
                plaka = Console.ReadLine();
                DURUM arabaDurum = OtoGaleri.DurumGetir(plaka);

                if (!AracGerecler.PlakaMi(plaka))
                {
                    Console.WriteLine("Geçerli bir plaka girin.");
                }
                else if (arabaDurum == DURUM.Galeride)
                {
                    Console.WriteLine("Hatalı giriş yapıldı. Araç zaten galeride.");
                }
                else if (arabaDurum == DURUM.Empty)
                {
                    Console.WriteLine("Galeriye ait bu plakada bir araç yok.");
                }
                else
                {
                    break;
                }
            }

            OtoGaleri.ArabaTeslimAl(plaka);
            Console.WriteLine("Araç galeride beklemeye alındı.");



        }

        public void ArabalariListele()
        {
            Console.WriteLine("-Tüm Araçlar-");
            Listele(OtoGaleri.Arabalar);
        }
        public void ArabalariListele(DURUM durum)
        {
            List<Araba> liste;
            if (durum == DURUM.Kirada)
            {
                Console.WriteLine("-Kiradaki Araçlar-");
                liste = OtoGaleri.Arabalar.Where(a => a.Durum == durum).ToList();

            }
            else if (durum == DURUM.Galeride)
            {
                Console.WriteLine("-Müsait Araçlar-");
                liste = OtoGaleri.Arabalar.Where(a => a.Durum == durum).ToList();
            }
            else
            {
                Console.WriteLine("-Tüm Araçlar-");
                liste = OtoGaleri.Arabalar;
            }

            Listele(liste);
            return;
        }
        public void Listele(List<Araba> liste)
        {
            //Toplam araç sayısı 0 ise listelenecek araç yok uyarısı verilsin.
            if (liste.Count == 0)
            {
                Console.WriteLine("Listelenecek araç yok.");
                return;
            }
            Console.WriteLine("Plaka".PadRight(15) + "Marka".PadRight(14) + "K. Bedeli".PadRight(15) + "Araç Tipi".PadRight(16) +
                    "K. Sayısı".PadRight(15) + "Durum");
            Console.WriteLine("".PadRight(85, '-'));

            foreach (Araba item in liste)
            {
                Console.WriteLine(item.Plaka.PadRight(15) + item.Marka.PadRight(14) + item.KiralamaBedeli.ToString().PadRight(15) + item.AracTipi.ToString().PadRight(16) + item.KiralanmaSayisi.ToString().PadRight(15) + item.Durum);
            }

        }
        public void KiralamaIptal()
        {
            Console.WriteLine("-Kiralama İptali-");
            if (OtoGaleri.KiradakiAracSayisi == 0)
            {
                Console.WriteLine("Kirada araç yok.");
                return;
            }

            string plaka;
            while (true)
            {
                Console.Write("Kiralaması iptal edilecek aracın plakası: ");
                plaka = Console.ReadLine();
                if (AracGerecler.PlakaMi(plaka) == false)
                {
                    Console.WriteLine("Giriş tanımlanamadı. Tekrar deneyin.");
                    continue;
                }

                DURUM aracDurum = OtoGaleri.AracDurum(plaka);
                if (aracDurum == DURUM.Empty)
                {
                    Console.WriteLine("Galeriye ait böyle bir araç yok.");
                }
                else if (aracDurum == DURUM.Galeride)
                {
                    Console.WriteLine("Hatalı giriş yapıldı. Araç zaten galeride. ");
                }
                else
                {
                    break;
                }
            }
            OtoGaleri.KiralamaIptal(plaka);
            Console.WriteLine("İptal gerçekleştirildi.");

        }
        public void YeniAraba()
        {
            Console.WriteLine("-Yeni Araç Ekle-");
            string plaka;
            while (true)
            {
                Console.Write("Plaka: ");
                plaka = Console.ReadLine();

                if (AracGerecler.PlakaMi(plaka) == false)
                {
                    Console.WriteLine("Bu şekilde plaka girişi yapamazsınız. Tekrar deneyin.");
                    continue;
                }
                DURUM durum = OtoGaleri.AracDurum(plaka);

                if (durum != DURUM.Empty)
                {
                    Console.WriteLine("Aynı plakada araç mevcut. Girdiğiniz plakayı kontrol edin.");
                }
                else
                {
                    break;
                }
            }

            Console.Write("Marka: ");
            string marka = Console.ReadLine();

            float kiralamaBedeli = AracGerecler.SayiAl("Kiralama bedeli: ");

            ARAC_TIPI aracTipi = AracGerecler.AracTipiAl();
            OtoGaleri.ArabaEkle(plaka, marka, kiralamaBedeli, aracTipi);
            Console.WriteLine("Araç başarılı bir şekilde eklendi.");


        }

        public void ArabaSil()
        {
            Console.WriteLine("-Araba Sil-");
            string plaka;
            if (OtoGaleri.Arabalar.Count == 0)
            {
                Console.WriteLine("Galeride silinecek araç yok.");
                return;
            }
            while (true)
            {
                Console.Write("Silinmek istenen araç plakasını girin: ");
                plaka = Console.ReadLine();
                if (!AracGerecler.PlakaMi(plaka))  //(AracGerecler.PlakaMi(plaka) != true)
                {
                    Console.WriteLine("Giriş tanımlanamadı. Tekrar deneyin.");
                }
                else if (OtoGaleri.AracDurum(plaka) == DURUM.Empty)
                {
                    Console.WriteLine("Galeriye ait böyle bir araç yok.");
                }
                else if (OtoGaleri.DurumGetir(plaka) == DURUM.Kirada)
                {
                    throw new Exception("Araç kirada olduğu için silme işlemi gerçekleştirilemedi.");
                }
                else
                {
                    break;
                }
            }
            try
            {
                OtoGaleri.ArabaSil(plaka);
                Console.WriteLine("Araç silindi.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        public void Menu()
        {
            Console.WriteLine("Galeri Otomasyon                    ");
            Console.WriteLine("1 - Araba Kirala(K)                 ");
            Console.WriteLine("2 - Araba Teslim Al(T)              ");
            Console.WriteLine("3 - Kiradaki arabaları listele(R)   ");
            Console.WriteLine("4 - Müsait arabaları listele(M)     ");
            Console.WriteLine("5 - Tüm arabaları listele(A)        ");
            Console.WriteLine("6 - Kiralama İptali(I)              ");
            Console.WriteLine("7 - Yeni araba Ekle(Y)              ");
            Console.WriteLine("8 - Araba sil(S)                    ");
            Console.WriteLine("9 - Bilgileri göster(G)             ");

        }
        public void BilgileriGoster()
        {

            Console.WriteLine("-Galeri Bilgileri-");
            Console.WriteLine("Toplam Araç Sayısı: " + OtoGaleri.ToplamAracSayisi);
            Console.WriteLine("Kiradaki Araç Sayısı: " + OtoGaleri.KiradakiAracSayisi);
            Console.WriteLine("Bekleyen Araç Sayısı: " + OtoGaleri.GaleridekiAracSayisi);
            Console.WriteLine("Toplam araç kiralama süresi: " + OtoGaleri.ToplamAracKiralanmaSuresi);
            Console.WriteLine("Toplam araç kiralama adedi: " + OtoGaleri.ToplamAracKirlanmaAdedi);
            Console.WriteLine("Ciro: " + OtoGaleri.Ciro);

        }
        public string SecimAl()
        {
            Console.Write("Seçiminiz: ");
            return Console.ReadLine().ToUpper();
        }



    }
}
