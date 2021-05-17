using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HospitalService.Repositories
{
    class NewsRepository
    {
        private String FileLocation = @"..\..\..\Data\news.json";
        private List<News> allNews;
        private static NewsRepository instance = null;

        public static NewsRepository getInstance()
        {
            if (instance == null)
            {
                instance = new NewsRepository();


            }
            return instance;
        }
        public NewsRepository()
        {
            allNews = new List<News>();
            allNews = JsonConvert.DeserializeObject<List<News>>(File.ReadAllText(FileLocation));
        }

        public void SerializeNews()
        {
            File.WriteAllText(FileLocation, JsonConvert.SerializeObject(allNews));
        }

        public List<News> getAll()
        {
            return allNews;
        }

        public void Save(News newNews)
        {
            allNews.Add(newNews);
            SerializeNews();
        }

        public void Delete(String title)
        {
            News n = allNews.Find(x => x.Title == title);
            allNews.Remove(n);
            SerializeNews();

        }

        public void Update(News newOne, News oldOne)
        {
            for (int i = 0; i < allNews.Count; i++)
            {
                if (allNews[i].Title == oldOne.Title)
                {
                    allNews[i] = newOne;
                    SerializeNews();
                    break;
                }
            }
        }

    }
}
