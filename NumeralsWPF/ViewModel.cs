using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using NumeralSystems;

namespace NumeralsWPF
{
    class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ConversionInfo conversion;
        IList<int> bases = new List<int>()
        { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, };

        public ViewModel()
        {
            conversion = new ConversionInfo();
            conversion.BaseFrom = 10;
            conversion.BaseTo = 10;
        }

        public void OnPropertyChanged(string property)
        {
            if (property != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public string NumberFrom
        {
            get { return conversion.NumberFrom; }
            set
            {
                if (conversion.NumberFrom != value)
                {
                    conversion.NumberFrom = value;
                    OnPropertyChanged("NumberFrom");
                }
            }
        }
        public string NumberTo
        {
            get { return conversion.NumberTo; }
            set
            {
                if (conversion.NumberTo != value)
                {
                    conversion.NumberTo = value;
                    OnPropertyChanged("NumberTo");
                }
            }
        }

        public IList<int> Bases
        {
            get { return bases; }
            set { bases = value; }
        }
        public int BaseFrom
        {
            get { return conversion.BaseFrom; }
            set
            {
                if (conversion.BaseFrom != value)
                {
                    conversion.BaseFrom = value;
                    OnPropertyChanged("BaseFrom");
                }
            }
        }
        public int BaseTo
        {
            get { return conversion.BaseTo; }
            set
            {
                if (conversion.BaseTo != value)
                {
                    conversion.BaseTo = value;
                    OnPropertyChanged("BaseTo");
                }
            }
        }

        private Relay clear;
        public Relay Clear
        {
            get
            {
                return clear = new Relay(obj =>
                {
                    NumberFrom = String.Empty;
                    NumberTo = String.Empty;
                    BaseTo = 10;
                    BaseFrom = 10;
                }, o => !string.IsNullOrEmpty(NumberFrom) || !string.IsNullOrEmpty(NumberTo) || BaseTo != 10 || BaseFrom != 10);
            }
        }

        private Relay calculate;
        public Relay Calculate
        {
            get
            {
                return calculate = new Relay(obj =>
                {
                    try
                    {
                        Number num = new Number(NumberFrom, BaseFrom);
                        NumberTo = num.ConvertToBaseN(BaseTo);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }, o => !string.IsNullOrEmpty(NumberFrom));
            }
        }

    }
}
