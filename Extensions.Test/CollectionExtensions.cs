using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Extensions.Test
{
    [TestFixture]
    public class CollectionExtensions
    {
        private IList<string> SecondtList;
        private IList<string> FirstList;

        [SetUp]
        public void Init()
        {
            
            FirstList = new List<string>()
                            {
                                "abc",
                                "acd",
                                "",
                                "123",
                                "abc",
                                "qwerty"
                            };


            SecondtList = new List<string>()
                              {
                                  "abc",
                                  "def",
                                  "000",
                                  "D/80",
                                  "176423$3",
                                  "123"
                              };
        }

        [Test]
        public void AddRangeAllowDuplicateValues()
        {
            FirstList.AddRange(SecondtList, false);

            Assert.AreEqual(FirstList.Count, 12);
        }

        [Test]
        public void AddRangeDoNotAllowDuplicateValues()
        {
            FirstList.AddRange(SecondtList, true);

            Assert.AreEqual(FirstList.Count, 10);
        }

        [Test]
        public void AddItemToListIfNotInSecondListShouldNotAddAsInSecondList()
        {
            int firstListCount = FirstList.Count;
            FirstList.AddIfNotInSecondList("def", SecondtList);
            Assert.AreEqual(firstListCount, FirstList.Count);
        }

        [Test]
        public void AddItemToListIfNotInSecondListShouldBeAddedAsNotInSecondList()
        {
            int firstListCount = FirstList.Count;
            FirstList.AddIfNotInSecondList("123456789", SecondtList);
            Assert.AreEqual(firstListCount+1, FirstList.Count);
        }

        [Test]
        public void RemoveAllIntancesOfItemFromCollection()
        {
            int amountToBeRemove = 2;
            int beforeRemoveCount = FirstList.Count;
            int removeCount = FirstList.RemoveAll("abc");
            Assert.AreEqual(amountToBeRemove, removeCount);
            Assert.AreEqual(beforeRemoveCount-amountToBeRemove, FirstList.Count);
        }

        [Test]
        public void RemoveAllItemsFromFirstListThatAreInSecondList()
        {
            int removedCount = FirstList.RemoveAll(SecondtList);
            Assert.AreEqual(removedCount, 3);
        }


        [Test]
        public void AddUniqueItemToColectionThatIsNotAlreadyInCollection()
        {
            int countBeforeAdd = FirstList.Count;
            string hello = "hello";
            FirstList.Add(hello,true);
            
            Assert.AreEqual(countBeforeAdd + 1, FirstList.Count);
            Assert.Contains(hello, FirstList.ToArray());
        }

        [Test]
        public void AddUnqiueItemToCollectionThatIsAlreadyInCollection()
        {
            string item = "acd";

            int countBeforeAdd = FirstList.Count;
            int countItemsBeforeAdd = FirstList.Count(s => s.Equals(item));
            
            FirstList.Add(item, true);

            Assert.AreEqual(countBeforeAdd, FirstList.Count);

            int countItemsAfterAdd = FirstList.Count(s => s.Equals(item));
            Assert.AreEqual(countItemsBeforeAdd, countItemsAfterAdd);
        }

        [Test]
        public void AddNonUniqueItemToColectionThatIsNotAlreadyInCollection()
        {
            int countBeforeAdd = FirstList.Count;
            string hello = "hello";
            FirstList.Add(hello, false);

            Assert.AreEqual(countBeforeAdd + 1, FirstList.Count);
            Assert.Contains(hello, FirstList.ToArray());
        }

        [Test]
        public void AddNonUnqiueItemToCollectionThatIsAlreadyInCollection()
        {
            string item = "acd";

            int countBeforeAdd = FirstList.Count;
            int countItemsBeforeAdd = FirstList.Count(s => s.Equals(item));

            FirstList.Add(item, false);

            Assert.AreEqual(countBeforeAdd +1, FirstList.Count);

            int countItemsAfterAdd = FirstList.Count(s => s.Equals(item));
            Assert.AreEqual(countItemsBeforeAdd + 1, countItemsAfterAdd);
        }
    }    
}
