using Lab1.Models;
using Lab1.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lab1.ViewModel
{
    class BirthDateViewModel : INotifyPropertyChanged
    {

        private User _user = new User();
        private RelayCommand<object> _calculateCommand;
        public string _age;
        public string _westernZodiacSign;
        public string _chineseZodiacSign;


        public string Age
        {
            get 
            {
                return _age;
            }
            set 
            {
                _age = value;
                OnPropertyChanged(nameof(Age));
            }
        }

        public string WesternZodiacSign 
        {
            get 
            {
                return _westernZodiacSign;
            }
            set
            {
                _westernZodiacSign = value;
                OnPropertyChanged(nameof(WesternZodiacSign));
            }
        }

        public string ChineseZodiacSign
        {
            get 
            {
                return _chineseZodiacSign; 
            }
            set
            {
                _chineseZodiacSign = value;
                OnPropertyChanged(nameof(ChineseZodiacSign));
            } 
        }

        public DateTime BirthDate
        {

            get
            {
                return _user.BirthDate;
            }
           set
            {
                _user.BirthDate = value;
            }
        }

        public RelayCommand<object> CalculateCommand
        {
            get
            {
                return _calculateCommand ??= new RelayCommand<object>(_ => Calculate(), CanExecute);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void Calculate()
        {
            int age = CalculateAge();
            if (age < 0 || age > 135)
            {
                Age = "";
                WesternZodiacSign = "";
                ChineseZodiacSign = "";
                MessageBox.Show("Wrong date!");
                return;
            }
            Age = age.ToString();
            WesternZodiacSign = GetWesternZodiacSign(_user.BirthDate);
            ChineseZodiacSign = GetChineseZodiacSign(_user.BirthDate);
            if (_user.BirthDate.Month == DateTime.Today.Month && _user.BirthDate.Day == DateTime.Today.Day)
                MessageBox.Show("Happy Birthday!");
        }

        private bool CanExecute(object parameter)
        {
            return !BirthDate.Equals(DateTime.MinValue);
        }

        private int CalculateAge()
        {
            DateTime today = DateTime.Today;
            int age = today.Year - _user.BirthDate.Year;
            if(_user.BirthDate.Date > today.AddYears(-age))
                age--;
            return age;
        }

        private string GetWesternZodiacSign(DateTime birthDate)
        {
            int month = birthDate.Month;
            int day = birthDate.Day;

            switch (month)
            {
                case 1:
                    return (day <= 20) ? "Capricorn" : "Aquarius";
                case 2:
                    return (day <= 19) ? "Aquarius" : "Pisces";
                case 3:
                    return (day <= 20) ? "Pisces" : "Aries";
                case 4:
                    return (day <= 20) ? "Aries" : "Taurus";
                case 5:
                    return (day <= 21) ? "Taurus" : "Gemini";
                case 6:
                    return (day <= 21) ? "Gemini" : "Cancer";
                case 7:
                    return (day <= 22) ? "Cancer" : "Leo";
                case 8:
                    return (day <= 23) ? "Leo" : "Virgo";
                case 9:
                    return (day <= 23) ? "Virgo" : "Libra";
                case 10:
                    return (day <= 23) ? "Libra" : "Scorpio";
                case 11:
                    return (day <= 22) ? "Scorpio" : "Sagittarius";
                case 12:
                    return (day <= 21) ? "Sagittarius" : "Capricorn";
                default:
                    return "None";
            }
        }

        private string GetChineseZodiacSign(DateTime birthDate)
        {
            int year = birthDate.Year;
            int chineseSign = year % 12;
            return ((ChineseZodiac)chineseSign).ToString();
        }


        enum ChineseZodiac
        {
            Monkey,
            Rooster,
            Dog,
            Pig,
            Rat,
            Ox,
            Tiger,
            Rabbit,
            Dragon,
            Snake,
            Horse,
            Sheep
        }

    }
}
