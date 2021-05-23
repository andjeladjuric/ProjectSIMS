using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Storage
{
    
    public class NewsStorage
    {
        private String FileLocation = @"..\..\..\Data\news.json";
        private List<News> allNews;
        private static NewsStorage instance = null;

        public static NewsStorage getInstance()
        {
            if (instance == null)
            {
                instance = new NewsStorage();


            }
            return instance;
        }
        public NewsStorage()
        {
            allNews = new List<News>();

           /*News n1 = new News
            {

                Title = "Uksršnji praznici",
                PublishingDate = new DateTime(2021, 04, 28),
                Content = "Zbog praznika klinika neće raditi od prvog do trećeg maja.",
                Roles = Role.svi
                
            };
            allNews.Add(n1);
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(allNews));*/
            allNews = JsonConvert.DeserializeObject<List<News>>(File.ReadAllText(FileLocation));
        }
        

        public List<News> getAll()
        {
            return allNews;
        }

        public void Save(News newNews)
        {
            allNews.Add(newNews);
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(allNews));
        }

        public void Delete(String title)
        {
            News n = allNews.Find(x => x.Title == title);
            allNews.Remove(n);
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(allNews));

        }

        public void Update(News newOne, News oldOne)
        {
            for (int i = 0; i < allNews.Count; i++)
            {
                if (allNews[i].Title == oldOne.Title)
                {
                    allNews[i] = newOne;
                    File.WriteAllText(FileLocation, JsonConvert.SerializeObject(allNews));
                }
            }
        }

       

        public int getNumberOfPublishedNews()
        {
            int numberOfPublishedNews = 0;

            foreach (News news in allNews)
            {
                numberOfPublishedNews++;
            }
            return numberOfPublishedNews;
        }

        public List<News> GetForRole(Role role)
        {
            List<News> foundNews = new List<News>();
            foreach (News news in allNews)
            {
                if (news.Roles.Equals(role) || news.Roles.Equals(Role.svi))
                    foundNews.Add(news);
            }
            return foundNews;
        }
        
    }
}
