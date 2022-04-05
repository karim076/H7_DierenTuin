using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Pickers;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Dierentuin
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            var bounds = ApplicationView.GetForCurrentView().VisibleBounds;
            var reso = Convert.ToString(bounds);
            res.Text = reso;
            this.SizeChanged += MainPage_SizeChanged;
        }

        private void MainPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var bounds = ApplicationView.GetForCurrentView().VisibleBounds;
            var reso = bounds.Width;
            if (reso < 600)
            {
                //resmin1.Fontsize = 10;
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var picker = new FileOpenPicker();
            picker.SuggestedStartLocation = PickerLocationId.Downloads;
            picker.FileTypeFilter.Add(".csv");

            //TODO: voeg hier je eigen code toe, zoals uit H5, paragraaf 7
            var file = await picker.PickSingleFileAsync();
            if (file == null)
            {
                tbFileStatus.Text = "Geen geldig bestand gekozen";
            }
            else
            {
                tbFileStatus.Text = file.Path;
            }

            using (var fileAcces = await file.OpenReadAsync())
            {
                using (var stream = fileAcces.AsStreamForRead())
                {
                    using (var reader = new StreamReader(stream))
                    {
                        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                        {
                            var records = csv.GetRecords<Animal>();
                            lvAnimals.ItemsSource = records;
                        }
                    }
                }
            }
        }
    }
}

