﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TwinCAT.Ads;
using System.Threading;
using System.Windows.Threading;

namespace WpfBeckhoff2023_3_11
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public TcAdsClient adsClient;
        public int boolHandle=0;
        public DispatcherTimer dispatcherTimer = new DispatcherTimer();
        
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            
            
            
        }

    

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            adsClient = new TcAdsClient();
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Start();
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            adsClient.Connect(851);
            boolHandle= adsClient.CreateVariableHandle(".bBool");
            
            
        }

        private void DispatcherTimer_Tick(object? sender, EventArgs e)
        {
            try
            {
                label.Text = adsClient.ReadAny(boolHandle, typeof(Boolean)).ToString();
                if (label.Text.ToUpper()=="TRUE")
                {
                    adsClient.WriteAny(boolHandle, false);
                }
                else
                {
                    adsClient.WriteAny(boolHandle, true) ;
                }
                
            }
            catch
            {
                MessageBox.Show("读取失败");
            }
        }
    }
}
