using SoapExtensions;
using System.Security.Cryptography.X509Certificates;

namespace SoapExtensions.Tests
{
    public class EnumerationTests
    {
        [Test]
        public void GetEnumerator_Range_ReturnsCustomEnumerator()
        {
            var range = 1..10;
            var enumerator = range.GetEnumerator();

            Assert.That(enumerator, Is.TypeOf<CustomIntEnumerator>());
        }

        [Test]
        public void GetEnumerator_Tuple_ReturnsCustomEnumeratorWithStep()
        {
            var tuple = (1, 10, 2);
            var enumerator = tuple.GetEnumerator();
            enumerator.MoveNext();

            Assert.That(enumerator, Is.TypeOf<CustomIntWithStepSizeEnumerator>());
            Assert.That(enumerator.Current, Is.EqualTo(1));
        }

        [Test]
        public void Range_IteratesCorrectAmount()
        {
            int counter = 0;
            foreach (var i in 0..10)
            {
                counter++;
            }

            Assert.That(counter, Is.EqualTo(11));
        }

        [Test]
        public void Tuple_IteratesByStepSize()
        {
            var counter = 0;
            foreach (var i in (0, 10, 2))
            {
                counter++;
            }

            Assert.That(counter, Is.EqualTo(6));
        }

        [Test]
        public void ForEach_Range_ExecutesAction()
        {
            var range = 1..10;
            var values = new List<int>();

            range.ForEach(i => values.Add(i));

            Assert.That(values, Is.EquivalentTo(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }));
        }

        [Test]
        public void ForEach_Enumerable_ExecutesAction()
        {
            var list = new List<int> { 1, 2, 3 };
            var values = new List<int>();

            list.ForEach(i => values.Add(i));

            Assert.That(values, Is.EquivalentTo(new[] { 1, 2, 3 }));
        }

        [Test]
        public void ForEachRef_List_PassesByRef()
        {
            var list = new List<int> { 1, 2, 3 };


            list.ForEachRef((ref int x) => x = 10);

            Assert.That(list, Is.EquivalentTo(new[] { 10, 10, 10 }));
        }

        [Test]
        public void FirstRef_List_ReturnsFirstMatchAndChangesByRefByVariable()
        {
            var list = new List<int> { 1, 2, 3 };

            ref var value = ref list.FirstRef(i => i == 2);
            value = 20;

            Assert.That(list, Is.EquivalentTo(new[] { 1, 20, 3 }));
        }

        [Test]
        public void FirstRef_List_ReturnsFirstMatchAndChanges()
        {
            var list = new List<int> { 1, 2, 3 };

            list.FirstRef(i => i == 2) = 20;


            Assert.That(list, Is.EquivalentTo(new[] { 1, 20, 3 }));
        }
        [Test]
        public void RemoveWhere_RemovesCorrectItems()
        {
            var list = new List<int> { 1, 2, 3, 4, 5 };
            list.RemoveWhere(i => i % 2 == 0); // Remove even numbers

            Assert.That(list, Is.EquivalentTo(new List<int> { 1, 3, 5 }));
        }

        [Test]
        public void RemoveWhere_RemovesNoItemsWhenPredicateIsFalse()
        {
            var list = new List<int> { 1, 2, 3, 4, 5 };
            list.RemoveWhere(i => false); // No items should be removed

            Assert.That(list, Is.EquivalentTo(new List<int> { 1, 2, 3, 4, 5 }));
        }

        [Test]
        public void RemoveWhere_RemovesAllItemsWhenPredicateIsTrue()
        {
            var list = new List<int> { 1, 2, 3, 4, 5 };
            list.RemoveWhere(i => true); // All items should be removed

            Assert.That(list, Is.Empty);
        }
    }
}