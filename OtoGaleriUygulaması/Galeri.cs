using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OtoGaleriUygulaması
{
    class Galeri
    {
        public List<Araba> Arabalar = new List<Araba>();


        public int ToplamAracSayisi
        {
            get
            {
                return this.Arabalar.Count;
            }
        }
        public int GaleridekiAracSayisi
        {
            get
            {
                //int adet = 0;
                //foreach (Araba item in this.Arabalar)
                //{
                //    if (item.Durum == DURUM.Galeride)
                //    {
                //        adet++;
                //    }
                //}
                //return adet;

                return this.Arabalar.Where(a => a.Durum == DURUM.Galeride).ToList().Count;
            }
        }
        public int KiradakiAracSayisi
        {
            get
            {
                return this.Arabalar.Where(t => t.Durum == DURUM.Kirada).ToList().Count;
            }
        }


        public int ToplamAracKiralanmaSuresi
        {
            get
            {
                return this.Arabalar.Sum(a => a.KiralanmaSureleri.Sum());
                //return this.Arabalar.Sum(a => a.ToplamKiralanmaSuresi);
            }
        } //arabalar listesindeki tüm arabaların toplamkiralanmasuresi'nin toplamı



        public int ToplamAracKirlanmaAdedi
        {
            get
            {
                return this.Arabalar.Sum(a => a.KiralanmaSayisi);
            }
        }//bu bilgi, her bir aracın kiralama sayılarının toplamıyla oluşturulacak



        public float Ciro
        {
            get
            {
                return this.Arabalar.Sum(a => a.ToplamKiralanmaSuresi * a.KiralamaBedeli);
            }
        }



        public void ArabaKirala(string plaka, int sure)
        {
            //plakaya ait aracı bulmak
            //bulunan aracın durum bilgisini güncellemek

            Araba a = this.Arabalar.Where(a => a.Plaka == plaka.ToUpper()).FirstOrDefault();
            if (a != null && a.Durum == DURUM.Galeride)
            {

                a.Durum = DURUM.Kirada;
                a.KiralanmaSureleri.Add(sure);
            }
            else if (!AracGerecler.PlakaMi(plaka))
            {
                Console.WriteLine("Giriş tanımlanamadı. Tekrar deneyin.");
            }
            else if (a != null && a.Durum != DURUM.Galeride)
            {
                throw new Exception("Araç zaten kirada.");
            }
            else if (AracGerecler.PlakaMi(plaka) && AracDurum(plaka) == DURUM.Empty)
            {
                throw new Exception("Galeriye ait böyle bir araç yok.");
            }

        }
        public void KiralamaIptal(string plaka)
        {
            Araba a = this.Arabalar.Where(a => a.Plaka == plaka.ToUpper()).FirstOrDefault();

            if (a != null)
            {
                a.Durum = DURUM.Galeride;
                a.KiralanmaSureleri.RemoveAt(a.KiralanmaSureleri.Count - 1);
            }
            else if (AracGerecler.PlakaMi(plaka) == false)
            {
                Console.WriteLine("Giriş tanımlanamadı. Tekrar deneyin.");
            }
            else if (a != null && a.Durum != DURUM.Kirada)
            {
                throw new Exception("Hatalı giriş yapıldı. Araç zaten galeride.");
            }
            else
            {
                throw new Exception("Galeriye ait böyle bir araç yok.");
            }



        }
        public DURUM DurumGetir(string plaka)
        {
            Araba a = this.Arabalar.Where(a => a.Plaka == plaka.ToUpper()).FirstOrDefault();
            if (a != null)
            {
                return a.Durum;
            }
            return DURUM.Empty;
        }


        public void ArabaTeslimAl(string plaka)
        {
            Araba a = this.Arabalar.Where(a => a.Plaka == plaka.ToUpper() && a.Durum == DURUM.Kirada).FirstOrDefault();

            if (a != null)
            {
                a.Durum = DURUM.Galeride;
            }
            else if (AracGerecler.PlakaMi(plaka) == false)
            {
                throw new Exception("Bu şekilde plaka girişi yapamazsınız. Tekrar deneyin.");
            }
            else if (AracDurum(plaka) == DURUM.Empty)
            {
                throw new Exception("Galeriye ait böyle bir araç yok.");
            }
            else if (AracDurum(plaka) == DURUM.Galeride)
            {
                throw new Exception("Araç zaten galeride. ");
            }

        }


        public Galeri()
        {
            SahteVeriGir();
        }

        public void SahteVeriGir()
        {

            Araba a = new Araba("34us2342".ToUpper(), "OPEL", 50, ARAC_TIPI.Hatchback);
            this.Arabalar.Add(a);
            this.Arabalar.Add(new Araba("34arb3434".ToUpper(), "FIAT", 70, ARAC_TIPI.Sedan));
            this.Arabalar.Add(new Araba("35arb3535".ToUpper(), "KIA", 60, ARAC_TIPI.SUV));

        }



        public DURUM AracDurum(string plaka)
        {
            Araba a = this.Arabalar.Where(a => a.Plaka == plaka.ToUpper())
                                    .FirstOrDefault();

            if (a == null)
            {
                return DURUM.Empty;
            }
            else
            {
                return a.Durum;
            }
        }

        public void ArabaEkle(string plaka, string marka, float kiralamaBedeli, ARAC_TIPI aracTipi)
        {
            Araba a = new Araba(plaka, marka, kiralamaBedeli, aracTipi);

            this.Arabalar.Add(a);
        }


        public void ArabaSil(string plaka)
        {
            if (!AracGerecler.PlakaMi(plaka))
            {
                throw new Exception("Giriş tanımlanamadı. Tekrar deneyin.");
            }

            Araba a = this.Arabalar.Where(x => x.Plaka == plaka.ToUpper()).FirstOrDefault();

            if (a != null && a.Durum == DURUM.Galeride)
            {
                this.Arabalar.Remove(a);
            }
            else if (a != null && a.Durum == DURUM.Kirada)
            {
                throw new Exception("Araç kirada olduğu için silme işlemi gerçekleştirilemedi.");
            }
            else
            {
                throw new Exception("Galeriye ait böyle bir araç yok.");
            }
        }


    }
}
