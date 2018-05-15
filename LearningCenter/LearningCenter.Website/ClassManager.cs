using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LearningCenter.Website.Repository;

namespace LearningCenter.Website.Business
{
    public interface IClassManager
    {
        ClassModel[] GetClasses { get; }
        ClassModel   GetClass   (int classId);
    }

    public class ClassModel
    {
        public int     Id          { get; set; }
        public string  Name        { get; set; }
        public string  Description { get; set; }
        public decimal Price       { get; set; }

        public ClassModel(int id, string name, string description, decimal price)
        {
            Id          = id;
            Name        = name;
            Description = description;
            Price       = price;
        }
    }

    public class ClassManager : IClassManager
    {
        private readonly IClassRepository classRepository;

        public ClassManager(IClassRepository classRepository)
        {
            this.classRepository = classRepository;
        }

        public ClassModel[] GetClasses
        {
            get
            {
                return classRepository.GetClasses
                    .Select(t => new ClassModel (t.Id, t.Name, t.Description, t.Price ))
                    .ToArray();
            }
        }

        public ClassModel GetClass(int classId)
        {
            var classModel = classRepository.GetClass(classId);
            return new ClassModel(classModel.Id, classModel.Name, classModel.Description, classModel.Price);
        }
    }
}
