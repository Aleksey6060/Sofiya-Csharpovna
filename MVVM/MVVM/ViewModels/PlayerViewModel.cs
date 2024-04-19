using MVVM.Models;
using MVVM.Models.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace MVVM.ViewModels.View
{
    public class PlayerViewModel : INotifyPropertyChanged
    {
        private MediaPlayer mediaPlayer = new MediaPlayer();
        private AudioModel currentAudio;

        
        public ICommand PlayCommand { get; private set; }
        public ICommand PauseCommand { get; private set; }
        public ICommand StopCommand { get; private set; }

        public PlayerViewModel()
        {
           
            PlayCommand = new RelayCommand(Play);
            PauseCommand = new RelayCommand(Pause);
            StopCommand = new RelayCommand(Stop);
        }

      
        public AudioModel CurrentAudio
        {
            get => currentAudio;
            set
            {
                currentAudio = value;
                OnPropertyChanged();
            }
        }

      
        public void Play()
        {
            if (CurrentAudio != null)
            {
                mediaPlayer.Open(new Uri(CurrentAudio.FilePath));
                mediaPlayer.Play();
            }
        }

        public void Pause()
        {
            mediaPlayer.Pause();
        }

        public void Stop()
        {
            mediaPlayer.Stop();
        }

     
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}
