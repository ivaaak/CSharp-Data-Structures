namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Tree<T> : IAbstractTree<T>
    {
        private List<Tree<T>> children;
        
        public Tree(T key, params Tree<T>[] children)
        {
            this.Key = key;
            this.children = new List<Tree<T>>();

            foreach (var child in children)
            {
                this.AddChild(child);
                child.Parent = this;
            }
        }

        public T Key { get; private set; }

        public Tree<T> Parent { get; private set; }

        public IReadOnlyCollection<Tree<T>> Children 
            => this.children.AsReadOnly();

        public void AddChild(Tree<T> child)
        {
            this.children.Add(child);
            child.Parent = this;
        }

        public void AddParent(Tree<T> parent)
        {
            this.Parent = parent;
            parent.children.Add(this);
        }

        public string AsString()
        {
            var sb = new StringBuilder();

            this.DfsAsString(sb, this, 0);

            return sb.ToString().Trim();
        }

        public IEnumerable<T> GetInternalKeys()
        {
            var queue = new Queue<Tree<T>>();
            var result = new List<T>();

            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                var currentSubtree = queue.Dequeue();

                if (currentSubtree.children.Count > 0 && currentSubtree.Parent != null)
                {
                    result.Add(currentSubtree.Key);
                }

                foreach (var child in currentSubtree.children)
                {
                    queue.Enqueue(child);
                }
            }
            return result;

            //with predicate:
            //return this.DfsWithResultKeys(tree => tree.children.Count > 0 && tree.Parent != null)
        }

        public IEnumerable<T> GetLeafKeys()
        {
            var queue = new Queue<Tree<T>>();
            var result = new List<T>();

            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                var currentSubtree = queue.Dequeue();

                if(currentSubtree.children.Count == 0)
                {
                    result.Add(currentSubtree.Key);
                }

                foreach (var child in currentSubtree.children)
                {
                    queue.Enqueue(child);
                }
            }
            return result;

            //return this.DfsWithResultKeys(x => x.children.Count==0)
        }

        public T GetDeepestKey()
        {
            return this.GetDeepestNode().Key;
        }

        private Tree<T> GetDeepestNode()
        {
            var leafs = this.DfsWithResultKeys(tree => tree.children.Count == 0);

            Tree<T> deepestNode = null;
            var maxDepth = 0;

            foreach (var leaf in leafs)
            {
                var depth = this.GetDepth(leaf);

                if(depth > maxDepth)
                {
                    maxDepth = depth;
                    deepestNode = leaf;
                }
            }
            return deepestNode;
        }

        private int GetDepth(Tree<T> leaf)
        {
            int depth = 0;
            var tree = leaf;
            while (tree.Parent != null)
            {
                depth++;
                tree = tree.Parent;
            }
            return depth;
        }

        public IEnumerable<T> GetLongestPath()
        {
            throw new NotImplementedException();
        }

        private void DfsAsString(StringBuilder sb, Tree<T> tree, int indent)
        {
            sb.Append(' ', indent)
                .AppendLine(tree.Key.ToString());
            //.AppendLine();

            foreach (var child in tree.children)
            {
                this.DfsAsString(sb, child, indent + 2);
            }
        }

        private IEnumerable<Tree<T>> DfsWithResultKeys(Predicate<Tree<T>> predicate)
        {
            var queue = new Queue<Tree<T>>();
            var result = new List<Tree<T>>();

            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                var currentSubtree = queue.Dequeue();

                if (predicate.Invoke(currentSubtree))
                {
                    result.Add(currentSubtree);
                }

                foreach (var child in currentSubtree.children)
                {
                    queue.Enqueue(child);
                }
            }
            return result;
        }
    }
}
