using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class GUI1 : Form
    {
        String adres_x = "", adres_y = "", adres1_x = "", adres1_y = "";
        FirebaseConnection con = new FirebaseConnection();
        private static List<PointLatLng> noktalar;
        public delegate void Messenger();
        public static Messenger mesafe_cagir;
        public static Messenger durdur;
        DataTable dt = new DataTable();//Kargo bilgilerinin gosterilmesi icin table olusturulur
        IFirebaseClient client;
        public string Id;
     
        public GUI1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            noktalar = new List<PointLatLng>();
            ekran_durumu(0);
        }
        public static void GUI2()//GUI2 ekranı olusturma ve delegate olarak cagrılacak fonksiyonlar belirlenir
        {
            GUI2 form2 = new GUI2();
            mesafe_cagir += new Messenger(form2.mesafe_hesapla);//GUI2 deki mesafe_hesapla fonksiyonunu cagırmak icin olusturulmus delegate
            durdur += new Messenger(form2.bekle);//GUI2 deki bekle fonksiyonunu cagırmak icin olusturulmus delegate
            Application.Run(form2);
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            Thread mainThread = Thread.CurrentThread;
            Thread Thread2 = new Thread(GUI2);Thread2.Start();//GUI2 ekranı thread olarak calismaya baslar
            Control.CheckForIllegalCrossThreadCalls = false;
            GMapProviders.GoogleMap.ApiKey = AppConfig.Key;
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            map.CacheLocation = @"cache";
            map.MinZoom = 5;map.MaxZoom = 100;map.Zoom = 10;
            map.DragButton = MouseButtons.Left;
            map.MapProvider = GMapProviders.GoogleMap;map.ShowCenter = false;
            map.SetPositionByKeywords("Kocaeli, Turkey");
            client = new FireSharp.FirebaseClient(con.config);
            dt.Columns.Add("kargoId");dt.Columns.Add("Durum");dt.Columns.Add("Adres");
            dataGridView1.DataSource = dt; 
        }
        private void btn_search_Click(object sender, EventArgs e)//Kargo gonderilecek adresin aranmasında kullanılır
        {
            if (!txt_search.Text.Trim().Equals(""))
            {
                GeoCoderStatusCode statusCode;
                var pointLatLng = GoogleMapProvider.Instance.GetPoint(txt_search.Text.Trim(), out statusCode);
                if (statusCode == GeoCoderStatusCode.OK)
                {
                    var pt = pointLatLng ?? default(PointLatLng);
                    adres_x = pointLatLng?.Lat.ToString();
                    adres_y = pointLatLng?.Lng.ToString();
                    marker_ve_konum(pt);
                }
                else{MessageBox.Show("Someting gone wrong. returning status code : " + statusCode);}
            }
            else{MessageBox.Show("İnvalid data load");  }
        }
        private void map_MouseClick(object sender, MouseEventArgs e)//Mouse ile adres seciminde kullanilir
        { 
            if (chkMouseClick.Checked == true && e.Button == MouseButtons.Left)
            {
                var nokta = map.FromLocalToLatLng(e.X, e.Y);
                double lat = nokta.Lat;double lng = nokta.Lng;
                harita_konumla(nokta); marker_ekle(nokta);
                var addresses = adres_al(nokta);
                if (addresses != null){txt_konum.Text =addresses[0];}
                else{txt_konum.Text = "Unable to load adress";}
            }
           
        }
        private void harita_konumla(PointLatLng point)//Haritanin konumlanmasinda kullanilir
        {
            map.Position = point;
        }                                                       
        private void marker_ekle(PointLatLng pointToAdd, GMarkerGoogleType markerType = GMarkerGoogleType.arrow)
        {//Markerlarin konulmasinda kullanilir

            var isaretler = new GMapOverlay("markers");
            var isaret = new GMarkerGoogle(pointToAdd, markerType);
            isaretler.Markers.Add(isaret);
            map.Overlays.Add(isaretler);
        }
        private List<string> adres_al(PointLatLng point)
        {
            List<Placemark> placemarks = null;
            var statusCode = GMapProviders.GoogleMap.GetPlacemarks(point, out placemarks);
            if (statusCode == GeoCoderStatusCode.OK && placemarks != null)
            {
                List<string> adresler = new List<string>();
                foreach (var placemark in placemarks) 
                {
                    adresler.Add(placemark.Address);
                }  
                return adresler;
            }
            return null;
        }
        private void marker_ve_konum(PointLatLng point)//Haritayi konumlandir ve Marker ekle
        {
            harita_konumla(point);
            marker_ekle(point);
        }
        private async void button1_Click_1(object sender, EventArgs e)//Kargo eklemek icin kullanilir
        {
            FirebaseResponse cevap = await client.GetAsync("kargoCounter");
            CounterClass alinan = cevap.ResultAs<CounterClass>();
            string adres;

            if (chkMouseClick.Checked == true)
            {
                adres = txt_konum.Text;
            }
            else
            {
                adres =txt_search.Text;
            }
            client = new FireSharp.FirebaseClient(con.config);
            Kargo kargo = new Kargo()
            {
                kargoId = (Convert.ToInt32(alinan.kargo_cnt) + 1).ToString(),
                userId = Id,
                Adres = adres,
                Durum = ""
            };
            var setter = client.Set("Kargo/" + kargo.kargoId, kargo);
            var nesne = new CounterClass
            {
                kargo_cnt = kargo.kargoId
            };
            SetResponse cevap1 = await client.SetAsync("kargoCounter", nesne);
            Console.WriteLine("kargo");
            durdur();  //GUI2 deki Bekle fonksiyonu cagirilir

        }
        private async void listele_Click(object sender, EventArgs e)//Kargo bilgilerini tabloda listelemek icin kullanilir
        {
            dt.Rows.Clear();
            int i = 0;
            FirebaseResponse cevap = await client.GetAsync("kargoCounter");
            CounterClass nesne = cevap.ResultAs<CounterClass>();
            int kargo_cnt = Convert.ToInt32(nesne.kargo_cnt);
            while (true)
            {
                if (i == kargo_cnt){break;}i++;
                try
                {
                    FirebaseResponse cevap1 = await client.GetAsync("Kargo/" + i);
                    Kargo nesne1 = cevap1.ResultAs<Kargo>();
                    DataRow row = dt.NewRow();
                    row["kargoId"] = nesne1.kargoId;
                    row["Durum"] = nesne1.Durum;
                    row["Adres"] = nesne1.Adres;
                    if (nesne1.Adres != "")
                    {if (Id== nesne1.userId)
                        {dt.Rows.Add(row);
                        }}}catch{}


            }
        }
        private async void kargo_kaldir_Click(object sender, EventArgs e)//Kargo isteginin silinmesinde kullanilir
        {
            FirebaseResponse cevap = await client.GetAsync("kargoCounter");
            CounterClass alinan = cevap.ResultAs<CounterClass>();
            client = new FireSharp.FirebaseClient(con.config);
            Kargo kargo = new Kargo()
            {
                kargoId = (Convert.ToInt32(alinan.kargo_cnt)).ToString(),
                userId = Id,Adres = "",Durum = ""
            };
            var setter = client.Update("Kargo/" + kargo.kargoId, kargo);
        }
        private  void ekran_durumu(int durum)//GUI1 ekraninda bulunan araclarin gorunurlugunu ayarlamakta kullanilir
        {
            
            if (durum == 1){
                yenisifre_txt.Visible = false; yenisifre_lbl.Visible = false;
                sil_btn.Visible = true; durum_sil_btn.Visible = true;
                delegate_btn.Visible = true; geri.Visible = true;
                chkMouseClick.Visible = true;txt_search.Visible = true;
                btn_search.Visible = true;txt_konum.Visible = true;
                button1.Visible = true;listele.Visible = true;
                dataGridView1.Visible = true; map.Visible = true;
                kargo_kaldir.Visible = true;sifre_lbl.Visible = false;
                kul_lbl.Visible = false;kul_txt.Visible = false;
                sifre_txt.Visible = false;giris_btn.Visible = false;
                sifre_degistir.Visible = false;kayit_ol_btn.Visible = false;
                kkul_txt.Visible = false;ksifre_txt.Visible = false;
                kayit_btn.Visible = false;sifre_degisti.Visible = false; }
            if (durum == 0)
            {
                yenisifre_txt.Visible = false;yenisifre_lbl.Visible = false;
                sil_btn.Visible = false; durum_sil_btn.Visible = false;
                 delegate_btn.Visible = false; geri.Visible = false;
                chkMouseClick.Visible = false;txt_search.Visible = false;
                btn_search.Visible = false;txt_konum.Visible = false;
                button1.Visible = false;listele.Visible = false;
                dataGridView1.Visible = false;map.Visible = false;
                kargo_kaldir.Visible = false; sifre_lbl.Visible = true;
                kul_lbl.Visible = true; kul_txt.Visible = true;
                sifre_txt.Visible = true;giris_btn.Visible = true;
                sifre_degistir.Visible = true;kayit_ol_btn.Visible = true;
                kkul_txt.Visible = false;ksifre_txt.Visible = false;
                kayit_btn.Visible = false; sifre_degisti.Visible = false;}
        }

        private async void sifre_degisti_Click(object sender, EventArgs e)//Kullanici sifresini degistirmek icin kullanilir
        {
            try
            {
                if (kkul_txt.Text != null && ksifre_txt.Text != null) {
                    con.client = new FireSharp.FirebaseClient(con.config);
                    con.response = await con.client.GetAsync("Users/" + kkul_txt.Text);
                    Users kullanici = con.response.ResultAs<Users>();
                    if (kkul_txt.Text == kullanici.Username && ksifre_txt.Text==kullanici.Password)
                    {
                        Users kullanici1 = new Users(){Id = kullanici.Id,Password = yenisifre_txt.Text, Username = kkul_txt.Text,};
                        var setter = client.Update("Users/" + kkul_txt.Text, kullanici1); }
                    else {MessageBox.Show("Kullanıcı adı veya şifre yanlış");}  }
                else{MessageBox.Show("Bilgilerinizi dogru giriniz");}
            }
            catch{ MessageBox.Show("Bilgilerinizi dogru giriniz"); }
            kul_txt.Visible = true;sifre_txt.Visible = true;
            giris_btn.Visible = true;sifre_degistir.Visible = true;
            kayit_ol_btn.Visible = true;kkul_txt.Visible = false;
            ksifre_txt.Visible = false;kayit_btn.Visible = false;
            sifre_degisti.Visible = false;
            yenisifre_txt.Visible = false; yenisifre_lbl.Visible = false;

        }

        private async void giris_btn_Click(object sender, EventArgs e)//Kullanici girisi icin kullanilir
        {
            try
            {
                if (kul_txt.Text != null && sifre_txt.Text != null)
                {
                    con.client = new FireSharp.FirebaseClient(con.config);
                    con.response = await con.client.GetAsync("Users/" + kul_txt.Text);
                    Users kullanici = con.response.ResultAs<Users>();

                    if (kul_txt.Text == kullanici.Username && sifre_txt.Text == kullanici.Password)
                    {Id = kullanici.Id ;ekran_durumu(1);}
                    else {MessageBox.Show("Kullanıcı adı veya şifre yanlış"); }
                }
                else {MessageBox.Show("Bilgilerinizi dogru giriniz");} }
            catch (Exception ex) {MessageBox.Show(ex.ToString());  }
        }

        private async void kayit_btn_Click(object sender, EventArgs e)//Kullanici kayiti icin kullanilir
        {
            if (kkul_txt.Text != null && ksifre_txt.Text != null)
            {
                if (kkul_txt.Text != "" && ksifre_txt.Text != "")
                { 
                FirebaseResponse cevap = await client.GetAsync("Counter");
            CounterClass alinan = cevap.ResultAs<CounterClass>();
            client = new FireSharp.FirebaseClient(con.config);
            Users kullanici = new Users(){Id = (Convert.ToInt32(alinan.cnt) + 1).ToString(),Password = ksifre_txt.Text,Username = kkul_txt.Text, };
               
                var setter = client.Set("Users/" + kkul_txt.Text, kullanici);
            var nesne = new CounterClass{cnt = kullanici.Id};
            SetResponse cevap1 = await client.SetAsync("Counter", nesne);
                }
                else { MessageBox.Show("Bilgilerinizi dogru giriniz"); }
            }
            else { MessageBox.Show("Bilgilerinizi dogru giriniz"); }
            kul_txt.Visible = true;sifre_txt.Visible = true;
            giris_btn.Visible = true; sifre_degistir.Visible = true;
            kayit_ol_btn.Visible = true; kkul_txt.Visible = false;
            ksifre_txt.Visible = false;kayit_btn.Visible = false;
            sifre_degisti.Visible = false;

        }
        private void kayit_ol_btn_Click(object sender, EventArgs e)//Kayit ol ekraninda kullanilan araclariri gorunur yapar
        {
            kul_txt.Visible = false;sifre_txt.Visible = false;
            giris_btn.Visible = false;sifre_degistir.Visible = false;
            kayit_ol_btn.Visible = false;kkul_txt.Visible = true;
            ksifre_txt.Visible = true;kayit_btn.Visible = true;
            sifre_degisti.Visible = false;
        }

        private async void sil_btn_Click(object sender, EventArgs e)// Iletimi yapilmis kargolari siler
        {
            FirebaseResponse cevap = await client.GetAsync("kargoCounter");
            CounterClass alinan = cevap.ResultAs<CounterClass>();
            int kargo_cnt = Convert.ToInt32(alinan.kargo_cnt);
            for (int i = kargo_cnt; i > 1; i--)
            {
                var setter = client.Delete("Kargo/" + i);
            }
            FirebaseResponse cevap1 = await client.GetAsync("kargoCounter");
            CounterClass alinan1 = cevap1.ResultAs<CounterClass>();
           
            CounterClass counterclass = new CounterClass()
            {
                kargo_cnt = "1"
            };
            var setter1 = client.Update("kargoCounter", counterclass);

        }

        private void geri_Click(object sender, EventArgs e)
        {
            ekran_durumu(0);
        }

        private async void durum_sil_btn_Click(object sender, EventArgs e)//tekrardan kullanilmasi icin kargolarin durumlarini siler
        {
            FirebaseResponse cevap = await client.GetAsync("kargoCounter");
            CounterClass alinan = cevap.ResultAs<CounterClass>();
            int kargo_cnt = Convert.ToInt32(alinan.kargo_cnt);
            for (int i = 2; i <= kargo_cnt; i++)
            {

                client = new FireSharp.FirebaseClient(con.config);
                FirebaseResponse cevap1 = await client.GetAsync("Kargo/"+i);
                Kargo alinan1 = cevap1.ResultAs<Kargo>();
                Kargo kargo = new Kargo()
                {
                    kargoId = Convert.ToString(i),
                    userId = Id,
                    Adres = alinan1.Adres,
                    Durum = ""
                };
                var setter = client.Update("Kargo/" + i, kargo);
            }


        }

      

        private void sifre_degistir_Click(object sender, EventArgs e)//Kullanici sifre degistirme ekranini gorunur hale getirir
        {
            yenisifre_txt.Visible = true; yenisifre_lbl.Visible = true;
            kul_txt.Visible = false;sifre_txt.Visible = false;
            giris_btn.Visible = false; sifre_degistir.Visible = false;
            kayit_ol_btn.Visible = false;kkul_txt.Visible = true;
            ksifre_txt.Visible = true;kayit_btn.Visible = false;
            sifre_degisti.Visible = true;

        }
        private void delegate_deneme_Click(object sender, EventArgs e)//GUI2 mesafe hesapla fonksiyonunu cagirir
        {
            mesafe_cagir();
        }

}}

