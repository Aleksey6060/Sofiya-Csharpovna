using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using System.Windows.Media.Imaging;

namespace KALENDAR
{

    public partial class ActivitySelectionWindow : Window
    {
        private DateTime selectedDate;
        private Dictionary<DateTime, List<string>> activities;
        private const string FileName = "activities.json";

        public ActivitySelectionWindow(DateTime selectedDate)
        {
            InitializeComponent();
            this.selectedDate = selectedDate;
            activities = LoadActivitiesFromFile();

            List<string> selectedActivities = GetSelectedActivitiesForDate(selectedDate);
            LoadActivities(selectedActivities);
        }

        private void LoadActivities(List<string> selectedActivities)
        {
            stackPanelActivities.Children.Clear();

            List<string> activityList = new List<string> { "CS2", "Dota2", "Palworld", "Minecraft", "Весёлая  ферма", "DayZ" };
            List<string> imagePaths = new List<string> { "cs2.png", "dota2.png", "palworld.png", "minecraft.png", "farm.png", "dayz.png" };

            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string imagesFolderPath = Path.Combine(desktopPath, "Images");

            foreach (var activity in activityList)
            {
                int index = activityList.IndexOf(activity);
                string imageFileName = imagePaths[index];
                string imagePath = Path.Combine(imagesFolderPath, imageFileName);

                StackPanel gamePanel = new StackPanel { Orientation = Orientation.Horizontal };

                Image image = new Image
                {
                    Source = new BitmapImage(new Uri(imagePath)),
                    Width = 50,
                    Height = 50
                };

                CheckBox checkBox = new CheckBox
                {
                    Content = activity,
                    IsChecked = selectedActivities.Contains(activity)
                };

                gamePanel.Children.Add(image);
                gamePanel.Children.Add(checkBox);

                stackPanelActivities.Children.Add(gamePanel);
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            List<string> selectedActivities = new List<string>();
            foreach (StackPanel gamePanel in stackPanelActivities.Children)
            {
                CheckBox checkBox = (CheckBox)gamePanel.Children[1];
                if (checkBox.IsChecked == true)
                {
                    selectedActivities.Add(checkBox.Content.ToString());
                }
            }
            activities[selectedDate] = selectedActivities;
            SaveActivitiesToFile();
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private Dictionary<DateTime, List<string>> LoadActivitiesFromFile()
        {
            if (File.Exists(FileName))
            {
                string json = File.ReadAllText(FileName);
                return JsonConvert.DeserializeObject<Dictionary<DateTime, List<string>>>(json) ?? new Dictionary<DateTime, List<string>>();
            }
            return new Dictionary<DateTime, List<string>>();
        }

        private void SaveActivitiesToFile()
        {
            string json = JsonConvert.SerializeObject(activities, Formatting.Indented);
            File.WriteAllText(FileName, json);
        }

        private List<string> GetSelectedActivitiesForDate(DateTime date)
        {
            if (activities.TryGetValue(date, out List<string> selectedActivities))
            {
                return selectedActivities;
            }
            return new List<string>();
        }
    }
}