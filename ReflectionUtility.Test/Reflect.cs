using System;
using System.Reflection;
using NUnit.Framework;

namespace ReflectionUtility.Test
{
    #region Classes used to help test only

    internal class MyTestClassCompareable : IComparable
    {
        public int MyInt { get; set; }

        #region Implementation of IComparable<in MyTestClassCompareable>

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other"/> parameter.Zero This object is equal to <paramref name="other"/>. Greater than zero This object is greater than <paramref name="other"/>. 
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public int CompareTo(object other)
        {
            return MyInt.CompareTo(((MyTestClassCompareable) other).MyInt);
        }

        #endregion
    }

    internal class MyTestClassGenericCompareable : IComparable<MyTestClassGenericCompareable>
    {
        public int MyInt { get; set; }

        #region Implementation of IComparable<in MyTestClassCompareable>

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other"/> parameter.Zero This object is equal to <paramref name="other"/>. Greater than zero This object is greater than <paramref name="other"/>. 
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public int CompareTo(MyTestClassGenericCompareable other)
        {
            return MyInt.CompareTo(other.MyInt);
        }

        #endregion
    }

    #endregion


    [TestFixture]
    public class Reflect
    {
        [Test]
        public void TestCanExecuteGenericMethodOnObject()
        {
            ReflectTestClass myTestClass = new ReflectTestClass();

            //test can do method with void return type
            myTestClass.HasMethodExecuted = false;
            Assert.IsFalse(myTestClass.HasMethodExecuted);
            object returnValue = ReflectionUtility.Reflect.InvokeGenericMethodOnObject(myTestClass, "ExecuteMe", typeof(ReflectTestClass), new object[] { new ReflectTestClass("Hello") } );
            Assert.IsNull(returnValue);
            Assert.IsTrue(myTestClass.HasMethodExecuted);

            //test can do method with return type T and no params
            myTestClass.HasMethodExecuted = false;
            Assert.IsFalse(myTestClass.HasMethodExecuted);
            returnValue = ReflectionUtility.Reflect.InvokeGenericMethodOnObject(myTestClass, "ExecuteMe", typeof(ReflectTestClass), null);
            Assert.IsNotNull(returnValue);
            Assert.IsInstanceOf<ReflectTestClass>(returnValue);
            Assert.IsTrue(myTestClass.HasMethodExecuted);

            //test can do method with return type T and params
            myTestClass.HasMethodExecuted = false;
            Assert.IsFalse(myTestClass.HasMethodExecuted);
            returnValue = ReflectionUtility.Reflect.InvokeGenericMethodOnObject(myTestClass, "ExecuteMe", typeof(ReflectTestClass), new object[] { new ReflectTestClass("Hello"), new ReflectTestClass("goodBye") });
            Assert.IsNotNull(returnValue);
            Assert.IsInstanceOf<ReflectTestClass>(returnValue);
            Assert.IsTrue(myTestClass.HasMethodExecuted);
        }

        [Test]
        public void TestCanGetPublicFieldInfo()
        {
            ReflectTestClass myTestClass = new ReflectTestClass();
            myTestClass.NameFieldPublic = "Bob";

            Assert.AreEqual("Bob", myTestClass.NameFieldPublic);
            FieldInfo fieldInfo = ReflectionUtility.Reflect.GetField(myTestClass, "NameFieldPublic");

            Assert.IsNotNull(fieldInfo);
            Assert.AreEqual("Bob", fieldInfo.GetValue(myTestClass).ToString());
        }

        [Test]
        public void TestCanGetPrivateFieldInfo()
        {
            ReflectTestClass myTestClass = new ReflectTestClass("Bob");

            FieldInfo fieldInfo = ReflectionUtility.Reflect.GetField(myTestClass, "NameFieldPrivate");

            Assert.IsNotNull(fieldInfo);
            Assert.AreEqual("Bob", fieldInfo.GetValue(myTestClass).ToString());
        }

        [Test]
        public void TestCanGetPublicPropertyInfo()
        {
            ReflectTestClass myTestClass = new ReflectTestClass();
            myTestClass.NamePropertyPublic = "Bob";

            Assert.AreEqual("Bob", myTestClass.NamePropertyPublic);
            PropertyInfo propertyInfo = ReflectionUtility.Reflect.GetProperty(myTestClass, "NamePropertyPublic");

            Assert.IsNotNull(propertyInfo);
            object value = propertyInfo.GetValue(myTestClass, null);
            Assert.IsNotNull(value);
            Assert.AreEqual("Bob", value.ToString());
        }

        [Test]
        public void TestCanGetPrivatePropertyInfo()
        {
            ReflectTestClass myTestClass = new ReflectTestClass("Bob");

            PropertyInfo propertyInfo = ReflectionUtility.Reflect.GetProperty(myTestClass, "NamePropertyPrivate");

            Assert.IsNotNull(propertyInfo);
            object value = propertyInfo.GetValue(myTestClass, null);
            Assert.IsNotNull(value);
            Assert.AreEqual("Bob", value.ToString());            
        }

        [Test]
        public void TestCanGetPrivateFieldValue()
        {
            ReflectTestClass myTestClass = new ReflectTestClass("Bob");

            string fieldValue = ReflectionUtility.Reflect.GetFieldValue<String>(myTestClass, "NameFieldPrivate");
            Assert.AreEqual("Bob", fieldValue);
        }

        [Test]
        public void TestCanGetPublicFieldValue()
        {
            ReflectTestClass myTestClass = new ReflectTestClass();
            myTestClass.NameFieldPublic = "Bob";

            Assert.IsNotNullOrEmpty(myTestClass.NameFieldPublic);
            string fieldValue = ReflectionUtility.Reflect.GetFieldValue<String>(myTestClass, "NameFieldPublic");
            Assert.AreEqual("Bob", fieldValue);
        }

        [Test]
        public void TestCanGetPrivatePropertyValue()
        {
            ReflectTestClass myTestClass = new ReflectTestClass("Bob");

            string propertyValue = ReflectionUtility.Reflect.GetPropertyValue<String>(myTestClass, "NamePropertyPrivate");
            Assert.AreEqual("Bob", propertyValue);
        }

        [Test]
        public void TestCanGetPublicPropertyValue()
        {
            ReflectTestClass myTestClass = new ReflectTestClass();
            myTestClass.NamePropertyPublic = "Bob";

            Assert.IsNotNullOrEmpty(myTestClass.NamePropertyPublic);
            string propertyValue = ReflectionUtility.Reflect.GetPropertyValue<String>(myTestClass, "NamePropertyPublic");
            Assert.AreEqual("Bob", propertyValue);
        }

        [Test]
        public void TestCanGetPropertyOrFieldValue()
        {
            ReflectTestClass myTestClass = new ReflectTestClass("Bob");

            string fieldValue = ReflectionUtility.Reflect.GetValue<String>(myTestClass, "NameFieldPrivate");
            Assert.IsNotNullOrEmpty(fieldValue);
            Assert.AreEqual("Bob", fieldValue);

            string propertyValue = ReflectionUtility.Reflect.GetValue<String>(myTestClass, "NamePropertyPrivate");
            Assert.IsNotNullOrEmpty(propertyValue);
            Assert.AreEqual("Bob", propertyValue);
        }

        [Test]
        public void TestCanGetTypeOfValueForGivenObject()
        {
            ReflectTestClass myTestClass = new ReflectTestClass();

            Type publicPropertyType = ReflectionUtility.Reflect.GetValueType(myTestClass, "NamePropertyPublic");
            Assert.IsTrue(publicPropertyType == typeof(String));

            Type publicFieldType = ReflectionUtility.Reflect.GetValueType(myTestClass, "NameFieldPublic");
            Assert.IsTrue(publicFieldType == typeof(String));
        }

        [Test]
        public void TestCanSetPublicFieldValue()
        {
            ReflectTestClass myTestClass = new ReflectTestClass();

            Assert.IsNullOrEmpty(myTestClass.NameFieldPublic);
            ReflectionUtility.Reflect.AssignFieldValue(myTestClass, "NameFieldPublic", "Bob");
            Assert.IsNotNullOrEmpty(myTestClass.NameFieldPublic);
            Assert.AreEqual("Bob", myTestClass.NameFieldPublic);
        }
       
        [Test]
        public void TestCanSetPublicPropertyValue()
        {
            ReflectTestClass myTestClass = new ReflectTestClass();

            Assert.IsNullOrEmpty(myTestClass.NamePropertyPublic);
            ReflectionUtility.Reflect.AssignPropertyValue(myTestClass, "NamePropertyPublic", "Bob");
            Assert.IsNotNullOrEmpty(myTestClass.NamePropertyPublic);
            Assert.AreEqual("Bob", myTestClass.NamePropertyPublic);
        }

        [Test]
        public void TestCanSetPrivateNameFieldValue()
        {
            ReflectTestClass myTestClass = new ReflectTestClass();

            Assert.IsNullOrEmpty(ReflectionUtility.Reflect.GetFieldValue<String>(myTestClass, "NameFieldPrivate"));
            ReflectionUtility.Reflect.AssignFieldValue(myTestClass, "NameFieldPrivate", "Bob");
            Assert.AreEqual("Bob", ReflectionUtility.Reflect.GetFieldValue<String>(myTestClass, "NameFieldPrivate"));
        }

        [Test]
        public void TestCanSetPrivatePropertyValue()
        {
            ReflectTestClass myTestClass = new ReflectTestClass();

            Assert.IsNullOrEmpty(ReflectionUtility.Reflect.GetPropertyValue<String>(myTestClass, "NamePropertyPrivate"));
            ReflectionUtility.Reflect.AssignPropertyValue(myTestClass, "NamePropertyPrivate", "Bob");
            Assert.AreEqual("Bob", ReflectionUtility.Reflect.GetPropertyValue<String>(myTestClass, "NamePropertyPrivate"));
        }

        [Test]
        public void TestCanDetectWhenATypeInheritsFromGenericInterface()
        {
            MyTestClassCompareable mc1 = new MyTestClassCompareable();
            MyTestClassGenericCompareable mc2 = new MyTestClassGenericCompareable();

            Assert.IsFalse(ReflectionUtility.Reflect.DoesObjectInheritFromGenericInterface(mc1, typeof(IComparable<>)));
            Assert.IsTrue(ReflectionUtility.Reflect.DoesObjectInheritFromGenericInterface(mc2, typeof(IComparable<>)));
        }
    }
}
