using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FireSharp.Interfaces;
using FireSharp.Response;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System.Threading;

namespace deneme
{
    public struct Kargo_bilgi//Kargo bilgilerinin tutulması icin kullanilan struct modeli
    {
        public Kargo_bilgi(string adres, string durum, string kargo_Id, string user_Id)
        {
            Adres = adres; Durum = durum; kargoId = kargo_Id; userId = user_Id;
        }
        public string Adres { get; } public string Durum { get; }
        public string kargoId { get; } public string userId { get; }
    }
    public partial class GUI2 : Form
    {
       
        FirebaseConnection con = new FirebaseConnection();
        static Kargo_bilgi[] Kargo_bilgileri; // kargo bilgilerinin tutuldugu struct dizisi
        static int bosadres, boyut, kargo_cnt, deger, bekleme_parameter = 0, son_konum_id;
        String adres_x = "", adres_y = "", adres1_x = "", adres1_y = "";
        static int[] son_yol, bos_adresler_id, dolu_adresler_id;
        int[] arr = Enumerable.Repeat(42, 10000).ToArray();
        private static List<PointLatLng> _noktalar;
        static double son_cevap = Int32.MaxValue;
        static string[,] ziyaretedilmemis;
        static double[,] graph1, graph;
        static string son_konum = "0";
        public string Id = "1";
        IFirebaseClient client;
        static bool[] ziyared_edilenler;
        public GUI2()
        {
            InitializeComponent();
            _noktalar = new List<PointLatLng>();
            Thread mainThread = Thread.CurrentThread;
        }
        
        private async void gMapControl1_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            GMapProviders.GoogleMap.ApiKey = AppConfig.Key;
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            map.CacheLocation = @"cache";
            GMapProviders.GoogleMap.ApiKey = AppConfig.Key;
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            map.MinZoom = 5;map.MaxZoom = 100;map.Zoom = 10;
            map.DragButton = MouseButtons.Left;map.ShowCenter = false;
            map.MapProvider = GMapProviders.GoogleMap;
            map.SetPositionByKeywords("Kocaeli, Turkey");
            client = new FireSharp.FirebaseClient(con.config);
        }

        private void harita_konumla(PointLatLng point)//map konumlandirmak icin kullanilir
        {
            map.Position = point;
        }
        private void marker_ekle(PointLatLng pointToAdd, GMarkerGoogleType markerType = GMarkerGoogleType.arrow)
        {//marker eklemek icin kullanilir
            var markers = new GMapOverlay("markers");
            var marker = new GMarkerGoogle(pointToAdd, markerType);
            markers.Markers.Add(marker);
            map.Overlays.Add(markers); 
        }
        private void marker_ve_konum(PointLatLng point)
        {
            harita_konumla(point);
            marker_ekle(point);
        }

        private static void nokta_ekle(String adres, String adres_x, String adres_y)//noktalara dizisine nokta ekler
        {

            if (!(adres_x.Trim().Equals("") && adres_y.Trim().Equals("")))
            {
                MessageBox.Show("Reserve Geocoding");
                var nokta = new PointLatLng(Convert.ToDouble(adres_x), Convert.ToDouble(adres_y));
                GeoCoderStatusCode statusCode;
                var yer_isareti = GoogleMapProvider.Instance.GetPlacemark(nokta, out statusCode);
                if (statusCode == GeoCoderStatusCode.OK){adres = yer_isareti?.Address; }
                else {adres = "Someting gone wrong. returning status code : " + statusCode; } 
            }
            else
            {
                if (!adres.Trim().Equals(""))
                {
                    GeoCoderStatusCode statusCode;
                    var enlem_boylam = GoogleMapProvider.Instance.GetPoint(adres.Trim(), out statusCode);
                    if (statusCode == GeoCoderStatusCode.OK)
                    {
                        var pt = enlem_boylam ?? default(PointLatLng);
                        adres_x = enlem_boylam?.Lat.ToString();
                        adres_y = enlem_boylam?.Lng.ToString();
                        _noktalar.Add(new PointLatLng(Convert.ToDouble(adres_x), Convert.ToDouble(adres_y)));

                    }
                    else {MessageBox.Show("Someting gone wrong. returning status code : " + statusCode);}}  
                else{MessageBox.Show("İnvalid data load");} 
            }
        }
        private static double rota_hesapla()//noktalar dizisindeki ilk iki nokta arasındaki mesafe alinir
        {
            var rota = GoogleMapProvider.Instance
                .GetRoute(_noktalar[0], _noktalar[1], false, false, 14);
            var rotalar = new GMapOverlay("routes");
            var r = new GMapRoute(rota.Points, "my route");
            rotalar.Routes.Add(r);
            double mesafe = rota.Distance;
            return mesafe;
            
        }
        public void bekle()//bekleme_parameter degeri arttirilarak yol_ciz fonksiyonunda yeni bir islem yapilmasina neden olur
        {
            bekleme_parameter = 1;
        }
        public async void mesafe_hesapla()
        {   // kargo_cnt firebase den alinir
            FirebaseResponse cevap = await client.GetAsync("kargoCounter");
            CounterClass nesne = cevap.ResultAs<CounterClass>();
            kargo_cnt = Convert.ToInt32(nesne.kargo_cnt);
            Kargo_bilgileri = new Kargo_bilgi[kargo_cnt];
            Kargo kargo1;
            string adres, durum, kaId, UsId;
            Kargo_bilgi deneme;
            for (int m = 1; m <= kargo_cnt; m++)//Kargo bilgilerinin tutuldugu struct dizisi olusturulur
            {
                var cevap1 = client.Get("Kargo/" + m);
                kargo1 = cevap1.ResultAs<Kargo>();
                adres = kargo1.Adres; durum = kargo1.Durum;
                kaId = kargo1.kargoId; UsId = kargo1.userId;
                deneme = new Kargo_bilgi(adres, durum, kaId, UsId);
                Kargo_bilgileri[m - 1] = deneme;
            }
            bosadres = 0;
            for (int xn = 0; xn < kargo_cnt; xn++)//bos adreslerin sayısı alınır
            {
                if (Kargo_bilgileri[xn].Adres == "")
                {
                    bosadres++;
                }

            }
            bos_adresler_id = new int[bosadres]; dolu_adresler_id = new int[kargo_cnt - bosadres];
            int bos_id = 0, dolu_id = 0;
            for (int xn = 0; xn < kargo_cnt; xn++)
            {
                if (Kargo_bilgileri[xn].Adres == "")
                {
                    bos_adresler_id[bos_id] = xn;//bos adreslerin Kargo_bilgileri[] struct dizisindeki konumu tutulur
                    bos_id++;
                }
                else
                {
                    dolu_adresler_id[dolu_id] = xn;//dolu adreslerin Kargo_bilgileri[] struct dizisindeki konumu tutulur
                    dolu_id++;
                }
            }
            deger = kargo_cnt - bosadres; boyut = deger;
            son_yol = new int[boyut + 1]; ziyared_edilenler = new bool[boyut];//dizi boyutlari atanir
            graph = new double[deger, deger];
            for (int z = 0; z < kargo_cnt; z++)//hesaplamalarin yapila bilmesi icin noktalar arasindaki mesafeler alinir
            {
                for (int n = 0; n < kargo_cnt; n++)
                {
                    if (Kargo_bilgileri[dolu_adresler_id[z]].Adres != "" && Kargo_bilgileri[dolu_adresler_id[n]].Adres != "")
                    {
                        nokta_ekle(Kargo_bilgileri[dolu_adresler_id[z]].Adres, adres_x, adres_y);
                        nokta_ekle(Kargo_bilgileri[dolu_adresler_id[n]].Adres, adres1_x, adres1_y);
                        double mesafe = rota_hesapla();graph[z, n] = mesafe; _noktalar.Clear();
                    }


                }
            }
            gezgin_kargo(graph);// gidilmesi gereken en kisa yol hesabi yapilir
            yol_ciz(); // hesaplanan rotada kargo teslimi yapilir
        }
        public async void yol_ciz()
        {
            for (int j = 0; j < deger; j++)//kargo noktalarina marker eklenir
            {
                nokta_ekle(Kargo_bilgileri[dolu_adresler_id[son_yol[j]]].Adres, adres_x, adres_y);
                nokta_ekle(Kargo_bilgileri[dolu_adresler_id[son_yol[j + 1]]].Adres, adres1_x, adres1_y);
                marker_ve_konum(_noktalar[0]); marker_ve_konum(_noktalar[1]); _noktalar.Clear();
            }
            for (int j = 0; j < deger; j++)//dagitimi yapilan yollar  gosterilir
            {
                if (bekleme_parameter == 0)
                {
                   
                    nokta_ekle(Kargo_bilgileri[dolu_adresler_id[son_yol[j]]].Adres, adres_x, adres_y);
                    System.Threading.Thread.Sleep(10000);
                    nokta_ekle(Kargo_bilgileri[dolu_adresler_id[son_yol[j + 1]]].Adres, adres1_x, adres1_y);
                    
                    son_konum = Kargo_bilgileri[dolu_adresler_id[son_yol[j + 1]]].Adres;
                    harita_konumla(_noktalar[0]); harita_konumla(_noktalar[1]);
                    var rota = GoogleMapProvider.Instance.GetRoute(_noktalar[0], _noktalar[1], false, false, 14);
                    var r = new GMapRoute(rota.Points, "my route"){Stroke = new Pen(Color.Red, 5)  };
                    var rotalar = new GMapOverlay("routes");
                    rotalar.Routes.Add(r); map.Overlays.Add(rotalar);
                    //System.Threading.Thread.Sleep(10000);
                    if (son_yol[j + 1] == 0) {continue;}
                    else
                    {   // teslim edilen kargolarda durum bildirimi yapilir 
                        FirebaseResponse resp = await client.GetAsync("kargoCounter");
                        CounterClass get = resp.ResultAs<CounterClass>();
                        client = new FireSharp.FirebaseClient(con.config);
                        if (Kargo_bilgileri[dolu_adresler_id[son_yol[j + 1]]].kargoId!="1") { 
                        Kargo kargo = new Kargo()
                        {
                            kargoId = Kargo_bilgileri[dolu_adresler_id[son_yol[j + 1]]].kargoId,
                            userId = Kargo_bilgileri[dolu_adresler_id[son_yol[j + 1]]].userId,
                            Adres = Kargo_bilgileri[dolu_adresler_id[son_yol[j + 1]]].Adres,
                            Durum = "teslim_edildi"

                        };
                        var setter = client.Update("Kargo/" + kargo.kargoId, kargo);}

                    }
                    _noktalar.Clear();// aralarindaki yollar hesaplanan konumlar silinir
                    son_konum_id = dolu_adresler_id[son_yol[j + 1]];//kesinti sonrasinda kullanilmak icin kargo konumu tutulur
                }

            }
            if (bekleme_parameter != 0) {mesafe_hesapla_kesinti();}//kesinti sonrasi kullanilan fonksiyon cagirimi
           
                
          
        }
        public async void mesafe_hesapla_kesinti()//yeni kargo ekleme sonrasi yeni hesaplamalar icin cagrılır
        {
            ziyaretedilmemis = new string[kargo_cnt + 4,2];
            FirebaseResponse cevap = await client.GetAsync("kargoCounter");
            CounterClass nesne = cevap.ResultAs<CounterClass>();
            kargo_cnt = Convert.ToInt32(nesne.kargo_cnt);
            Kargo_bilgileri = new Kargo_bilgi[kargo_cnt];
            Kargo kargo1;
            string adres, durum,kaId,UsId;
            Kargo_bilgi deneme;
            for (int m = 1; m <= kargo_cnt; m++)//kargo bilgilerinin tutuldugu struct yenilenir
            {
                var cevap1 = client.Get("Kargo/" + m);
                kargo1 = cevap1.ResultAs<Kargo>();
                adres = kargo1.Adres; durum = kargo1.Durum;
                kaId = kargo1.kargoId; UsId = kargo1.userId;
                deneme = new Kargo_bilgi(adres, durum, kaId, UsId);
                Kargo_bilgileri[m - 1] = deneme;
            }
            bosadres = 0;
            for (int xn = 0; xn < kargo_cnt; xn++)//bos adreslerinsayısı alınır
            {if (Kargo_bilgileri[xn].Adres == "") { bosadres++;} }

            bos_adresler_id = new int[bosadres]; dolu_adresler_id = new int[kargo_cnt - bosadres];
            int bos_id = 0, dolu_id = 0;

            for (int xn = 0; xn < kargo_cnt; xn++)
            {
                if (Kargo_bilgileri[xn].Adres == "")
                {
                    bos_adresler_id[bos_id] = xn;//bos adreslerin Kargo_bilgileri[] struct dizisindeki konumu tutulur
                    bos_id++;
                }
                else
                {
                    dolu_adresler_id[dolu_id] = xn;//dolu adreslerin Kargo_bilgileri[] struct dizisindeki konumu tutulur
                    dolu_id++;
                }
            }
            ziyaretedilmemis[0, 0] = son_konum;
            ziyaretedilmemis[0, 1] = Convert.ToString(son_konum_id);deger = 1;
            for (int i = 0; i < kargo_cnt; i++)
            {
                if (Kargo_bilgileri[dolu_adresler_id[i]].Adres != "" && Kargo_bilgileri[dolu_adresler_id[i]].Durum == "" )
                {
                    ziyaretedilmemis[deger,0] = Kargo_bilgileri[dolu_adresler_id[i]].Adres;
                    ziyaretedilmemis[deger, 1] = Convert.ToString(dolu_adresler_id[i]);
                    deger++;
                }
            }
            boyut = deger; son_yol = new int[boyut + 1]; ziyared_edilenler = new bool[boyut];
            graph1 = new double[deger, deger];
            
            for (int z = 0; z < deger; z++)
            {
                for (int n = 0; n < deger; n++)
                {
                    nokta_ekle(ziyaretedilmemis[z,0], adres_x, adres_y);
                    nokta_ekle(ziyaretedilmemis[n,0], adres1_x, adres1_y);
                        double mesafe1 = rota_hesapla();
                        graph1[z, n] = mesafe1;
                    _noktalar.Clear();
                    
                }
            }
            gezgin_kargo(graph1);// en kisa yol hesaplamasi yapilir
            yol_ciz2();
        }
        
        public async void yol_ciz2() //yeni kayit sonrasi yol cizimi
        {
            System.Threading.Thread.Sleep(10000);
            int j ;    
            for (j=0 ; j < deger-1; j++)
            {
                
                nokta_ekle(ziyaretedilmemis[son_yol[j],0], adres_x, adres_y);
                System.Threading.Thread.Sleep(10000);
                nokta_ekle(ziyaretedilmemis[son_yol[j + 1],0], adres1_x, adres1_y);
                harita_konumla(_noktalar[0]); harita_konumla(_noktalar[1]); marker_ve_konum(_noktalar[1]);
                var rota = GoogleMapProvider.Instance.GetRoute(_noktalar[0], _noktalar[1], false, false, 14);
                var r = new GMapRoute(rota.Points, "my route"){ Stroke = new Pen(Color.Blue, 5)};
                var rotalar = new GMapOverlay("routes"); rotalar.Routes.Add(r);
                map.Overlays.Add(rotalar);
                _noktalar.Clear();
                FirebaseResponse resp = await client.GetAsync("kargoCounter");
                CounterClass get = resp.ResultAs<CounterClass>();
                client = new FireSharp.FirebaseClient(con.config);
                if (Kargo_bilgileri[Convert.ToInt32(ziyaretedilmemis[son_yol[j + 1], 1])].kargoId.ToString()!="1") { 
                Kargo kargo = new Kargo() //teslim edilen kargo bilgileri guncellenir
                {
                    kargoId = Kargo_bilgileri[Convert.ToInt32(ziyaretedilmemis[son_yol[j+1] , 1])].kargoId.ToString(),
                    Adres = Kargo_bilgileri[Convert.ToInt32(ziyaretedilmemis[son_yol[ j+1] , 1])].Adres,
                    userId = Id,Durum = "teslim_edildi" };
                    var setter = client.Update("Kargo/" + kargo.kargoId, kargo); }}

            nokta_ekle(ziyaretedilmemis[son_yol[j ],0], adres_x, adres_y);
            System.Threading.Thread.Sleep(10000);
            nokta_ekle(Kargo_bilgileri[0].Adres, adres1_x, adres1_y);
            harita_konumla(_noktalar[0]); harita_konumla(_noktalar[1]);
            var rota1 = GoogleMapProvider.Instance.GetRoute(_noktalar[0], _noktalar[1], false, false, 14);
             
            var r1 = new GMapRoute(rota1.Points, "my route") {Stroke = new Pen(Color.Blue, 5)};
            var rotalar1 = new GMapOverlay("routes");
            rotalar1.Routes.Add(r1);
            map.Overlays.Add(rotalar1);
            System.Threading.Thread.Sleep(10000);
            _noktalar.Clear();

        }
        static void son_yol_degistir(int[] eldeki_yol)//eldeki_yol dizisi son_yol dizisine kopyalanir
        {
            for (int i = 0; i < boyut; i++) {
                son_yol[i] = eldeki_yol[i];}
            son_yol[boyut] = eldeki_yol[0];
            
        }
        static double en_kucuk(double[,] adj, int i)//en kucuk yolu bulmak icin kullanilan fonksiyon
        {
            double min = Int32.MaxValue;
            for (int k = 0; k < boyut; k++) { 
                if (adj[i, k] < min && i != k) { 
                    min = adj[i, k]; } }
            return min;
        }

        static double ikinci_en_kucuk(double[,] adj, int i)//en kucuk ikinci yolu bulmak icin kullanilan fonksiyon
        {
            double ilk = Int32.MaxValue, ikinci = Int32.MaxValue;
            for (int j = 0; j < boyut; j++)
            {
                if (i == j) { 
                    continue;}

                if (adj[i, j] <= ilk)
                {
                    ikinci = ilk;
                    ilk = adj[i, j];
                }
                else if (adj[i, j] <= ikinci &&
                        adj[i, j] != ilk)
                {
                    ikinci = adj[i, j]; }   
            }
            return ikinci;
        }
        static void gezgin_kargo_2(double[,] adj, double anlik_degisim, double eldeki_agirlik,
                    int seviye, int[] eldeki_yol)
        {
            if (seviye == boyut)
            {
                if (adj[eldeki_yol[seviye - 1], eldeki_yol[0]] != 0)
                {
                    double eldeki_cevap = eldeki_agirlik +
                            adj[eldeki_yol[seviye - 1], eldeki_yol[0]];
                    if (eldeki_cevap < son_cevap)
                    {
                        son_yol_degistir(eldeki_yol);
                        son_cevap = eldeki_cevap;
                    }
                }
                return;
            }
            for (int i = 0; i < boyut; i++)
            {
                if (adj[eldeki_yol[seviye - 1], i] != 0 &&
                        ziyared_edilenler[i] == false)
                {
                    double adim = anlik_degisim;
                    eldeki_agirlik += adj[eldeki_yol[seviye - 1], i];
                    if (seviye == 1)
                        anlik_degisim -= ((en_kucuk(adj, eldeki_yol[seviye - 1]) +
                                        en_kucuk(adj, i)) / 2);
                    else
                        anlik_degisim -= ((ikinci_en_kucuk(adj, eldeki_yol[seviye - 1]) +
                                        en_kucuk(adj, i)) / 2);
                    if (anlik_degisim + eldeki_agirlik < son_cevap)
                    {
                        eldeki_yol[seviye] = i;
                        ziyared_edilenler[i] = true;
                        gezgin_kargo_2(adj, anlik_degisim, eldeki_agirlik, seviye + 1,
                            eldeki_yol);
                    }
                    eldeki_agirlik -= adj[eldeki_yol[seviye - 1], i];
                    anlik_degisim = adim;
                    ziyared_edilenler.Fill(false);

                    for (int j = 0; j <= seviye - 1; j++)
                        ziyared_edilenler[eldeki_yol[j]] = true;
                }
            }
        }
        static void gezgin_kargo(double[,] adj)//en kisa yolu bulan fonksiyon
        {/*
            int[] eldeki_yol = new int[boyut + 1];
            double anlik_degisim = 0;
            eldeki_yol.Fill(-1);
            ziyared_edilenler.Fill(false);
            for (int i = 0; i < boyut; i++){
                anlik_degisim += (en_kucuk(adj, i) +
                            ikinci_en_kucuk(adj, i)); }

            anlik_degisim = (anlik_degisim == 1) ? anlik_degisim / 2 + 1 :
                                        anlik_degisim / 2;
            ziyared_edilenler[0] = true;
            eldeki_yol[0] = 0;
            gezgin_kargo_2(adj, anlik_degisim, 0, 1, eldeki_yol);*/
        }

    }
    public static class ArrayExtensions
    {
        public static void Fill<T>(this T[] originalArray, T with)
        {
            for (int i = 0; i < originalArray.Length; i++)
            {
                originalArray[i] = with;
            }
        }
    }
}
