using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearningCenter.Website.Repository
{
    public interface IClassRepository
    {
        ClassModel[] GetClasses { get; }
        ClassModel   GetClass   (int classID);
    }

    public class ClassModel
    {
        public int     Id          { get; set; }
        public string  Name        { get; set; }
        public string  Description { get; set; }
        public decimal Price       { get; set; }
    }

    public class ClassRepository : IClassRepository
    {
        public ClassModel[] GetClasses
        {
            get
            {
                return DatabaseAccessor.Instance.Classes
                    .Select(t => new ClassModel
                    {
                        Id          = t.ClassId,
                        Name        = t.ClassName,
                        Description = t.ClassDescription,
                        Price       = t.ClassPrice
                    })
                    .ToArray();
            }
        }

        public ClassModel GetClass(int classId)
        {
            var aClass = DatabaseAccessor.Instance.Classes
                .Where(t => t.ClassId == classId)
                .Select(t => new ClassModel { Id = t.ClassId, Name = t.ClassName, Description = t.ClassDescription, Price = t.ClassPrice })
                .First();
            return aClass;
        }
    }
}