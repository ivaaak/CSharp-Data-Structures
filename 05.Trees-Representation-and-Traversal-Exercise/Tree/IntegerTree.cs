namespace Tree
{
    using System;
    using System.Collections.Generic;

    public class IntegerTree : Tree<int>, IIntegerTree
    {
        public IntegerTree(int key, params Tree<int>[] children)
            : base(key, children)
        {
        }

        //DFS
        public IEnumerable<IEnumerable<int>> GetPathsWithGivenSum(int sum)
        {
            var result = new List<List<int>>();

            var currentPath = new LinkedList<int>();
            currentPath.AddFirst(this.Key);
            int currentSum = this.Key;
            this.Dfs(this, result, currentPath, ref currentSum, sum);

            return result;
        }

        private void Dfs(
            Tree<int> subtree, 
            List<List<int>> result,
            LinkedList<int> currentPath, 
            ref int currentSum, 
            int wantedSum)
        {
            foreach (var child in subtree.Children)
            {
                currentSum += child.Key;
                currentPath.AddLast(child.Key);
                this.Dfs(child, result, currentPath, ref currentSum, wantedSum);
            }

            if (currentSum == wantedSum)
            {
                result.Add(new List<int>(currentPath));
            }

            currentSum -= subtree.Key;
            currentPath.RemoveLast();
        }

        //BFS
        public IEnumerable<Tree<int>> GetSubtreesWithGivenSum(int sum)
        {
            var result = new List<Tree<int>>();

            var allSubtrees = this.GetAllNodesBfs();

            foreach (var subtree in allSubtrees)
            {
                if(this.SubtreeHasGivenSum(subtree, sum))
                {
                    result.Add(subtree);
                }
            }
            return result;
        }

        private bool SubtreeHasGivenSum(Tree<int> subtree, int wantedSum)
        {
            int sum = subtree.Key;

            this.DfsGetSubtreeSum(subtree, wantedSum, ref sum);

            return sum == wantedSum;
        }

        private void DfsGetSubtreeSum(Tree<int> subtree, int wantedSum, ref int sum)
        {
            foreach (var item in subtree.Children)
            {
                sum += item.Key;
                this.DfsGetSubtreeSum(item, wantedSum, ref sum);
            }
        }

        private IEnumerable<Tree<int>> GetAllNodesBfs()
        {
            var queue = new Queue<Tree<int>>();
            var result = new List<Tree<int>>();

            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                var subTree = queue.Dequeue();
                result.Add(subTree);

                foreach (var child in subTree.Children)
                {
                    queue.Enqueue(child);
                }
            }
            return result;
        }

        private void Bfs(IntegerTree integerTree, 
            IEnumerable<Tree<int>> result, 
            LinkedList<int> currentPath, 
            ref int currentSum, int sum)
        {
            foreach (var child in integerTree.Children)
            {
                currentSum += child.Key;
                currentPath.AddLast(child.Key);
                this.Bfs(integerTree, result, currentPath, ref currentSum, sum);
            }

            currentSum -= integerTree.Key;
            currentPath.RemoveLast();
        }
    }
}
