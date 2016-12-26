using System;

namespace ASP.NET.TwoWayModel.Entities
{
    public class ClassPropertyEntity
    {
        public ClassPropertyEntity(ClassPropertyEntity entity)
            : this(entity.ClassName, entity.ClassProperty, entity.ClassPropertyParams)
        {
        }

        public ClassPropertyEntity(String className, String classProperty, String classPropertyParams)
        {
            ClassName = className;
            ClassProperty = classProperty;
            ClassPropertyParams = classPropertyParams;
        }

        public String ClassName { get; private set; }
        public String ClassProperty { get; private set; }
        public String ClassPropertyParams { get; private set; }
    }
}