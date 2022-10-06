using System;
using System.Collections.Generic;

using System.Text;

namespace OtoGaleriUygulaması
{
    class AracGerecler
    {
        static public bool PlakaMi(string veri)
        {
            //Ön koşul: Girilen plaka minimum 7 maksimum 9 haneli, ilk iki hanesi harflerden oluşmalı, 6 ve 7.(5 ve 6. indeks) haneler sayı olmalı ve plakaların 3. haneleri(2. indeks) mutlaka harf olmalıdır.
            if (veri.Length > 6 && veri.Length < 10
                && SayiMi(veri.Substring(0, 2))
                && HarfMi(veri.Substring(2, 1)))
            {
                //11A1111 formatındaki plakalar için uygun şartlar sağlanıyorsa plakadır.
                if (veri.Length == 7 && SayiMi(veri.Substring(3)))
                {
                    return true;
                }
                //11AA111 ve 11AA1111 formatındaki plakalar için uygun şartlar sağlanıyorsa plakadır.
                else if (veri.Length < 9 && HarfMi(veri.Substring(3, 1)) && SayiMi(veri.Substring(4)))
                {
                    return true;
                }
                //11AAA11, 11AAA111 ve 11AAA1111 formatındaki plakalar için uygun şartlar sağlanıyorsa plakadır.
                else if (HarfMi(veri.Substring(3, 2)) && SayiMi(veri.Substring(5)))
                {
                    return true;
                }
            }
            return false;    //Bu şartlardan hiçbiri sağlanmıyorsa plaka değildir
        }



        //string metotların çalışması için veriyi string tipinde aldık, daha sonra char olarak karşılaştırma yapabilmek için 0. indek deki elemanı
        //ASCII tablodaki değerine göre kontrol ettik.
        static public bool HarfMi(string veri)
        {
            veri = veri.ToUpper();

            for (int i = 0; i < veri.Length; i++)
            {
                int kod = (int)veri[i];//Karakterin ASCII kod tablosundaki değerini alır.
                if ((kod >= 65 && kod <= 90) != true)//büyük harflerin ASCII tablodaki değerlerleri dışında girilmişse metot false döndürür.
                {
                    return false;
                }
            }

            return true;
        }

        //foreach birden fazla sayı girildiğinde birden fazlası için ASCII tablodaki yerini kontrol edebilmeli, o yüzden bool kontrol
        //değerini oluşturduk.

        static public bool SayiMi(string giris)
        {
            foreach (char item in giris)
            {
                if (!Char.IsNumber(item))
                {
                    return false;
                }
            }
            return true;
        }

        static public int SayiAl(string mesaj)
        {
            int sayi;

            do
            {
                Console.Write(mesaj);
                string giris = Console.ReadLine();

                if (int.TryParse(giris, out sayi))
                {
                    return sayi;
                }
                else
                {
                    Console.WriteLine("Hatalı giriş yapıldı, tekrar deneyin.");
                }

            } while (true);

        }
        static public ARAC_TIPI AracTipiAl()
        {
            Console.WriteLine("Araç tipi: ");
            Console.WriteLine("SUV için 1");
            Console.WriteLine("Hatchback için 2");
            Console.WriteLine("Sedan için 3");

            while (true)
            {
                Console.Write("Seçiminiz: ");
                string secim = Console.ReadLine();

                switch (secim)
                {
                    case "1":
                        return ARAC_TIPI.SUV;

                    case "2":
                        return ARAC_TIPI.Hatchback;

                    case "3":
                        return ARAC_TIPI.Sedan;

                    default:
                        Console.WriteLine("Hatalı giriş yapıldı tekrar deneyin.");
                        break;
                }
            }
        }
    }
}
