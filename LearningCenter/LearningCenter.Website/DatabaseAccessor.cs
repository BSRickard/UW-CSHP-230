using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearningCenter.Website.Repository
{
    public class DatabaseAccessor
    {
        private static readonly MiniCstructorEntities entities;

        static DatabaseAccessor()
        {
            entities = new MiniCstructorEntities();
            entities.Database.Connection.Open();
        }

        public static MiniCstructorEntities Instance
        {
            get
            {
                return entities;
            }
        }
    }
}