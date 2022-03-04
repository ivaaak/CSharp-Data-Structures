namespace Demo
{
    using System;
    using Tree;

    class Program
    {
        static void Main(string[] args)
        {
            var input = new string[] { "9 17", "91 171", "29 127", "39 157", "95 171", "2 167", "19 127" };

            var treeFactory = new IntegerTreeFactory();

            var tree = treeFactory.CreateTreeFromStrings(input);
        }
    }
}
