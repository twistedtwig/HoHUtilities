using System;
using NUnit.Framework;

namespace Extensions.Test
{
    #region test classes used for testing only

    public class MyTestClass
    {
        public int MyInt { get; set; }
    }

    public class MyTestClassCompareable : IComparable<MyTestClassCompareable>
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
        public int CompareTo(MyTestClassCompareable other)
        {
            return MyInt.CompareTo(other.MyInt);
        }

        #endregion
    }

    #endregion


    [TestFixture]
    public class ObjectExtensionsTest
    {

        [Test]
        public void TestThatTwoStringsAreComparedCorrectly()
        {
            String s1 = "Hello";
            String s2 = "Hello";

            Assert.IsTrue(s1.CompareIfSame(s2));

            String s3 = "Hello";
            String s4 = "hello";

            Assert.IsFalse(s3.CompareIfSame(s4));
            
            String s5 = "Hello";
            String s6 = "Hello  ";

            Assert.IsFalse(s5.CompareIfSame(s6));
            
            String s7 = "Hello";
            String s8 = "ffdsafasd";

            Assert.IsFalse(s7.CompareIfSame(s8));
        }

        [Test]
        public void TestThatTwoIntsAreComparedCorrectly()
        {
            int i1 = 123;
            int i2 = 123;

            Assert.IsTrue(i1.CompareIfSame(i2));

            int i3 = 1123;
            int i4 = 123;

            Assert.IsFalse(i3.CompareIfSame(i4));

        }

        [Test]
        public void TestThatTwoCustomObjectsAreComparedCorrectly()
        {
            MyTestClass mc1 = new MyTestClass {MyInt = 5};
            MyTestClass mc2 = new MyTestClass { MyInt = 5 }; 

            Assert.IsFalse(mc1.CompareIfSame(mc2));

            MyTestClassCompareable mc3 = new MyTestClassCompareable { MyInt = 5 };
            MyTestClassCompareable mc4 = new MyTestClassCompareable { MyInt = 5 };

            Assert.IsTrue(mc3.CompareIfSame(mc4));
        }

        [Test]
        public void TestThatNullsAreComparedCorrectly()
        {
            MyTestClass mc1 = new MyTestClass { MyInt = 5 };
            MyTestClass mc2 = new MyTestClass { MyInt = 5 }; 

            Assert.IsFalse(mc1.CompareIfSame(null));

            mc1 = null;
            Assert.IsFalse(mc1.CompareIfSame(mc2));

            mc2 = null;
            Assert.IsTrue(mc1.CompareIfSame(mc2));
        }
    }
}
