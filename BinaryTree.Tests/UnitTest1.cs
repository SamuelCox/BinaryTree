using Xunit;
using System.Linq;

namespace BinaryTree.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Contains_Returns_True_Correctly()
        {
            var tree = new BinaryTree<int>();
            tree.Add(5);
            tree.Add(4);
            tree.Add(6);

            var containsFive = tree.Contains(5);
            Assert.True(containsFive);
        }

        [Fact]
        public void Contains_Returns_True_Multiple_Correctly()
        {
            var tree = new BinaryTree<int>();
            tree.Add(5);
            tree.Add(4);
            tree.Add(6);

            var containsFive = tree.Contains(6);
            Assert.True(containsFive);
        }

        [Theory]
        [InlineData(4)]
        [InlineData(6)]
        [InlineData(3)]
        [InlineData(2)]
        [InlineData(5)]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(7)]
        [InlineData(9)]
        public void Remove_Returns_True_Multiple_Correctly(int item)
        {
            var items = new[] { 5,2,6,3, 1, 10, 4, 7, 9};
            var itemsNotRemoved = items.Where(x => x != item);
            var tree = new BinaryTree<int>();
            tree.Add(5);
            tree.Add(2);
            tree.Add(6);
            tree.Add(3);
            tree.Add(1);
            tree.Add(10);
            tree.Add(4);
            tree.Add(7);
            tree.Add(9);
            //   5
            //  2   6
            // 1 3    10
            //0   4  7    
            //         9
            var remove = tree.Remove(item);
            Assert.True(remove && !tree.Contains(item));
            foreach (var itemNotRemoved in itemsNotRemoved)
            {
                Assert.True(tree.Contains(itemNotRemoved));
            }
        }

        // [Fact]
        // public void Remove_Removes_And_Returns_True_Correctly()
        // {
        //     var tree = new BinaryTree<int>();
        //     tree.Add(5);


        //     var wasRemoved = tree.Remove(5);
        //     Assert.True(wasRemoved);
        // }

        // [Fact]
        // public void Remove_WithMultiple_Removes_And_Returns_True_Correctly()
        // {
        //     var tree = new BinaryTree<int>();
        //     tree.Add(5);
        //     tree.Add(6);
        //     tree.Add(7);


        //     var wasRemoved = tree.Remove(6);
        //     Assert.True(wasRemoved);
        // }

        [Fact]
        public void Remove_Does_Not_Remove_If_Doesnt_Exist()
        {
            var tree = new BinaryTree<int>();
            tree.Add(5);
            tree.Add(6);
            tree.Add(7);


            var wasRemoved = tree.Remove(9);
            Assert.False(wasRemoved);
        }

    }
}
