﻿using NewGameStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewGameStore.DAL.Repositories
{
    interface IPublisherRepository : IDisposable
    {
        IEnumerable<Publisher> GetPublishers();

        Publisher GetPubByID(int? id);

        void InsertPublisher(Publisher Publisher);

        void DeletePublisher(int PublisherID);

        void UpdatePublisher(Publisher Publisher);

        bool publisherExists(Publisher Publisher);

        void Save();
    }
}