HenUp – QA Checklist (v0.9)
1) Akış & Durum Yönetimi



MUTE OLAYI TAM OLARAK DOĞRU ÇALIŞMIYOR. MUTE YAPTIKTAN SONRA SETTİNGSE GİRİNCE SESLER SON KAYITLI SEVİYELERİYLE AÇILIYOR. TEKRAR MUTE TUŞUNA BASINCA MUTE OLMUYOR, BİR SONRAKİ BASIŞTA OLUYOR. MUTE-VOLUME EŞLEŞTİRMESİNİ YAPMAK LAZIM. 



3) Skor & Persist


Nasıl: Oyunu kapatıp aç (WebGL’de sayfayı refresh).

Beklenen: Best Score ve ses tercihleri duruyor.

4) UI / UX

 Butonlar çalışma garantisi (runtime bağlama)

Nasıl: Menü butonlarına art arda tıkla, sahneler arası dolaş.

Beklenen: Çift listener oluşmaz, “boş” referans hatası yok (Console temiz).

 UI ölçekleme

Nasıl: Pencereleri farklı boyutlara getir (1280×720 / tam ekran).

Beklenen: HUD taşmıyor, skor okunur; butonlar erişilebilir; wrap-around UI’yı kesmiyor.

 How to Play metni (placeholder)

Nasıl: (Eklemeyi planlıyorsun) şimdilik boşsa atla; eklediğinde kısalık/berraklık kontrol et.

Beklenen: Kontroller tek satırda, gereksiz uzun değil.

5) Ses (Audio)

 BGM loop pürüzsüz

Nasıl: Müzik loop’unda geçişi dinle (kulaklıkla).

Beklenen: Pop/klik yok; başa sararken boşluk sezilmez (varsa export/loop noktası düzelt).

 SFX çakışma

Nasıl: Peş peşe atlamalar/inişler yap; UI tıklamalarını üst üste bindir.

Beklenen: Patlama/clip yok; volüm dengesi yerinde.

 Music/SFX mute & volume

Nasıl: Slider’ları değiştir; Menü/Oyun arası geç; oyunu kapat-aç.

Beklenen: Değerler anında uygulanır, yeniden girişte aynı kalır.

 İlk etkileşim zorunluluğu (WebGL)

Nasıl: Sayfayı yenileyip hiç tıklamadan bekle; sonra Play’e bas.

Beklenen: Play ile birlikte ses başlar; tarayıcı sessizlik politikasına takılmaz.

6) Performans & Stabilite

 FPS stabilitesi

Nasıl: 2–3 dk oyna; profiler/Stats ile bak.

Beklenen: FPS düşüşü yok; GC spike minimal (Pooling sayesinde).

 Pool geri dönüşümü

Nasıl: Uzun süre tırman; pool’daki objeler doğru reuse ediliyor mu (Scene’de sayılar sabit)?

Beklenen: Instantiate/Destroy fırtınası yok; konsolda uyarı yok.

 Hata/uyarı logları

Nasıl: Konsolu aç; tüm akışları dolaş.

Beklenen: Kırmızı (Error) yok; sarı (Warning) kabul edilebilir düzeyde (tercihen 0).

7) WebGL Build & itch hazırlığı

 WebGL local run

Nasıl: Builds/WebGL al; local server (Unity “Build & Run”).

Beklenen: Oyun açılır; input/ ses normal.

 Boyut ve sıkıştırma

Nasıl: Player Settings → Compression Format (Gzip/Brotli).

Beklenen: Build boyutu makul; ilk yükleme süresi kısa.

 Tarayıcılar

Nasıl: Chrome + Safari’de dener misin?

Beklenen: Davranış aynı; ses politikası farkında sorun yok.

8) Kenar Durumları

 Aşırı hız / ekranda “delik”

Nasıl: Çok hızlı wrap-around denemeleri yap.

Beklenen: Platform atlamalarda ghost collision yok.

 Restart sonrası temizlik

Nasıl: Game Over → Restart; birkaç tur yap.

Beklenen: Zorluk çarpanı doğru sıfırlanır; skor/ pool/ kamera minY resetlenir.

 Input kilitlenmesi

Nasıl: Settings → geri → hemen hareket/ zıplama dene.

Beklenen: Input akıyor; fokus kaybı yok.

9) İçerik & Lisans (yayın öncesi)

 Kredi metni

Nasıl: SFX kaynağı/ müzik üretimi notu; README/itch sayfasına yaz.

Beklenen: Lisans uygunluğu şeffaf.

 Oyun adı & meta

Nasıl: “HenUp” başlık, kısa açıklama, kontroller, küçük 2–3 ekran görüntüsü.

Beklenen: itch.io sayfası anlaşılır ve hızlı okunur.