using System;
using System.Collections.Generic;

namespace restoran
{
    class Program
    {
        class MenuItem
        {
            public string Name { get; set; }
            public decimal fiyat { get; set; }
        }

        class masa
        {
            public int no { get; set; }
            public bool IsOccupied { get; set; }
            public List<string> Orders { get; set; } = new List<string>();

            public void sipariseEkle(string order)
            {
                Orders.Add(order);
            }

            public void ClearOrders()
            {
                Orders.Clear();
            }
        }

        class Restaurant
        {
            private List<MenuItem> menu;
            private List<masa> masas;
            private const string adminPassword = "0000";
            private decimal totalSales = 0;

            public Restaurant()
            {
                menu = menuOlustur();
                masas = new List<masa>
                {
                    new masa { no = 1, IsOccupied = false },
                    new masa { no = 2, IsOccupied = false },
                    new masa { no = 3, IsOccupied = false }
                };
            }

            private List<MenuItem> menuOlustur()
            {
                return new List<MenuItem>
                {
                    new MenuItem { Name = "Mercimek Çorbası", fiyat = 20 },
                    new MenuItem { Name = "Tarhana Çorbası", fiyat = 25 },
                    new MenuItem { Name = "Turlu", fiyat = 40 },
                    new MenuItem { Name = "Bezelye", fiyat = 35 },
                    new MenuItem { Name = "Pasta", fiyat = 30 },
                    new MenuItem { Name = "Tulumba", fiyat = 20 },
                    new MenuItem { Name = "Ayran", fiyat = 10 },
                    new MenuItem { Name = "Su", fiyat = 5 },
                    new MenuItem { Name = "Çay", fiyat = 0 } // Çay ücretsiz
                };
            }

            public void Run()
            {
                while (true)
                {
                    Console.Write("1. Sipariş Al\n2. Admin Girişi\nSeçim yapınız (1/2): ");
                    string secenek = Console.ReadLine();
                    if (secenek == "1")
                    {
                        foreach (var masa in masas)
                        {
                            if (!masa.IsOccupied)
                            {
                                siparisAl(masa);
                            }
                        }
                    }
                    else if (secenek == "2")
                    {
                        AdminLogin();
                    }
                }
            }

            private void siparisAl(masa masa)
            {
                Console.WriteLine($"\nMasa {masa.no} için sipariş alınacak.");
                masa.IsOccupied = true; // Masa dolu olarak işaretlendi
                decimal totalBill = 0;

                for (int i = 0; i < 4; i++)
                {
                    Console.WriteLine($"Müşteri {i + 1} için sipariş:");
                    for (int j = 0; j < menu.Count; j++)
                    {
                        Console.WriteLine($"{j + 1}. {menu[j].Name} - {menu[j].fiyat} TL");
                    }

                    Console.Write("Sipariş vermek istiyor musunuz? (E/H): ");
                    string wantsToOrder = Console.ReadLine();
                    if (wantsToOrder.ToUpper() == "E")
                    {
                        Console.Write("Sipariş numarasını giriniz: ");
                        int siparisListesi = int.Parse(Console.ReadLine()) - 1;
                        masa.sipariseEkle($"Müşteri {i + 1}: {menu[siparisListesi].Name}");
                        totalBill += menu[siparisListesi].fiyat;

                        // Başka bir isteğiniz var mı?
                        Console.Write("Başka bir isteğiniz var mı? (E/H): ");
                        string extraRequest = Console.ReadLine();
                        while (extraRequest.ToUpper() == "E")
                        {
                            Console.Write("Sipariş numarasını giriniz: ");
                            siparisListesi = int.Parse(Console.ReadLine()) - 1;
                            masa.sipariseEkle($"Müşteri {i + 1}: {menu[siparisListesi].Name}");
                            totalBill += menu[siparisListesi].fiyat;
                            Console.Write("Başka bir isteğiniz var mı? (E/H): ");
                            extraRequest = Console.ReadLine();
                        }
                    }
                    else
                    {
                        masa.sipariseEkle($"Müşteri {i + 1}: Çay (Ücretsiz)");
                    }
                }

                // Alınan siparişleri ekrana yazdır
                Console.WriteLine($"\nMasa {masa.no} için alınan siparişler:");
                foreach (var order in masa.Orders)
                {
                    Console.WriteLine(order);
                }

                // Hesap ödeme sorusu
                Console.Write("Ödeme yapmak istiyor musunuz? (E/H): ");
                string wantsToPay = Console.ReadLine();
                if (wantsToPay.ToUpper() == "E")
                {
                    Console.WriteLine($"Hesap: {totalBill} TL");
                    Console.WriteLine("Hesap ödendi.");
                    totalSales += totalBill;
                    masa.IsOccupied = false; // Masa tekrar boş olarak işaretlendi
                    masa.ClearOrders(); // Siparişler temizlendi
                }
                else
                {
                    Console.WriteLine("Ödeme yapılmadı, diğer masaya geçiliyor.");
                    masa.IsOccupied = false; // Masa tekrar boş olarak işaretlendi
                    masa.ClearOrders(); // Siparişler temizlendi
                }
            }

            private void AdminLogin()
            {
                Console.Write("Admin şifresini giriniz: ");
                string password = Console.ReadLine();
                if (password == adminPassword)
                {
                    AdminPanel();
                }
                else
                {
                    Console.WriteLine("Şifre hatalı. Ana menüye dönülüyor.");
                }
            }

            private void AdminPanel()
            {
                while (true)
                {
                    Console.WriteLine("\nAdmin paneline hoşgeldiniz.");
                    Console.WriteLine("1. Ürün Güncelle\n2. Yeni Ürün Ekle\n3. Toplam Satış Tutarı\n4. Çıkış");
                    Console.Write("Seçim yapınız (1/2/3/4): ");
                    string secenek = Console.ReadLine();
                    if (secenek == "1")
                    {
                        menuGuncelleme();
                    }
                    else if (secenek == "2")
                    {
                        
                    }
                    else if (secenek == "3")
                    {
                        Console.WriteLine($"Toplam Satış Tutarı: {totalSales} TL");
                    }
                    else if (secenek == "4")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Geçersiz seçim. Tekrar deneyiniz.");
                    }
                }
            }

            private void menuGuncelleme()
            {
                Console.WriteLine("Mevcut Menü:");
                for (int i = 0; i < menu.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {menu[i].Name} - {menu[i].fiyat} TL");
                }
                Console.Write("Güncellemek istediğiniz ürün numarasını giriniz: ");
                int urunListesi = int.Parse(Console.ReadLine()) - 1;
                if (urunListesi >= 0 && urunListesi < menu.Count)
                {
                    Console.Write("Yeni ürün adını giriniz: ");
                    menu[urunListesi].Name = Console.ReadLine();
                    Console.Write("Yeni ürün fiyatını giriniz: ");
                    menu[urunListesi].fiyat = decimal.Parse(Console.ReadLine());
                    Console.WriteLine("Ürün güncellendi.");
                }
                else
                {
                    Console.WriteLine("Geçersiz ürün numarası. Ana menüye yönlendiriliyorsunuz");
                }

                
                    {
                        while (true)
                        {
                            Console.WriteLine("\nAdmin paneline hoşgeldiniz.");
                            Console.WriteLine("1. Ürün Güncelle\n2. Yeni Ürün Ekle\n3. Toplam Satış Tutarı\n4. Çıkış");
                            Console.Write("Seçim yapınız (1/2/3/4): ");
                            string secenek = Console.ReadLine();
                            if (secenek == "1")
                            {
                                menuGuncelleme();
                            }
                            else if (secenek == "2")
                            {
                                
                            }
                            else if (secenek == "3")
                            {
                                
                            }
                            else if (secenek == "4")
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Geçersiz seçim. Tekrar deneyiniz.");
                            }
                        }
                    }

                    
                    {
                        Console.Write("Yeni ürün adını giriniz: ");
                        string yeniUrunAdi = Console.ReadLine();
                        Console.Write("Yeni ürün fiyatını giriniz: ");
                        decimal newfiyat = decimal.Parse(Console.ReadLine());

                        menu.Add(new MenuItem { Name = yeniUrunAdi, fiyat = newfiyat });

                        Console.WriteLine("Yeni ürün eklendi.");
                    }

                
                    {
                        decimal totalSales = 0;
                        foreach (var masa in masas)
                        {
                            foreach (var order in masa.Orders)
                            {
                                foreach (var item in menu)
                                {
                                    if (order.Contains(item.Name))
                                    {
                                        totalSales += item.fiyat;
                                    }
                                }
                            }
                        }

                        Console.WriteLine($"Toplam satış tutarı: {totalSales} TL");
                    }
                }

                static void Main(string[] args)
                {
                    Restaurant restoran = new Restaurant();
                    restoran.Run();
                }
            }
        }
    }



