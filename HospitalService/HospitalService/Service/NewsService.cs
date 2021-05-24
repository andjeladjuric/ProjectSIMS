using HospitalService.Repositories;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalService.Service
{
    public class NewsService
    {
        private NewsRepository repository;
        public NewsService()
        {
            repository = new NewsRepository();
        }

        public int GetNumberOfPublishedNews()
        {
            int numberOfPublishedNews = 0;
            List<News> allNews = GetAll();
            foreach (News news in allNews)
            {
                numberOfPublishedNews++;
            }
            return numberOfPublishedNews;
        }

        public List<News> GetForRole(Role role)
        {
            List<News> foundNews = new List<News>();
            List<News> allNews = GetAll();
            foreach (News news in allNews)
            {
                if (news.Roles.Equals(role) || news.Roles.Equals(Role.svi))
                    foundNews.Add(news);
            }
            return foundNews;
        }

        public List<News> GetAll() => repository.GetAll();
        public void Add(News news) => repository.Save(news);
        public void Delete(String title) => repository.Delete(title);
        public void Update(News newOne, News oldOne) => repository.Update(newOne, oldOne);
    }
}
