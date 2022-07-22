# Yazlab1 1.Proje

# KARGO DAĞITIM SİSTEMİ

# Özet
Proje akıllı kargo dağıtım sistemi yapan bir masaüstü uygulamasıdır. Uygulama C# kullanılarak geliştirilmiştir.
# 1. Giriş
Kullanıcı uygulamayı açtığında karşısına login ekranı gelir. Login ekranında önceden açmış olduğu kayıtla giriş yapabilir.
Kullanıcı görünen username ve şifre ile sisteme giriş yapar. Eğer kullanıcı önceden kayıt açmadıysa kayıt ol butonuna tıklayarak yeni
kayıt açabilir. Kullanıcı isterse eski şifresini yeni şifreyle değiştirebilir.
Kullanıcı giriş yaptıktan sonra karşısına ana
ekran çıkar .Bu ekranda ister adresi yazarak isterse haritada işaretleyerek kargolarının gideceği adresleri belirleyebilir silebilir.
Kullanıcı isterse kargolarının durumunu
listede görebilir. Tüm kargolarını listeden
silebilir. Eğer istemesi durumunda
kargolarının durumunu sıfırlayabilir. Kargo yola çıktıktan sonra kullanıcı istemesi durumunda kargoyu durdurup önceden belirlediği kargo adresini ekletip ziyaret
edilmemiş noktalar arasında yeni bir rota
çizdirebilir.


# 2. Temel Bilgiler
Proje geliştirmede:
Projede bulut tabanlı bir database olan “Firebase” kullanılmıştır. Firebase ile işlem
yapabilmek için “FireSharp” kütüphanesini kullandık. Haritadan gerekli olan bilgileri alabilmek için GoogleMaps API kullanılmıştır. Kütüphane olarak Gmap kütüphanesi olan “GMap.NET” kullanılmıştır.
Programlama dili olarak “C#” kullanılmıştır.
Program geliştirme ortamı olarak “Visual Studio
Code” kullanılmıştır.
# 3. Tasarım
Projenin geliştirilmesi aşağıdaki başlıklar altında
inceleyelim.
## 3.1 Ara yüz
### 3.1.1 GUI1
GUI1 de ilk olarak karşımıza giriş ekranı gelir.
Giriş ekranı kullanıcıdan ismini ve şifresini ister. Kullanıcının kaydı yoksa yeni kayıt açabilir. Eğer şifresini değiştirmek isterse şifresini
değiştirebilir. Giriş ekranında başarılı bir giriş yaptıktan sonra kargo işlemlerine geçiş
yapabilirsiniz. Yeni kargo eklemek veya kaldırmak istediğinde kullanıcı haritadan yada manuel olarak adres göndermelidir. Kullanıcı istediği takdirde gönderdiği tüm kargoları görebilir, kaldırabilir. Kullanıcı istediği takdirde aralarından seçtiği herhangi bir kargoyu da kaldırabilir. Kullanılan fonksiyonlar aşağıdaki gibidir.
#### button1_Click_1 
butonuna tıkladığınız zaman yeni bir kargo ekleyebilirsiniz
#### listele_Click
butonu ile elimizdeki kargoların listesini ve durumlarını görebiliriz.
#### kargo_kaldir_Click 
butonu vasıtasıyla kaldırmak istediğiniz herhangi bir kargoyu kaldırabilirsiniz.
#### sifre_degisti_Click 
butonu ile eski şifrenizi yeni şifreniz ile değiştirebilirsiniz.
#### giris_btn_Click 
giriş butonu ile kullanıcının girdiği bilgilerin doğruluğuna göre işlem yapabilmek için giriş yapabilirsiniz.
#### kayit_btn_Click 
Bu butonu kullandığınız takdirde yeni bir kayıt oluşturup hızlıca giriş yapabilirsiniz.
#### kayit_ol_btn_Click
Kayıt olma ekranının görünür olmasını sağlar.
#### sifre_degistir_Click
Şifre değiştir ekranının görünür olmasını sağlar.
#### delegate_btn 
kargo dağıtım işlemini başlatır.
#### map_MouseClick
haritadan kullanılmak üzere bilgi alır.
#### harita_konumla 
Haritanının yüklenmesini sağlar
#### marker_ekle
Eklenen adresleri haritaya ekler.
####adres_al 
ile haritada tıklanılan yerin adresi alınır.
#### marker_ve_konum
haritanın yüklenmesini ve markerların haritada işaretlenmesini sağlar.
#### geri_Click 
kargo ekleme ekranından ana sayfaya gelmeyi sağlar.



### 3.1.2 Genel Rota (GUI2)
Kullanıcı GUI2 ekranında GUI1 ekranında aldığı adreslerin kargo tarafından en kısa şekilde nasıl
Bir rotayla hareket ettiğini görebilir. Kullanıcı istemesi durumunda kargo hareket ederken yeni bir adres gönderebilir. Bu durumda ise kargo yeni eklenen adresler ile ziyaret edilmemiş adresler arasında yeni bir kısa yol hesaplar ve bu şekilde rotayı tamamlayıp başladığı yere geri dönebilir. struct Kargo_bilgi Struct’ın içinde
databaseden çekilen bilgiler tutulur.

#### harita_konumla 
Haritanın konumunu ayarlar.

#### marker_ve_konum
Marker’ı ekler ve konumuna gönderirir.

#### nokta_ekle
bu fonksiyonda eklenen adreslerin konumları alınarak hazırlanır.

#### rota_hesapla 
iki adres arasındaki mesafeyi hesaplar.
#### mesafehesapla 
Kullanıcı tarfından gönderilen tüm adresler arasında mesafe hesaplanıp gezgin_kargo fonksiyonuna gönderir.Bu şekilde en kısa yol hesaplanır.
#### yol_ciz 
Bu fonksiyonda mesafehesapla fonksiyonunda hesaplaplanmış rotaları çizer.
#### mesafe_hesapla_kesinti 
Kullanıcı bu fonksiyon sayesinde yola çıkmış kargoya yeni bir kargo eklendiği takdirde oluşan yeni yolu hesaplatmış olur.
#### yol_ciz2 
Bu fonksiyon sayesinde kargonun hareketinden sonra eklenen noktayla birlikte çizilen yeni rotayı görmüş oluruz.
#### son_yol_degistir 
Bulunan en kısa yolu son yolla değiştirir.
#### en_kucuk 
Alınan yollar arasındaki en kısa mesafeyi hesaplar
#### en_kucuk_ikinci 
Alınan yollar arasındaki en kısa ikinci mesafeyi hesaplar
#### gezgin_kargo_2 
Mesafeleri kendi aralarında kıyaslamasını yapar.
#### gezgin_kargo 
Önceden alınmış noktaların arasındaki mesafeler ile işlemler yaparak gidilebilecek en kısa mesafeyi hesaplar.
## 3.2 Bulut Platform
Bulut platform kısmında Firebase kullanılmıştır.
## 3.3 Harita
Harita olarak Google maps kullanılmıştır.
# 4- Pseudo Code
-Basla
-eger Şifre degiştire butonuna basılırsa şifre degiştirilir
-eger Kayıt oluştur butonuna basılırsa yeni kullanici hesabı olusturulur
-eger giris yap butonuna basılırsa giris yapılır
-kargo adresi search butonu ile arama
yapılır
-kargo adresi map üzerinden secmek için enable mouse click chech boxa basılır
-kargo butonuna bas ve kargo gönderiminde bulun
-listele butonu ile kargo bilgilerini gör
-kargo kaldır butonu ile secilen
kargoyu sil
-durum sil buttonu ile kargoları
tekrar kullanmak icin duzenle
-sil butonu ile kargo bilgilerini sil
-basla butonu ile alınan kargolar dagıtıma cıkar ve teslim edilir
-dagıtım esnasında kargo eklenirse yeni kargo bilgileri alınır ve tekrardan rota cizilir
-cıkış yapılır ve uygulama biter

# 5-Deneysel Sonuçlar ve Ekran Görüntüleri

![image](https://user-images.githubusercontent.com/58952369/180211920-1984cddd-8517-49a7-82d3-659aa8fbc787.png)
![image](https://user-images.githubusercontent.com/58952369/180211930-2d79e8c9-efa0-4e73-908d-3d73dc623eb4.png)
![image](https://user-images.githubusercontent.com/58952369/180211954-1fb21274-6ee8-49ce-be62-1eec39e00690.png)
![image](https://user-images.githubusercontent.com/58952369/180212262-aba773fa-5f00-494e-b9ff-3a6d6f3ef37b.png)
![image](https://user-images.githubusercontent.com/58952369/180212278-41e56155-20c3-4524-a358-f5314e1419bb.png)
![image](https://user-images.githubusercontent.com/58952369/180212302-754ea9b4-8c12-4fe7-a9c9-2772db8f702d.png)
![image](https://user-images.githubusercontent.com/58952369/180212327-43b2fa35-9c59-4687-91b8-322ca66fcfe5.png)
![image](https://user-images.githubusercontent.com/58952369/180212343-f113c747-1da4-4d3f-b087-559fe4bb17c7.png)
![image](https://user-images.githubusercontent.com/58952369/180212369-0bca49d6-9289-4ca3-b998-61b48a6d6e19.png)
![image](https://user-images.githubusercontent.com/58952369/180212391-786dba95-a762-40f4-bd17-42b520a8166b.png)




# 6-Kaynakça
https://stackoverflow.com

https://www.geeksforgeeks.org/

https://www.youtube.com

https://github.com

https://firebase.google.com/docs

https://www.w3schools.com/

