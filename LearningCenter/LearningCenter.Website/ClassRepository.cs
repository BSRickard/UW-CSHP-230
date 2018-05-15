using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearningCenter.Website.Repository
{
    public interface IClassRepository
    {
        ClassModel[] Classes { get; }
        ClassModel   Class(int classID);
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
        public ClassModel[] Classes
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

        public ClassModel Class(int classId)
        {
            var aClass = DatabaseAccessor.Instance.Classes
                .Where(t => t.ClassId == classId)
                .Select(t => new ClassModel { Id = t.ClassId, Name = t.ClassName, Description = t.ClassDescription, Price = t.ClassPrice })
                .First();
            return aClass;
        }
    }
}