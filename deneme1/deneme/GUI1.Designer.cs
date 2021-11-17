
namespace deneme
{
    partial class GUI1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.map = new GMap.NET.WindowsForms.GMapControl();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.chkMouseClick = new System.Windows.Forms.CheckBox();
            this.txt_search = new System.Windows.Forms.TextBox();
            this.btn_search = new System.Windows.Forms.Button();
            this.txt_konum = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.listele = new System.Windows.Forms.Button();
            this.kargo_kaldir = new System.Windows.Forms.Button();
            this.kul_txt = new System.Windows.Forms.TextBox();
            this.sifre_txt = new System.Windows.Forms.TextBox();
            this.kkul_txt = new System.Windows.Forms.TextBox();
            this.ksifre_txt = new System.Windows.Forms.TextBox();
            this.kul_lbl = new System.Windows.Forms.Label();
            this.sifre_lbl = new System.Windows.Forms.Label();
            this.sifre_degisti = new System.Windows.Forms.Button();
            this.giris_btn = new System.Windows.Forms.Button();
            this.kayit_btn = new System.Windows.Forms.Button();
            this.kayit_ol_btn = new System.Windows.Forms.Button();
            this.sifre_degistir = new System.Windows.Forms.Button();
            this.delegate_btn = new System.Windows.Forms.Button();
            this.sil_btn = new System.Windows.Forms.Button();
            this.durum_sil_btn = new System.Windows.Forms.Button();
            this.geri = new System.Windows.Forms.Button();
            this.yenisifre_txt = new System.Windows.Forms.TextBox();
            this.yenisifre_lbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Margin = new System.Windows.Forms.Padding(2);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(445, 486);
            this.splitter1.TabIndex = 0;
            this.splitter1.TabStop = false;
            // 
            // map
            // 
            this.map.Bearing = 0F;
            this.map.CanDragMap = true;
            this.map.EmptyTileColor = System.Drawing.Color.Navy;
            this.map.GrayScaleMode = false;
            this.map.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.map.LevelsKeepInMemory = 5;
            this.map.Location = new System.Drawing.Point(0, 0);
            this.map.Margin = new System.Windows.Forms.Padding(2);
            this.map.MarkersEnabled = true;
            this.map.MaxZoom = 2;
            this.map.MinZoom = 2;
            this.map.MouseWheelZoomEnabled = true;
            this.map.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.map.Name = "map";
            this.map.NegativeMode = false;
            this.map.PolygonsEnabled = true;
            this.map.RetryLoadTile = 0;
            this.map.RoutesEnabled = true;
            this.map.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.map.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.map.ShowTileGridLines = false;
            this.map.Size = new System.Drawing.Size(445, 486);
            this.map.TabIndex = 1;
            this.map.Zoom = 0D;
            this.map.MouseClick += new System.Windows.Forms.MouseEventHandler(this.map_MouseClick);
            // 
            // chkMouseClick
            // 
            this.chkMouseClick.AutoSize = true;
            this.chkMouseClick.Location = new System.Drawing.Point(450, 15);
            this.chkMouseClick.Margin = new System.Windows.Forms.Padding(2);
            this.chkMouseClick.Name = "chkMouseClick";
            this.chkMouseClick.Size = new System.Drawing.Size(117, 17);
            this.chkMouseClick.TabIndex = 13;
            this.chkMouseClick.Text = "enable mouse click";
            this.chkMouseClick.UseVisualStyleBackColor = true;
            // 
            // txt_search
            // 
            this.txt_search.Location = new System.Drawing.Point(450, 49);
            this.txt_search.Margin = new System.Windows.Forms.Padding(2);
            this.txt_search.Name = "txt_search";
            this.txt_search.Size = new System.Drawing.Size(150, 20);
            this.txt_search.TabIndex = 14;
            // 
            // btn_search
            // 
            this.btn_search.Location = new System.Drawing.Point(450, 85);
            this.btn_search.Margin = new System.Windows.Forms.Padding(2);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(114, 19);
            this.btn_search.TabIndex = 15;
            this.btn_search.Text = "search";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // txt_konum
            // 
            this.txt_konum.Location = new System.Drawing.Point(449, 120);
            this.txt_konum.Margin = new System.Windows.Forms.Padding(2);
            this.txt_konum.Name = "txt_konum";
            this.txt_konum.Size = new System.Drawing.Size(150, 62);
            this.txt_konum.TabIndex = 16;
            this.txt_konum.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(449, 210);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(56, 23);
            this.button1.TabIndex = 17;
            this.button1.Text = "Kargo Kayıt";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(450, 246);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(351, 228);
            this.dataGridView1.TabIndex = 18;
            // 
            // listele
            // 
            this.listele.Location = new System.Drawing.Point(511, 210);
            this.listele.Name = "listele";
            this.listele.Size = new System.Drawing.Size(75, 23);
            this.listele.TabIndex = 19;
            this.listele.Text = "Listele";
            this.listele.UseVisualStyleBackColor = true;
            this.listele.Click += new System.EventHandler(this.listele_Click);
            // 
            // kargo_kaldir
            // 
            this.kargo_kaldir.Location = new System.Drawing.Point(592, 210);
            this.kargo_kaldir.Name = "kargo_kaldir";
            this.kargo_kaldir.Size = new System.Drawing.Size(75, 23);
            this.kargo_kaldir.TabIndex = 20;
            this.kargo_kaldir.Text = "Kargo Kaldır";
            this.kargo_kaldir.UseVisualStyleBackColor = true;
            this.kargo_kaldir.Click += new System.EventHandler(this.kargo_kaldir_Click);
            // 
            // kul_txt
            // 
            this.kul_txt.Location = new System.Drawing.Point(699, 51);
            this.kul_txt.Margin = new System.Windows.Forms.Padding(2);
            this.kul_txt.Name = "kul_txt";
            this.kul_txt.Size = new System.Drawing.Size(128, 20);
            this.kul_txt.TabIndex = 21;
            // 
            // sifre_txt
            // 
            this.sifre_txt.Location = new System.Drawing.Point(699, 88);
            this.sifre_txt.Margin = new System.Windows.Forms.Padding(2);
            this.sifre_txt.Name = "sifre_txt";
            this.sifre_txt.Size = new System.Drawing.Size(128, 20);
            this.sifre_txt.TabIndex = 22;
            // 
            // kkul_txt
            // 
            this.kkul_txt.Location = new System.Drawing.Point(699, 51);
            this.kkul_txt.Margin = new System.Windows.Forms.Padding(2);
            this.kkul_txt.Name = "kkul_txt";
            this.kkul_txt.Size = new System.Drawing.Size(128, 20);
            this.kkul_txt.TabIndex = 23;
            // 
            // ksifre_txt
            // 
            this.ksifre_txt.Location = new System.Drawing.Point(699, 88);
            this.ksifre_txt.Margin = new System.Windows.Forms.Padding(2);
            this.ksifre_txt.Name = "ksifre_txt";
            this.ksifre_txt.Size = new System.Drawing.Size(128, 20);
            this.ksifre_txt.TabIndex = 24;
            // 
            // kul_lbl
            // 
            this.kul_lbl.AutoSize = true;
            this.kul_lbl.Location = new System.Drawing.Point(614, 51);
            this.kul_lbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.kul_lbl.Name = "kul_lbl";
            this.kul_lbl.Size = new System.Drawing.Size(63, 13);
            this.kul_lbl.TabIndex = 25;
            this.kul_lbl.Text = "Kullanici adi";
            // 
            // sifre_lbl
            // 
            this.sifre_lbl.AutoSize = true;
            this.sifre_lbl.Location = new System.Drawing.Point(614, 88);
            this.sifre_lbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.sifre_lbl.Name = "sifre_lbl";
            this.sifre_lbl.Size = new System.Drawing.Size(28, 13);
            this.sifre_lbl.TabIndex = 26;
            this.sifre_lbl.Text = "Sifre";
            // 
            // sifre_degisti
            // 
            this.sifre_degisti.Location = new System.Drawing.Point(612, 151);
            this.sifre_degisti.Margin = new System.Windows.Forms.Padding(2);
            this.sifre_degisti.Name = "sifre_degisti";
            this.sifre_degisti.Size = new System.Drawing.Size(73, 19);
            this.sifre_degisti.TabIndex = 27;
            this.sifre_degisti.Text = "şifrenizi degistiriniz";
            this.sifre_degisti.UseVisualStyleBackColor = true;
            this.sifre_degisti.Click += new System.EventHandler(this.sifre_degisti_Click);
            // 
            // giris_btn
            // 
            this.giris_btn.Location = new System.Drawing.Point(699, 151);
            this.giris_btn.Margin = new System.Windows.Forms.Padding(2);
            this.giris_btn.Name = "giris_btn";
            this.giris_btn.Size = new System.Drawing.Size(56, 19);
            this.giris_btn.TabIndex = 28;
            this.giris_btn.Text = "giris yap";
            this.giris_btn.UseVisualStyleBackColor = true;
            this.giris_btn.Click += new System.EventHandler(this.giris_btn_Click);
            // 
            // kayit_btn
            // 
            this.kayit_btn.Location = new System.Drawing.Point(770, 151);
            this.kayit_btn.Margin = new System.Windows.Forms.Padding(2);
            this.kayit_btn.Name = "kayit_btn";
            this.kayit_btn.Size = new System.Drawing.Size(56, 19);
            this.kayit_btn.TabIndex = 29;
            this.kayit_btn.Text = "kayıt olusturunuz";
            this.kayit_btn.UseVisualStyleBackColor = true;
            this.kayit_btn.Click += new System.EventHandler(this.kayit_btn_Click);
            // 
            // kayit_ol_btn
            // 
            this.kayit_ol_btn.Location = new System.Drawing.Point(663, 175);
            this.kayit_ol_btn.Margin = new System.Windows.Forms.Padding(2);
            this.kayit_ol_btn.Name = "kayit_ol_btn";
            this.kayit_ol_btn.Size = new System.Drawing.Size(56, 19);
            this.kayit_ol_btn.TabIndex = 30;
            this.kayit_ol_btn.Text = "kayıt ol";
            this.kayit_ol_btn.UseVisualStyleBackColor = true;
            this.kayit_ol_btn.Click += new System.EventHandler(this.kayit_ol_btn_Click);
            // 
            // sifre_degistir
            // 
            this.sifre_degistir.Location = new System.Drawing.Point(745, 175);
            this.sifre_degistir.Margin = new System.Windows.Forms.Padding(2);
            this.sifre_degistir.Name = "sifre_degistir";
            this.sifre_degistir.Size = new System.Drawing.Size(56, 19);
            this.sifre_degistir.TabIndex = 31;
            this.sifre_degistir.Text = "sifre degistir";
            this.sifre_degistir.UseVisualStyleBackColor = true;
            this.sifre_degistir.Click += new System.EventHandler(this.sifre_degistir_Click);
            // 
            // delegate_btn
            // 
            this.delegate_btn.Location = new System.Drawing.Point(663, 9);
            this.delegate_btn.Margin = new System.Windows.Forms.Padding(2);
            this.delegate_btn.Name = "delegate_btn";
            this.delegate_btn.Size = new System.Drawing.Size(56, 19);
            this.delegate_btn.TabIndex = 32;
            this.delegate_btn.Text = "basla";
            this.delegate_btn.UseVisualStyleBackColor = true;
            this.delegate_btn.Click += new System.EventHandler(this.delegate_deneme_Click);
            // 
            // sil_btn
            // 
            this.sil_btn.Location = new System.Drawing.Point(672, 210);
            this.sil_btn.Margin = new System.Windows.Forms.Padding(2);
            this.sil_btn.Name = "sil_btn";
            this.sil_btn.Size = new System.Drawing.Size(68, 23);
            this.sil_btn.TabIndex = 34;
            this.sil_btn.Text = "sil";
            this.sil_btn.UseVisualStyleBackColor = true;
            this.sil_btn.Click += new System.EventHandler(this.sil_btn_Click);
            // 
            // durum_sil_btn
            // 
            this.durum_sil_btn.Location = new System.Drawing.Point(745, 210);
            this.durum_sil_btn.Margin = new System.Windows.Forms.Padding(2);
            this.durum_sil_btn.Name = "durum_sil_btn";
            this.durum_sil_btn.Size = new System.Drawing.Size(70, 23);
            this.durum_sil_btn.TabIndex = 35;
            this.durum_sil_btn.Text = "durum sil";
            this.durum_sil_btn.UseVisualStyleBackColor = true;
            this.durum_sil_btn.Click += new System.EventHandler(this.durum_sil_btn_Click);
            // 
            // geri
            // 
            this.geri.Location = new System.Drawing.Point(748, 9);
            this.geri.Name = "geri";
            this.geri.Size = new System.Drawing.Size(75, 23);
            this.geri.TabIndex = 36;
            this.geri.Text = "geri";
            this.geri.UseVisualStyleBackColor = true;
            this.geri.Click += new System.EventHandler(this.geri_Click);
            // 
            // yenisifre_txt
            // 
            this.yenisifre_txt.Location = new System.Drawing.Point(699, 120);
            this.yenisifre_txt.Name = "yenisifre_txt";
            this.yenisifre_txt.Size = new System.Drawing.Size(128, 20);
            this.yenisifre_txt.TabIndex = 37;
            // 
            // yenisifre_lbl
            // 
            this.yenisifre_lbl.AutoSize = true;
            this.yenisifre_lbl.Location = new System.Drawing.Point(617, 126);
            this.yenisifre_lbl.Name = "yenisifre_lbl";
            this.yenisifre_lbl.Size = new System.Drawing.Size(52, 13);
            this.yenisifre_lbl.TabIndex = 38;
            this.yenisifre_lbl.Text = "Yeni Şifre";
            // 
            // GUI1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumAquamarine;
            this.ClientSize = new System.Drawing.Size(835, 486);
            this.Controls.Add(this.yenisifre_lbl);
            this.Controls.Add(this.yenisifre_txt);
            this.Controls.Add(this.geri);
            this.Controls.Add(this.durum_sil_btn);
            this.Controls.Add(this.sil_btn);
            this.Controls.Add(this.delegate_btn);
            this.Controls.Add(this.sifre_degistir);
            this.Controls.Add(this.kayit_ol_btn);
            this.Controls.Add(this.kayit_btn);
            this.Controls.Add(this.giris_btn);
            this.Controls.Add(this.sifre_degisti);
            this.Controls.Add(this.sifre_lbl);
            this.Controls.Add(this.kul_lbl);
            this.Controls.Add(this.ksifre_txt);
            this.Controls.Add(this.kkul_txt);
            this.Controls.Add(this.sifre_txt);
            this.Controls.Add(this.kul_txt);
            this.Controls.Add(this.kargo_kaldir);
            this.Controls.Add(this.listele);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txt_konum);
            this.Controls.Add(this.btn_search);
            this.Controls.Add(this.txt_search);
            this.Controls.Add(this.chkMouseClick);
            this.Controls.Add(this.map);
            this.Controls.Add(this.splitter1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "GUI1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "GUI1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Splitter splitter1;
        private GMap.NET.WindowsForms.GMapControl map;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.CheckBox chkMouseClick;
        private System.Windows.Forms.TextBox txt_search;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.RichTextBox txt_konum;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button listele;
        private System.Windows.Forms.Button kargo_kaldir;
        private System.Windows.Forms.TextBox kul_txt;
        private System.Windows.Forms.TextBox sifre_txt;
        private System.Windows.Forms.TextBox kkul_txt;
        private System.Windows.Forms.TextBox ksifre_txt;
        private System.Windows.Forms.Label kul_lbl;
        private System.Windows.Forms.Label sifre_lbl;
        private System.Windows.Forms.Button sifre_degisti;
        private System.Windows.Forms.Button giris_btn;
        private System.Windows.Forms.Button kayit_btn;
        private System.Windows.Forms.Button kayit_ol_btn;
        private System.Windows.Forms.Button sifre_degistir;
        private System.Windows.Forms.Button delegate_btn;
        private System.Windows.Forms.Button sil_btn;
        private System.Windows.Forms.Button durum_sil_btn;
        private System.Windows.Forms.Button geri;
        private System.Windows.Forms.TextBox yenisifre_txt;
        private System.Windows.Forms.Label yenisifre_lbl;
    }
}

