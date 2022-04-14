using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using NetworkService.Model;
using NetworkService.Pomoc;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using System.Windows.Media;
using System.ComponentModel;
using System.Windows.Media.Imaging;

namespace NetworkService.ViewModel
{
    class DropDownViewModel : BindableBase
    {
        public static Saobracaj draggItm = null;
        private bool dragging = false;
        private static bool exists = false;
        private int selectedIndex = 0;

        private ListView lv;
        public BindingList<Saobracaj> Items { get; set; }
        //komande 
        public MyICommand<ListView> MLBUCommand { get; set; }
        public MyICommand<Saobracaj> SCCommand { get; set; }
        public MyICommand<Canvas> DCommand { get; set; }//on drop
        public MyICommand<Canvas> FreeCommand { get; set; }//on free
        public MyICommand<Canvas> DOCommand { get; set; }//drag over
        public MyICommand<Canvas> DLCommand { get; set; }//drag over leave
        public MyICommand<Canvas> LCommand { get; set; }//on load
        public MyICommand<ListView> LLWCommand { get; set; }
        public List<Canvas> CanvasList { get; set; } = new List<Canvas>();

        //obj koji su dodati u canvas
        public static Dictionary<string, Saobracaj> CanvasObj { get; set; } = new Dictionary<string, Saobracaj>();
        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                selectedIndex = value;
                OnPropertyChanged("SelectedIndex");

            }
        }
        

        public DropDownViewModel()
        {
            Items = new BindingList<Saobracaj>();

            foreach (Saobracaj sa in SaobracajViewModel.Saobracaji)
            {
                exists = false;
                foreach (Saobracaj SaCn in CanvasObj.Values)
                {
                    if (sa.Id == SaCn.Id)
                    {
                        exists = true;
                        break;
                    }

                }
                if (exists == false)
                    Items.Add(new Saobracaj(sa));
            }
            
            MLBUCommand = new MyICommand<ListView>(OnMLBU);
            SCCommand = new MyICommand<Saobracaj>(SelectionChange);
            DCommand = new MyICommand<Canvas>(OnDrop);
            FreeCommand = new MyICommand<Canvas>(OnFree);
            DOCommand = new MyICommand<Canvas>(OnDragOver);
            DLCommand = new MyICommand<Canvas>(OnDragLeave);
            LCommand = new MyICommand<Canvas>(OnLoad);
            LLWCommand = new MyICommand<ListView>(OnLLW);

        }
        public void OnLLW(ListView listview)
        {
            //lv dobija vrednost ListView-a gde su obj
            lv = listview;
        }
        public void OnLoad(Canvas c)
        {
            if (CanvasObj.ContainsKey(c.Name))
            {
                BitmapImage logo = new BitmapImage();//za jednostavniji prikaz slike
                logo.BeginInit();//signal za pocetak inicijalizacije
                string temp = CanvasObj[c.Name].Type.Name.ToString() + ".jpg";
                logo.UriSource = new Uri("pack://application:,,,/pictures/" + temp, UriKind.Absolute);
                logo.EndInit();
                c.Background = new ImageBrush(logo);
                ((TextBlock)(c).Children[1]).Text = "";
                c.Resources.Add("taken", true);
                CheckValue(c);

            }
            if (!CanvasList.Contains(c))
            {
                CanvasList.Add(c);
            }
        }

        private void CheckValue(Canvas c)
        {
            Dictionary<int, Saobracaj> temp = new Dictionary<int, Saobracaj>();
            foreach (var r in SaobracajViewModel.Saobracaji)
            {
                temp.Add(r.Id, r);
            }
            Task.Delay(1000).ContinueWith(_ =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    ((Border)(c).Children[0]).BorderBrush = Brushes.Transparent;

                    if (CanvasObj.Count != 0)
                    {
                        if (CanvasObj.ContainsKey(c.Name))
                        {
                            if (temp[CanvasObj[c.Name].Id].Value <= 7000 || temp[CanvasObj[c.Name].Id].Value >= 15000)
                            {
                                ((Border)(c).Children[0]).BorderBrush = Brushes.Red;
                            }
                        }
                    }
                });
                CheckValue(c);
            });
        }
        public void OnDragLeave(Canvas c)
        {//kad se skloni slika sa kanvasa koja se prevlaci
            if (((TextBlock)(c).Children[1]).Text == "!")
            {
                ((TextBlock)(c).Children[1]).Text = "";
                ((TextBlock)(c).Children[1]).Background = Brushes.Transparent;
            }
        }
        public void OnDragOver(Canvas c)
        {//prilikom prelaska preko zauzete povrsine
            if (c.Resources["taken"] != null)
            {
                ((TextBlock)(c).Children[1]).Text = "!";
                ((TextBlock)(c).Children[1]).Background = Brushes.Salmon;
            }
        }
        public void OnFree(Canvas c)
        {
            try
            {
                if (c.Resources["taken"] != null)
                {
                    //ako je
                    c.Background = Brushes.White;
                    foreach (Saobracaj s in SaobracajViewModel.Saobracaji)
                    {
                        if (!Items.Contains(s) && CanvasObj[c.Name].Id == s.Id)
                            Items.Add(new Saobracaj(s));
                    }
                    c.Resources.Remove("taken");
                    CanvasObj.Remove(c.Name);
                }
            }
            catch (Exception) { }

        }
        public void OnDrop(Canvas c)
        {
            if (((TextBlock)(c).Children[1]).Text == "!")
            {
                ((TextBlock)(c).Children[1]).Text = "";
                ((TextBlock)(c).Children[1]).Foreground = Brushes.White;
            }
            if (draggItm != null)
            {
                if (c.Resources["taken"] == null)
                {
                    BitmapImage logo = new BitmapImage();
                    logo.BeginInit();
                    string temp = draggItm.Type.Name.ToString() + ".jpg";
                    logo.UriSource = new Uri("pack://application:,,,/pictures/" + temp, UriKind.Absolute);

                    logo.EndInit();
                    c.Background = new ImageBrush(logo);
                    CanvasObj[c.Name] = draggItm;
                    c.Resources.Add("taken", true);
                    Items.Remove(Items.Single(x => x.Id == draggItm.Id));
                    SelectedIndex = 0;
                    CheckValue(c);
                }
                dragging = false;
            }
        }
        public void OnMLBU(ListView lw)
        {//podizanje klika, sve stavi na null/false ->ponisti sve
            draggItm = null;
            lw.SelectedItem = null;
            dragging = false;
        }
        public void SelectionChange(Saobracaj s)
        {
            if (!dragging)
            {//izvrsi promenu ako nema pomeranja
                dragging = true;
                draggItm = new Saobracaj(s);
                DragDrop.DoDragDrop(lv, draggItm, DragDropEffects.Move);
            }
        }
    }
}
